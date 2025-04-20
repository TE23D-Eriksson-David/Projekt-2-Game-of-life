using SC = System.Console;
using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(800, 600, "Game Of Life");
Choice UserInput;
SimulationState State = SimulationState.None;
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

    if (CreateOnce)
    {
        int CellSize = Cell.GetSize();
        Board.CreateMatrix(BoardSizeX, BoardSizeY, CellSize);
        CreateOnce = false;
    }
    Board.DrawMatrix();

    State = Game_Logic.EvaluateUserInput(UserInput, MousePressedPosition);
    Simulation_Rules.Run(State);

    Board.UpdateMatrix(State);
    Thread.Sleep(50);

    // Board.DrawNextMatrix(State);

}








// Board.Draw(BoardSizeX, BoardSizeY, CellSize);