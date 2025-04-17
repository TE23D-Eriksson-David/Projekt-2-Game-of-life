using SC = System.Console;
using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;

Raylib.SetTargetFPS(30);
Raylib.InitWindow(800, 600, "Game Of Life");
Choice UserInput = Choice.None;
Vector2 MousePressedPosition;
Vector2 BoardSizeY;
Vector2 BoardSizeX;



while (!Raylib.WindowShouldClose())
{
    Interface.SetChoice(Choice.None);
    Interface.Draw();
    UserInput = Interface.GetChoice();
    // SC.WriteLine(UserInput);
    MousePressedPosition = Interface.GetMousePosition();
    int CellStorlek = Cell.GetSize();
    Interface.GetBoardDimentions(out BoardSizeY, out BoardSizeX);
    // Board.Draw(BoardSizeX, BoardSizeY, CellStorlek);

    if (UserInput == Choice.Start)
    {
        SC.WriteLine("CerateMatrix");
        Board.CerateMatrix(BoardSizeX, BoardSizeY, CellStorlek);
        Interface.SetChoice(Choice.None);
    }
    
}
