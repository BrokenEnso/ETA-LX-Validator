using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using System.Globalization;

namespace eDRVS
{
    class LXRules
    {
        static string[] YesBlank = new string[] { "1", "0", "" };
        static string[] YesNoBlank = new string[] { "1", "2", "0", "" };
        static string[] YesNoNoinfo = new string[] { "1", "2", "3" };
        static double MaxMoney = 999999.99;
        static double ZeroMoney = 0.00;

        DateTime ReportDate;
        public LXRules(DateTime reportDate)
        {
            ReportDate = reportDate;
        }

        int lastObservationNumber = -1;

        public List<Func<CsvReader, FormatResult>> FormatRules
        {
            get
            {
                var FormatRules = new List<Func<CsvReader, FormatResult>>();
                FormatRules.Add(c => {
                    var valid = true;
                    if(lastObservationNumber == -1)
                    {
                        lastObservationNumber = int.Parse(c[0]);
                    }
                    else
                    {
                        var i = int.Parse(c[0]);
                        valid = i == lastObservationNumber + 1;
                        lastObservationNumber = i;
                    }
                    return new FormatResult { Index = 0, Valid =  valid};
                });
                FormatRules.Add(c => new FormatResult { Index = 1, Valid = IsValidLength(c[1], 9) });
                FormatRules.Add(c => new FormatResult { Index = 2, Valid = IsValidDateFormat(c[2], true) });
                FormatRules.Add(c => new FormatResult { Index = 3, Valid = InArray(c[3], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 4, Valid = IsValidDateFormat(c[4], true) });
                FormatRules.Add(c => new FormatResult { Index = 5, Valid = IsValidDateFormat(c[5], true) });
                FormatRules.Add(c => new FormatResult { Index = 6, Valid = IsValidDateFormat(c[6], true) });
                FormatRules.Add(c => new FormatResult { Index = 7, Valid = InArray(c[7], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 8, Valid = InArray(c[8], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 9, Valid = InArray(c[9], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 10, Valid = InArray(c[10], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 11, Valid = InArray(c[11], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 12, Valid = InArray(c[12], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 13, Valid = InArray(c[13], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 14, Valid = InArray(c[14], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 15, Valid = InArray(c[15], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 16, Valid = InArray(c[16], new string[] { "1", "2", "3", "4", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 17, Valid = InArray(c[17], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 18, Valid = InArray(c[18], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 19, Valid = InArray(c[19], new string[] { "1", "2", "3", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 20, Valid = InArray(c[20], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 21, Valid = InArray(c[21], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 22, Valid = InArray(c[22], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 23, Valid = InArray(c[23], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 24, Valid = IsValidDateFormat(c[24], true) });
                FormatRules.Add(c => new FormatResult { Index = 25, Valid = InArray(c[25], new string[] { "1", "2", "3", "0", "" }) });
                FormatRules.Add(c => new FormatResult
                {
                    Index = 26,
                    Valid = InArray(c[26], new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "87", "88", "89", "90", "91", "00", "0", "" })
                });
                FormatRules.Add(c => new FormatResult { Index = 27, Valid = InArray(c[27], new string[] { "1", "2", "3", "4", "5", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 28, Valid = InArray(c[28], new string[] { "1", "2", "3", "4", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 29, Valid = InArray(c[29], YesNoBlank) });
                FormatRules.Add(c => new FormatResult { Index = 30, Valid = InArray(c[30], YesBlank) });
                FormatRules.Add(c => new FormatResult { Index = 31, Valid = IsValidDateFormat(c[31], true) });
                FormatRules.Add(c => new FormatResult { Index = 32, Valid = IsValidDateFormat(c[32], true) });
                FormatRules.Add(c => new FormatResult { Index = 33, Valid = IsValidDateFormat(c[33], true) });
                FormatRules.Add(c => new FormatResult { Index = 34, Valid = IsValidDateFormat(c[34], true) });
                FormatRules.Add(c => new FormatResult { Index = 35, Valid = IsValidDateFormat(c[35], true) });
                FormatRules.Add(c => new FormatResult { Index = 36, Valid = IsValidDateFormat(c[36], true) });
                FormatRules.Add(c => new FormatResult { Index = 37, Valid = IsValidDateFormat(c[37], true) });
                FormatRules.Add(c => new FormatResult { Index = 38, Valid = IsValidDateFormat(c[38], true) });
                FormatRules.Add(c => new FormatResult { Index = 39, Valid = IsValidDateFormat(c[39], true) });
                FormatRules.Add(c => new FormatResult { Index = 40, Valid = IsValidDateFormat(c[40], true) });
                FormatRules.Add(c => new FormatResult { Index = 41, Valid = IsValidDateFormat(c[41], true) });
                FormatRules.Add(c => new FormatResult { Index = 42, Valid = IsValidDateFormat(c[42], true) });
                FormatRules.Add(c => new FormatResult { Index = 43, Valid = IsValidDateFormat(c[43], true) });
                FormatRules.Add(c => new FormatResult { Index = 44, Valid = IsValidDateFormat(c[44], true) });
                FormatRules.Add(c => new FormatResult { Index = 45, Valid = IsValidDateFormat(c[45], true) });
                FormatRules.Add(c => new FormatResult { Index = 46, Valid = IsValidDateFormat(c[46], true) });
                FormatRules.Add(c => new FormatResult { Index = 47, Valid = IsValidDateFormat(c[47], true) });
                FormatRules.Add(c => new FormatResult { Index = 48, Valid = IsValidDateFormat(c[48], true) });
                FormatRules.Add(c => new FormatResult { Index = 49, Valid = IsValidDateFormat(c[49], true) });
                FormatRules.Add(c => new FormatResult { Index = 50, Valid = IsValidDateFormat(c[50], true) });
                FormatRules.Add(c => new FormatResult { Index = 51, Valid = IsValidDateFormat(c[51], true) });
                FormatRules.Add(c => new FormatResult { Index = 52, Valid = IsValidDateFormat(c[52], true) });
                FormatRules.Add(c => new FormatResult { Index = 53, Valid = IsValidDateFormat(c[53], true) });
                FormatRules.Add(c => new FormatResult { Index = 54, Valid = IsValidDateFormat(c[54], true) });
                FormatRules.Add(c => new FormatResult { Index = 55, Valid = IsValidDateFormat(c[55], true) });
                FormatRules.Add(c => new FormatResult { Index = 56, Valid = IsValidDateFormat(c[56], true) });
                FormatRules.Add(c => new FormatResult { Index = 57, Valid = IsValidDateFormat(c[57], true) });
                FormatRules.Add(c => new FormatResult { Index = 58, Valid = IsValidDateFormat(c[58], true) });
                FormatRules.Add(c => new FormatResult { Index = 59, Valid = IsValidDateFormat(c[59], true) });
                FormatRules.Add(c => new FormatResult { Index = 60, Valid = IsValidDateFormat(c[60], true) });
                FormatRules.Add(c => new FormatResult { Index = 61, Valid = IsValidDateFormat(c[61], true) });
                FormatRules.Add(c => new FormatResult { Index = 62, Valid = IsValidDateFormat(c[62], true) });
                FormatRules.Add(c => new FormatResult { Index = 63, Valid = IsValidDateFormat(c[63], true) });
                FormatRules.Add(c => new FormatResult { Index = 64, Valid = IsValidDateFormat(c[64], true) });
                FormatRules.Add(c => new FormatResult { Index = 65, Valid = IsValidDateFormat(c[65], true) });
                FormatRules.Add(c => new FormatResult { Index = 66, Valid = IsValidDateFormat(c[66], true) });
                FormatRules.Add(c => new FormatResult { Index = 67, Valid = IsValidDateFormat(c[67], true) });
                FormatRules.Add(c => new FormatResult { Index = 68, Valid = IsValidDateFormat(c[68], true) });
                FormatRules.Add(c => new FormatResult { Index = 69, Valid = IsValidDateFormat(c[69], true) });
                FormatRules.Add(c => new FormatResult { Index = 70, Valid = IsValidDateFormat(c[70], true) });
                FormatRules.Add(c => new FormatResult { Index = 71, Valid = IsValidDateFormat(c[71], true) });
                FormatRules.Add(c => new FormatResult { Index = 72, Valid = InArray(c[72], new string[] { "00", "01", "02", "03", "04", "05", "98", "99", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 73, Valid = InArray(c[73], YesNoNoinfo) });
                FormatRules.Add(c => new FormatResult { Index = 74, Valid = InArray(c[74], new string[] { "1", "2", "3", "4", "5", "6", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 75, Valid = InArray(c[75], YesNoNoinfo) });
                FormatRules.Add(c => new FormatResult { Index = 76, Valid = InArray(c[76], new string[] { "1", "2", "3", "4", "5", "6", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 77, Valid = InArray(c[77], YesNoNoinfo) });
                FormatRules.Add(c => new FormatResult { Index = 78, Valid = InArray(c[78], new string[] { "1", "2", "3", "4", "5", "6", "0", "" }) });
                FormatRules.Add(c => new FormatResult { Index = 79, Valid = IsValidCurrency(c[79]) });
                FormatRules.Add(c => new FormatResult { Index = 80, Valid = IsValidCurrency(c[80]) });
                FormatRules.Add(c => new FormatResult { Index = 81, Valid = IsValidCurrency(c[81]) });
                FormatRules.Add(c => new FormatResult { Index = 82, Valid = IsValidCurrency(c[82]) });
                FormatRules.Add(c => new FormatResult { Index = 83, Valid = IsValidCurrency(c[83]) });
                FormatRules.Add(c => new FormatResult { Index = 84, Valid = InArray(c[84], new string[] { "1", "2", "3", "4", "5", "6", "7", "0" }) });
                FormatRules.Add(c => new FormatResult { Index = 85, Valid = IsValidDateFormat(c[85], true) });
                FormatRules.Add(c => new FormatResult { Index = 86, Valid = IsValidLength(c[86], 75) });
                FormatRules.Add(c => new FormatResult { Index = 87, Valid = IsValidLength(c[87], 75) });
                FormatRules.Add(c => new FormatResult { Index = 88, Valid = IsValidLength(c[88], 75) });
                FormatRules.Add(c => new FormatResult { Index = 89, Valid = IsValidLength(c[89], 75) });
                FormatRules.Add(c => new FormatResult { Index = 90, Valid = IsValidLength(c[90], 4) });
                return FormatRules;
            }
        }
        public List<Func<CsvReader, EditCheckErrorResult>> RejectRules 
        {
            get
            {
                var EditCheckErrorRules = new List<Func<CsvReader, EditCheckErrorResult>>();
                EditCheckErrorRules.Add(c => new EditCheckErrorResult
                {
                    Valid = c[1].Trim() != "",
                    Message = "A. Individual Identifier is missing or invalid."
                });
                EditCheckErrorRules.Add(c =>
                {
                    bool valid = false;

                    if ((IsValidDateFormat(c[24], false) && InArray(c[16], new string[] { "1", "2", "3" })) || IsValidDateFormat(c[4], false))
                    {
                        valid = true;
                    }

                    return new EditCheckErrorResult { Valid = valid, Message = "A. Date of Program Participation is missing or invalid and the individual is not a Covered Person." };
                });
                //Column 25 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = false, Message = "" };

                    if (InArray(c[16], new string[] { "1", "2", "3" }) && !IsValidDateFormat(c[24], false))
                    {
                        retval.Message = "A. Covered Person Entry Date cannot be blank or 0 if participant is an Eligible Veteran.";
                    }
                    else
                    {
                        if (IsBlankOrZero(c[4]) || IsBlankOrZero(c[24]) || ParseYYYYMMDD(c[24]) <= ParseYYYYMMDD(c[4]))
                        {
                            retval.Valid = true;
                        }
                        else
                        {
                            retval.Message = "B. Covered Person Entry Date cannot be after the Date of Program Participatoin.";
                        }
                    }

                    return retval;
                });
                //Column 72 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = false, Message = "A. Date of Exit must be greater than or equal to the Date of Program Participation, and cannot be blank if Other Reasons for Exit is 01 - 06." };

                    if (IsBlankOrZero(c[71]) || (IsBlankOrZero(c[4]) || ParseYYYYMMDD(c[71]) >= ParseYYYYMMDD(c[4])))// InArray(c[16], new string[] { "1", "2", "3" }) || IsValidDateFormat(c[24], false))
                    {
                        retval.Valid = true;
                    }

                    if (InArray(c[72], new string[] { "01", "02", "03", "04", "05" }) || IsValidDateFormat(c[24], false))
                    {
                        retval.Valid = true;
                    }

                    return retval;
                });
                //Column 73 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "B. Other Reasons for Exit is specified (01 - 06), but no Date of Exit is given." };

                    if (c[71].Trim() == "" && !InArray(c[72], new string[] { "00", "99", "" }))
                    {
                        retval.Valid = false;
                    }

                    return retval;
                });

                //Column 74 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (InArray(c[74], new string[] { "1", "2", "3", "4", "5" }) && IsNotEq(c[73], "1"))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Employed in 1st Quarter After Exit Quarter must be 1 (Yes) if Type of Employment Match is 1-5 (Wage records, Employment Records, or Supplemental).";
                    }

                    if (IsBlankOrZero(c[74]) && IsNotEq(c[73], "2"))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Employed in 1st Quarter After Exit Quarter must be 2 (No) if Type of Employment Match is blank or 0 (Not Employed).";
                    }

                    if (InArray(c[74], new string[] { "6" }) && IsNotEq(c[73], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Employed in 1st Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Type of Employment Match is 6 (Information Not Yet Available).";
                    }

                    if (!IsBlank(c[71]) && IsEq(c[73], "3") && QuartersBetween(ReportDate, ParseYYYYMMDD(c[71])) >= 3)
                    {
                        retval.Valid = false;
                        retval.Message = "D. Employed in 1st Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Date of Exit is blank.";
                    }

                    if (IsEq(c[71], "") && IsNotEq(c[73], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "E. Employed in 1st Quarter After Exit Quarter cannot be 3 (Information Not Yet Available) if report quarter is 3 or more quarters after exit quarter.";
                    }

                    return retval;
                });

                //Column 75 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (IsBetween(c[81], ZeroMoney, MaxMoney) && !InArray(c[74], new string[] { "1", "2", "3", "4" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Type of Employment Match 1st Quarter After Exit Quarter must be 1-4 (Wage or Employment Records) if Wages is >0.";
                    }

                    if (IsBlankOrZero(c[81]) && !InArray(c[74], new string[] { "5", "0", "" }))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Type of Employment Match 1st Quarter After Exit Quarter must be 5, blank or 0 (Supplemental or Not Employed) if Wages is blank or 0.";
                    }

                    if (IsEq(c[81], MaxMoney.ToString()) && IsNotEq(c[74], "6"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Type of Employment Match 1st Quarter After Exit Quarter must be 6 (Information Not Yet Available) if Wages are 999999.99 (Not Yet Available).";
                    }

                    return retval;
                });

                //Column 76 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (InArray(c[69], new string[] { "1", "2", "3", "4", "5" }) && IsNotEq(c[75], "1"))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Employed in 2nd Quarter After Exit Quarter must be 1 (Yes) if Type of Employment Match is 1-5 (Wage records, Employment Records, or Supplemental).";
                    }

                    if (IsBlankOrZero(c[76]) && IsNotEq(c[75], "2"))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Employed in 2nd Quarter After Exit Quarter must be 2 (No) if Type of Employment Match is blank or 0 (Not Employed).";
                    }

                    if (IsEq(c[76], "6") && IsNotEq(c[75], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Employed in 2nd Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Type of Employment Match is 6 (Information Not Yet Available).";
                    }

                    if (!IsBlank(c[71]) && IsEq(c[75], "3") && QuartersBetween(ReportDate, ParseYYYYMMDD(c[71])) >= 4)
                    {
                        retval.Valid = false;
                        retval.Message = "D. Employed in 2nd Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Date of Exit is blank.";
                    }

                    if (IsEq(c[71], "") && IsNotEq(c[75], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "E. Employed in 2nd Quarter After Exit Quarter cannot be 3 (Information Not Yet Available) if report quarter is 4 or more quarters after exit quarter.";
                    }

                    return retval;
                });

                //Column 77 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (IsBetween(c[82], ZeroMoney, MaxMoney) && !InArray(c[76], new string[] { "1", "2", "3", "4" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Type of Employment Match 2nd Quarter After Exit Quarter must be 1-4 (Wage or Employment Records) if Wages is >0.";
                    }

                    if (IsBlankOrZero(c[82]) && !InArray(c[76], new string[] { "5", "0", "" }))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Type of Employment Match 2nd Quarter After Exit Quarter must be 5, blank or 0 (Supplemental or Not Employed) if Wages is blank or 0.";
                    }

                    if (IsEq(c[82], MaxMoney.ToString()) && !IsEq(c[76], "6"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Type of Employment Match 2nd Quarter After Exit Quarter must be 6 (Information Not Yet Available) if Wages";
                    }

                    return retval;
                });

                //Column 78 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (InArray(c[78], new string[] { "1", "2", "3", "4", "5" }) && IsNotEq(c[77], "1"))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Employed in 3rd Quarter After Exit Quarter must be 1 (Yes) if Type of Employment Match is 1-5 (Wage records, Employment Records, or Supplemental).";
                    }

                    if (InArray(c[78], new string[] { "", "0" }) && IsNotEq(c[77], "2"))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Employed in 3rd Quarter After Exit Quarter must be 2 (No) if Type of Employment Match is blank or 0 (Not Employed).";
                    }

                    if (InArray(c[78], new string[] { "6" }) && IsNotEq(c[77], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Employed in 3rd Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Type of Employment Match is 6 (Information Not Yet Available).";
                    }

                    if(!IsBlank(c[71]) && IsEq(c[77], "3") && QuartersBetween(ReportDate, ParseYYYYMMDD(c[71])) >= 4)
                    {
                        retval.Valid = false;
                        retval.Message = "D. Employed in 3rd Quarter After Exit Quarter must be 3 (Information Not Yet Available) if Date of Exit is blank.";
                    }

                    if (IsEq(c[71], "") && IsNotEq(c[77], "3"))
                    {
                        retval.Valid = false;
                        retval.Message = "E. Employed in 3rd Quarter After Exit Quarter cannot be 3 (Information Not Yet Available) if report quarter is 5 or more quarters after exit quarter.";
                    }

                    return retval;
                });

                //Column 79 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (IsBetween(c[83], ZeroMoney, MaxMoney) && !InArray(c[78], new string[] { "1", "2", "3", "4" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Type of Employment Match 3rd Quarter After Exit Quarter must be 1-4 (Wage or Employment Records) if Wages is >0.";
                    }

                    if (IsBlankOrZero(c[83]) && !InArray(c[78], new string[] { "5", "0", "" }))
                    {
                        retval.Valid = false;
                        retval.Message = "B. Type of Employment Match 3rd Quarter After Exit Quarter must be 5, blank or 0 (Supplemental or Not Employed) if Wages is blank or 0.";
                    }

                    if (IsEq(c[81], MaxMoney.ToString()) && IsNotEq(c[78], "6"))
                    {
                        retval.Valid = false;
                        retval.Message = "C. Type of Employment Match 3rd Quarter After Exit Quarter must be 6 (Information Not Yet Available) if Wages are 999999.99 (Not Yet Available).";
                    }

                    return retval;
                });

                //Column 80-84 is validated by the format check

                //Column 85 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (IsValidDateFormat(c[85]) && IsEq(c[84], "0"))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Type of Recognized Credential cannot be 0 if Date of Attainment of the Recognized Credential is a valid date.";
                    }

                    return retval;
                });

                //Column 86 EditCheck
                EditCheckErrorRules.Add(c =>
                {
                    var retval = new EditCheckErrorResult { Valid = true, Message = "" };

                    if (!IsBlank(c[85]) 
                        && (!IsBlank(c[4]) && ParseYYYYMMDD(c[85]) >= ParseYYYYMMDD(c[4]))
                        && (!IsBlank(c[71]) && IsDateBefore(ParseYYYYMMDD(c[85]), LastDateOf3QAfterExit(ParseYYYYMMDD(c[71]))))
                       )
                    {
                        retval.Valid = false;
                        retval.Message = "A. If Type of Recognized Credential is not 0, Date of Attainment of the Recognized Credential must be > the Date of Paticipation < the last date of the third quarter after the exit quarter.";
                    }

                    return retval;
                });

                return EditCheckErrorRules;
            }
        }
        public List<Func<CsvReader, EditCheckWarnningResult>> WarrningRules
        {
            get
            {
                var EditCheckWarrningRules = new List<Func<CsvReader, EditCheckWarnningResult>>();
                //Column 3
                EditCheckWarrningRules.Add(c => 
                {
                    bool valid = true;
                    var dob = ParseYYYYMMDD(c[2]);
                    var dopp = ParseYYYYMMDD(c[4]);
                    TimeSpan age = dopp - dob;

                    if (age.GetApproxYears() > 100 || age.GetApproxYears() < 9)
                    {
                        valid = false;
                    }

                    return new EditCheckWarnningResult { Valid = valid, Message = "A. Date of Birth is invalid--age is <9 or >100 at Date of Program Participation." };
                });
                //Column 6
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    //This check is done first to reduce parse exceptions of blank fields.
                    if (IsBlank(c[5]))
                    {
                        if (IsBlank(c[32]))
                        {
                            retval.Valid = false;
                            retval.Message = "C. Date of First Self Service cannot be blank if Date of Last Self Service has a valid date.";
                            return retval;
                        }
                        else
                        {
                            return retval;
                        }
                    }

                    var dofss = ParseYYYYMMDD(c[5]);

                    if (!IsBlank(c[4]) && dofss >= ParseYYYYMMDD(c[4]))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Date of First Self Service is before the Date of Program Participation.";
                    }
                    else
                    {
                        if (!IsBlank(c[71]) && dofss <= ParseYYYYMMDD(c[71]))
                        {
                            retval.Valid = false;
                            retval.Message = "B. Date of First Self Service is after the Date of Exit.";
                        }
                    }

                    return retval;
                });
                //Column 7
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    int[] serviceFieldRange = new int[] { 34, 35, 36, 37, 38, 39, 40, 41, 42, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 };
                    //This check is done first to reduce parse exceptions of blank fields.
                    if (IsBlank(c[6]))
                    {
                        if (IsRangeOfDatesBlank(c, serviceFieldRange))
                        {
                            retval.Valid = false;
                            retval.Message = "C. Date of First Staff Assisted Service cannot be blank if Date of Last Self Service has a valid date.";
                            return retval;
                        }
                        else
                        {
                            return retval;
                        }
                    }

                    var dofsas = ParseYYYYMMDD(c[6]);

                    if (!IsBlank(c[4]) && dofsas >= ParseYYYYMMDD(c[4]))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Date of First Staff Assisted Service is before the Date of Program Participation.";
                    }
                    else
                    {
                        if (!IsBlank(c[71]) && dofsas <= ParseYYYYMMDD(c[71]))
                        {
                            retval.Valid = false;
                            retval.Message = "B. Date of First Staff Assisted Service is after the Date of Exit.";
                        }
                    }

                    return retval;
                });
                //Column 16
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    string vetStatus = c[16].Trim();

