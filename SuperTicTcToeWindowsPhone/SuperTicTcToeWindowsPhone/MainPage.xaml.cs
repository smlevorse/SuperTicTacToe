using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SuperTicTcToeWindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public char[, , ,] gameBoard;
        public char[,] outerBoard;
        public char player;
        //bool aiEnabled;
        //bool aiTurn;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Process button pressed
            Button buttonPressed = (Button)sender;
            int index = buttonPressed.TabIndex;
            Button tempButton;

            //declare variables
            int inRow;					//inner row
            int outRow;					//outter row
            int inCol;					//inner column
            int outCol;					//outter column
            int n = index;				//copy of index to use for processing 

            //display marker
            buttonPressed.Content = "" + player;

            //convert index to trinary and store to variables
            inCol = n % 3;
            n = n / 3;
            inRow = n % 3;
            n = n / 3;
            outCol = n % 3;
            n = n / 3;
            outRow = n;

            //Record the player's mark
            gameBoard[outRow, outCol, inRow, inCol] = player;

            //check for 3-in-a-row in the internal board
            if (threeInARow(packageBoard(gameBoard, outRow, outCol)) != '-')
            {
                outerBoard[outRow, outCol] = threeInARow(packageBoard(gameBoard, outRow, outCol));

                //disable and hide that internal board
                enableBoard(index / 9, false);
                hideBoard(index / 9, false);

                //make the correct label visible and display the character's mark
                switch (index / 9)
                {
                    case 0:
                        {
                            lbl00.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl00.Text = "" + player;
                            break;
                        }
                    case 1:
                        {
                            lbl01.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl01.Text = "" + player;
                            break;
                        }
                    case 2:
                        {
                            lbl02.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl02.Text = "" + player;
                            break;
                        }
                    case 3:
                        {
                            lbl10.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl10.Text = "" + player;
                            break;
                        }
                    case 4:
                        {
                            lbl11.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl11.Text = "" + player;
                            break;
                        }
                    case 5:
                        {
                            lbl12.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl12.Text = "" + player;
                            break;
                        }
                    case 6:
                        {
                            lbl20.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl20.Text = "" + player;
                            break;
                        }
                    case 7:
                        {
                            lbl21.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl21.Text = "" + player;
                            break;
                        }
                    case 8:
                        {
                            lbl22.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            lbl22.Text = "" + player;
                            break;
                        }
                    default:
                        {
                            //Insert Error Message
                            break;
                        }
                }//switch
            }//if

            //check for overall win
            if (threeInARow(outerBoard) != '-')
            {
                //display win
                //Display win
                vbxHidden.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Grid.SetColumnSpan(vbxWinner, 2);
                lblX.Text = "Player " + threeInARow(outerBoard) + "wins!";

                //disable all of the boards
                for (int i = 0; i < 9; i++)
                    enableBoard(i, false);
            }
            else
            {

                //Determine which buttons should be enabled after turn, enable those and disable the rest
                if (outerBoard[inRow, inCol] != '-' || isFull(packageBoard(gameBoard, inRow, inCol)))
                {
                    foreach (Control control in gridBoard.Children)
                    {
                        if (control is Button)
                        {
                            tempButton = (Button)control;
                            if(tempButton.TabIndex < 81 && tempButton.Content == "" && tempButton.Visibility.Equals(Windows.UI.Xaml.Visibility.Visible))
                                {
                                    control.IsEnabled = true;
                                }
                        }
                    }

                    rectPlayingBoard.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                        enableBoard(i, false);
                    enableBoard(inRow * 3 + inCol, true);
                    Grid.SetColumn(rectPlayingBoard, 2 + 7 * inCol);
                    Grid.SetRow(rectPlayingBoard, 2 + 7 * inRow);

                }

                buttonPressed.IsEnabled = false;

                //toggle player
                if (player == 'X')
                    changePlayer('O');
                else if (player == 'O')
                    changePlayer('X');

                //code if we have an AI
                //if (aiEnabled && player == ai.marker)
                //{
                //    List<Button> collection = new List<Button>();
                //    foreach (Control control in this.Controls)
                //        if (control is Button && control.TabIndex < 81)
                //            collection.Add((Button)control);
                //    buttonPressed = ai.makeMove(gameBoard, outerBoard, collection);
                //    buttonPressed.PerformClick();
                //}
            }
        }

        public void changePlayer(char newPlayer)
        {
            player = newPlayer;
            switch (player)
            {
                case 'X':
                    Grid.SetColumn(rectPlayer, 0);
                    break;
                case 'O':
                    Grid.SetColumn(rectPlayer, 1);
                    break;
                default:
                    break;
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
                if (board[i, 0] != '-' && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                {
                    return board[i, 0];
                }

            //check columns
            for (int i = 0; i < 3; i++)
                if (board[0, i] != '-' && board[0, i] == board[1, i] && board[0, i] == board[2, i])
                    return board[0, i];

            //check diagonals
            if (board[1, 1] != '-' && ((board[1, 1] == board[0, 0] && board[1, 1] == board[2, 2])
                    || (board[1, 1] == board[0, 2] && board[1, 1] == board[2, 0])))
                return board[1, 1];
            return '-';

        }

        //Disables an internal board based on its index
        public void enableBoard(int index, bool enabled)
        {
            Button temp;
            foreach (Control control in gridBoard.Children)
            {
                if (control is Button)
                {
                    temp = (Button)control;
                    if (temp.TabIndex >= index * 9 && control.TabIndex < index * 9 + 9
                    && temp.Content == "")
                    {
                        control.IsEnabled = enabled;
                    }
                }
            }
        }

        //Hides an internal board based on its index
        public void hideBoard(int index, bool visible)
        {
            foreach (Button control in gridBoard.Children)
            {
                if (control is Button && control.TabIndex >= index * 9 && control.TabIndex < index * 9 + 9)
                {
                    if (visible)
                        control.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    else
                        control.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }

        //checks to see if a board is full but has no 3 in a rows
        public static bool isFull(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == '-')
                        return false;
            return true;
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            foreach (Button control in gridBoard.Children)
            {
                control.IsEnabled = true;
                control.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ((Button)control).Content = "";
            }
            foreach (TextBlock control in gridBoard.Children)
            {
                control.Text = "";
                control.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

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

            vbxHidden.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Grid.SetColumnSpan(vbxWinner, 1);
            lblX.Text = "X";
            //aiEnabled = radComputer.Checked;
            //if (aiEnabled)
            //{
            //    switch (player)
            //    {
            //        case 'X':
            //            ai = new playerAI('O');
            //            break;
            //        case 'O':
            //            ai = new playerAI('X');
            //            break;
            //    }
            //}
        }
    }
}
