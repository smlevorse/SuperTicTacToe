using System;
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

				//toggle player
				if (player == 'X')
					changePlayer('O');
				else if (player == 'O')
					changePlayer('X');

				buttonPressed.Enabled = false;
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
		public bool isFull(char[,] board)
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
		}
	}
}
