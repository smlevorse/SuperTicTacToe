perTicTacToe
==============

A program to play Super TicTacToe.

The Game
==============
The board is set up as a giant TicTacToe board with each cell containing another TicTacToe board as such:

....1.2.3....4.5.6....7.8.9....
a..._|_|_ || _|_|_ || _|_|_...a
b..._|_|_ || _|_|_ || _|_|_...b
c... | |  ||  | |  ||  | | ...c
....=======================....
d..._|_|_ || _|_|_ || _|_|_...d
e..._|_|_ || _|_|_ || _|_|_...e
f... | |  ||  | |  ||  | | ...f
....=======================....
g..._|_|_ || _|_|_ || _|_|_...g
h..._|_|_ || _|_|_ || _|_|_...h
i... | |  ||  | |  ||  | | ...i
....1.2.3....4.5.6....7.8.9....

Gameplay:

The game starts with one player(X) placing an X in any of the cells.
From then on, the players take turns placing their respective markers in whichever board is determined by the previous
player's move. Each cell on the internal boards corresponds to one of the boards located in the external board:

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a...r|s|t || r|s|t || r|s|t...a | a..._|_|_ || _|_|_ || _|_|_...a
b...u|v|w || u|v|w || u|v|w...b | b..._|R|_ || _|S|_ || _|T|_...b
c...x|y|z || x|y|z || x|y|z...c | c... | |  ||  | |  ||  | | ...c
....=======================.... | ....=======================....
d...r|s|t || r|s|t || r|s|t...d | d..._|_|_ || _|_|_ || _|_|_...d
e...u|v|w || u|v|w || u|v|w...e | e..._|U|_ || _|V|_ || _|W|_...e
f...x|y|z || x|y|z || x|y|z...f | f... | |  ||  | |  ||  | | ...f
....=======================.... | ....=======================....
g...r|s|t || r|s|t || r|s|t...g | g..._|_|_ || _|_|_ || _|_|_...g
h...u|v|w || u|v|w || u|v|w...h | h..._|X|_ || _|Y|_ || _|Z|_...h
i...x|y|z || x|y|z || x|y|z...i | i... | |  ||  | |  ||  | | ...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

Notice each cell's location corresponds to a location of an internal board in the external board. When a player places
his marker in cell, his oppenent must place his next piece in the corresponding board. For instance, if player X places
an X in cell d6, marked 't', player O would have to place an O in the board marked 'T' in the upper right(a7, a8, a9, 
b7, b8, b9, c7, c8, or c9):

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|_|_ || _|_|_ || _|_|_...a | a..._|_|_ || _|_|_ || _|_|_...a
b..._|_|_ || _|_|_ || _|_|_...b | b..._|_|_ || _|_|_ || _|_|_...b
c... | |  ||  | |  ||  | | ...c | c... | |  ||  | |  ||  | |O...c
....=======================.... | ....=======================....
d..._|_|_ || _|_|X || _|_|_...d | d..._|_|_ || _|_|X || _|_|_...d
e..._|_|_ || _|_|_ || _|_|_...e | e..._|_|_ || _|_|_ || _|_|_...e
f... | |  ||  | |  ||  | | ...f | f... | |  ||  | |  ||  | | ...f
....=======================.... | ....=======================....
g..._|_|_ || _|_|_ || _|_|_...g | g..._|_|_ || _|_|_ || _|_|_...g
h..._|_|_ || _|_|_ || _|_|_...h | h..._|_|_ || _|_|_ || _|_|_...h
i... | |  ||  | |  ||  | | ...i | i... | |  ||  | |  ||  | | ...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

