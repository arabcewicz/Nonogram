namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    public class NonogramResolverTest10X10WakeUp
    {
        [Test]
        public void ShouldResolve()
        {
            const string text = @"#
#--- rows ---
2,2
2,4,2
1,3,2,1
4,3
4,3
3,4
2,5
6
4
2,2
#--- cols ---
2
2,4
1,6,1
5,3
4,3
1,4
9
1,6,1
2,4
2
";
            var resolver = new NonogramResolver(10, 10);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result.ForEach(Console.WriteLine);
        }
    }
}