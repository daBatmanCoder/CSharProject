using System;
using System.Collections.Generic;
using System.Text;

namespace BackDamka
{
    public class Board
    {
        Square[,] TheBoard;
        int rowLength;

        public Board(int sizeOfBoard)
        {
            this.TheBoard = new Square[sizeOfBoard, sizeOfBoard];
            this.rowLength = sizeOfBoard;

            initBoard();
        }

        private void initBoard()
        {
            int sizeOfBoard = this.rowLength;

            for (int i = 0; i < sizeOfBoard; i++)
            {
                for (int j = 0; j < sizeOfBoard; j++)
                {
                    Square newTile = new Square(i, j);
                    TheBoard[i, j] = newTile;

                    if ((i + j) % 2 != 0) // we only play on the "black" tiles
                    {
                        if (i < ((sizeOfBoard / 2) - 1)) // top rows for the O players 
                        {
                            TheBoard[i, j].SetColour(eCheckerColours.O);
                        }
                        else
                        {
                            if (i >= ((sizeOfBoard / 2) + 1)) // down rows for the X players
                            {
                                TheBoard[i, j].SetColour(eCheckerColours.X);
                            }
                        }
                    }
                }
            }
        }
        public Square[,] GetBoard()
        {
            return this.TheBoard;
        }

        public int GetRowLength()
        {
            return this.rowLength;
        }

        public void InitPlayers(out User i_User1, out User i_User2, string i_FirstName, string i_SecondName)
        {
            i_User1 = new User((eUserTypes)1, i_FirstName);
            i_User2 = new User((eUserTypes)0, i_SecondName);
            foreach (Square sq in TheBoard)
            {
               if((int)sq.GetColour() == 1)
                {
                    i_User1.squareListOfCheckers.Add(sq);
                }
               else
                {
                    if ((int)sq.GetColour() == 2)
                    {
                        i_User2.squareListOfCheckers.Add(sq);
                    }
                }
            }
        }

