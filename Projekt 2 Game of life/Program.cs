using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;
using SC = System.Console;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(800, 600, "Game Of Life");
SC.Title = "My window";

Choice UserInput; // enum för spellarens vall
SimulationState State; // enum om simulationen körs
Vector2 MousePressedPosition;
Vector2 BoardSizeY;
Vector2 BoardSizeX;

bool CreateOnce = true; // En engångsvariabel för att skappa Matrixarna en gång


while (!Raylib.WindowShouldClose())
{
    Interface.SetChoice(Choice.None); // Sätter användarens val till inget så att den bara görs en gång.
    Interface.Draw(); // Ritar utt interfacet
    UserInput = Interface.GetChoice(); // Hämtar inputen från användarn, vilket val som har trykts.
    MousePressedPosition = Interface.GetMousePosition(); // hämtar X & Y kordinaten i en vector där användaren trykte. 
    Interface.GetBoardDimentions(out BoardSizeY, out BoardSizeX);  // Hämtar ramens inskrivnings kordinater, beroende på de övre och undre linijerna.

    if (CreateOnce){ // Görs en gång
        SC.WriteLine("do");
        int CellSize = Cell.GetSize(); // hämtar storleken på cellen
        Board.CreateMatrix(BoardSizeX, BoardSizeY, CellSize); // Skappar de två matrixerna 2dListor.
        CreateOnce = false;
    }

    Board.DrawMatrix(); // Rita utt Brädet/Matrixen

    State = Game_Logic.EvaluateUserInput(UserInput, MousePressedPosition, out CreateOnce); // Utför beslutet som spellarn har fattat och returnar statet för simulationen.
    Simulation_Rules.Run(State); // Kör simulationen enligt statet och för in resultatet i nästa matrix.

    Board.UpdateMatrix(State); // För in nästa matrix i nuvarande, görs bara om simulationen körs.
    Thread.Sleep(50); // Liten delay på en 20 dels sekund

}