When player O places his marker in the upper right board, play X must then place his piece in correspondence to player
O's piece. For instance if player O places his piece in c9, then player X must then place his piece in the board marked
'Z'

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|_|_ || _|_|_ || _|_|_...a | a..._|_|_ || _|_|_ || _|_|_...a
b..._|_|_ || _|_|_ || _|_|_...b | b..._|_|_ || _|_|_ || _|_|_...b
c... | |  ||  | |  ||  | |O...c | c... | |  ||  | |  ||  | |O...c
....=======================.... | ....=======================....
d..._|_|_ || _|_|X || _|_|_...d | d..._|_|_ || _|_|X || _|_|_...d
e..._|_|_ || _|_|_ || _|_|_...e | e..._|_|_ || _|_|_ || _|_|_...e
f... | |  ||  | |  ||  | | ...f | f... | |  ||  | |  ||  | | ...f
....=======================.... | ....=======================....
g..._|_|_ || _|_|_ || _|_|_...g | g..._|_|_ || _|_|_ || _|_|_...g
h..._|_|_ || _|_|_ || _|_|_...h | h..._|_|_ || _|_|_ || X|_|_...h
i... | |  ||  | |  ||  | | ...i | i... | |  ||  | |  ||  | | ...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

When a player gets a 3-in-a-row, or three of his pieces in the same internal board in a straight line(horizontally,
virtically, or diagonally) he wins that board and now may use it towards victory.

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|_|_ || O|_|_ || _|_|_...a | a..._|_|_ || O|_|_ || _|_|_...a
b..._|_|X || _|_|_ || _|_|_...b | b..._|_|X || _|_|_ || _|_|_...b
c... | |  ||  | |  ||  | |O...c | c... | |  ||  | |  ||  | |O...c
....=======================.... | ....=======================....
d..._|_|_ || _|_|X || _|X|_...d | d..._|_|_ || \   / || _|X|_...d
e..._|O|_ || _|X|_ || _|O|_...e | e..._|O|_ ||   X   || _|O|_...e
f... | |  || X| |O ||  | | ...f | f... | |  || /   \ ||  | | ...f
....=======================.... | ....=======================....
g..._|_|_ || _|_|_ || _|_|_...g | g..._|_|_ || _|_|_ || _|_|_...g
h..._|_|O || _|_|_ || X|_|_...h | h..._|_|O || _|_|_ || X|_|_...h
i... | |  ||  | |  || X| | ...i | i... | |  ||  | |  || X| | ...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

When all nine cells in an internal board are filled yet there is still no winner in that board, it is a tie and the
board is decared null. It cannot be used towards victory.

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|O|_ || O|_|_ || _|_|_...a | a..._|O|_ || O|_|_ || _|_|_...a
b...O|_|X || X|_|_ || X|_|_...b | b...O|_|X || X|_|_ || X|_|_...b
c... | |  ||  | |  ||  | |O...c | c... | |  ||  | |  ||  | |O...c
....=======================.... | ....=======================....
d...X|O|O || \   / || _|X|_...d | d...|\-|| || \   / || _|X|_...d
e...O|O|X ||   X   || X|O|O...e | e...||\|| ||   X   || X|O|O...e
f...X|X|O || /   \ ||  | | ...f | f...||-\| || /   \ ||  | | ...f
....=======================.... | ....=======================....
g.../¯¯¯\ || _|_|_ || \   /...g | g.../¯¯¯\ || _|_|_ || \   /...g
h...|   | || X|O|_ ||   x  ...h | h...|   | || X|O|_ ||   x  ...h
i...\___/ ||  | |  || /   \...i | i...\___/ ||  | |  || /   \...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

