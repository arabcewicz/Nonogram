namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    public class NonogramResolverTest20X20Irregular
    {
        [Test]
        public void ShouldResolve()
        {
            const string text = @"#
#--- rows ---
2,2,6,1
3,2,4,1
14,1,3
4,1,1,7
6,2,5,3
#----
2,6,1,1,1
1,1,3,3,4
4,3,4
2,10,1
2,1,6,3,4
#----
1,3,5,4
3,3,2,1,1
1,5,2,1,1
1,3,9
1,4,3,3
#----
2,5,3,1,3
11,2,4
1,14
2,3,3
1,1,6,1
#--- cols ---
1,2,1,2,1,1
10,1,2
4,2,1,4,1
7,4,1
1,2,2,1,3,2
#----
5,3,1,4,1
1,1,6,7
3,5,8
2,2,5,1,3,1
1,1,1,5,3,1
#----
1,1,2,3,1,1,4
1,3,1,1,2,3,3
1,1,1,1,4,7
1,4,3,1,3,1
2,2,1,2,1,1
#----
4,1,4,4
1,1,2,2,2,3
4,1,2,7
5,1,5
5,1,2,1,1,1
";
            var resolver = new NonogramResolver(20, 20);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result.ForEach(Console.WriteLine);
        }
    }
}