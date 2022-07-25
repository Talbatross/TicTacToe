namespace TicTacToe
{
    public class BestMove
    {
        public int Value { get; private set; } = int.MinValue;
        public int Row { get; private set; }= -1;
        public int Column { get; private set; }= -1;
        
        public void SetBest(int val, int row, int column)
        {
            Value = val;
            Row = row;
            Column = column;
        }

        public void Clear()
        {
            Value = int.MinValue;
            Row = -1;
            Column = -1;
        }
        
    }
}