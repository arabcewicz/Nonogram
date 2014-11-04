namespace arabcewicz.NonogramTests.Unit
{
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class LineSpecificationTests
    {
        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 1, 2 }, 5)]
        [TestCase(new[] { 1, 2, 2 }, 12)]
        public void ShouldBeFeasibleWhenIsCorrect(int[] specification, int length)
        {
            var spec = new LineSpecification(specification);

            var result = spec.IsFeasibleForLineLength(length);

            result.ShouldBe(true);
        }

        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 1, 2 }, 4)]
        [TestCase(new[] { 1, 2, 2 }, 7)]
        public void ShouldCoveringBeDeterminedCorrectly(int[] specification, int covering)
        {
            var spec = new LineSpecification(specification);

            var result = spec.Covering;

            result.ShouldBe(covering);
        }

        [Test]
        public void ShouldNotBeFeasibleWhenIsUncorrect()
        {
            var spec = new LineSpecification(new[] { 1, 2 });

            var result = spec.IsFeasibleForLineLength(3);

            result.ShouldBe(false);
        }
    }
}