        public List<string> RandomMoveList(bool i_XTurn) // if the computer plays he is always O!!!
        {
            List<string> theListOfAllPossibleMoves = new List<string>();
            char xLocationInChar, yLocationInChar;
            StringBuilder returnString;
            eCheckerColours checkerColour;

            if (i_XTurn)
            {
                checkerColour = eCheckerColours.X;
            }
            else
            {
                checkerColour = eCheckerColours.O;
            }

            foreach (Square sq in TheBoard)
            {
                if (sq.GetColour().Equals(checkerColour))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (sq.GetElementFromArray(i) != null)
                        {
                            xLocationInChar = (char)(sq.GetXlocation() + 'a');
                            yLocationInChar = (char)(sq.GetYlocation() + 'A');
                            returnString = new StringBuilder();
                            returnString.Append(yLocationInChar);
                            returnString.Append(xLocationInChar);
                            returnString.Append(">");
                            returnString.Append(sq.GetElementFromArray(i));
                            theListOfAllPossibleMoves.Add(returnString.ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return theListOfAllPossibleMoves;
        } 

        public string RunComputerMove(bool i_XTurn)
        {
            Random rnd1;
            int theFirstNumberFromTheList;
            List<string> theListOfAllPossibleMoves = RandomMoveList(i_XTurn);

            rnd1 = new Random();
            theFirstNumberFromTheList = rnd1.Next(theListOfAllPossibleMoves.Count);
            
            return theListOfAllPossibleMoves[theFirstNumberFromTheList]; 
        }

        public bool CheckValidMove(Board TheBoard, string i_TheMove, bool i_XTurn, out bool o_DidAte)
        {
            o_DidAte = false;
            bool returnFlag = true;
            bool isValidMove = true;
            string sourceMove, destMove = "";

            if (!checkValidString(i_TheMove, TheBoard.GetBoard().GetLength(1)))
            {
                returnFlag = false;
            }
            else
            {
                sourceMove = i_TheMove.Substring(0, 2);
                destMove = i_TheMove.Substring(3);
                isValidMove = TheBoard.MovePiece(sourceMove, destMove, i_XTurn, out o_DidAte);
            }

            if (isValidMove && returnFlag)
            {
                if (i_XTurn) // X turn
                {
                    if (destMove[1] == 'a')
                    {
                        TheBoard.GetBoard()[(int)(destMove[1] - 'a'), (int)(destMove[0] - 'A')].SetIsKingBlock(true);
                    }
                }
                else // O turn
                {
                    if (destMove[1] == 'a' + TheBoard.GetBoard().GetLength(1) - 1)
                    {
                        TheBoard.GetBoard()[(int)(destMove[1] - 'a'), (int)(destMove[0] - 'A')].SetIsKingBlock(true);
                    }
                }
            }

            return returnFlag && isValidMove;
        }

        private static bool checkValidString(string i_TheString, int i_LengthOfBoard)
        {
            bool returnflag = false;
            bool isOdd = true;
            int count = 0;

            if (i_TheString.Length == 5)
            {
                for (int i = 0; i < i_TheString.Length; i++)
                {
                    isOdd = Math.Abs(i_TheString[0] - i_TheString[1]) % 2 == 1 && Math.Abs(i_TheString[3] - i_TheString[4]) % 2 == 1;
                    if (isOdd)
                    {
                        if (i == 0 || i == 3)
                        {

                            returnflag = i_TheString[i] >= 'A' && i_TheString[i] < 'A' + i_LengthOfBoard;

                        }
                        else if (i == 1 || i == 4)
                        {

                            returnflag = i_TheString[i] >= 'a' && i_TheString[i] < 'a' + i_LengthOfBoard;
                        }
                        else if (i == 2)
                        {

                            returnflag = i_TheString[i] == '>';
                        }
                        else
                        {
                            break;
                        }

                        if (returnflag)
                        {
                            count++;
                        }
                    }
                }
            }

            return returnflag && count == 5;
        }

        public int TotalNumberOfPoints()
        {
            int XPoints = 0;
            int OPoints = 0;

            foreach (Square oneSquare in TheBoard)
            {
                if (oneSquare.GetColour().Equals(eCheckerColours.X))
                {
                    if (oneSquare.GetIsKing())
                    {
                        XPoints += 4;
                    }
                    else
                    {
                        XPoints += 1;
                    }
                }
                else if (oneSquare.GetColour().Equals(eCheckerColours.O))
                {
                    if (oneSquare.GetIsKing())
                    {
                        OPoints += 4;
                    }
                    else
                    {
                        OPoints += 1;
                    }
                }
            }

            return Math.Abs(XPoints - OPoints);
        }

        public bool MovePiece(string i_SourceLocation, string i_DestinationLocation, bool i_XTurn, out bool o_GotMoreEating)
        {
            o_GotMoreEating = false;
            bool returnFlag = false;
            bool notMatchXO = true;
            int xSourceLocation = (int)(i_SourceLocation[1] - 'a'); //  source [x, ?] where x = 0-7
            int ySourceLocation = (int)(i_SourceLocation[0] - 'A'); // source [?, y] where y = 0-7 [1,2]
            int xDestLocation = (int)(i_DestinationLocation[1] - 'a');//  dest [x, ?] where x = 0-7
            int yDestLocation = (int)(i_DestinationLocation[0] - 'A');// dest [?, y] where y = 0-7 [2,3]

            if(i_XTurn)
            {
                if (TheBoard[xSourceLocation, ySourceLocation].GetColour().Equals(eCheckerColours.O))
                {
                    notMatchXO = false;
                }
            }
            else
            {
                if (TheBoard[xSourceLocation, ySourceLocation].GetColour().Equals(eCheckerColours.X))
                {
                    notMatchXO = false;
                }
            }

            if(notMatchXO)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i_DestinationLocation.Equals(TheBoard[xSourceLocation, ySourceLocation].GetElementFromArray(i)))
                    {
                        TheBoard[xDestLocation, yDestLocation].SetColour(TheBoard[xSourceLocation, ySourceLocation].GetColour());
                        TheBoard[xSourceLocation, ySourceLocation].SetColour(eCheckerColours.Empty);
                        TheBoard[xDestLocation, yDestLocation].SetIsKingBlock(TheBoard[xSourceLocation, ySourceLocation].GetIsKing());
                        TheBoard[xSourceLocation, ySourceLocation].SetIsKingBlock(false);
                        if (Math.Abs(xSourceLocation - xDestLocation) == 2) // did EAT@!
                        {
                            TheBoard[((xDestLocation + xSourceLocation) / 2), ((yDestLocation + ySourceLocation) / 2)].SetColour(eCheckerColours.Empty);
                            TheBoard[((xDestLocation + xSourceLocation) / 2), ((yDestLocation + ySourceLocation) / 2)].SetIsKingBlock(false);
                            if(o_GotMoreEating = CheckIfContinueEating(TheBoard[xDestLocation, yDestLocation]))
                            {
                                updateContinueEating(TheBoard[(int)(i_DestinationLocation[1] - 'a'), (int)(i_DestinationLocation[0] - 'A')]);
                            }
                        }

                        returnFlag = true;
                        break;
                    }
                }
            }
           
            return returnFlag;
        }

