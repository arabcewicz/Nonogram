namespace arabcewicz.Nonogram
{
    using System;

    public class CellResolvedEventArgs : EventArgs
    {
        public int ColIndex { get; set; }
        public LineType Resolver { get; set; }
        public int RowIndex { get; set; }
        public Cell Value { get; set; }

        public CellResolvedEventArgs Copy()
        {
            return (CellResolvedEventArgs)MemberwiseClone();
        }
    }
}