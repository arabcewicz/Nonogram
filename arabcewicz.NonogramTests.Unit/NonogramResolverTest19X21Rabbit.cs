namespace arabcewicz.NonogramTests.Unit
{
    using System;
    using System.Linq;
    using arabcewicz.Nonogram;
    using NUnit.Framework;
    using Shouldly;

    public class NonogramResolverTest19X21Rabbit
    {
        [Test]
        public void ShouldResolve()
        {
            const string text = @"#
#--- rows ---
2
3
3
3
3,3
#------
2,5
3,5
8
3,3,4
17
#-----
6,11
3,13
9,4
8,3,2,1
6,7,2
#----
13,2
4,6,2
5,3,4
3,10
#--- cols ---
3
5
6,2
2,5,2
8,3,3
#----
5,4,6
5,4,7
3,3,8,1
3,7,2
3,7,2
#-----
3,5,1,2
2,5,3,1
4,4,1
4,4,1
4,6
#----
4,5
10
6,1
2,1
4
#----
2
";
            var resolver = new NonogramResolver(19, 21);
            resolver.LoadSpecificationFromText(text);
            var result = resolver.Resolve().ToList();

            result.Count().ShouldBe(1);
            result.ForEach(Console.WriteLine);
        }
    }
}