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

        internal int ReviewNumWithDiagonal => ReviewNum + 1;

        internal void SetElement(int row, int col, bool value=true)
        {
            Matrix[row, col] = value;
            int d = value ? 1 : -1;
            ColumnSum[col]+=d;
            RowSum[row]+=d;
        }

        internal bool[,] GetMatrix()
        {
            SetDiagonal(true);
            for(int n=0; n<PeerNum*ReviewNum; n++)
            {
                (int r, int c) = GetRandomAllowedLocation(ReviewNumWithDiagonal);
                SetElement(r, c);
            }
            SetDiagonal(false);
            return Matrix;
        }

        private void SetDiagonal(bool v)
        {
            for (int i= 0; i < PeerNum; i++)
                SetElement(i, i, v);
        }

        internal bool IsEnabled(int row, int col, int reviewCount)
        {
            if (IsSomeoneFilled(row, col, reviewCount) || Matrix[row, col])
                return false;

            // For every column: can it still be fulfilled with non-filled rows and without diagonal?
            SetElement(row, col, true);
            bool isOK = true;
            for(int c=0; c<PeerNum; c++)
            {
                int missingCount = reviewCount - ColumnSum[c];
                int availableRowCount = GetAvailableRowCount(c, reviewCount);

                if (missingCount > availableRowCount)
                {
                    isOK = false;
                    break;
                }
            }
            SetElement(row, col, false);
            return isOK;
        }

        private int GetAvailableRowCount(int column, int reviewCount)
        {
            int availableRowCount = 0;
            for (int r = 0; r < PeerNum; r++)
                if (RowSum[r] < reviewCount && column != r)
                    availableRowCount++;
            return availableRowCount;
        }

        private bool IsSomeoneFilled(int row, int col, int reviewCount)
        {
            return ColumnSum[col] >= reviewCount || RowSum[row] >= reviewCount;
        }

        internal (int row, int col) GetRandomAllowedLocation(int reviewCount)
        {
            if (ReviewNum == 0)
                throw new ArgumentException("Bingo.ReviewNum cannot be zero.");
            var n = PeerNum;
            int i = random.Next(n*n);
            while(!IsEnabled(i/n, i%n, reviewCount))
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
