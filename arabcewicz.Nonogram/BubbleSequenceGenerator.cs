namespace arabcewicz.Nonogram
{
    using System.Collections.Generic;
    using System.Linq;

    public class BubbleSequenceGenerator
    {
        private readonly List<int[]> _result = new List<int[]>();

        private readonly int[] _workingBuffer;

        public BubbleSequenceGenerator(int length, int rank)
        {
            Rank = rank;
            Length = length;
            _workingBuffer = new int[Length];
        }

        public int Rank { get; private set; }
        public int Length { get; set; }

        public List<int[]> GenerateSequences()
        {
            Try(0);
            return _result;
        }

        private void Try(int position)
        {
            for (var k = 0; k <= Rank; k++)
            {
                _workingBuffer[position] = k;
                var sum = _workingBuffer.Take(position).Sum();
                if (sum <= Rank)
                {
                    if (position < Length - 1)
                    {
                        Try(position + 1);
                    }
                    else
                    {
                        if (_workingBuffer.Sum() == Rank)
                        {
                            _result.Add(_workingBuffer.Clone() as int[]);
                        }
                    }
                }
            }
        }
    }
}