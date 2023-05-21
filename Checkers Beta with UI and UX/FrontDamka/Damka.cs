using BackDamka;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FrontDamka
{
    public partial class Damka : Form
    {
        private Button[,]   m_MatrixOfButtons;
        private Board       m_TheGameBoard;
        private InitForm    m_GameSettingsForm;
        private string      m_CopyOfLastPlayerMove;
        private string      m_FirstPlayerName;
        private string      m_SecondPlayerName;
        private bool        m_FirstGame = true;
        private bool        m_XTurn = true;
        private bool        m_ThePieceThatMakesTheMove = false;
        private bool        m_SettingsOK = false;
        private bool        m_SomeoneGotMoreToEat = false;
        private bool        m_GuidedAccess = false;
        private int         m_NumOfPlayers;
        private int         m_XPoints = 0;
        private int         m_OPOints = 0;
        private int         m_SizeOfBoard;

        public Damka(out bool o_SettingsOk)
        { 
            InitializeComponent();
            initGameSettings(out o_SettingsOk);
        }

        private void ensureSettings()
        {
            m_GameSettingsForm = new InitForm();
            m_GameSettingsForm.ShowDialog();
            if (m_GameSettingsForm.DialogResult == DialogResult.Yes)
            {
                m_SettingsOK = true;
            }
        }

        private void initGameSettings(out bool o_SettingsOk)
        {
            o_SettingsOk = false;
            User player1, player2;
            bool XWon;

            if(m_FirstGame)
            {
                ensureSettings();
            }

            if (m_SettingsOK)
            {
                m_FirstGame = false;
                o_SettingsOk = true;
                m_SizeOfBoard = m_GameSettingsForm.SizeOfBoard;
                initFormSize(m_SizeOfBoard);
                buildTheBoard();
                updateLabels();
                initBoard(m_TheGameBoard);
                m_TheGameBoard.InitPlayers(out player1, out player2, m_FirstPlayerName, m_SecondPlayerName);
                m_TheGameBoard.updateSquarePossibleMove(true);
            }
        }

        private void buildTheBoard()
        {
             m_TheGameBoard = new Board(m_SizeOfBoard);
             m_MatrixOfButtons = new Button[m_SizeOfBoard, m_SizeOfBoard];
        }

        private void updateLabels()
        {
            labelPlayer1.Text = m_GameSettingsForm.TextBoxPlayerOne + ":";
            labelPlayer2.Text = m_GameSettingsForm.TextBoxPlayerTwo + ":";
            labelPlayer1.Top = 20;
            labelPlayer2.Top = 20;
            labelPlayer1.Left = this.Size.Width / 8;
            labelPlayer2.Left = this.Size.Width / 8 * 4;
            labelPlayer1Points.Left = labelPlayer1.Right + 1;
            labelPlayer1Points.Top = labelPlayer1.Top;
            labelPlayer2Points.Left = labelPlayer2.Right + 1;
            labelPlayer2Points.Top = labelPlayer2.Top;
            m_NumOfPlayers = m_GameSettingsForm.NumOfPlayers;
            m_FirstPlayerName = m_GameSettingsForm.TextBoxPlayerOne;
            m_SecondPlayerName = m_GameSettingsForm.TextBoxPlayerTwo;
            CheckBox guidedAccess = new CheckBox();
            guidedAccess.Top = this.Bottom - 75;
            guidedAccess.Left = this.Right / 4;
            guidedAccess.Text = "Guided Access";
            guidedAccess.Enabled = true;
            guidedAccess.Click += new System.EventHandler(this.guidedAccess_Click);
            Controls.Add(guidedAccess);
        }

        private void guidedAccess_Click(object sender, EventArgs e)
        {
            Button buttOfClick;

            m_GuidedAccess = !m_GuidedAccess;
            if (m_GuidedAccess == false)
            {
                if (checkIfAlreadySomeoneClicked(out buttOfClick))
                {
                    removeAllPossibleMoveColor(findLocationOfSender(buttOfClick));
                }
            }
        }

        private void initBoard(Board m_TheGameBoard)
        {
            int boardLength = m_TheGameBoard.GetRowLength();

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    Button oneButton = new Button();
                    initButtonLocation(oneButton, i , j);
                }
            }
        }

        private void initButtonLocation(Button oneButton, int i_XLocation, int i_YLocation)
        {
            oneButton.Width = 35;
            oneButton.Height = 35;
            m_MatrixOfButtons[i_XLocation, i_YLocation] = oneButton;

            if ((i_XLocation + i_YLocation) % 2 == 0)
            {
                oneButton.Enabled = false;
                oneButton.BackColor = SystemColors.GrayText;
            }
            else
            {
                oneButton.BackColor = Color.White;
                if (!m_TheGameBoard.GetBoard()[i_XLocation, i_YLocation].GetColour().Equals(eCheckerColours.Empty))
                {
                    oneButton.Text = m_TheGameBoard.GetBoard()[i_XLocation, i_YLocation].GetColour().ToString();
                    oneButton.Font = new Font(Control.DefaultFont, FontStyle.Bold);
                }
            }

            oneButton.Click += new System.EventHandler(this.oneButton_Click);
            initLocationOfButton(ref oneButton, i_XLocation, i_YLocation, m_MatrixOfButtons);
            this.Controls.Add(oneButton);
        }

        private void initFormSize(int i_TheBoardLength)
        {
            switch (i_TheBoardLength)
            {
                case 6:
                    this.Size = new Size(260, 350);
                    break;
                case 8:
                    this.Size = new Size(350, 420);
                    break;
                case 10:
                    this.Size = new Size(440, 490);
                    break;
            }
        }

        private void initLocationOfButton(ref Button i_OneButton, int i_XLocation, int i_YLocation, Button[,] i_MatrixOfButtons)
        {
            if (i_YLocation == 0)
            {
                if (i_XLocation == 0)
                {
                    i_OneButton.Top = labelPlayer1.Bottom + 10;
                    i_OneButton.Left = labelPlayer1.Left - 10;
                }
                else
                {
                    i_OneButton.Top = i_MatrixOfButtons[i_XLocation - 1, i_YLocation].Bottom;
                    i_OneButton.Left = i_MatrixOfButtons[i_XLocation - 1, i_YLocation].Left;
                }
            }
            else
            {
                i_OneButton.Left = i_MatrixOfButtons[i_XLocation, i_YLocation - 1].Right;
                i_OneButton.Top = i_MatrixOfButtons[i_XLocation, i_YLocation - 1].Top;
            }
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            Button buttOfClick;

            if (!checkIfAlreadySomeoneClicked(out buttOfClick))
            {
                firstClickOnAnyButton(sender);
            }
            else
            {
                someoneAlreadyClicked(sender, buttOfClick);
            }

            if (m_NumOfPlayers == 1 && !m_XTurn) // now computer needs to play.
            {
                computerMove();
            }
            
            if(m_TheGameBoard.IsGameEnded(m_XTurn)) // IF X WON NOT WORKING...
            {
                endOfGameProcedure();
            }
        }

        private void firstClickOnAnyButton(object i_Sender)
        {
            m_ThePieceThatMakesTheMove = false;
            (i_Sender as Button).BackColor = Color.LightBlue;
            if (m_GuidedAccess)
            {
                showAllValidMoves(i_Sender);
            }
        }

        private void someoneAlreadyClicked(object i_Sender, Button i_ButtonOfClick)
        {
            string theMoveInComplete;
            bool isValidMove, didAte;

            m_ThePieceThatMakesTheMove = true;
            (i_Sender as Button).BackColor = Color.White;
            theMoveInComplete = generateMove(i_Sender);
            isValidMove = m_TheGameBoard.CheckValidMove(m_TheGameBoard, theMoveInComplete, m_XTurn, out didAte);
            if (isValidMove)
            {
                validMoveProcedure(theMoveInComplete, didAte, i_Sender);
            }
            else
            {
                notValidMoveProcedure(i_ButtonOfClick, i_Sender);
            }
        }

        private void notValidMoveProcedure(Button i_ButtonOfClick, object i_Sender)
        {
            if (m_ThePieceThatMakesTheMove && i_ButtonOfClick != i_Sender)
            {
                MessageBox.Show("Not a valid move, please try again.");
            }

            if (convertPointToString(findLocationOfSender(i_ButtonOfClick)).Equals(convertPointToString(findLocationOfSender(i_Sender as Button))) && m_GuidedAccess)
            {
                removeAllPossibleMoveColor(findLocationOfSender(i_ButtonOfClick));
            }
        }

        private void validMoveProcedure(string i_TheMove, bool i_DidAte, object sender)
        {
            if (m_GuidedAccess)
            {
                removeAllPossibleMoveColor(getSourceLocation());
            }

            if (m_SomeoneGotMoreToEat)
            {
                m_SomeoneGotMoreToEat = false;
            }

            goValidMoveChecking(i_DidAte, i_TheMove.Substring(3));
            updateBoard(getSourceLocation(), findLocationOfSender(sender));
        }

        private string generateMove(object sender)
        {
            Point DestLocation, sourceLocation;
            string destMove, sourceMove;

            DestLocation = findLocationOfSender(sender);
            destMove = convertPointToString(DestLocation);
            sourceLocation = getSourceLocation();
            sourceMove = convertPointToString(sourceLocation);

            return string.Format("{0}>{1}", sourceMove, destMove);
        }

        private void removeAllPossibleMoveColor(Point i_SourcePoint)
        {
            int Xlocation, Ylocation;
            string dest;

            for (int i = 0; i < 4; i++)
            {
                dest = m_TheGameBoard.GetBoard()[i_SourcePoint.X, i_SourcePoint.Y].GetElementFromArray(i);
                if (dest == null)
                {
                    break;
                }
                else
                {
                    Xlocation = (int)dest[1] - 'a';
                    Ylocation = (int)dest[0] - 'A';
                    m_MatrixOfButtons[Xlocation, Ylocation].BackColor = Color.White;
                }
            }
        }

        private void showAllValidMoves(object sender)
        {
            Point locationOfSquare = findLocationOfSender(sender);
            int Xlocation, Ylocation;
            string dest;

            for(int i = 0; i < 4; i++)
            {
                dest = m_TheGameBoard.GetBoard()[locationOfSquare.X, locationOfSquare.Y].GetElementFromArray(i);
                if(dest == null)
                {
                    break;
                }
                else
                {
                    Xlocation = (int)dest[1] - 'a';
                    Ylocation = (int)dest[0] - 'A';
                    m_MatrixOfButtons[Xlocation, Ylocation].BackColor = Color.Red;
                }
            }
        }

        private void computerMove()
        {
            string theMoveInComplete;
            bool isValidMove;
            bool didAte;

            while (m_TheGameBoard.RandomMoveList(m_XTurn).Count != 0)
            {
                theMoveInComplete = m_TheGameBoard.RunComputerMove(m_XTurn);
                if (isValidMove = m_TheGameBoard.CheckValidMove(m_TheGameBoard, theMoveInComplete, m_XTurn, out didAte))
                {

                    string sourceMove = theMoveInComplete.Substring(0, 2);
                    string destMove = theMoveInComplete.Substring(3);
                    int xSource = (int)(sourceMove[1] - 'a');
                    int ySource = (int)(sourceMove[0] - 'A');
                    int xDest = (int)(destMove[1] - 'a');
                    int yDest = (int)(destMove[0] - 'A');

                    Point sourceLocation = new Point(xSource, ySource);
                    Point destLocation = new Point(xDest, yDest);

                    if (isValidMove)
                    {
                        if (m_SomeoneGotMoreToEat)
                        {
                            m_SomeoneGotMoreToEat = false;
                        }

                        goValidMoveChecking(didAte, theMoveInComplete.Substring(3));
                        updateBoard(sourceLocation, destLocation);
                    }
                    if (!didAte)
                    {
                        break;
                    }
                }
            }
        }

        private void endOfGameProcedure()
        {
            string MessageBoxMessage = "";
            DialogResult messageBoxResult;
            
            switch(whoWonTheGame())
            {
                case 0: // draw
                    MessageBoxMessage = string.Format("Tie!{0}Another Round?", Environment.NewLine);
                    break;
                case 1: // X won
                    MessageBoxMessage = string.Format("{0} Won!{1}Another Round?", labelPlayer1.Text, Environment.NewLine);
                    m_XPoints += m_TheGameBoard.TotalNumberOfPoints();
                    labelPlayer1Points.Text = m_XPoints.ToString();
                    break;
                case 2: // O won
                    MessageBoxMessage = string.Format("{0} Won!{1}Another Round?", labelPlayer2.Text, Environment.NewLine);
                    m_OPOints += m_TheGameBoard.TotalNumberOfPoints();
                    labelPlayer2Points.Text = m_OPOints.ToString();
                    break;
            }

            messageBoxResult = MessageBox.Show(MessageBoxMessage, "End Of Game", MessageBoxButtons.YesNo);
            if (messageBoxResult == DialogResult.Yes)
            { 
                anotherGame();
            }
            else
            {
                if(messageBoxResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        private void anotherGame()
        {
            m_TheGameBoard = new Board(m_SizeOfBoard);
            reInitBoard();
            m_TheGameBoard.updateSquarePossibleMove(true);
            m_XTurn = true;
        }

        private void reInitBoard()
        {
            int boardLength = m_TheGameBoard.GetRowLength();
            deleteAllMatrixText();

            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_MatrixOfButtons[i, j].BackColor = Color.White;
                        if (!m_TheGameBoard.GetBoard()[i, j].GetColour().Equals(eCheckerColours.Empty))
                        {
                            m_MatrixOfButtons[i, j].Text = m_TheGameBoard.GetBoard()[i, j].GetColour().ToString();
                            m_MatrixOfButtons[i, j].Font = new Font(Control.DefaultFont, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        private void deleteAllMatrixText()
        {
            foreach(Button oneButton in m_MatrixOfButtons)
            {
                oneButton.Text = string.Empty;
            }
        }

        private int numOfCheckers(bool i_XTurn)
        {
            int countNumOfCheckers = 0;

            if(i_XTurn)
            {
                foreach(Button oneButton in m_MatrixOfButtons)
                {
                    if(i_XTurn)
                    {
                        if (oneButton.Text.Equals("X") || oneButton.Text.Equals("Z"))
                        {
                            countNumOfCheckers++;
                        }
                    }
                    else
                    {
                        if (oneButton.Text.Equals("O") || oneButton.Text.Equals("Q"))
                        {
                            countNumOfCheckers++;
                        }
                    }
                }
            }

            return countNumOfCheckers;
        }

        private int whoWonTheGame()
        {
            int numberToReturn;

            if (numOfCheckers(true) == 0 && numOfCheckers(false) == 0)
            {
                numberToReturn =  0;
            }
            else
            {
                if (m_TheGameBoard.ifXWon())
                {
                    numberToReturn = 1;
                }
                else
                {
                    numberToReturn = 2;
                }
            }

            return numberToReturn;
        }

        private void goValidMoveChecking(bool i_DidAte, string i_DestMove)
        {
            if (!i_DidAte)
            {
                m_XTurn = !m_XTurn;
                m_TheGameBoard.updateSquarePossibleMove(m_XTurn);
            }
            else
            {
                if (!m_SomeoneGotMoreToEat)
                {
                    m_CopyOfLastPlayerMove = i_DestMove;
                }

                m_SomeoneGotMoreToEat = true;

                if (!m_TheGameBoard.UpdateMoreEating(m_CopyOfLastPlayerMove))
                {
                    m_XTurn = !m_XTurn;
                }
            }
        }

        private void updateBoard(Point sourceLocation, Point destLocation)
        {
            m_MatrixOfButtons[destLocation.X, destLocation.Y].Text = m_MatrixOfButtons[sourceLocation.X, sourceLocation.Y].Text;
            m_MatrixOfButtons[destLocation.X, destLocation.Y].Font = new Font(Control.DefaultFont, FontStyle.Bold);
            m_MatrixOfButtons[sourceLocation.X, sourceLocation.Y].Text = string.Empty;
            m_MatrixOfButtons[sourceLocation.X, sourceLocation.Y].BackColor = Color.White;
            if (Math.Abs(sourceLocation.X - destLocation.X) == 2)
            {
                m_MatrixOfButtons[((destLocation.X + sourceLocation.X) / 2), ((destLocation.Y + sourceLocation.Y) / 2)].Text = string.Empty;
            }

            updateKingsAndQueens();
        }

        private void updateKingsAndQueens()
        {
            foreach(Square sq in m_TheGameBoard.GetBoard())
            {
                if(sq.GetIsKing())
                {
                    if (sq.GetColour().Equals(eCheckerColours.O))
                    {
                        m_MatrixOfButtons[sq.GetXlocation(), sq.GetYlocation()].Text = "Q";
                    }
                    else
                    {
                        m_MatrixOfButtons[sq.GetXlocation(), sq.GetYlocation()].Text = "Z";
                    }
                }
            }
        }

        private static string convertPointToString(Point i_ThePointToConvert)
        {
            string stringOfPoint;

            stringOfPoint = string.Format("{0}{1}", ((char)(i_ThePointToConvert.Y + 'A')), ((char)(i_ThePointToConvert.X + 'a')));
            
            return stringOfPoint;
        }

        private Point getSourceLocation()
        {
            Point pointWhoIsBackgroundIsBlue = new Point();

            for (int i = 0; i < m_MatrixOfButtons.GetLength(0); i++)
            {
                for (int j = 0; j < m_MatrixOfButtons.GetLength(1); j++)
                {
                    if (m_MatrixOfButtons[i, j].BackColor == Color.LightBlue)
                    {
                        pointWhoIsBackgroundIsBlue = new Point(i, j);
                        break;
                    }
                }
            }

            return pointWhoIsBackgroundIsBlue;
        }

        private bool checkIfAlreadySomeoneClicked(out Button o_ThePoint)
        {
            o_ThePoint = new Button();
            bool isPointWhoIsBackgroundIsBlue = false;

            for (int i = 0; i < m_MatrixOfButtons.GetLength(0); i++)
            {
                for (int j = 0; j < m_MatrixOfButtons.GetLength(1); j++)
                {
                    if (m_MatrixOfButtons[i, j].BackColor == Color.LightBlue)
                    {
                        o_ThePoint = m_MatrixOfButtons[i, j];
                        isPointWhoIsBackgroundIsBlue = !isPointWhoIsBackgroundIsBlue;
                        break;
                    }
                }
            }

            return isPointWhoIsBackgroundIsBlue;
        }

        private Point findLocationOfSender(object sender)
        {
            Point pointToReturn = new Point();

            for(int i = 0; i < m_MatrixOfButtons.GetLength(0); i++)
            {
                for(int j = 0; j < m_MatrixOfButtons.GetLength(1); j++)
                {
                    if ((sender as Button).Location == m_MatrixOfButtons[i, j].Location)
                    {
                        pointToReturn = new Point(i, j);
                        break;
                    }
                }
            }

            return pointToReturn;
        }
    }
}
