using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTicTacToeUI
{
	public partial class frmGame : Form
	{
		public char[,,,] gameBoard;
		public char[,] outerBoard;
		public char player;
        bool aiEnabled;
        bool aiTurn;
        public playerAI ai;

		public frmGame()
		{
			InitializeComponent();
		}
		private void frmGame_Load(object sender, EventArgs e)
		{
			gameBoard = new char[3, 3, 3, 3];
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					for (int k = 0; k < 3; k++)
						for (int l = 0; l<3; l++)
							gameBoard[i, j, k, l]='-';
			outerBoard = new char[3, 3];
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					outerBoard[i, j] = '-';
			changePlayer('X');

            aiEnabled = radComputer.Checked;
            if (aiEnabled)
                ai = new playerAI('O');
		}

		public void changePlayer(char newPlayer)
		{
			player = newPlayer;
			lblPlayer.Text = "Player " + player;
		}

		public void cmdBoard_Click(object sender, EventArgs e)
		{
			//Process button pressed
			Button buttonPressed = (Button)sender;
			int index = buttonPressed.TabIndex;

			//declare variables
			int inRow;					//inner row
			int outRow;					//outter row
			int inCol;					//inner column
			int outCol;					//outter column
			int n = index;				//copy of index to use for processing 

			//display marker
			buttonPressed.Text = "" + player;
			
			//convert index to trinary and store to variables
			inCol = n % 3;
			n = n / 3;
			inRow = n % 3;
			n = n / 3;
			outCol = n % 3;
			n = n / 3;
			outRow = n;

			//Display move
			txtConsole.AppendText(player +": " + outRow + ", " + outCol + ", " + inRow + ", " + inCol + "\r\n");

			//Record the player's mark
			gameBoard[outRow, outCol, inRow, inCol] = player;

			//check for 3-in-a-row in the internal board
			if (threeInARow(packageBoard(gameBoard, outRow, outCol)) != '-')
			{
				txtConsole.AppendText(player + " won board " + outRow + ", " + outCol + "\r\n");
				outerBoard[outRow,outCol] = threeInARow(packageBoard(gameBoard, outRow, outCol));

				//disable and hide that internal board
				enableBoard(index / 9, false);
				hideBoard(index / 9, false);

				//make the correct label visible and display the character's mark
				switch (index/9)
				{
					case 0:
					{
						lbl00.Visible = true;
						lbl00.Text = "" + player;
						break;
					}
					case 1:
					{
						lbl01.Visible = true;
						lbl01.Text = "" + player;
						break;
					}
					case 2:
					{
						lbl02.Visible = true;
						lbl02.Text = "" + player;
						break;
					}
					case 3:
					{
						lbl10.Visible = true;
						lbl10.Text = "" + player;
						break;
					}
					case 4:
					{
						lbl11.Visible = true;
						lbl11.Text = "" + player;
						break;
					}
					case 5:
					{
						lbl12.Visible = true;
						lbl12.Text = "" + player;
						break;
					}
					case 6:
					{
						lbl20.Visible = true;
						lbl20.Text = "" + player;
						break;
					}
					case 7:
					{
						lbl21.Visible = true;
						lbl21.Text = "" + player;
						break;
					}
					case 8:
					{
						lbl22.Visible = true;
						lbl22.Text = "" + player;
						break;
					}
					default:
					{
						txtConsole.AppendText("Error in changing label\r\nIndex: " + index / 9);
						break;
					}
				}//switch
			}//if

			//check for overall win
			if (threeInARow(outerBoard) != '-')
			{
				//display win
				lblPlayer.Text = "Player " + player + " wins!";
				txtConsole.AppendText("Player " + player + " has won the game! \r\n");

				//disable all of the boards
				for (int i = 0; i < 9; i++)
					enableBoard(i, false);
			}
			else
			{

				//Determine which buttons should be enabled after turn, enable those and disable the rest
				if (outerBoard[inRow, inCol] != '-' || isFull(packageBoard(gameBoard, inRow, inCol)))
				{
					foreach (Control control in this.Controls)
					{
						if (control is Button && control.TabIndex < 81 && control.Text == "" && control.Visible)
						{
							control.Enabled = true;
						}
					}
				}
				else
				{
					for (int i = 0; i < 9; i++)
						enableBoard(i, false);
					enableBoard(inRow * 3 + inCol, true);

				}

                buttonPressed.Enabled = false;
                
                //toggle player
                if (player == 'X')
                    changePlayer('O');
                else if (player == 'O')
                    changePlayer('X');

                if (aiEnabled && player == ai.marker)
                {
                    List<Button> collection = new List<Button>();
                    foreach(Control control in this.Controls)
                        if (control is Button && control.TabIndex < 81)
                            collection.Add((Button)control);
                    buttonPressed = ai.makeMove(gameBoard, outerBoard, collection);
                    buttonPressed.PerformClick();
                }
                  

				
			}
		}

		//takes the internal board and places it into an array for other methods
		public static char[,] packageBoard(char[, , ,] board, int row, int col) 
		{
			char[,] packageArr = new char[3, 3];
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					packageArr[i, j] = board[row, col, i, j];
			return packageArr;
 
		}

		//Checks for a three in a row in the board it is passed
		public static char threeInARow(char[,] board)
		{
			//check rows
			for (int i = 0; i < 3; i++)
				if (board[i,0] != '-' && board[i,0] == board[i,1] && board[i,0] == board[i,2])
				{
					return board[i,0];
				}

			//check columns
			for (int i = 0; i < 3; i++)
				if (board[0,i] != '-' && board[0,i] == board[1,i] && board[0,i] == board[2,i])
					return board[0,i];

			//check diagonals
			if (board[1,1] != '-' && ((board[1,1] == board[0,0] && board[1,1] == board[2,2])
					|| (board[1,1] == board[0,2] && board[1,1] == board[2,0])))
				return board[1,1];
			return '-';
 
		}

		//Disables an internal board based on its index
		public void enableBoard(int index, bool enabled)
		{
			foreach (Control control in this.Controls )
			{
				if (control is Button && control.TabIndex >= index * 9 && control.TabIndex < index * 9 + 9
					&& control.Text=="")
				{
					control.Enabled = enabled;
				}
			}
		}

		//Hides an internal board based on its index
		public void hideBoard(int index, bool visible)
		{
			foreach (Control control in this.Controls)
			{
				if (control is Button && control.TabIndex >= index * 9 && control.TabIndex < index * 9 + 9)
				{
					control.Visible = visible;
				}
			}
		}

		//checks to see if a board is full but has no 3 in a rows
		public static bool isFull(char[,] board)
		{
 			for(int i=0; i<3; i++)
				for (int j = 0; j < 3; j++)
					if (board[i, j] == '-')
						return false;
			return true;
		}

		private void cmdReset_Click(object sender, EventArgs e)
		{
			foreach (Control control in this.Controls)
			{
 				if(control is Button && control != cmdReset)
				{
					control.Enabled = true;
					control.Visible = true;
					control.Text = "";
				}
				else if (control is Label && control != lblPlayer)
				{
					control.Text = "";
					control.Visible = false;
				}
				else if (control == lblPlayer)
				{
					changePlayer(player);
				}
			}

			txtConsole.AppendText("\r\n");
			gameBoard = new char[3, 3, 3, 3];
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					for (int k = 0; k < 3; k++)
						for (int l = 0; l < 3; l++)
							gameBoard[i, j, k, l] = '-';
			outerBoard = new char[3, 3];
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					outerBoard[i, j] = '-';

            aiEnabled = radComputer.Checked;
            if (aiEnabled)
            {
                switch(player)
                { 
                    case 'X': 
                    ai = new playerAI('O');
                    break;
                    case 'O':
                    ai = new playerAI('X');
                    break;
                }
            }
		}
	}// Partial class

    public class ButtonRef
    {
        public Button but { get; set; }
        public int val { get; set; }

        public ButtonRef(Button button, int value)
        {
            but = button;
            val = value;
        }
    }

    /*
     * STOP!
     * You're about to see some hideous code! When I was coding all of the "Check for" methods, 
     * I was focussing on getting it working, not efficiency, so there is a lot of cody that is 
     * the same and could be condensed into one method. Also some methods don't work as inteded 
     * yet.
    */

    public class playerAI 
    {
        public char marker { get; set; }
        public static List<ButtonRef> buttons = new List<ButtonRef>();

        public playerAI(char player)
        {
            marker = player;   
        }

        public playerAI() {
            marker = 'X';
        }

        public Button makeMove(char[,,,] gameBoard, char[,] outerBoard, List<Button> ctrColl)
        {
            //Detect which buttons are enabled and store them to arraylist
            foreach (Button control in ctrColl)
            {
                if (control.Enabled)
                    buttons.Add(new ButtonRef((Button)control, 0));
            }

            //run through tests
            checkForWinningMove(gameBoard, outerBoard);
            checkForGivenWin(gameBoard, outerBoard);
            checkForInternalWin(gameBoard, outerBoard);
            checkForGivenBoard(gameBoard, outerBoard);
            checkForTwoInARow(gameBoard, outerBoard);
            checkForCorner(gameBoard, outerBoard);
            checkForClosedBoard(gameBoard, outerBoard);

            //sort objects
            int val;
            int index;
            Random randomizer =  new Random();
            List<ButtonRef> sortedButtons = new List<ButtonRef>();

            while (buttons.Count > 0)
            {
                val = buttons[0].val;
                index = 0;
                for (int i = 1; i < buttons.Count; i++)
                    if (buttons[i].val > val)
                    {
                        val = buttons[i].val;
                        index = i;
                    }
                sortedButtons.Add(buttons[index]);
                buttons.RemoveAt(index);       
            }

            //check for equal priority and randomly chose
            {
                index = 1;
                val = sortedButtons[0].val;
                while (val == sortedButtons[index].val)
                    index++;
            }

            return sortedButtons[randomizer.Next(0, index)].but;
        }

        //See if the AI can win
        private void checkForWinningMove(char[,,,] gameBoard, char [,] outerBoard)
        {
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];


            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                tempBoard[outRow, outCol, inRow, inCol] = marker;

                if (frmGame.threeInARow(frmGame.packageBoard(tempBoard, outRow, outCol)) != '-')
                    tempOuterBoard[outRow, outCol] = frmGame.threeInARow(frmGame.packageBoard(tempBoard, outRow, outCol));

                if (frmGame.threeInARow(tempOuterBoard) == marker)
                    testClick.val += 1000000;

                tempBoard[outRow, outCol, inRow, inCol] = '-';
            }


        }

        //make sure the AI doesn't give the player a win
        private void checkForGivenWin(char[, , ,] gameBoard, char[,] outerBoard)
        {
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];

            char[,] tempInnerBoard = new char[3, 3];

            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                tempBoard[outRow, outCol, inRow, inCol] = marker;
                if (!frmGame.isFull(frmGame.packageBoard(tempBoard, inRow, outRow)) && tempOuterBoard[inRow, outCol] == '-')
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    tempInnerBoard[k, l] = tempBoard[inRow, inCol, k, l];
                                }
                            }
                            if (tempInnerBoard[i, j] == '-')
                                if (frmGame.threeInARow(tempInnerBoard) != '-' && frmGame.threeInARow(tempInnerBoard) != marker)
                                    switch (marker)
                                    {
                                        case 'X':
                                            tempInnerBoard[i, j] = 'O';
                                            break;
                                        case 'O':
                                            tempInnerBoard[i, j] = 'X';
                                            break;
                                    }
                            if (frmGame.threeInARow(tempInnerBoard) != marker && frmGame.threeInARow(tempInnerBoard) != '-')
                                testClick.val -= 1000;
                        }
                    }//first loop
                }//if
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    if (tempBoard[i, j, k, l] == '-' && tempOuterBoard[i,j] == '-')
                                    {
                                        switch (marker)
                                        {
                                            case 'X':
                                                tempBoard[i, j, k, l] = 'O';
                                                break;
                                            case 'O':
                                                tempBoard[i, j, k, l] = 'X';
                                                break;
                                        }
                                        if (frmGame.threeInARow(frmGame.packageBoard(tempBoard, i, j)) != '-' && frmGame.threeInARow(frmGame.packageBoard(tempBoard, i, j)) != marker)
                                            tempOuterBoard[i, j] = frmGame.threeInARow(frmGame.packageBoard(tempBoard, i, j));
                                        if (frmGame.threeInARow(tempOuterBoard) != '-' && frmGame.threeInARow(tempOuterBoard) != marker)
                                            testClick.val -= 1000;
                                        tempBoard[i, j, k, l] = '-';
                                        tempOuterBoard[i, j] = '-';
                                    }

                                }
                            }
                        }
                    }//loop
                }//else
                tempBoard[outRow, outCol, inRow, inCol] = '-';
            }//foreach
        }

        //Prioritize winning a board
        private void checkForInternalWin(char[, , ,] gameBoard, char[,] outerBoard)
        { 
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];


            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                tempBoard[outRow, outCol, inRow, inCol] = marker;
                if (frmGame.threeInARow(frmGame.packageBoard(tempBoard, outRow, outCol)) == marker)
                    testClick.val += 10;
                tempBoard[outRow, outCol, inRow, inCol] = '-';
            }
        }

        //Avoid giving opponent other boards
        private void checkForGivenBoard(char[, , ,] gameBoard, char[,] outerBoard)
        {
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];
            
            char[,] tempInnerBoard = new char[3, 3];

            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                tempBoard[outRow, outCol, inRow, inCol] = marker;
                if (!frmGame.isFull(frmGame.packageBoard(tempBoard, inRow, outRow)) && tempOuterBoard[inRow, outCol] == '-')
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    tempInnerBoard[k, l] = tempBoard[inRow, inCol, k, l];
                                }
                            }
                            if (tempInnerBoard[i, j] == '-')
                                if (frmGame.threeInARow(tempInnerBoard) != '-' && frmGame.threeInARow(tempInnerBoard) != marker)
                                    testClick.val -= 3;
                        }
                    }//first loop
                }//if
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    if (tempBoard[i, j, k, l] == '-' && tempOuterBoard[i, j] == '-')
                                    {
                                        switch (marker)
                                        {
                                            case 'X':
                                                tempBoard[i, j, k, l] = 'O';
                                                break;
                                            case 'O':
                                                tempBoard[i, j, k, l] = 'X';
                                                break;
                                        }
                                        if (frmGame.threeInARow(frmGame.packageBoard(tempBoard, i, j)) != '-' && frmGame.threeInARow(frmGame.packageBoard(tempBoard, i, j)) != marker)
                                            testClick.val -= 3;
                                        tempBoard[i, j, k, l] = '-';
                                        tempOuterBoard[i, j] = '-';
                                    }

                                }
                            }
                        }
                    }//loop
                }//else
                tempBoard[outRow, outCol, inRow, inCol] = '-';
            }//foreach
        }

        //Look for getting two in a row that is not blocked
        private void checkForTwoInARow(char[, , ,] gameBoard, char[,] outerBoard)
        {
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];


            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;
            int counter; 

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                counter = 0;

                tempBoard[outRow, outCol, inRow, inCol] = marker;

                //check row
                for (int i = 0; i < 3; i++)
                    if (tempBoard[outRow, outCol, inRow, i] == marker)
                        counter++;
                    else if (tempBoard[outRow, outCol, inRow, i] == '-')
                        counter += 0;
                    else
                        counter = 0;
                testClick.val += counter;

                //check column
                for (int i = 0; i < 3; i++)
                    if (tempBoard[outRow, outCol, i, inCol] == marker)
                        counter++;
                    else if (tempBoard[outRow, outCol, inRow, i] == '-')
                        counter += 0;
                    else
                        counter = 0;
                testClick.val += counter;

                //check diagonal
                for (int i = 0; i < 3; i++)
                    if (tempBoard[outRow, outCol, i, i] == marker)
                        counter++;
                    else if (tempBoard[outRow, outCol, inRow, i] == '-')
                        counter += 0;
                    else
                        counter = 0;
                testClick.val += counter;

                //check row
                for (int i = 0; i < 3; i++)
                    if (tempBoard[outRow, outCol, 2-i, i] == marker)
                        counter++;
                    else if (tempBoard[outRow, 2-i, inRow, i] == '-')
                        counter += 0;
                    else
                        counter = 0;
                testClick.val += counter;

                tempBoard[outRow, outCol, inRow, inCol] = '-';
            }
 
        }

        //prioritize corners
        private void checkForCorner(char[, , ,] gameBoard, char[,] outerBoard)
        { 
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];


            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                if (inRow % 2 == 0 && inCol % 2 == 0)
                    testClick.val++;
            }
        
        }

        //avoid sending opponent to closed boards
        private static void checkForClosedBoard(char[, , ,] gameBoard, char[,] outerBoard)
        { 
            //create temporary arrays
            char[, , ,] tempBoard = new char[3, 3, 3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            tempBoard[i, j, k, l] = gameBoard[i, j, k, l];
            char[,] tempOuterBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tempOuterBoard[i, j] = outerBoard[i, j];


            //declare variables
            int outRow;
            int outCol;
            int inRow;
            int inCol;
            int n;

            foreach (ButtonRef testClick in buttons)
            {
                n = testClick.but.TabIndex;
                inCol = n % 3;
                n = n / 3;
                inRow = n % 3;
                n = n / 3;
                outCol = n % 3;
                n = n / 3;
                outRow = n;

                if (frmGame.isFull(frmGame.packageBoard(tempBoard, inRow, inCol)) || tempOuterBoard[inRow, inCol] != '-')
                    testClick.val-=2;
            }
        }
    }
}
