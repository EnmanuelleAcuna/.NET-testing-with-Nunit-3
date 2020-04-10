using System;
using System.Collections.Generic;
using System.Text;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests {
    [TestFixture]
    public class LoanRepaymentCalculatorShould {
        // Data driven tests

        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(decimal Principal, decimal InterestRate, int TermInYears, decimal ExpectedMonthlyPayment) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));

            Assert.That(MonthlyRepayment, Is.EqualTo(ExpectedMonthlyPayment));
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment_SimplifiedTestCase(decimal Principal, decimal InterestRate, int TermInYears) {
            var SUT = new LoanRepaymentCalculator();

            return SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal Principal, decimal InterestRate, int TermInYears, decimal ExpectedMonthlyPayment) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));

            Assert.That(MonthlyRepayment, Is.EqualTo(ExpectedMonthlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal Principal, decimal InterestRate, int TermInYears) {
            var SUT = new LoanRepaymentCalculator();

            return SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCSVData), "GetTestCases", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_CSV(decimal Principal, decimal InterestRate, int TermInYears, decimal ExpectedMonthlyPayment) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));

            Assert.That(MonthlyRepayment, Is.EqualTo(ExpectedMonthlyPayment));
        }

        [Test]
        public void CalculateMonthlyRepayment_Combinatorial(
            [Values(100_000, 200_000, 500_000)]decimal Principal,
            [Values(6.5, 10, 20)] decimal InterestRate,
            [Values(10, 20, 30)] int TermInYears
            ) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));
        }

        [Test]
        [Sequential]
        public void CalculateMonthlyRepayment_Sequential(
            [Values(200_000, 200_000, 500_000)]decimal Principal,
            [Values(6.5, 10, 10)] decimal InterestRate,
            [Values(30, 30, 30)] int TermInYears,
            [Values(1264.14, 1755.14, 4387.86)] decimal ExpectedMonthlyRepayment
            ) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));

            Assert.That(MonthlyRepayment, Is.EqualTo(ExpectedMonthlyRepayment));
        }

        [Test]
        public void CalculateMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)]decimal Principal,
            [Range(0.5, 20, 0.5)] decimal InterestRate,
            [Values(10, 20, 30)] int TermInYears
            ) {
            var SUT = new LoanRepaymentCalculator();

            var MonthlyRepayment = SUT.CalculateMonthlyRepayment(new LoanAmount("USD", Principal), InterestRate, new LoanTerm(TermInYears));
        }
    }
}