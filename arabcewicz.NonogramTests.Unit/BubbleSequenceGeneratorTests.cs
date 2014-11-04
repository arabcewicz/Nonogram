namespace arabcewicz.NonogramTests.Unit
{
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class BubbleSequenceGeneratorTests
    {
        [Test]
        public void ShouldGenerateSequencesForLimit3AndLength3()
        {
            var tmp = new BubbleSequenceGenerator(3, 3);

            var result = tmp.GenerateSequences();

            result.Count.ShouldBe(10);
            CollectionAssert.Contains(result, new[] { 0, 0, 3 });
            CollectionAssert.Contains(result, new[] { 0, 1, 2 });
            CollectionAssert.Contains(result, new[] { 0, 2, 1 });
            CollectionAssert.Contains(result, new[] { 0, 3, 0 });
            CollectionAssert.Contains(result, new[] { 1, 0, 2 });
            CollectionAssert.Contains(result, new[] { 1, 1, 1 });
            CollectionAssert.Contains(result, new[] { 1, 2, 0 });
            CollectionAssert.Contains(result, new[] { 2, 0, 1 });
            CollectionAssert.Contains(result, new[] { 2, 1, 0 });
            CollectionAssert.Contains(result, new[] { 3, 0, 0 });
        }

        [Test]
        public void ShouldGenerateSequencesForLimit4AndLength2()
        {
            var tmp = new BubbleSequenceGenerator(2, 4);

            var result = tmp.GenerateSequences();

            result.Count.ShouldBe(5);
            CollectionAssert.Contains(result, new[] { 0, 4 });
            CollectionAssert.Contains(result, new[] { 1, 3 });
            CollectionAssert.Contains(result, new[] { 2, 2 });
            CollectionAssert.Contains(result, new[] { 3, 1 });
            CollectionAssert.Contains(result, new[] { 4, 0 });
        }

        [Test]
        public void ShouldGenerateZerosWhenRankIsZero()
        {
            var gen = new BubbleSequenceGenerator(2, 0);

            var result = gen.GenerateSequences();

            result.Count.ShouldBe(1);
            CollectionAssert.Contains(result, new[] { 0, 0 });
        }
    }
}