        public bool IsGameEnded(bool i_XTurn)
        {
            bool returnFlag = false;
            List<string> theListOfAllPossibleMovesX = RandomMoveList(true); // randomMoveList gets if it is XTURN
            List<string> theListOfAllPossibleMovesO = RandomMoveList(false);

            if (i_XTurn)
            {
                if(theListOfAllPossibleMovesX.Count == 0)
                {
                    returnFlag = true;
                }
            }
            else
            {
                if (theListOfAllPossibleMovesO.Count == 0)
                {
                    returnFlag = true;
                }
            }

            return theListOfAllPossibleMovesX.Count == 0 && theListOfAllPossibleMovesO.Count == 0;
        }

        private bool CheckIfContinueEating(Square i_oneBoardSquare)
        {
            return canEatDownLeft(i_oneBoardSquare) || canEatDownRight(i_oneBoardSquare) || canEatUpRight(i_oneBoardSquare) || canEatUpLeft(i_oneBoardSquare);
        }

        public void updateSquarePossibleMove(bool i_XTurn) // possible moves without eating 
        {
            int indexer;
            string moveAsString;
            bool oneEaten = false;
            Square theChosenOne = new Square(1,1);
            StringBuilder destLocation = new StringBuilder();

            deleteAllPossibleMoves();
            foreach (Square checkerPlayer in TheBoard)  
            {
                indexer = 0;
                moveAsString = "";
                if ((checkerPlayer.GetXlocation() + checkerPlayer.GetYlocation()) % 2 == 1)
                {
                    if(i_XTurn)
                    {
                        if(checkerPlayer.GetColour().Equals(eCheckerColours.O))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (checkerPlayer.GetColour().Equals(eCheckerColours.X))
                        {
                            continue;
                        }
                    }

                    if (upRight(checkerPlayer)) // up right
                    {
                        moveAsString += (char)(checkerPlayer.GetYlocation() + 1 + (int)'A');
                        moveAsString += (char)(checkerPlayer.GetXlocation() - 1 + (int)'a');
                        checkerPlayer.SetMove(moveAsString, indexer);
                        indexer++;
                    }
                    else
                    {
                        if (canEatUpRight(checkerPlayer))
                        {
                            oneEaten = true;
                            theChosenOne = checkerPlayer;
                            break;
                        }
                    }

                    moveAsString = "";
                    if (upLeft(checkerPlayer)) // up left
                    {
                        moveAsString += (char)(checkerPlayer.GetYlocation() - 1 + (int)'A');
                        moveAsString += (char)(checkerPlayer.GetXlocation() - 1 + (int)'a');
                        checkerPlayer.SetMove(moveAsString, indexer);
                        indexer++;
                    }
                    else
                    {
                        if(canEatUpLeft(checkerPlayer))
                        {
                            oneEaten = true;
                            theChosenOne = checkerPlayer;
                            break;
                        }
                    }

                    moveAsString = "";
                    if (downRight(checkerPlayer)) // down right
                    {
                        moveAsString += (char)(checkerPlayer.GetYlocation() + 1 + (int)'A');
                        moveAsString += (char)(checkerPlayer.GetXlocation() + 1 + (int)'a');
                        checkerPlayer.SetMove(moveAsString, indexer);
                        indexer++;
                    }
                    else
                    {
                        if(canEatDownRight(checkerPlayer))
                        {
                            oneEaten = true;
                            theChosenOne = checkerPlayer;
                            break;
                        }
                    }

                    moveAsString = "";
                    if (downLeft(checkerPlayer)) // down left
                    {
                        moveAsString += (char)(checkerPlayer.GetYlocation() - 1 + (int)'A');
                        moveAsString += (char)(checkerPlayer.GetXlocation() + 1 + (int)'a');
                        checkerPlayer.SetMove(moveAsString, indexer);
                        indexer++;
                    }
                    else
                    {
                        if(canEatDownLeft(checkerPlayer))
                        {
                            oneEaten = true;
                            theChosenOne = checkerPlayer;
                            break;
                        }
                    }
                }
            }

            if (oneEaten)
            {
                deleteAllPossibleMoves();
                addAllEatingMoves();
            }

            deleteAllPossibleMoves(i_XTurn);
        }

