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
		public Boolean won;
		public Boolean firstTurn;
		public char player;

		public frmGame()
		{
			InitializeComponent();
		}
		private void frmGame_Load(object sender, EventArgs e)
		{
			gameBoard = new char[3, 3, 3, 3];
			outerBoard = new char[3, 3];
			won = false;
			firstTurn = true;
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
			string location;
			int inRow;					//inner row
			int outRow;					//outter row
			int inCol;					//inner column
			int outCol;					//outter column
			int trinIndex;				//trinary index

			//display marker
			buttonPressed.Text = "" + player;
			
			//convert index to trinary

			location = String.Format("0:0000", Convert.ToString(index, 3));
			
			//set location variables
			inRow = location.ElementAt(2);
			outRow = location.ElementAt(0);
			inCol = location.ElementAt(3);
			outCol = location.ElementAt(1);

			lblPlayer.Text = "" + outRow + ", " + outCol + ", " + inRow + ", " + inCol;

			if (player == 'X')
				changePlayer('O');
			else if (player == 'O')
				changePlayer('X');

			buttonPressed.Enabled = false;
		}

	}
}
