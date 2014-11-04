namespace arabcewicz.Nonogram
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public struct LineSpecification : IEnumerable<int>
    {
        private readonly int[] _specification;

        public LineSpecification(int[] specification)
        {
            _specification = specification;
        }

        public int Length { get { return _specification.Length; } }

        public int Covering { get { return _specification.Sum() + Length - 1; } }

        public int this[int index] { get { return _specification[index]; } }

        public IEnumerator<int> GetEnumerator()
        {
            return _specification.ToList().GetEnumerator();
        }

        public bool IsFeasibleForLineLength(int length)
        {
            return length >= Covering;
        }

        public override string ToString()
        {
            return string.Join(",", _specification);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _specification.GetEnumerator();
        }
    }
}