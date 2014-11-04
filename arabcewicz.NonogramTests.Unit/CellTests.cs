namespace arabcewicz.NonogramTests.Unit
{
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class CellTests
    {
        [Test]
        public void ShouldInitializeCellArrayAsUnknown()
        {
            var cell = new Cell[1];
            cell[0].ShouldBe(Cell.Unknown);
        }

        [Test]
        public void ShouldToStringReturnPropoerSymbol()
        {
            Cell.Black.ToString().ShouldBe("@");
            Cell.White.ToString().ShouldBe("X");
            Cell.Unknown.ToString().ShouldBe("-");
        }

        [Test]
        public void TestAndOperator()
        {
            (Cell.Black & Cell.Black).ShouldBe(Cell.Black);
            (Cell.Black & Cell.White).ShouldBe(Cell.Unknown);
            (Cell.Black & Cell.Unknown).ShouldBe(Cell.Unknown);
            (Cell.Black & Cell.White).ShouldBe(Cell.Unknown);
            (Cell.White & Cell.White).ShouldBe(Cell.White);
            (Cell.Black & Cell.Unknown).ShouldBe(Cell.Unknown);
            (Cell.Unknown & Cell.Black).ShouldBe(Cell.Unknown);
            (Cell.Unknown & Cell.White).ShouldBe(Cell.Unknown);
            (Cell.Unknown & Cell.Unknown).ShouldBe(Cell.Unknown);
        }

        [Test]
        public void TestEqualityOperator()
        {
            // ReSharper disable EqualExpressionComparison
            (Cell.Black == Cell.Black).ShouldBe(true);
            (Cell.Black == Cell.White).ShouldBe(false);
            (Cell.Black == Cell.Unknown).ShouldBe(false);
            (Cell.White == Cell.Black).ShouldBe(false);
            (Cell.White == Cell.White).ShouldBe(true);
            (Cell.White == Cell.Unknown).ShouldBe(false);
            (Cell.Unknown == Cell.Black).ShouldBe(false);
            (Cell.Unknown == Cell.White).ShouldBe(false);
            (Cell.Unknown == Cell.Unknown).ShouldBe(true);
            // ReSharper restore EqualExpressionComparison
        }
    }
}