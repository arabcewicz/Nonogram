namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    public class NonogramResolverTest20X25Cherries
    {
        [Test]
        public void ShouldResolve()
        {
            const string text = @"#
#--- rows ---
3
3,4
3,8
3,11
14
#----
4,4
2
1,1
1,1
1,1
#----
1,1
1,1
1,1
4,1
3,2,4
#----
4,1,3,2
6,4,1
6,6
4,6
4
#--- cols ---
0
0
0
0
0
#------
4
1,6
2,6
3,3,4
3,2,2,3
#------
3,1,4
3,1
3
3
3,1
#----
2,2,4
3,2,6
3,8
5,1,4
5,2,3
#----
5,4
5
3
3
1
";
            var resolver = new NonogramResolver(20, 25);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(54);
            result.ForEach(Console.WriteLine);
        }
    }
}