        public bool ifXWon(bool i_XTurn)
        {
            eCheckerColours theManForTheJob;
            int count = 0;
            bool oppentHaveMoreThenOnePiece = false;
            List<string> theListOfAllPossibleMoves;

            if (i_XTurn)
            {
                theManForTheJob = eCheckerColours.O;
                theListOfAllPossibleMoves = RandomMoveList(i_XTurn); // all possible moves of O
            }
            else
            {
                theManForTheJob = eCheckerColours.X;
                theListOfAllPossibleMoves = RandomMoveList(!i_XTurn); // all possible moves of X
            }

            foreach (Square sq in TheBoard)
            {
                if (sq.GetColour().Equals(theManForTheJob))
                {
                    count++;
                }

                if (count != 0)
                {
                    oppentHaveMoreThenOnePiece = true;
                    break;
                }
            }

            return !oppentHaveMoreThenOnePiece && theListOfAllPossibleMoves.Count == 0;
        }

        private void updateContinueEating(Square i_SquareSettings)
        {
            int indexer = 0;
            string moveAsString = "";

            if (canEatUpLeft(i_SquareSettings))
            {
                moveAsString += (char)(i_SquareSettings.GetYlocation() - 2 + (int)'A');
                moveAsString += (char)(i_SquareSettings.GetXlocation() - 2 + (int)'a');
                i_SquareSettings.SetMove(moveAsString, indexer);
                indexer++;
            }

            moveAsString = "";
            if (canEatDownLeft(i_SquareSettings))
            {
                moveAsString += (char)(i_SquareSettings.GetYlocation() - 2 + (int)'A');
                moveAsString += (char)(i_SquareSettings.GetXlocation() + 2 + (int)'a');
                i_SquareSettings.SetMove(moveAsString, indexer);
                indexer++;

            }

            moveAsString = "";
            if (canEatDownRight(i_SquareSettings))
            {
                 moveAsString += (char)(i_SquareSettings.GetYlocation() + 2 + (int)'A');
                 moveAsString += (char)(i_SquareSettings.GetXlocation() + 2 + (int)'a');
                i_SquareSettings.SetMove(moveAsString, indexer);
                 indexer++;
                
            }

            moveAsString = "";
            if (canEatUpRight(i_SquareSettings))
            {
                moveAsString += (char)(i_SquareSettings.GetYlocation() + 2 + (int)'A');
                moveAsString += (char)(i_SquareSettings.GetXlocation() - 2 + (int)'a');
                i_SquareSettings.SetMove(moveAsString, indexer);
                indexer++;
               
            }
        }

