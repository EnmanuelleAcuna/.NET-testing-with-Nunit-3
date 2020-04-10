using Loans.Domain.Applications;
using NUnit.Framework.Constraints;

namespace Loans.Tests {
    class MonthlyRepaymentGreaterThanZeroConstraint : Constraint {
        public string ExpectedProductName { get; }
        public decimal ExpectedInterestRate { get; }

        public MonthlyRepaymentGreaterThanZeroConstraint(string ExpectedProductName, decimal ExpectedInterestRate) {
            this.ExpectedProductName = ExpectedProductName;
            this.ExpectedInterestRate = ExpectedInterestRate;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual Actual) {
            MonthlyRepaymentComparison Comparison = Actual as MonthlyRepaymentComparison;

            if (Comparison is null) {
                return new ConstraintResult(this, Actual, ConstraintStatus.Error);
            }

            if (Comparison.InterestRate == ExpectedInterestRate &&
                Comparison.ProductName == ExpectedProductName &&
                Comparison.MonthlyRepayment > 0) {
                return new ConstraintResult(this, Actual, ConstraintStatus.Success);
            }

            return new ConstraintResult(this, Actual, ConstraintStatus.Failure);
        }
    }
}