If a player is sent to an internal board that is etiher already won or is null, he can place his marker in any cell
that is not in a board that is null or won. In this case, it is player X's turn, and he must play in the upper middle
board. When he plays in cell c4, player O is told to play in the bottom left. Since that board has already been won,
player O can place his marker anywhere such as c9:

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|O|_ || O|_|_ || _|_|_...a | a..._|O|_ || O|_|_ || _|_|_...a
b...O|_|X || X|_|_ || X|_|_...b | b...O|_|X || X|_|_ || X|_|_...b
c... | |  || X| |  ||  | |O...c | c... | |  || X| |  ||  |O|O...c
....=======================.... | ....=======================....
d...|\-|| || \   / || _|X|_...d | d...|\-|| || \   / || _|X|_...d
e...||\|| ||   X   || X|O|O...e | e...||\|| ||   X   || X|O|O...e
f...||-\| || /   \ ||  | | ...f | f...||-\| || /   \ ||  | | ...f
....=======================.... | ....=======================....
g.../¯¯¯\ || _|_|_ || \   /...g | g.../¯¯¯\ || _|_|_ || \   /...g
h...|   | || X|O|_ ||   x  ...h | h...|   | || X|O|_ ||   x  ...h
i...\___/ ||  | |  || /   \...i | i...\___/ ||  | |  || /   \...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

Victory:

In order to win, a player must win three internal boards in a straight line(horizontally, vertically, or diagonally).

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a...O|O|X || O|_|_ || /¯¯¯\...a | a...\   / || O|_|_ || /¯¯¯\...a
b...O|_|X || X|_|_ || |   |...b | b...  X   || X|_|_ || |   |...b
c... | |X || X| |  || \___/...c | c.../   \ || X| |  || \___/...c
....=======================.... | ....=======================....
d...|\-|| || \   / || \   /...d | d...|\-|| || \   / || \   /...d
e...||\|| ||   X   ||   X  ...e | e...||\|| ||   X   ||   X  ...e
f...||-\| || /   \ || /   \...f | f...||-\| || /   \ || /   \...f
....=======================.... | ....=======================....
g.../¯¯¯\ || _|_|_ || \   /...g | g.../¯¯¯\ || _|_|_ || \   /...g
h...|   | || X|O|O ||   x  ...h | h...|   | || X|O|O ||   x  ...h
i...\___/ ||  |X|  || /   \...i | i...\___/ ||  |X|  || /   \...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....

It is also possible to tie if all nine boards have been won or nullified and neither player has three boards in a
straight line. This is very dissapointing to both parties. Sometimes you can consider whoever won more internal boards
the victor, but still feel the shame in knowing that you didn't actually win.

I learned this game from some friends in highschool and it kept us entertained whenever we had a sub or finished our
work early. There may be other variations of the game.

The Program
==============

There are several objectives of this program:
  -Familiarize myself with GitHub, it functions and its uses
  -Actually make a digitial version of this game to help more people learn about it
  -Start building a portfolio of professional projects

Rough Plan:
  1) I will probably start with a simple text based version of the game in Java. 
  2) From there I will work on porting it to a version with a user interface possibly in C#
  3) Web app?
  4) Mobile app?

Notes:
~Starting College soon. College will take up time and slow down development
~Incoming student hackathon might be a good place to build a team of people to work on this with.

Copy Past Board
==============

....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
a..._|_|_ || _|_|_ || _|_|_...a | a..._|_|_ || _|_|_ || _|_|_...a
b..._|_|_ || _|_|_ || _|_|_...b | b..._|_|_ || _|_|_ || _|_|_...b
c... | |  ||  | |  ||  | | ...c | c... | |  ||  | |  ||  | | ...c
....=======================.... | ....=======================....
d..._|_|_ || _|_|_ || _|_|_...d | d..._|_|_ || _|_|_ || _|_|_...d
e..._|_|_ || _|_|_ || _|_|_...e | e..._|_|_ || _|_|_ || _|_|_...e
f... | |  ||  | |  ||  | | ...f | f... | |  ||  | |  ||  | | ...f
....=======================.... | ....=======================....
g..._|_|_ || _|_|_ || _|_|_...g | g..._|_|_ || _|_|_ || _|_|_...g
h..._|_|_ || _|_|_ || _|_|_...h | h..._|_|_ || _|_|_ || _|_|_...h
i... | |  ||  | |  ||  | | ...i | i... | |  ||  | |  ||  | | ...i
....1.2.3....4.5.6....7.8.9.... | ....1.2.3....4.5.6....7.8.9....
