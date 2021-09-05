Battleships project
===================

This project is an excercise in designing and building C# solutions

Assumptions
-----------
* The game is conducted between 2 players
* The game takes place on a 10 x 10 board
* There are 10 ships placed on the board (4 x 2 field long ships, 3 x 3 field long ships, 2 x 4 field long ships, 1 x 5 field long ships)
* The ships can be placed on the field in a way that they will not fall out of bounds of the board in whole or in part
* The ships can be placed next to the boarde of the board (i.e. fields A1, A10, J10)
* The ships cant be placed next to other ships (they have to have at least 1 field space from other ships in all directions)
* Second (and subsequent) miss on a previously aimed field yields same results as on first shot resulting in lost turn
* Second (and subsequent) hit on a previously hit ship field yields same results as on first shot resulting in lost turn

Projects
--------
* Battleships - contains business logic for the game (rules)
* BattleshipsTests - contains tests for the business logic