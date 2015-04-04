namespace arabcewicz.Nonogram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NonogramResolver
    {
        private List<CellResolvedEventArgs> _cellsResolved = new List<CellResolvedEventArgs>();
        private List<LineResolver> _colsResolvers;
        private List<LineSpecification> _colsSpec;
        private Grid _grid;
        private List<LineResolver> _rowsResolvers;
        private List<LineSpecification> _rowsSpec;

        public NonogramResolver(int rows, int cols)
        {
            Cols = cols;
            Rows = rows;
            _grid = new Grid(rows, cols);
        }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public bool IsResolved
        {
            get { return _rowsResolvers.All(r => r.IsResolved) && _colsResolvers.All(r => r.IsResolved); }
        }

        public NonogramResolver Copy()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var result = new NonogramResolver(Rows, Cols)
                             {
                                 _rowsSpec = _rowsSpec, 
                                 _colsSpec = _colsSpec, 
                             };

            result._rowsResolvers = new List<LineResolver>(Rows);
            foreach (LineResolver resolver in _rowsResolvers)
            {
                result._rowsResolvers.Add(resolver.Copy());
            }

            result._colsResolvers = new List<LineResolver>(Cols);
            foreach (LineResolver resolver in _colsResolvers)
            {
                result._colsResolvers.Add(resolver.Copy());
            }

            result._cellsResolved = new List<CellResolvedEventArgs>(_cellsResolved.Count);
            foreach (CellResolvedEventArgs cellResolved in _cellsResolved)
            {
                result._cellsResolved.Add(cellResolved.Copy());
            }

            result._grid = _grid.Copy();

            return result;
        }

        public IEnumerable<Tuple<int, Line>> GetCandidatesOfFirstUnresolvedRow()
        {
            return _rowsResolvers.First(r => !r.IsResolved).GetUnresolvedCandidates();
        }

        public Line GetGridCol(int index)
        {
            var result = new Line(Rows);
            for (var i = 0; i < Rows; i++)
            {
                result[i] = _grid[i, index];
            }

            return result;
        }

        public Line GetGridRow(int index)
        {
            var result = new Line(Cols);
            for (var i = 0; i < Cols; i++)
            {
                result[i] = _grid[index, i];
            }

            return result;
        }

        public void LoadSpecificationFromText(string text)
        {
            _rowsSpec = new List<LineSpecification>(Rows);
            _colsSpec = new List<LineSpecification>(Cols);

            var lines = text.Split('\n');
            var linesRead = 0;

            foreach (string lineString in lines)
            {
                if (lineString.StartsWith("#") || lineString == string.Empty)
                {
                    continue;
                }

                var line = lineString.Split(',');

                if (linesRead < Rows)
                {
                    _rowsSpec.Add(new LineSpecification(line.Select(int.Parse).ToArray()));
                }
                else
                {
                    _colsSpec.Add(new LineSpecification(line.Select(int.Parse).ToArray()));
                }

                linesRead++;
            }
        }

        public IEnumerable<Grid> Resolve()
        {
            var result = new List<Grid>();
            InitializeLineResolvers();

            DetermineDeterminable();

            if (IsResolved)
            {
                result.Add(_grid);
            }
            else
            {
                var backtracker = new NonogramBacktracer(this);
                var guessingResult = backtracker.DoJob();
                result.AddRange(guessingResult.Select(r => r._grid));
            }

            return result;
        }

        public void StartListeningForEvents()
        {
            foreach (LineResolver resolver in _rowsResolvers)
            {
                resolver.CellResolved += CellResolvedEventHandler;
                resolver.LineResolved += LineResolvedEventHandler;
            }

            foreach (LineResolver resolver in _colsResolvers)
            {
                resolver.CellResolved += CellResolvedEventHandler;
                resolver.LineResolved += LineResolvedEventHandler;
            }
        }

        public void StopListeningForEvents()
        {
            foreach (LineResolver resolver in _rowsResolvers)
            {
                resolver.CellResolved -= CellResolvedEventHandler;
                resolver.LineResolved -= LineResolvedEventHandler;
            }

            foreach (LineResolver resolver in _colsResolvers)
            {
                resolver.CellResolved -= CellResolvedEventHandler;
                resolver.LineResolved -= LineResolvedEventHandler;
            }
        }

        public override string ToString()
        {
            return _grid.ToString();
        }

        public void TryToResolveGiven(Tuple<int, Line> candidate)
        {
            var rowIndex = candidate.Item1;
            _rowsResolvers[rowIndex].ResolveGivenGuess(GetGridRow(rowIndex), candidate.Item2);
        }

        private void CellResolvedEventHandler(object sender, CellResolvedEventArgs e)
        {
            _grid[e.RowIndex, e.ColIndex] = e.Value;
            _cellsResolved.Add(e);
        }

        private void DetermineDeterminable()
        {
            foreach (LineResolver row in _rowsResolvers.Where(row => !row.IsResolved))
            {
                if (IsResolved)
                {
                    break;
                }

                row.ResolveGivenCellValues(GetGridRow(row.Index), new List<Tuple<int, Cell>>());
            }

            foreach (LineResolver col in _colsResolvers.Where(col => !col.IsResolved))
            {
                if (IsResolved)
                {
                    break;
                }

                col.ResolveGivenCellValues(GetGridCol(col.Index), new List<Tuple<int, Cell>>());
            }
        }

        private void InitializeLineResolvers()
        {
            _rowsResolvers = new List<LineResolver>(Rows);
            _colsResolvers = new List<LineResolver>(Cols);

            var rowIndex = 0;
            foreach (LineSpecification rowSpec in _rowsSpec)
            {
                var lineResolver = new LineResolver(Cols, rowSpec, rowIndex, LineType.Row);
                lineResolver.GenerateCandidates();
                _rowsResolvers.Add(lineResolver);
                rowIndex++;
            }

            var colIndex = 0;
            foreach (LineSpecification colSpec in _colsSpec)
            {
                var lineResolver = new LineResolver(Rows, colSpec, colIndex, LineType.Col);
                lineResolver.GenerateCandidates();
                _colsResolvers.Add(lineResolver);
                colIndex++;
            }

            StartListeningForEvents();
        }

        private void LineResolvedEventHandler(object sender, EventArgs e)
        {
            while (_cellsResolved.Any())
            {
                var first = _cellsResolved[0];
                var resolver = first.Resolver;

                if (resolver == LineType.Row)
                {
                    var theSameEvents =
                        _cellsResolved.Where(a => a.Resolver == resolver && a.ColIndex == first.ColIndex).ToList();
                    _cellsResolved.RemoveAll(a => a.Resolver == resolver && a.ColIndex == first.ColIndex);

                    var changedCells = theSameEvents.Select(a => new Tuple<int, Cell>(a.RowIndex, a.Value));

                    _colsResolvers[first.ColIndex].ResolveGivenCellValues(GetGridCol(first.ColIndex), changedCells);
                }
                else
                {
                    var theSameEvents =
                        _cellsResolved.Where(a => a.Resolver == resolver && a.RowIndex == first.RowIndex).ToList();
                    _cellsResolved.RemoveAll(a => a.Resolver == resolver && a.RowIndex == first.RowIndex);

                    var changedCells = theSameEvents.Select(a => new Tuple<int, Cell>(a.ColIndex, a.Value));

                    _rowsResolvers[first.RowIndex].ResolveGivenCellValues(GetGridRow(first.RowIndex), changedCells);
                }
            }
        }
    }
}