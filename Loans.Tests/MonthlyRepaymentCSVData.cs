using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Loans.Tests {
    public class MonthlyRepaymentCSVData {
        public static IEnumerable GetTestCases(string CSVFileName) {
            var CSVLines = File.ReadAllLines(CSVFileName);

            var TestCases = new List<TestCaseData>();

            foreach (var Line in CSVLines) {
                string[] Values = Line.Replace(" ", "").Split(",");

                decimal Principal = decimal.Parse(Values[0]);
                decimal InterestRate = decimal.Parse(Values[1]);
                int TermInYears = int.Parse(Values[2]);
                decimal ExpectedRepayment = decimal.Parse(Values[3]);

                TestCases.Add(new TestCaseData(Principal, InterestRate, TermInYears, ExpectedRepayment));
            }

            return TestCases;
        }
    }
}
