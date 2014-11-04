namespace arabcewicz.NonogramTests.Unit
{
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class LineTests
    {
        [Test]
        public void ShouldAndOperatorThrowExceptionWhenLinesAreNotTheSameLenght()
        {
            var line1 = new Line(new[] { Cell.Black, Cell.Unknown });
            var line2 = new Line(new[] { Cell.Black });

            // ReSharper disable once UnusedVariable
            Should.Throw<LinesAreNotTheSameLengthException>(() => { var result = line1 & line2; });
        }

        [Test]
        public void ShouldAndOperatorWorkWhenLinesAreTheSameLength()
        {
            var line1 =
                new Line(
                    new[]
                        {
                            Cell.Black, Cell.Black, Cell.Black, Cell.White, Cell.White, Cell.White, Cell.Unknown, 
                            Cell.Unknown, Cell.Unknown
                        });
            var line2 =
                new Line(
                    new[]
                        {
                            Cell.Black, Cell.White, Cell.Unknown, Cell.Black, Cell.White, Cell.Unknown, Cell.Black, 
                            Cell.White, Cell.Unknown
                        });

            var result = line1 & line2;

            result.Length.ShouldBe(9);
            result[0].ShouldBe(Cell.Black);
            result[1].ShouldBe(Cell.Unknown);
            result[2].ShouldBe(Cell.Unknown);
            result[3].ShouldBe(Cell.Unknown);
            result[4].ShouldBe(Cell.White);
            result[5].ShouldBe(Cell.Unknown);
            result[6].ShouldBe(Cell.Unknown);
            result[7].ShouldBe(Cell.Unknown);
            result[8].ShouldBe(Cell.Unknown);
        }

        [Test]
        public void ShouldBeEqualWhenLinesHasTheSameCells()
        {
            var line1 = new Line(new[] { Cell.Black, Cell.Unknown, Cell.White });
            var line2 = new Line(new[] { Cell.Black, Cell.Unknown, Cell.White });

            var result = line1 == line2;

            result.ShouldBe(true);
        }

        [Test]
        public void ShouldCopyAsDeepClone()
        {
            var line = new Line(new[] { Cell.Black, Cell.Unknown });

            var copy = line.Copy();

            ReferenceEquals(line, copy).ShouldBe(false);

            // ReSharper disable once ReferenceEqualsWithValueType
            ReferenceEquals(line[0], copy[0]).ShouldBe(false);
            (line[0] == copy[0]).ShouldBe(true);
        }

        [Test]
        public void ShouldIndexOperatorReturnProperCell()
        {
            var line = new Line(new[] { Cell.Black, Cell.White });

            var cell = line[0];

            cell.ShouldBe(Cell.Black);
        }

        [Test]
        public void ShouldNotBeEqualWhenLinesHasDifferentCells()
        {
            var line1 = new Line(new[] { Cell.Black, Cell.Unknown, Cell.Unknown });
            var line2 = new Line(new[] { Cell.Black, Cell.Unknown, Cell.White });

            var result = line1 == line2;

            result.ShouldBe(false);
        }
    }
}