namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Collections.Generic;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class LineResolverTest
    {
        [Test]
        public void ShouldHaveOneCandidateWhenCreatedWithZeroSpec()
        {
            var resolver = new LineResolver(2, new LineSpecification(new[] { 0 }), 0, LineType.Row);

            resolver.ToString().ShouldBe("0:Row:[0]:1");
        }

        [Test]
        public void ShouldThrowEventsWhenResolveCell()
        {
            var cellResolvedCount = 0;
            var lineResolvedCount = 0;

            var resolver = new LineResolver(2, new LineSpecification(new[] { 1 }), 0, LineType.Row);
            var list = new List<Tuple<int, Cell>> { new Tuple<int, Cell>(1, Cell.White) };
            resolver.CellResolved += (sender, eventArgs) => { cellResolvedCount++; };
            resolver.LineResolved += (sender, eventArgs) => { lineResolvedCount++; };

            resolver.ResolveGivenCellValues(new Line(new[] { Cell.Black, Cell.Unknown }), list);

            resolver.IsResolved.ShouldBe(true);
            cellResolvedCount.ShouldBe(1);
            lineResolvedCount.ShouldBe(1);
        }

        // [Test]
        // public void ShouldRecalculateStateForN7K1_2()
        // {
        // var resolver = new LineResolver(7, new LineSpecification(new[] { 1, 2 }), 0, LineType.Row);
        // resolver.GenerateCandidates();
        // resolver.RecalculateInit(new Line(7));
        // resolver.StateLine.ShouldBe(new Line(7));
        // resolver.CellsResolved.ShouldBe(0);
        // }

        // [Test]
        // public void ShouldRecalculateStateForN5K1_2()
        // {
        // var resolver = new LineResolver(5, new LineSpecification(new[] { 1, 2 }), 0, LineType.Row);
        // resolver.GenerateCandidates();
        // resolver.RecalculateInit(new Line(7));
        // resolver.StateLine.ShouldBe(
        // new Line(new[] { Cell.Unknown, Cell.Unknown, Cell.Unknown, Cell.Black, Cell.Unknown }));
        // resolver.CellsResolved.ShouldBe(1);
        // }
    }
}