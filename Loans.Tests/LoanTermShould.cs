using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loans.Tests {
    [TestFixture]
    public class LoanTermShould {
        [Test]
        public void ReturnTermInMonths() {
            // Arrange, Act, Assert phases

            // SUT System Under Test
            var SUT = new LoanTerm(1); // Arrange

            // Act & Assert
            Assert.That(SUT.ToMonths(), Is.EqualTo(12), "Months should be 12 * number of years");

            // Act
            //var NumberOfMonths = SUT.ToMonths();

            //// Assert
            //Assert.That(NumberOfMonths, Is.EqualTo(12));
        }

        [Test]
        public void StoreYears() {
            // Ejemplo sin Act phase

            // Arrange
            var SUT = new LoanTerm(1);

            // Assert
            Assert.That(SUT.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality() {
            // We can check if 2 values are equal or two references point to the same object in memory

            // int are value types
            //var a = 1;
            //var b = 1;

            //Assert.That(a, Is.EqualTo(b));

            // class (references types)
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality() {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample() {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);

            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(y, Is.SameAs(x));
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        public void TestDouble() {
            double a = 1.0 / 3.0;

            // Add tolerance to floating values
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));

            // Add tolerance percentage to floating values
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears() {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            // Property name "Message" not type safe
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("Message")
                .EqualTo("Please specify a value greater than 0. (Parameter 'years')"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Message
                .EqualTo("Please specify a value greater than 0. (Parameter 'years')"));

            // Correct Exception and ParamName but don't care about the message
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("ParamName")
                .EqualTo("years"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(
                ex => ex.ParamName.Equals("years")));
        }
    }
}
