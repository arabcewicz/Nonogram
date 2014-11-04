namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    public class NonogramResolverTest15X15
    {
        [Test]
        public void ShouldResolve()
        {
            const string text = @"#
#--- rows ---
3,3
2,3,2
2,1
2,1
2,5
#---
5,5
12
6,5
1,11
2,3,1
#----
2,5
3
5
8
13
#--- cols ---
1,1,1,1
1,1,1,1,1
1,2
1,4,3
2,5,3
#------
3,5,4
3,9
3,5
5,2
1,1,3,2
#-----
3,7,1
1,5,1
1,5,1
5,1
6
";
            var resolver = new NonogramResolver(15, 15);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result.ForEach(Console.WriteLine);
        }
    }
}