        public bool UpdateMoreEating(string i_PlayersMove)
        {
            bool returnFlag = false;
            Square theSquare = TheBoard[(int)(i_PlayersMove[1] - 'a'), (int)(i_PlayersMove[0] - 'A')];
            
            if(canEatUpRight(theSquare) || canEatDownLeft(theSquare) || canEatUpLeft(theSquare) || canEatDownRight(theSquare))
            {
                updateContinueEating(theSquare);
                returnFlag = true;
            }

            return returnFlag;
        }

        private void addAllEatingMoves()
        {
            string moveAsString;
            int indexer;

            foreach (Square ch in TheBoard)
            {
                indexer = 0;
                moveAsString = "";
                if ((ch.GetXlocation() + ch.GetYlocation()) % 2 == 1)
                {
                    if (canEatUpRight(ch))
                    {
                        moveAsString += (char)(ch.GetYlocation() + 2 + (int)'A');
                        moveAsString += (char)(ch.GetXlocation() - 2 + (int)'a');
                        ch.SetMove(moveAsString, indexer);
                        indexer++;
                    }

                    moveAsString = "";
                    if (canEatUpLeft(ch))
                    {
                        moveAsString += (char)(ch.GetYlocation() - 2 + (int)'A');
                        moveAsString += (char)(ch.GetXlocation() - 2 + (int)'a');
                        ch.SetMove(moveAsString, indexer);
                        indexer++;
                    }

                    moveAsString = "";
                    if (canEatDownRight(ch))
                    {
                        moveAsString += (char)(ch.GetYlocation() + 2 + (int)'A');
                        moveAsString += (char)(ch.GetXlocation() + 2 + (int)'a');
                        ch.SetMove(moveAsString, indexer);
                        indexer++;
                    }

                    moveAsString = "";
                    if (canEatDownLeft(ch))
                    {
                        moveAsString += (char)(ch.GetYlocation() - 2 + (int)'A');
                        moveAsString += (char)(ch.GetXlocation() + 2 + (int)'a');
                        ch.SetMove(moveAsString, indexer);
                        indexer++;
                    }
                }
            }
        }

        public void deleteAllPossibleMoves()
        {
            foreach (Square ch in TheBoard)
            {
                ch.RestartMoves();
            }
        }

        private void deleteAllPossibleMoves(bool i_XTurn)
        {
            foreach (Square checkerPlayer in TheBoard)
            {
                if(i_XTurn && checkerPlayer.GetColour().Equals(eCheckerColours.O))
                {
                    checkerPlayer.RestartMoves();
                }

                if (!i_XTurn && checkerPlayer.GetColour().Equals(eCheckerColours.X))
                {
                    checkerPlayer.RestartMoves();
                }

                if (checkerPlayer.GetColour().Equals(eCheckerColours.Empty))
                {
                    checkerPlayer.RestartMoves();
                }
            }
        }

        private bool upRight(Square i_Player)
        {
            bool okFlag = false;
            int PlayerxLocation = i_Player.GetXlocation();
            int PlayeryLocation = i_Player.GetYlocation();

            if (i_Player.GetColour().Equals(eCheckerColours.X) || i_Player.GetIsKing())
            {
                if((PlayerxLocation != 0) && (PlayeryLocation != GetRowLength() - 1))
                {
                    if (TheBoard[PlayerxLocation - 1, PlayeryLocation + 1].GetColour().Equals(eCheckerColours.Empty)) // equals "Empty"
                    {
                        okFlag = true;
                    }
                }
            }

            return okFlag;
        }

