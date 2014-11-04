namespace arabcewicz.Nonogram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LineResolver
    {
        private List<Line> _candidates = new List<Line>();

        public LineResolver(int lineLength, LineSpecification lineSpecification, int index, LineType lineType)
        {
            LineLength = lineLength;
            LineSpecification = lineSpecification;
            Index = index;
            LineType = lineType;
            GenerateCandidates();
        }

        public event EventHandler<CellResolvedEventArgs> CellResolved;
        public event EventHandler LineResolved;

        public int LineLength { get; private set; }
        public LineSpecification LineSpecification { get; private set; }
        public int Index { get; private set; }
        public LineType LineType { get; private set; }
        public int Covering { get { return LineSpecification.Covering; } }

        public bool IsResolved { get; private set; }

        public LineResolver Copy()
        {
            var result = new LineResolver(LineLength, LineSpecification, Index, LineType)
                             {
                                 _candidates =
                                     new List<Line>(
                                     _candidates.Count), 
                                 IsResolved = IsResolved
                             };
            foreach (Line candidate in _candidates)
            {
                result._candidates.Add(candidate.Copy());
            }

            return result;
        }

        public IEnumerable<Tuple<int, Line>> GetUnresolvedCandidates()
        {
            return _candidates.Select(candidate => new Tuple<int, Line>(Index, candidate));
        }

        public void ResolveGivenCellValues(Line baseLine, IEnumerable<Tuple<int, Cell>> cells)
        {
            RemoveMismatchedCandidates(cells);
            Resolve(baseLine);
        }

        public void ResolveGivenGuess(Line baseLine, Line guess)
        {
            RemoveCandidatesExcept(guess);
            Resolve(baseLine);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:[{2}]:{3}", Index, LineType, LineSpecification, _candidates.Count);
        }

        protected virtual void OnCellResolved(CellResolvedEventArgs e)
        {
            var handler = CellResolved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLineResolved(EventArgs e)
        {
            var handler = LineResolved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void GenerateCandidates()
        {
            if (Covering == 0)
            {
                _candidates.Add(new Line(Enumerable.Repeat(Cell.White, LineLength).ToArray()));
                return;
            }

            var generator = new BubbleSequenceGenerator(LineSpecification.Length + 1, LineLength - Covering);

            var bubblesList = generator.GenerateSequences();
            foreach (var bubbles in bubblesList)
            {
                var b = 0;
                var resultList = new List<Cell>(LineLength + 1);
                foreach (int spec in LineSpecification)
                {
                    resultList.AddRange(Enumerable.Repeat(Cell.White, bubbles[b++]));
                    resultList.AddRange(Enumerable.Repeat(Cell.Black, spec));
                    resultList.Add(Cell.White);
                }

                resultList.RemoveAt(resultList.Count - 1);
                resultList.AddRange(Enumerable.Repeat(Cell.White, bubbles[bubbles.Length - 1]));

                _candidates.Add(new Line(resultList.ToArray()));
            }
        }

        private void RemoveCandidatesExcept(Line guess)
        {
            _candidates.RemoveAll(c => c != guess);
            IsResolved = true;
        }

        private void RemoveMismatchedCandidates(IEnumerable<Tuple<int, Cell>> cells)
        {
            foreach (var cell in cells)
            {
                _candidates.RemoveAll(c => c[cell.Item1] != cell.Item2);
            }

            switch (_candidates.Count)
            {
                case 1:
                    IsResolved = true;
                    break;
                case 0:
                    throw new BacktraceException();
            }
        }

        private void Resolve(Line baseLine)
        {
            var newLine = _candidates.Aggregate((current, candidate) => current & candidate);
            if (newLine == baseLine)
            {
                return;
            }

            for (var i = 0; i < LineLength; i++)
            {
                if (newLine[i] != baseLine[i])
                {
                    var cellEventArgs = new CellResolvedEventArgs
                                            {
                                                RowIndex = LineType == LineType.Row ? Index : i, 
                                                ColIndex = LineType == LineType.Col ? Index : i, 
                                                Value = newLine[i], 
                                                Resolver = LineType
                                            };
                    OnCellResolved(cellEventArgs);
                }
            }

            OnLineResolved(EventArgs.Empty);
        }
    }
}