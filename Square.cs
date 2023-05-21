namespace BackDamka
{
    public enum eCheckerColours
    {
        Empty,
        O,
        X
    }

    public class Square
    {
         int Xlocation;
         int Ylocation;
         bool isKingBlock;

         eCheckerColours Colour = eCheckerColours.Empty;
         string[] possibleMoves;

        public Square(int Xlocation, int Ylocation)
        {
            this.Xlocation = Xlocation;
            this.Ylocation = Ylocation;
            this.isKingBlock = false;
            this.possibleMoves = new string[4];
        }

        public eCheckerColours GetColour()
        {
            return Colour;
        }

        public int GetXlocation()
        {
            return this.Xlocation;
        }

        public int GetYlocation()
        {
            return this.Ylocation;
        }

        public void RestartMoves()
        {
            for(int i = 0; i < 4; i++)
            {
                SetMove(null, i);
            }
        }

        public string GetElementFromArray(int i_Index)
        {
            return this.possibleMoves[i_Index];
        }

        public bool GetIsKing()
        {
            return this.isKingBlock;
        }

        public void SetIsKingBlock(bool i_KingBlock)
        {
            this.isKingBlock = i_KingBlock;
        }

        public void SetMove(string i_Move, int index)
        {
            this.possibleMoves[index] = i_Move;
        }

        public void SetColour(eCheckerColours i_NewColour)
        {
            Colour = i_NewColour;
        }

        public bool IsSquareEmpty()
        {
            return Colour == 0;
        }
    }
}
