namespace arabcewicz.Nonogram
{
    using System.Collections.Generic;
    using System.Linq;

    public class NonogramBacktracer
    {
        private readonly List<NonogramResolver> _solutions = new List<NonogramResolver>();
        private readonly Stack<NonogramResolver> _stack = new Stack<NonogramResolver>();

        public NonogramBacktracer(NonogramResolver resolver)
        {
            Resolver = resolver;
        }

        public NonogramResolver Resolver { get; set; }

        public IEnumerable<NonogramResolver> DoJob()
        {
            DoSteps();
            return _solutions;
        }

        public void DoSteps()
        {
            _stack.Push(Resolver.Copy());

            var candidates = Resolver.GetCandidatesOfFirstUnresolvedRow().ToList();

            foreach (var candidate in candidates)
            {
                try
                {
                    Resolver.TryToResolveGiven(candidate);
                    if (!Resolver.IsResolved)
                    {
                        DoSteps();
                    }
                    else
                    {
                        Resolver.StopListeningForEvents();
                        _solutions.Add(Resolver);
                    }
                }
                catch (BacktraceException) 
                { }
                Resolver = _stack.Peek().Copy();
                Resolver.StartListeningForEvents();
            }

            Resolver = _stack.Pop();
            Resolver.StartListeningForEvents();
        }
    }
}