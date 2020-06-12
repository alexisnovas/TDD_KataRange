using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using Range_Exercise;

namespace TestsProject
{
    [TestFixture]
    internal sealed class TestRangeClass
    {
        static void Main()
        {
        }

        [Test]
        public void Constructor_IntervalWithSpaceInFront_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range(" (1,5)");
            char space = ' ';
            char firstChar = range.interval[0];

            Assert.That(firstChar, Is.Not.EqualTo(space));
        }

        [Test]
        public void Constructor_IntervalWithSpaceInEnd_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(1,5) ");
            char space = ' ';
            char lastChar = range.interval.Last();

            Assert.That(lastChar, Is.Not.EqualTo(space));
        }

        [Test]
        public void Constructor_firstIndexCorrect_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(1,5)");
            bool result = false;

            if (range.interval[0] == '(' || range.interval[0] == '[')
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Constructor_firstIndexIncorrect_Pass()
        {
            Assert.That(() => new Range_Exercise.Range("#0,2)"), Throws.Exception);
        }

        [Test]
        public void Constructor_lastIndexCorrect_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10]");
            bool result = false;

            if (range.interval.Last() == ')' || range.interval.Last() == ']')
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Constructor_lastIndexIncorrect_Pass()
        {
            Assert.That(() => new Range_Exercise.Range("(0,2#"), Throws.Exception);
        }

        [Test]
        public void Constructor_IntervalContainsLetters_Exception()
        {
            Assert.That(() => new Range_Exercise.Range("(0,2r)"), Throws.Exception);
        }

        [Test]
        public void Constructor_leftNumber_rightNumber_areNotIntergers_Exception()
        {
            Assert.That(() => new Range_Exercise.Range("(0,^2)"), Throws.Exception);
        }

        [Test]
        public void Constructor_leftNumber_IsGreaterThan_rightNumber_Exception()
        {
            Assert.That(() => new Range_Exercise.Range("(12,7]"), Throws.Exception);
        }

        [Test]
        public void Constructor_leftSideOpen_rightSideOpen_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(2,10)");

            bool result = false;

            if (range.AllPoints().First() == 3 && range.AllPoints().Last() == 9)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Constructor_leftSideOpen_rightSideClosed_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(2,10]");

            bool result = false;

            if (range.AllPoints().First() == 3 && range.AllPoints().Last() == 10)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Constructor_leftSideClosed_rightSideOpened_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10)");

            bool result = false;

            if (range.AllPoints().First() == 2 && range.AllPoints().Last() == 9)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Constructor_leftSideClosed_rightSideClosed_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10]");

            bool result = false;

            if (range.AllPoints().First() == 2 && range.AllPoints().Last() == 10)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_RangeContainsSomeNumber_True()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10)");

            bool result = range.Contains(6);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_RangeNotContainsSomeNumber_False()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10)");

            bool result = range.Contains(12);

            Assert.IsFalse(result);
        }

        [Test]
        public void DoesNotContains_RangeNotContainsSomeNumber_True()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10)");

            bool result = range.DoesNotContains(12);

            Assert.IsTrue(result);
        }

        [Test]
        public void DoesNotContains_RangeContainsSomeNumber_False()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[2,10)");

            bool result = range.DoesNotContains(4);

            Assert.IsFalse(result);
        }

        [Test]
        public void AllPoints_GiveAllPointsCorrectly_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[15,20)");
            List<int> expected = new List<int>();
            expected.Add(15);
            expected.Add(16);
            expected.Add(17);
            expected.Add(18);
            expected.Add(19);

            Assert.That(range.AllPoints(), Is.EqualTo(expected));
        }

        [Test]
        public void OverlapsRange_OverlapExist_True()
        { 
            Range_Exercise.Range range1 = new Range_Exercise.Range("(2,8]");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[4,12]");

            bool result = range1.OverlapsRange(range2);

            Assert.IsTrue(result);
        }

        [Test]
        public void OverlapsRange_OverlapDoesNotExist_False()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20)");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[23,30)");

            bool result = range1.OverlapsRange(range2);

            Assert.IsFalse(result);
        }

        [Test]
        public void ContainsRange_AllRangeContained_True()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20)");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[16,19]");

            bool result = range1.ContainsRange(range2);

            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsRange_NotAllRangeContained_False()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20)");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[16,23]");

            bool result = range1.ContainsRange(range2);

            Assert.IsFalse(result);
        }

        [Test]
        public void DoesNotContainsRange_RangeNotContained_True()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20)");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[22,30]");

            bool result = range1.DoesNotContainsRange(range2);

            Assert.IsTrue(result);
        }

        [Test]
        public void DoesNotContainsRange_RangeContained_False()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20)");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[16,18]");

            bool result = range1.DoesNotContainsRange(range2);

            Assert.IsFalse(result);
        }

        [Test]
        public void EndPoints_leftSideOpen_rightSideOpen_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(15,20)");
            int[] expected = range.EndPoints();
            bool result = false;

            if (range.leftNumber + 1 == expected[0] && range.rightNumber - 1 == expected[1])
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void EndPoints_leftSideOpen_rightSideClosed_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("(15,20]");
            int[] expected = range.EndPoints();
            bool result = false;

            if (range.leftNumber + 1 == expected[0] && range.rightNumber == expected[1])
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void EndPoints_leftSideClosed_rightSideOpen_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[15,20)");
            int[] expected = range.EndPoints();
            bool result = false;

            if (range.leftNumber == expected[0] && range.rightNumber - 1 == expected[1])
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void EndPoints_leftSideClosed_rightSideClosed_Pass()
        {
            Range_Exercise.Range range = new Range_Exercise.Range("[15,20]");
            int[] expected = range.EndPoints();
            bool result = false;

            if (range.leftNumber == expected[0] && range.rightNumber == expected[1])
            {
                result = true;
            }

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_RangesAreEqual_True()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20]");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[15,20]");

            bool result = range1.Equals(range2);

            Assert.IsTrue(result);
        }


        [Test]
        public void Equals_RangesAreNotEqual_False()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20]");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[12,20]");

            bool result = range1.Equals(range2);

            Assert.IsFalse(result);
        }

        [Test]
        public void NotEquals_RangesAreNotEqual_True()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20]");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[12,20]");

            bool result = range1.NotEquals(range2);

            Assert.IsTrue(result);
        }

        [Test]
        public void NotEquals_RangesNotEqual_False()
        {
            Range_Exercise.Range range1 = new Range_Exercise.Range("[15,20]");
            Range_Exercise.Range range2 = new Range_Exercise.Range("[15,20]");

            bool result = range1.NotEquals(range2);

            Assert.IsFalse(result);
        }











    }
}


