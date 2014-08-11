package javaConsoleVersion;

public class SuperTicTacToe {

	public static void main(String[] args) {
		char gameBoard[][][][] ={{{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}},
							  
							 {{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}},
							 
							 {{{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}},
							  {{'-','-','-'},{'-','-','-'},{'-','-','-'}}}};
		printBoard(gameBoard);
		
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
						System.out.print(board[i][j][k][l]);
					}
					System.out.print(" || ");
				}
				System.out.println();
			}
			System.out.println("=============================");
		}
	}
}
