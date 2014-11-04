namespace arabcewicz.Nonogram
{
    using System;
    using System.Text;

    public class Grid
    {
        private readonly Cell[,] _grid;

        public Grid(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            _grid = new Cell[rows, cols];
        }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public Cell this[int rowIndex, int colIndex]
        {
            get { return _grid[rowIndex, colIndex]; }
            set { _grid[rowIndex, colIndex] = value; }
        }

        public Grid Copy()
        {
            var result = new Grid(Rows, Cols);
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    result[r, c] = this[r, c];
                }
            }

            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder(_grid.Length + _grid.GetLength(0));
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Cols; c++)
                {
                    result.Append(_grid[r, c]);
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }
}