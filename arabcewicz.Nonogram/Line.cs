namespace arabcewicz.Nonogram
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Line : IEnumerable<Cell>
    {
        private readonly Cell[] _line;

        public Line(int length)
        {
            _line = new Cell[length];
        }

        public Line(Cell[] line)
        {
            _line = line;
        }

        public int Length { get { return _line.Length; } }

        public Cell this[int index] { get { return _line[index]; } set { _line[index] = value; } }

        public static Line operator &(Line l1, Line l2)
        {
            if (l1.Length != l2.Length)
            {
                throw new LinesAreNotTheSameLengthException();
            }

            var result = new Line(l1.Length);
            for (var i = 0; i < l1.Length; i++)
            {
                result._line[i] = l1._line[i] & l2._line[i];
            }

            return result;
        }

        public static bool operator ==(Line left, Line right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !Equals(left, right);
        }

        public Line Copy()
        {
            var result = new Line(Length);
            for (var i = 0; i < Length; i++)
            {
                result[i] = _line[i];
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Line)obj);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return _line.Select(c => c).GetEnumerator();
        }

        public override int GetHashCode()
        {
            return _line != null ? _line.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _line);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _line.GetEnumerator();
        }

        protected bool Equals(Line other)
        {
            for (var i = 0; i < Length; i++)
            {
                if (_line[i] != other._line[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}