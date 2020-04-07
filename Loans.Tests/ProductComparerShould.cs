using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests {
    [TestFixture]
    public class ProductComparerShould {
        [Test]
        public void ReturnCorrectNumberOfComparisons() {
            var Products = new List<LoanProduct> {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            var SUT = new ProductComparer(new LoanAmount("USD", 200_000m), Products);

            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(Comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons() {
            var Products = new List<LoanProduct> {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            var SUT = new ProductComparer(new LoanAmount("USD", 200_000m), Products);

            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(Comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct() {
            var Products = new List<LoanProduct> {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            var SUT = new ProductComparer(new LoanAmount("USD", 200_000m), Products);

            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            // Need to also know the expected monthly repayment
            var ExpectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(Comparisons, Does.Contain(ExpectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues() {
            var Products = new List<LoanProduct> {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            var SUT = new ProductComparer(new LoanAmount("USD", 200_000m), Products);

            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            // Don't care about the expected monthly repayment, only the product is there
            // In case or refactor this (Properties) has to be changed manually
            //Assert.That(Comparisons, Has.Exactly(1)
            //    .Property("ProductName").EqualTo("a")
            //    .And
            //    .Property("InterestRate").EqualTo(1)
            //    .And.
            //    Property("MonthlyRepayment").GreaterThan(0));

            // With lambda predicate
            Assert.That(Comparisons, Has.Exactly(1)
                .Matches<MonthlyRepaymentComparison>(
                item => item.ProductName.Equals("a") &&
                item.InterestRate == 1 &&
                item.MonthlyRepayment > 0));
        }
    }
}