                    if (InArray(c[15], new string[] { "1", "2" }) && vetStatus != "1")
                    {
                        retval.Valid = false;
                        retval.Message = "A. Veteran Status must be 1 if Eligible Veterans is 1 or 2.";
                    }

                    return retval;
                });
                //Column 17
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    string vetStatus = c[16].Trim();

                    if (!InArray(vetStatus, new string[] { "1", "2" }))
                    {
                        if (InArray(c[18], new string[] { "1" }) )
                        {
                            retval.Valid = false;
                            retval.Message = "A. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Campaign Veteran is 1 (Yes).";
                        }
                        else
                        {
                            if (InArray(c[19], new string[] { "1", "2" }))
                            {
                                retval.Valid = false;
                                retval.Message = "B. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Disabled Veteran is 1 or 2 (Yes).";
                            }
                            else
                            {

                                if (InArray(c[21], new string[] { "1" }))
                                {
                                    retval.Valid = false;
                                    retval.Message = "C. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Recently Separated Veteran is 1 (Yes).";
                                }
                                else
                                {

                                    if (InArray(c[22], new string[] { "1" }))
                                    {
                                        retval.Valid = false;
                                        retval.Message = "D. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Homeless Veteran is 1 (Yes).";
                                    }
                                    else
                                    {

                                        if (InArray(c[17], new string[] { "1" }))
                                        {
                                            retval.Valid = false;
                                            retval.Message = "E. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Post 9/11 Veteran is 1 (Yes).";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return retval;
                });
                //Column 18
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[21], new string[] { "1" }) && !InArray(c[17], new string[] { "1"}))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Eligible Veteran Status must be 1 or 2 (Yes, Eligible Veteran) if Campaign Veteran is 1 (Yes).";
                    }

                    return retval;
                });
                //Column 19
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[16], new string[] { "1", "2" }) && !InArray(c[18], new string[] { "1", "2" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Campaign Veteran cannot be blank or 0 if participant is an Eligible Veteran.";
                    }

                    return retval;
                });
                //Column 20
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[16], new string[] { "1", "2" }) && !InArray(c[19], new string[] { "1", "2", "3" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Disabled Veteran cannot be blank or 0 if participant is an Eligible Veteran.";
                    }

                    return retval;
                });
                //Column 22
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[16], new string[] { "1", "2" }) && !InArray(c[21], new string[] { "1", "2" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Recently Separated Veteran cannot be blank or 0 if participant is an Eligible Veteran.";
                    }

                    return retval;
                });
                //Column 23
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[16], new string[] { "1", "2" }) && !InArray(c[22], new string[] { "1", "2" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Homeless Veteran cannot be blank or 0 if participant is an Eligible Veteran.";
                    }

                    return retval;
                });
                //Column 28
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    if (InArray(c[26], new string[] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" }) && !InArray(c[22], new string[] { "1", "2", "4" }))
                    {
                        retval.Valid = false;
                        retval.Message = "A. School Status at Participation and Highest School Grade Completed values are contradictory.";
                    }
                    else
                    {
                        if (InArray(c[26], new string[] { "13", "14", "15", "16", "17", "87", "88", "90", "91" }) && !InArray(c[22], new string[] { "3", "5" }))
                        {
                            retval.Valid = false;
                            retval.Message = "B. School Status at Participation and Highest School Grade Completed values are contradictory.";
                        }
                    }

                    return retval;
                });
                //Column 33
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                    //This check is done first to reduce parse exceptions of blank fields.
                    if (IsBlank(c[32]))
                    {
                        if (IsBlank(c[5]))
                        {
                            retval.Valid = false;
                            retval.Message = "C. Most Recent Date Received Self Services cannot be blank if Date of First Self Service has a valid date.";
                            return retval;
                        }
                        else
                        {
                            return retval;
                        }
                    }

                    var c33 = ParseYYYYMMDD(c[32]);

                    if (!IsBlank(c[4]) && c33 >= ParseYYYYMMDD(c[4]))
                    {
                        retval.Valid = false;
                        retval.Message = "A. Most Recent Date Received Self Services is before the Date of Program Participation.";
                    }
                    else
                    {
                        if (!IsBlank(c[71]) && c33 <= ParseYYYYMMDD(c[71]))
                        {
                            retval.Valid = false;
                            retval.Message = "B. Most Recent Date Received Self Services is after the Date of Exit.";
                        }
                    }

                    return retval;
                });

                var group = new Dictionary<int, string>();
                group.Add(34, "Most Recent Date Received Staff Assisted Services");
                group.Add(35, "Most Recent Date Received Staff Assisted Services (DVOP)");
                group.Add(36, "Most Recent Date Received Staff Assisted Services (LVER)");
                group.Add(37, "Most Recent Date Received Intensive Services");
                group.Add(38, "Most Recent Date Received Intensive Services (DVOP)");
                group.Add(39, "Most Recent Date Received Intensive Services (LVER)");
                group.Add(40, "Most Recent Date Received Career Guidance");
                group.Add(41, "Most Recent Date Received Career Guidance (DVOP)");
                group.Add(42, "Most Recent Date Received Career Guidance (LVER)");
                group.Add(43, "Most Recent Date Received Self-Service Workforce Information");
                group.Add(44, "Most Recent Date Received Staff Assisted Workforce Information Services");
                group.Add(45, "Most Recent Date Attended TAP Employment Workshop (DVOP)");
                group.Add(46, "Most Recent Date Attended TAP Employment Workshop (LVER)");
                group.Add(47, "Most Recent Date Received Job Search Activities");
                group.Add(48, "Most Recent Date of Job Search Activities (DVOP)");
                group.Add(49, "Most Recent Date of Job Search Activities (LVER)");
                group.Add(50, "Most Recent Date Referred to WIA Services");
                group.Add(51, "Most Recent Date Referred to Employment");
                group.Add(52, "Most Recent Date Referred to Employment (DVOP)");
                group.Add(53, "Most Recent Date Referred to Employment (LVER)");
                group.Add(54, "Most Recent Date Referred to Federal Training");
                group.Add(55, "Most Recent Date Referred to Federal Training (DVOP)");
                group.Add(56, "Most Recent Date Referred to Federal Training (LVER)");
                group.Add(57, "Most Recent Date Placed in Federal Training");
                group.Add(58, "Most Recent Date Placed in Federal Training (DVOP)");
                group.Add(59, "Most Recent Date Placed in Federal Training (LVER)");
                group.Add(60, "Most Recent Date Referred to Federal Job");
                group.Add(61, "Most Recent Date Referred to Federal Job (DVOP)");
                group.Add(62, "Most Recent Date Referred to Federal Job (LVER)");
                group.Add(63, "Most Recent Date Entered Into Federal Job");
                group.Add(64, "Most Recent Date Entered Into Federal Job (DVOP)");
                group.Add(65, "Most Recent Date Entered Into Federal Job (LVER)");
                group.Add(66, "Most Recent Date Referred to a Federal Contractor Job");
                group.Add(67, "Most Recent Date Referred to a Federal Contractor Job (DVOP)");
                group.Add(68, "Most Recent Date Referred to a Federal Contractor Job (LVER)");
                group.Add(69, "Most Recent Date Entered Into Federal Contractor Job");
                group.Add(70, "Most Recent Date Entered Into Federal Contractor Job (DVOP)");
                group.Add(71, "Most Recent Date Entered Into Federal Contractor Job (LVER)");

                //Column 34-71
                foreach (var kv in group)
                {
                    EditCheckWarrningRules.Add(c =>
                    {
                        EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };

                        if (IsBlank(c[kv.Key - 1]))
                        {
                            return retval;
                        }

                        var col = ParseYYYYMMDD(c[kv.Key - 1]);

                        if (!IsBlank(c[4]) && col >= ParseYYYYMMDD(c[4]))
                        {
                            retval.Valid = false;
                            retval.Message = "A. " + kv.Value + " is before the Date of Program Participation.";
                        }
                        else
                        {
                            if (!IsBlank(c[71]) && col <= ParseYYYYMMDD(c[71]))
                            {
                                retval.Valid = false;
                                retval.Message = "B. " + kv.Value + " is after the Date of Exit.";
                            }
                        }

                        return retval;
                    });
                }

                //Column 80
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    if (!IsBlank(c[79]))
                    {
                        var d = double.Parse(c[79]);
                        double OneHundredGrand = 100000.00;

                        if (d > OneHundredGrand || d != MaxMoney)
                        {
                            retval.Valid = false;
                            retval.Message = "B. Wages 3rd Quarter Prior to Participation cannot be > 100000.";
                        }
                    }

                    return retval;
                });
                //Column 81
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    if (!IsBlank(c[80]))
                    {
                        var d = double.Parse(c[80]);
                        double OneHundredGrand = 100000.00;

                        if (d > OneHundredGrand || d != MaxMoney)
                        {
                            retval.Valid = false;
                            retval.Message = "B. Wages 2nd Quarter Prior to Participation cannot be > 100000.";
                        }
                    }

                    return retval;
                });
                //Column 82
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    if (!IsBlank(c[81]))
                    {
                        var d = double.Parse(c[81]);
                        double FiftyGrand = 50000.00;

                        if (d > FiftyGrand || d != MaxMoney)
                        {
                            retval.Valid = false;
                            retval.Message = "B. Wages 1st Quarter After Exit Quarter cannot be > 50000.";
                        }
                    }

                    return retval;
                });
                //Column 83
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    if (!IsBlank(c[82]))
                    {
                        var d = double.Parse(c[82]);
                        double FiftyGrand = 50000.00;

                        if (d > FiftyGrand || d != MaxMoney)
                        {
                            retval.Valid = false;
                            retval.Message = "B. Wages 2nd Quarter After Exit Quarter cannot be > 50000.";
                        }
                    }

                    return retval;
                });
                //Column 84
                EditCheckWarrningRules.Add(c =>
                {
                    EditCheckWarnningResult retval = new EditCheckWarnningResult { Valid = true, Message = "" };
                    if (!IsBlank(c[83]))
                    {
                        var d = double.Parse(c[83]);
                        double FiftyGrand = 50000.00;

                        if (d > FiftyGrand || d != MaxMoney)
                        {
                            retval.Valid = false;
                            retval.Message = "B. Wages 3rd Quarter After Exit Quarter cannot be > 50000.";
                        }
                    }

                    return retval;
                });

                return EditCheckWarrningRules;
            }
        }
        
        private static bool IsDateBefore(DateTime d1, DateTime d2)
        {
            return d1 < d2;
        }

        private static DateTime LastDateOf3QAfterExit(DateTime date)
        {
            var MonthsIn3Quarters = 9;
            var d1 = date.AddMonths(MonthsIn3Quarters);
            int d1QuarterNumber = (d1.Month - 1) / 3 + 1;
            DateTime d1FirstDayOfQuarter = new DateTime(d1.Year, (d1QuarterNumber - 1) * 3 + 1, 1);
            DateTime d1LastDayOfQuarter = d1FirstDayOfQuarter.AddMonths(3).AddDays(-1);

            return d1LastDayOfQuarter;
        }

        private static int QuartersBetween(DateTime d1, DateTime d2)
        {
            var t = d1 - d2;

            return Math.Abs(t.GetApproxMonths() / 3);
        }

        private static bool IsBetween(string p, double ZeroMoney, double MaxMoney)
        {
            var d = double.Parse(p);
            return (d > ZeroMoney && d < MaxMoney);
        }

        private static bool IsRangeOfDatesBlank(CsvReader c, int[] range)
        {
            var retval = true;

            for (var i = 0; i < range.Length; i++)
            {
                if (!IsBlank(c[i]))
                {
                    retval = false;
                    break;
                }
            }

            return retval;
        }

        private static bool IsBlankOrZero(string p)
        {
            var s = p.Trim();
            return (p == "" || p == "0");
        }

        private static bool IsEq(string p, string p_2)
        {
            return p.Trim().Equals(p_2.Trim());
        }

        private static bool IsNotEq(string p, string p_2)
        {
            return !IsEq(p, p_2);
        }

        private static void LogColumnError(CsvReader csv, int index)
        {
            Console.Error.WriteLine("Invalied colomn ({2}) value ({1}) at line {0}", csv.CurrentRecordIndex, csv[index], index + 1);
        }

        private static bool IsValidLength(string input, int len)
        {
            return input.Trim().Length <= len;
        }

        private static bool IsValidCurrency(string input)
        {
            var retval = false;

            if (IsValidLength(input, 0))
            {
                retval = true;
            }
            else
            {
                var m = decimal.Parse(input);

                if (m >= 0.0M && m <= 999999.99M)
                {
                    retval = true;
                }
            }
            return retval;
        }

        private static bool IsValidDateFormat(string input, bool allowEmpty = false)
        {
            bool retval = false;
            var len = input.Trim().Length;
            if (allowEmpty && len == 0)
            {
                retval = true;
            }
            else
                if (len == 8)
                {
                    int i;
                    if (int.TryParse(input, out i))
                    {
                        retval = true;
                    }
                }

            return retval;
        }

        private static bool IsBlank(string input)
        {
            return (input.Trim().Length == 0);
        }

        private static bool InArray(string value, string[] array)
        {
            return Array.IndexOf(array, value) > -1;
        }

        private static DateTime ParseYYYYMMDD(string input)
        {
            return DateTime.ParseExact(input, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
