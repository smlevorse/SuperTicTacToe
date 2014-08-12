package javaConsoleVersion;

import java.util.Scanner;

public class SuperTicTacToe {

	public static void main(String[] args) {
		//Initialize game board
		char gameBoard[][][][] ={{{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}},
							  
							 {{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}},
							 
							 {{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}}};
		char outerBoard[][] = {{'-','-','-'},
				               {'-','-','-'},
				               {'-','-','-'}};
//		String gameBoard[][][][] = new String[3][3][3][3];                   Code used to test the printBoard() method
//		
//		for(int i=0; i<3; i++)
//			for(int j=0; j<3; j++)
//				for(int k=0; k<3; k++)
//					for(int l=0; l<3; l++)
//						gameBoard[i][j][k][l]= "" + i + j + k + l;
//		printBoard(gameBoard);
		
		//declare variables
		char player = 'X';					//Holds the player whose turn it is
		int playingBoard[] = new int[2];	//Holds the location of the board the player has to play in
		int playerLocation[] = new int[2];	//Holds the location of the cell the player wants to play in
		Scanner in = new Scanner(System.in);//Input
		boolean won = false;				//Has someone won the game
		char tiar = '-';							//Three in a row
		
		//allow first player to pick board
		System.out.println("Player X, enter pick a board to start in.");
		System.out.print("Row:");
		playingBoard[0]=in.nextInt();
		while(playingBoard[0] >= 3 || playingBoard[0] < 0)
		{
			System.out.println("Row must be between 0 and 2");
			System.out.print("Row:");
			playingBoard[0]=in.nextInt();
		}
		System.out.print("Column:");
		playingBoard[1]=in.nextInt();
		while(playingBoard[1] >= 3 || playingBoard[1] < 0)
		{
			System.out.println("Column must be between 0 and 2");
			System.out.print("Column:");
			playingBoard[1]=in.nextInt();
		}
		
		//Play the game
		while(!won)
		{
			//Get player's cell
			System.out.println("Player " + player + ", pick a cell to place an " + player + ":");
			System.out.print("Row:");
			playerLocation[0]=in.nextInt();
			while(playerLocation[0] >= 3 || playerLocation[0] < 0)
			{
				System.out.println("Row must be between 0 and 2");
				System.out.print("Row:");
				playerLocation[0]=in.nextInt();
			}
			System.out.print("Column:");
			playerLocation[1]=in.nextInt();
			while(playerLocation[1] >= 3 || playerLocation[1] < 0)
			{
				System.out.println("Column must be between 0 and 2");
				System.out.print("Column:");
				playerLocation[1]=in.nextInt();
			}
			//make sure the board at that location is empty:
			if((gameBoard[playingBoard[0]][playingBoard[1]][playerLocation[0]][playerLocation[1]] == '-') 
					&& (outerBoard[playingBoard[0]][playingBoard[1]] == '-'))
			{
				//put player's marker on the board
				gameBoard[playingBoard[0]][playingBoard[1]][playerLocation[0]][playerLocation[1]] = player;
				
				//check for winning that internal board and change the external board to that value
				tiar=threeInARow(packageBoard(gameBoard, playingBoard[0], playingBoard[1]));
				outerBoard[playingBoard[0]][playingBoard[1]]=tiar;
				
				//Change board to explain winning that board
				switch (tiar)
				{
					case 'X':
					{
						gameBoard[playingBoard[0]][playingBoard[1]][0][0]='\\';
						gameBoard[playingBoard[0]][playingBoard[1]][0][1]='-';
						gameBoard[playingBoard[0]][playingBoard[1]][0][2]='/';
						gameBoard[playingBoard[0]][playingBoard[1]][1][0]='-';
						gameBoard[playingBoard[0]][playingBoard[1]][1][1]='X';
						gameBoard[playingBoard[0]][playingBoard[1]][1][2]='-';
						gameBoard[playingBoard[0]][playingBoard[1]][2][0]='/';
						gameBoard[playingBoard[0]][playingBoard[1]][2][1]='-';
						gameBoard[playingBoard[0]][playingBoard[1]][2][2]='\\';
						break;
					}
					
					case 'O':
					{
						gameBoard[playingBoard[0]][playingBoard[1]][0][0]='/';
						gameBoard[playingBoard[0]][playingBoard[1]][0][1]='\u203E';
						gameBoard[playingBoard[0]][playingBoard[1]][0][2]='\\';
						gameBoard[playingBoard[0]][playingBoard[1]][1][0]='|';
						gameBoard[playingBoard[0]][playingBoard[1]][1][1]='-';
						gameBoard[playingBoard[0]][playingBoard[1]][1][2]='|';
						gameBoard[playingBoard[0]][playingBoard[1]][2][0]='\\';
						gameBoard[playingBoard[0]][playingBoard[1]][2][1]='_';
						gameBoard[playingBoard[0]][playingBoard[1]][2][2]='/';
						break;
					}
				
				}
				
				//Check for overall win
				tiar=threeInARow(outerBoard);
				if (tiar != '-')
				{
					System.out.println("Player " + tiar + " wins!");
					won=true;
				}	
				
				//switch player
				if(player=='X')
					player='O';
				else if(player=='O')
					player='X';
				
				//make the player's cell location the new board location
				playingBoard[0]=playerLocation[0];
				playingBoard[1]=playerLocation[1];
			}
			else
				System.out.println("That space is already full");
			//print the board
			printBoard(gameBoard);
		}
		
	}

	public static void printBoard(char[][][][] board) {
		System.out.println("=============================");
		for(int i=0; i<3; i++)
		{
			for (int j=0; j<3; j++)
			{
				for(int k=0; k<3; k++)
				{
					System.out.print("|| ");
					for(int l=0; l<3; l++)
					{
						System.out.print(board[i][k][j][l]);
					}
					System.out.print(" || ");
				}
				System.out.println();
			}
			System.out.println("=============================");
		}
	}
	
	//returns an internal board packaged as a 3x3 array to be used to check for a 3-in-a-row
	public static char[][] packageBoard(char[][][][] board, int row, int col)
	{
		char packageArr[][] = new char[3][3];
		for(int i=0; i<3; i++)
			for(int j=0; j<3; j++)
				packageArr[i][j]=board[row][col][i][j];
		
		return packageArr;
	}
	
	public static char threeInARow(char[][] board)
	{
		//check rows
		for(int i=0; i<3; i++)
			if(board[i][0] == board[i][1] && board[i][0] == board[i][2])
				return board[i][0];
		
		//check columns
		for(int i=0; i<3; i++)
			if(board[0][i] == board[1][i] && board[0][i] == board[2][i])
				return board[0][i];
		
		//check diagonals
		if((board[1][1]== board[0][0] && board[1][1] == board [2][2]) 
				|| (board[1][1] == board[0][2] || board[1][1] == board[2][0]))
			return board[1][1];
		return '-';
	}
}

