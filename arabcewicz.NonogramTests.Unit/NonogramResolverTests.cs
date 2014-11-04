namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class NonogramResolverTests
    {
        [Test]
        public void ShouldResolve2X2WithZeroColumn()
        {
            const string spec = @"#
#--- rows ---
1
1
#--- cols ---
0
2";
            var resolver = new NonogramResolver(2, 2);
            resolver.LoadSpecificationFromText(spec);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result.ForEach(Console.WriteLine);
        }

        [Test]
        public void ShouldResolve3X3WithoutBacktracing()
        {
            var nl = Environment.NewLine;

            var spec = "#--- rows ---" + nl + "1,1" + nl + "2" + nl + "1" + nl + "#--- cols ---" + nl + "2" + nl + "1"
                       + nl + "1,1";

            var resolver = new NonogramResolver(3, 3);
            resolver.LoadSpecificationFromText(spec);

            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result[0].ToString().ShouldBe("@X@" + nl + "@@X" + nl + "XX@" + nl);
            Console.WriteLine(resolver.ToString());
        }

        [Test]
        public void ShouldResolve5X5OneForwardNoBackwards()
        {
            const string text = @"#
#--- rows ---
1
1,1
1,1
5
1
#--- cols ---
2
1,1
1,2
1,1
2
";
            var resolver = new NonogramResolver(5, 5);
            resolver.LoadSpecificationFromText(text);

            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(3);

            result[0].ToString().ShouldBe(@"XX@XX
X@X@X
@X@XX
@@@@@
XXXX@
");
            result[1].ToString().ShouldBe(@"XX@XX
X@X@X
@XXX@
@@@@@
XX@XX
");
            result[2].ToString().ShouldBe(@"XX@XX
X@X@X
XX@X@
@@@@@
@XXXX
");

            result.ForEach(Console.WriteLine);
        }

        [Test]
        public void ShouldResolve5X5OneStepBackwards()
        {
            const string text = @"#
#--- rows ---
1,1
2
3
2
1,1
#--- cols ---
1,1
2
3
2
1,1
";
            var resolver = new NonogramResolver(5, 5);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(6);
            result.ForEach(Console.WriteLine);
        }

        [Test]
        public void ShouldResolve5X5WithoutSolution()
        {
            const string text = @"#
#--- rows ---
2
2
3
2
2
#--- cols ---
2
2
3
2
2
";
            var resolver = new NonogramResolver(5, 5);
            resolver.LoadSpecificationFromText(text);

            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(0);
        }
    }
}