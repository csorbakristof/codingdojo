namespace _20180905PeerReviewBingo
{
    internal class BingoChecker
    {
        public BingoChecker(int reviewerPerTarget)
        {
            this.ReviwerPerTarget = reviewerPerTarget;
        }

        public int ReviwerPerTarget { get; internal set; }

        internal bool IsValid(bool[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;
            if (!IsDiagonalEmply(matrix))
                return false;
            if (!AllRowSumsEqual(matrix, ReviwerPerTarget))
                return false;
            if (!AllColSumsEqual(matrix, ReviwerPerTarget))
                return false;
            return true;
        }

        private bool AllRowSumsEqual(bool[,] matrix, int n)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int cnt = 0;
                for (int col = 0; col < matrix.GetLength(1); col++)
                    if (matrix[row, col])
                        cnt++;
                if (cnt != n)
                    return false;
            }
            return true;
        }

        private bool AllColSumsEqual(bool[,] matrix, int n)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                int cnt = 0;
                for (int row = 0; row < matrix.GetLength(1); row++)
                    if (matrix[row, col])
                        cnt++;
                if (cnt != n)
                    return false;
            }
            return true;
        }

        private bool IsDiagonalEmply(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (matrix[i, i])
                    return false;
            return true;
        }
    }
}
