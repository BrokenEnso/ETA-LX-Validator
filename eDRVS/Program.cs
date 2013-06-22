using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Globalization;

namespace eDRVS
{
    struct FormatResult
    {
        public int Index { get; set; }
        public bool Valid { get; set; }
    }

    struct EditCheckErrorResult
    {
        //public int Index { get; set; }
        public bool Valid { get; set; }
        public string Message { get; set; }
    }

    struct EditCheckWarnningResult
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
    }

    struct ShortLX
    {
        public string ID { get; set;}
        public long RecID { get; set; }
        public DateTime? CovPersEntDate { get; set;}
        public DateTime? ProgParticDate { get; set;}
        public DateTime? ExitDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.Error.WriteLine("File Not Found.");
                Console.ReadLine();
                return;
            }
            var filename = args[0];

            var errorFilename = filename + ".error.log";
            var warningFilename = filename + ".warning.log";

            using (var errorLog = File.CreateText(errorFilename))
            using (var warrningLog = File.CreateText(warningFilename))
            {
                long errorCount = 0;
                long rejectCount = 0;
                long warningCount = 0;
                const long MaxErrorCount = 1000;
                const long MaxRejectCount = 10000;
                long recordsProcessed = 0;

                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();

                var dupCheckRecs = new List<ShortLX>(Convert.ToInt32((new FileInfo(filename)).Length / 1000));

                var lxRules = new LXRules(DateTime.Now);

                var FormatRules = lxRules.FormatRules;
                var EditCheckErrorRules = lxRules.RejectRules;
                var EditCheckWarnningRules = lxRules.WarrningRules;

                using (var file = File.OpenText(filename))
                {
                    var csv = new CsvReader(file, false);

                    while (csv.ReadNextRecord())
                    {
                        bool formatErrorExists = false;

                        if (errorCount > MaxErrorCount)
                        {
                            formatErrorExists = true;
                            errorLog.WriteLine("Exceded maximumn allowable format errors. Stoping file processing.");
                            break;
                        }

                        if (rejectCount > MaxRejectCount)
                        {
                            formatErrorExists = true;
                            errorLog.WriteLine("Exceded maximumn allowable format errors. Stoping file processing.");
                            break;
                        }

                        if (csv.FieldCount < 91)
                        {
                            formatErrorExists = true;
                            errorLog.WriteLine("Invalid nummber of columns ({0}) on line {1}.", csv.FieldCount, csv.CurrentRecordIndex + 1);
                            errorCount++;
                            continue;
                        }

                        foreach (var rule in FormatRules)
                        {
                            var r = rule(csv);
                            if (!r.Valid)
                            {
                                formatErrorExists = true;
                                errorCount++;
                                errorLog.WriteLine("Invalied colomn ({2}) value ({1}) at line {0}", csv.CurrentRecordIndex, csv[r.Index], r.Index + 1);
                            }
                        }

                        if (formatErrorExists) { continue; } 

                        foreach (var check in EditCheckErrorRules)
                        {
                            var c = check(csv);

                            if (!c.Valid)
                            {
                                errorLog.WriteLine("Line: {0} Error: {1}", csv.CurrentRecordIndex + 1, c.Message);
                                formatErrorExists = true;
                                rejectCount++;
                            }
                        }

                        if (!formatErrorExists)
                        {
                            foreach (var check in EditCheckWarnningRules)
                            {
                                var c = check(csv);

                                if (!c.Valid)
                                {
                                    warningCount++;
                                    warrningLog.WriteLine("Line: {0} Warrning: {1}", csv.CurrentRecordIndex + 1, c.Message.Substring(0, 10));
                                }
                            }

                            dupCheckRecs.Add(new ShortLX
                            {
                                ID = csv[1],
                                RecID = csv.CurrentRecordIndex,
                                ProgParticDate = ParseYYYYMMDD(csv[4]),
                                CovPersEntDate = ParseYYYYMMDD(csv[24]),
                                ExitDate = ParseYYYYMMDD(csv[64])
                            });
                        }
                    }

                    recordsProcessed = csv.CurrentRecordIndex + 1;
                }
                var dups = dupCheckRecs.GroupBy(x => x.ID)
                             .Where(g => g.Count() > 1)
                             .Select(g => g)
                             .ToList();
                dupCheckRecs = null;

                //A. CovPersEntDate.Count > 1
                var dupCheck1 = dups.Select(g => g.GroupBy(i => i.CovPersEntDate).Where(i => i.Count() > 1).Select(i => i.Key)).ToList();

                //B. (ProgParticDate || ExitDate) Between (ProgParticDate || ExitDate + 90)
                var dupCheck22 = dups.Where(g => g.Any(i1 => g.Any(i2 =>
                                                                        (i1.RecID != i2.RecID)
                                                                         && (i2.ExitDate.HasValue)
                                                                         && (IsBetweenInclusive(i1.ProgParticDate.Value, i2.ProgParticDate.Value, i2.ExitDate.Value.AddDays(90)))
                                                                        )
                                                        )
                                            ).ToList();

                //C. !Max(ProgParticDate) && ExitDate == Blank
                var dupCheck3 = (from g in dups
                                 let max = g.Max(i => i.ProgParticDate)
                                 where g.Any(i => i.ExitDate == null && i.ProgParticDate != max)
                                 select g.Key).ToList();
                timer.Stop();
                var ts = timer.Elapsed;
                Console.WriteLine("Processing completed in {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("Dup check 1: {0}", dupCheck1.Count());
                Console.WriteLine("Dup check 2: {0}", dupCheck22.Count());
                Console.WriteLine("Dup check 3: {0}", dupCheck3.Count());
                Console.WriteLine("Number of format errors {0}.", errorCount);
                Console.WriteLine("Number of reject errors {0}.", rejectCount);
                Console.WriteLine("Number of warnings {0}.", warningCount);
                Console.WriteLine("Processed {0} lines.", recordsProcessed);
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private static bool IsBetweenInclusive(DateTime target, DateTime start, DateTime end)
        {
            return target >= start && target <= end;
        }

        public static void LogColumnError(CsvReader csv, int index)
        {
            Console.Error.WriteLine("Invalied colomn ({2}) value ({1}) at line {0}", csv.CurrentRecordIndex, csv[index], index + 1);
        }

        public static DateTime? ParseYYYYMMDD(string input)
        {
            if (input.Trim().Equals(string.Empty))
            {
                return null;
            }

            return DateTime.ParseExact(input, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
