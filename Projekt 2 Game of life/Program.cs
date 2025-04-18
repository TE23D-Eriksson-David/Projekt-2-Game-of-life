using SC = System.Console;
using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;

Raylib.SetTargetFPS(30);
Raylib.InitWindow(800, 600, "Game Of Life");
Choice UserInput;
Vector2 MousePressedPosition;
Vector2 BoardSizeY;
Vector2 BoardSizeX;

bool CreateOnce = true;



while (!Raylib.WindowShouldClose())
{
    Interface.SetChoice(Choice.None);
    Interface.Draw();
    UserInput = Interface.GetChoice();
    MousePressedPosition = Interface.GetMousePosition();
    Interface.GetBoardDimentions(out BoardSizeY, out BoardSizeX);

    if (CreateOnce){
        int CellSize = Cell.GetSize();
        Board.CerateMatrix(BoardSizeX, BoardSizeY, CellSize);
        CreateOnce = false;
    }
    Board.DrawCells();

    Game_Logic.EvaluateUserImput(UserInput,MousePressedPosition);

}








// Board.Draw(BoardSizeX, BoardSizeY, CellSize);