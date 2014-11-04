namespace arabcewicz.Nonogram
{
    public struct Cell
    {
        public static Cell Black = new Cell(true);
        public static Cell Unknown = new Cell(null);
        public static Cell White = new Cell(false);

        private readonly bool? _value;

        private Cell(bool? value) : this()
        {
            _value = value;
        }

        public static Cell operator &(Cell c1, Cell c2)
        {
            if (c1._value == true && c2._value == true)
            {
                return new Cell(true);
            }

            if (c1._value == false && c2._value == false)
            {
                return new Cell(false);
            }

            return new Cell(null);
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Cell other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Cell && Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            switch (_value)
            {
                case true:
                    return "@";
                case false:
                    return "X";
                default:
                    return "-";
            }
        }
    }
}