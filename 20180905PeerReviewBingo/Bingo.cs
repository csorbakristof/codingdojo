using System;
using System.Linq;

namespace _20180905PeerReviewBingo
{
    internal class Bingo
    {
        public Bingo()
        {
        }

        public Random random = new Random();

        public int[] ColumnSum { get; internal set; }
        public int[] RowSum { get; internal set; }
        public bool[,] Matrix { get; set; }

        public int PeerNum { get; set; }
        public int ReviewNum { get; set; }

        internal void UpdateColumnCountersWithNewElement(int row, int col)
        {
            ColumnSum[col]++;
            RowSum[row]++;
        }

        internal bool[,] GetMatrix()
        {
            for(int n=0; n<PeerNum*ReviewNum; n++)
            {
                (int r, int c) = GetRandomAllowedLocation();
                Matrix[r, c] = true;
                UpdateColumnCountersWithNewElement(r, c);
            }
            return Matrix;
        }

        internal bool IsEnabled(int row, int col, int reviewCount)
        {
            return (ColumnSum[col] < reviewCount && RowSum[row] < reviewCount
                && !Matrix[row,col]
                && row != col);
        }

        internal (int row, int col) GetRandomAllowedLocation()
        {
            if (ReviewNum == 0)
                throw new ArgumentException("Bingo.ReviewNum cannot be zero.");
            var n = PeerNum;
            int i = random.Next(n*n);
            while(!IsEnabled(i/n, i%n, ReviewNum))
                i = ++i % (n * n);
            return (row: i/n, col: i%n);
        }

        internal void Init(int n)
        {
            PeerNum = n;
            ColumnSum = new int[n];
            RowSum = new int[n];
            Matrix = new bool[n, n];
        }
    }
}