        public bool canEatUpRight(Square i_OnePiece)
        {
            bool returnFlag = false;
            bool flagOfUpRight = false;

            if (i_OnePiece.GetXlocation() > 1 && i_OnePiece.GetYlocation() < TheBoard.GetLength(1) - 2) // out of range for eating
            {
                if (i_OnePiece.GetColour().Equals(eCheckerColours.X)) // IM X
                {
                    if (!upRight(i_OnePiece) && TheBoard[i_OnePiece.GetXlocation() - 1, i_OnePiece.GetYlocation() + 1].GetColour().Equals(eCheckerColours.O)) // there is someone and is O
                    {
                        flagOfUpRight = true;
                    }
                }
                else // then the player is a king O
                {
                    if (i_OnePiece.GetIsKing()) // then it will be O for sure
                    {
                        if (!upRight(i_OnePiece) && !TheBoard[i_OnePiece.GetXlocation() - 1, i_OnePiece.GetYlocation() + 1].GetColour().Equals(i_OnePiece.GetColour())) // there is someone and is O
                        {
                            flagOfUpRight = true;
                        }
                    }
                }

                if (flagOfUpRight)
                {
                    if (TheBoard[i_OnePiece.GetXlocation() - 2, i_OnePiece.GetYlocation() + 2].GetColour().Equals(eCheckerColours.Empty))
                    {
                        returnFlag = true;
                    }
                }
            }
           
            return returnFlag;
        }

        private bool upLeft(Square i_Player)
        {
            bool okFlag = false;
            int PlayerxLocation = i_Player.GetXlocation();
            int PlayeryLocation = i_Player.GetYlocation();

            if (i_Player.GetColour().Equals(eCheckerColours.X) || i_Player.GetIsKing())
            {
                if ((PlayerxLocation != 0) && (PlayeryLocation != 0))
                {
                    if (TheBoard[PlayerxLocation - 1, PlayeryLocation - 1].GetColour().Equals(eCheckerColours.Empty))
                    {
                        okFlag = true;
                    }
                }
            }

            return okFlag;
        }

        public bool canEatUpLeft(Square i_OnePiece)
        {
            bool returnFlag = false;
            bool flagOfUpLeft = false;

            if (i_OnePiece.GetXlocation() > 1 && i_OnePiece.GetYlocation() > 1) // out of range for eating
            {
                if (i_OnePiece.GetColour().Equals(eCheckerColours.X)) // IM X
                {
                    if (!upLeft(i_OnePiece) && TheBoard[i_OnePiece.GetXlocation() - 1, i_OnePiece.GetYlocation() - 1].GetColour().Equals(eCheckerColours.O)) // there is someone and is O
                    {
                        flagOfUpLeft = true;
                    }
                }
                else // then the player is a king O
                {
                    if (i_OnePiece.GetIsKing())
                    {
                        if (!upLeft(i_OnePiece) && !TheBoard[i_OnePiece.GetXlocation() - 1, i_OnePiece.GetYlocation() - 1].GetColour().Equals(i_OnePiece.GetColour())) // there is someone and is O
                        {
                            flagOfUpLeft = true;
                        }
                    }
                }

                if (flagOfUpLeft)
                {
                    if (TheBoard[i_OnePiece.GetXlocation() - 2, i_OnePiece.GetYlocation() - 2].GetColour().Equals(eCheckerColours.Empty))
                    {
                        returnFlag = true;
                    }
                }
            }

            return returnFlag;
        }

