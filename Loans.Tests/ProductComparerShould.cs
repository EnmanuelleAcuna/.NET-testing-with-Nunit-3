using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests {
    [TestFixture]
    [ProductComparison]
    public class ProductComparerShould {
        private List<LoanProduct> Products;
        private ProductComparer SUT;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            // Simulate long setup init time for this list of products.
            // We asume that this list will not be modified by any tests
            // as this will potentially break other tests (i.e. break test isolation)
            Products = new List<LoanProduct> {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            // Run after last test in this test class (fixture) executes
            // e.g. disposing of shared expensive setup performed in OneTimeSetUp

            // Products.Dispose(); e.g. if products implemented IDisposable
        }

        [SetUp]
        public void SetUp() {
            SUT = new ProductComparer(new LoanAmount("USD", 200_000m), Products);
        }

        [TearDown]
        public void TearDown() {
            // Runs after each test executes
            // SUT.Dispose();
        }

        [Test]
        public void ReturnCorrectNumberOfComparisons() {
            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(Comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons() {
            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(Comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct() {
            List<MonthlyRepaymentComparison> Comparisons = SUT.CompareMonthlyRepayments(new LoanTerm(30));

            // Need to also know the expected monthly repayment
            var ExpectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(Comparisons, Does.Contain(ExpectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues() {
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
            //Assert.That(Comparisons, Has.Exactly(1)
            //    .Matches<MonthlyRepaymentComparison>(
            //    item => item.ProductName.Equals("a") &&
            //    item.InterestRate == 1 &&
            //    item.MonthlyRepayment > 0));

            Assert.That(Comparisons, Has.Exactly(1).Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }
    }
}
