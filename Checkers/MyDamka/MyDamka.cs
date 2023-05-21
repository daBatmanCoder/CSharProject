using System;
using Ex02.ConsoleUtils;
using BackDamka;
using System.Collections.Generic;

namespace FrontDamka
{
    public class MyDamka
    {
        public static void runDamka()
        {
            Console.WriteLine("Please enter your name (max 10 characters, no spaces)");
            string nameOfFirstUser = Console.ReadLine();
            string anotherGame = "Y";
            string nameOfSecondUser;
            string messageAfterBoard;
            bool TryParseSizeOfBoard = false;
            bool flagToHelp = true;
            bool XWon;
            int sizeOfBoard = 0;
            int numOfPlayers;
            int XPoints = 0;
            int OPoints = 0;
            int countTurns = 0;
            User player1, player2;

            while (!checkName(nameOfFirstUser))
            {
                Console.WriteLine("Invalid name, please try again");
                nameOfFirstUser = Console.ReadLine();
            }

            Console.WriteLine("Please enter the size of board you would like to play (6,8 or 10)");
            TryParseSizeOfBoard = int.TryParse(Console.ReadLine(), out sizeOfBoard);
            while (flagToHelp)
            {
                if (TryParseSizeOfBoard)
                {
                    if (sizeOfBoard == 6 || sizeOfBoard == 8 || sizeOfBoard == 10)
                    {
                        break;
                    }
                }

                Console.WriteLine("Invalid number, please try again");
                TryParseSizeOfBoard = int.TryParse(Console.ReadLine(), out sizeOfBoard);
            }

            Console.WriteLine("How many players plays the game? (plese enter 1 or 2) ");
            TryParseSizeOfBoard = int.TryParse(Console.ReadLine(), out numOfPlayers);
            while (flagToHelp)
            {
                if (TryParseSizeOfBoard)
                {
                    if (numOfPlayers == 1 || numOfPlayers == 2)
                    {
                        break;
                    }
                }

                Console.WriteLine("Invalid number, please try again");
                TryParseSizeOfBoard = int.TryParse(Console.ReadLine(), out numOfPlayers);               
            }
            if (numOfPlayers == 2)
            {
                Console.WriteLine("please enter the second user name (max 10 characters, no spaces)");
                nameOfSecondUser = Console.ReadLine();
                while (!checkName(nameOfSecondUser))
                {
                    Console.WriteLine("Invalid name, please try again");
                    nameOfSecondUser = Console.ReadLine();
                }
            }
            else
            {
                nameOfSecondUser = "Computer";
            }
            
            while(anotherGame.Equals("Y") || anotherGame.Equals("y"))
            {
                Board theGameBoard = new Board(sizeOfBoard);

                theGameBoard.InitPlayers(out player1, out player2, nameOfFirstUser, nameOfSecondUser);
                theGameBoard.updateSquarePossibleMove(true);
                PlayTheGame(theGameBoard, player1, player2, numOfPlayers, out XWon);
                countTurns++;
                if (XWon)
                {
                    XPoints += theGameBoard.TotalNumberOfPoints();
                }
                else
                {
                    OPoints += theGameBoard.TotalNumberOfPoints();
                }
                Screen.Clear();
                PrintTheBoard(theGameBoard.GetBoard().GetLength(1), theGameBoard);
                messageAfterBoard = string.Format(@"{5}[===========================================] 
   The TOTAL points after {0} games is:       
   For {1} the number of points is: {2}       
   For {3} the number of points is: {4}   
[===========================================]
 {5}Would you like to play another game? ('Y' for new game, Any key for exit)", countTurns, player1.GetName(), XPoints.ToString(), player2.GetName(), OPoints.ToString(), Environment.NewLine);
                Console.WriteLine(messageAfterBoard);
                anotherGame = Console.ReadLine();
                Screen.Clear();
            }

            endMessage(XPoints, OPoints, nameOfFirstUser, nameOfSecondUser);
        }

        private static void endMessage(int i_XPoints, int i_OPoints, string i_PlayerOneName, string i_PlayerTwoName)
        {
            string messageAfterBoard;

            if (i_XPoints > i_OPoints) // X won
            {
                messageAfterBoard = string.Format(@"WOWWWWWWW {0} WON THE GAME, BY {1} POINTS!!!!
BETTER LUCK NEXT TIME {2}(LOSER). BYE NOW.", i_PlayerOneName, i_XPoints - i_OPoints, i_PlayerTwoName);
            }
            else if(i_OPoints > i_XPoints) // O won
            {
                messageAfterBoard = string.Format(@"WOWWWWWWW {0} WON THE GAME, BY {1} POINTS!!!!
BETTER LUCK NEXT TIME {2}(LOSER). BYE NOW.", i_PlayerTwoName, i_OPoints - i_XPoints, i_PlayerOneName);
            }
            else // draw
            {
                messageAfterBoard = string.Format(@"THIS IS A DRAW.... YOU BOTH ARE MATCHED OPPONENTS, BYE NOW.");
            }

            Console.WriteLine(messageAfterBoard);
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void PlayTheGame(Board i_TheGameBoard, User i_Player1, User i_Player2, int i_NumOfPlayers, out bool o_XWon)
        {
            o_XWon = true;
            string PlayerMove;
            bool XPlayerMove = true;
            bool moveWasValid = false;
            bool DidAte = false;
            string copyOfPlayerMoveEaten = "";
            bool SomeoneGotMoreToEat = false;
            string messageAfterBoard = "";
            string NameOfWhoDidTheMove = "", NameOfPlayerWhoDidntMove = "";
            eUserTypes TypeOfWhoDidTheMove = eUserTypes.O, TypeOfWhoDidntTheMove = eUserTypes.O;

            Screen.Clear();
            PrintTheBoard(i_TheGameBoard.GetBoard().GetLength(1), i_TheGameBoard);
            Console.WriteLine(i_Player1.GetName() + "'s turn (" + i_Player1.GetUserType().ToString() + "):");
            PlayerMove = Console.ReadLine();
            while (!i_TheGameBoard.IsGameEnded(XPlayerMove) && !PlayerMove.Equals("Q"))
            {
                if (XPlayerMove)
                {
                    NameOfWhoDidTheMove = i_Player1.GetName();
                    TypeOfWhoDidTheMove = i_Player1.GetUserType();
                    NameOfPlayerWhoDidntMove = i_Player2.GetName();
                    TypeOfWhoDidntTheMove = i_Player2.GetUserType();
                    PlayerMove = i_TheGameBoard.RunComputerMove(XPlayerMove);
                    if (moveWasValid = i_TheGameBoard.CheckValidMove(i_TheGameBoard, PlayerMove, XPlayerMove, out DidAte))
                    {
                        SomeoneGotMoreToEat = false;
                        XPlayerMove = false;
                    }
                }
                else // O turn's
                {
                    NameOfWhoDidTheMove = i_Player2.GetName();
                    TypeOfWhoDidTheMove = i_Player2.GetUserType();
                    NameOfPlayerWhoDidntMove = i_Player1.GetName();
                    TypeOfWhoDidntTheMove = i_Player1.GetUserType();
                    if (i_NumOfPlayers == 1)
                    {
                        PlayerMove = i_TheGameBoard.RunComputerMove(XPlayerMove);
                    }
                    if (moveWasValid = i_TheGameBoard.CheckValidMove(i_TheGameBoard, PlayerMove, XPlayerMove, out DidAte))
                    {
                        XPlayerMove = true;
                    }
                }

                if (moveWasValid)
                {
                    if (SomeoneGotMoreToEat)
                    {
                        SomeoneGotMoreToEat = false;
                    }

                    if (DidAte)
                    {
                        messageAfterBoard = string.Format(@"{0}'s move was ({1}): {2}
{3}'s Turn ({4}):", NameOfWhoDidTheMove, TypeOfWhoDidTheMove, PlayerMove, NameOfWhoDidTheMove, TypeOfWhoDidTheMove);
                    }
                    else
                    {
                        messageAfterBoard = string.Format(@"{0}'s move was ({1}): {2}
{3}'s Turn ({4}):", NameOfWhoDidTheMove, TypeOfWhoDidTheMove, PlayerMove, NameOfPlayerWhoDidntMove, TypeOfWhoDidntTheMove);
                    }

                    moveWasValid = false;
                }
                else
                {
                    messageAfterBoard = "Invalid move, please try again";
                    if (!SomeoneGotMoreToEat)
                    {
                        if (XPlayerMove)
                        {
                            messageAfterBoard += "\n" + i_Player1.GetName() + "'s turn (" + i_Player1.GetUserType().ToString() + "):";
                        }
                        else
                        {
                            messageAfterBoard += "\n" + i_Player2.GetName() + "'s turn (" + i_Player2.GetUserType().ToString() + "):";
                        }
                    }
                    else
                    {
                        if (XPlayerMove) // It was O turns before update XPLAYERMOBER
                        {
                            XPlayerMove = true;
                            messageAfterBoard += string.Format(@"{2}{0}'s Turn ({1}):", i_Player1.GetName(), i_Player1.GetUserType().ToString(), Environment.NewLine);
                        }
                        else
                        {
                            XPlayerMove = false;
                            messageAfterBoard += string.Format(@"{2}{0}'s Turn ({1}):", i_Player2.GetName(), i_Player2.GetUserType().ToString(), Environment.NewLine);
                        }
                    }
                }

                Screen.Clear();
                PrintTheBoard(i_TheGameBoard.GetBoard().GetLength(1), i_TheGameBoard);
                if (DidAte)// have option to eat more?
                { // if the player ate then we check if more possible eating
                    if (!SomeoneGotMoreToEat)
                    {
                        copyOfPlayerMoveEaten = PlayerMove;
                    }

                    SomeoneGotMoreToEat = true;
                    if (i_TheGameBoard.UpdateMoreEating(copyOfPlayerMoveEaten.Substring(3)))
                    {
                        if (XPlayerMove) // It was O turns before update XPLAYERMOBER
                        {
                            XPlayerMove = false;
                        }
                        else
                        {
                            XPlayerMove = true;
                        }
                    }
                }
                else
                {
                    i_TheGameBoard.updateSquarePossibleMove(XPlayerMove);
                }

                Console.WriteLine(messageAfterBoard);
                if (i_NumOfPlayers == 2 || XPlayerMove)
                {
                    PlayerMove = Console.ReadLine();
                }
            } // end of while ( game )

            if (XPlayerMove) // returns who won the game!
            {
                if(PlayerMove.Equals("Q"))
                {
                    o_XWon = false;
                }
                else
                {
                    o_XWon = i_TheGameBoard.ifXWon(XPlayerMove);
                }
            }
            else
            {
                if (PlayerMove.Equals("Q"))
                {
                    o_XWon = true;
                }
                else
                {
                    o_XWon = i_TheGameBoard.ifXWon(!XPlayerMove);
                }
            }
        }

        private static void PrintTheBoard(int i_SizeOfBoard, Board i_TheGameBoard)
        {
            var theBoardToBePrinted = new System.Text.StringBuilder();
            string gaps = "   ";
            string lineToPrint = "";

            for (int i = 65; i < 65 + i_SizeOfBoard; i++)
            {
                gaps += ((char)i + "   ");
            }

            theBoardToBePrinted.AppendLine(gaps);
            theBoardToBePrinted.AppendLine(generateSeperator(i_SizeOfBoard));
            for (int i = 'a'; i < 'a' + i_SizeOfBoard; i++)
            {
                lineToPrint = "";
                lineToPrint += (char)i + "|";
                for (int j = 0; j < i_SizeOfBoard; j++)
                {
                    foreach(Square sq in i_TheGameBoard.GetBoard())
                    {
                        if (sq.GetXlocation() == (i - (int)'a') && sq.GetYlocation() == j)
                        {
                            if (sq.IsSquareEmpty())
                            {
                                lineToPrint += "  ";
                            }
                            else
                            {
                                if(sq.GetIsKing())
                                {
                                    if(sq.GetColour().Equals(eCheckerColours.O))
                                    {
                                        lineToPrint += " " + "Q";
                                    }
                                    else
                                    {
                                        lineToPrint += " " + "Z";
                                    }
                                }
                                else
                                {
                                    lineToPrint += " " + sq.GetColour();
                                }
                            }
                        }
                    }

                    lineToPrint += " |";
                }

                theBoardToBePrinted.AppendLine(lineToPrint);
                theBoardToBePrinted.AppendLine(generateSeperator(i_SizeOfBoard));
            }
            
            Console.Write(theBoardToBePrinted);
        }

        private static string generateSeperator(int i_NumOfLines)
        {
            string seperatorString = " ==";

            for (int i = 0; i < i_NumOfLines - 1; i++)
            {
                seperatorString += "====";
            }

            seperatorString += "===";

            return seperatorString;
        }

        private static bool checkName(string i_NameOfUser)
        {
            bool returnFlag = true;

            for(int i = 0; i < i_NameOfUser.Length; i++)
            {
                if (!char.IsLetter(i_NameOfUser[i]))
                {
                    returnFlag = !returnFlag;
                    break;
                }
            }

            return returnFlag && (i_NameOfUser.Length < 11);
        }
    }
}