        private bool downRight(Square i_Player)
        {
            bool okFlag = false;
            int PlayerxLocation = i_Player.GetXlocation();
            int PlayeryLocation = i_Player.GetYlocation();

            if (i_Player.GetColour().Equals(eCheckerColours.O) || i_Player.GetIsKing())
            {
                if ((PlayerxLocation != GetRowLength() - 1) && (PlayeryLocation != GetRowLength() - 1))
                {
                    if (TheBoard[PlayerxLocation + 1, PlayeryLocation + 1].GetColour().Equals(eCheckerColours.Empty))
                    {
                        okFlag = true;
                    }
                }
            }

            return okFlag;
        }

        public bool canEatDownRight(Square i_OnePiece)
        {
            bool returnFlag = false;
            bool flagOfDownRight = false;

            if (i_OnePiece.GetXlocation() < TheBoard.GetLength(1) - 2 && i_OnePiece.GetYlocation() < TheBoard.GetLength(1) - 2) // out of range for eating
            {
                if (i_OnePiece.GetColour().Equals(eCheckerColours.O)) // IM O
                {
                    if (!downRight(i_OnePiece) && TheBoard[i_OnePiece.GetXlocation() + 1, i_OnePiece.GetYlocation() + 1].GetColour().Equals(eCheckerColours.X)) // there is someone and is O
                    {
                        flagOfDownRight = true;
                    }
                }
                else // then the player is a king X
                {
                    if (i_OnePiece.GetIsKing())
                    {
                        if (!downRight(i_OnePiece) && !TheBoard[i_OnePiece.GetXlocation() + 1, i_OnePiece.GetYlocation() + 1].GetColour().Equals(i_OnePiece.GetColour())) // there is someone and is O
                        {
                            flagOfDownRight = true;
                        }
                    }
                }

                if (flagOfDownRight)
                {
                    if (TheBoard[i_OnePiece.GetXlocation() + 2, i_OnePiece.GetYlocation() + 2].GetColour().Equals(eCheckerColours.Empty))
                    {
                        returnFlag = true;
                    }
                }
            }

            return returnFlag;
        }

        private bool downLeft(Square i_Player)
        {
            bool okFlag = false;
            int PlayerxLocation = i_Player.GetXlocation();
            int PlayeryLocation = i_Player.GetYlocation();

            if (i_Player.GetColour().Equals(eCheckerColours.O) || i_Player.GetIsKing())
            {
                if ((PlayerxLocation != GetRowLength() - 1) && (PlayeryLocation != 0))
                {
                    if (TheBoard[PlayerxLocation + 1, PlayeryLocation - 1].GetColour().Equals(eCheckerColours.Empty))
                    {
                        okFlag = true;
                    }
                }
            }

            return okFlag;
        }

        public bool canEatDownLeft(Square i_OnePiece)
        {
            bool returnFlag = false;
            bool flagOfDownLeft = false;

            if (i_OnePiece.GetXlocation() < TheBoard.GetLength(1) - 2 && i_OnePiece.GetYlocation() > 1) // out of range for eating
            {
                if (i_OnePiece.GetColour().Equals(eCheckerColours.O))//IM O
                {
                    if (!downLeft(i_OnePiece) && TheBoard[i_OnePiece.GetXlocation() + 1, i_OnePiece.GetYlocation() - 1].GetColour().Equals(eCheckerColours.X)) // there is someone and is O
                    {
                        flagOfDownLeft = true;
                    }
                }
                else // then the player is a king X
                {
                    if (i_OnePiece.GetIsKing())
                    {
                        if (!downLeft(i_OnePiece) && !TheBoard[i_OnePiece.GetXlocation() + 1, i_OnePiece.GetYlocation() - 1].GetColour().Equals(i_OnePiece.GetColour())) // there is someone and is O
                        {
                            flagOfDownLeft = true;
                        }
                    }
                }

                if (flagOfDownLeft)
                {
                    if (TheBoard[i_OnePiece.GetXlocation() + 2, i_OnePiece.GetYlocation() - 2].GetColour().Equals(eCheckerColours.Empty))
                    {
                        returnFlag = true;
                    }
                }
            }

            return returnFlag;
        }

    }
}
