using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;
using SC = System.Console;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(800, 600, "Game Of Life");
SC.Title = "My window";

Choice userInput; // enum för spellarens vall
SimulationState state; // enum om simulationen körs
Vector2 mousePressedPosition;
Vector2 boardSizeY;
Vector2 boardSizeX;

bool createOnce = true; // En engångsvariabel för att skappa Matrixarna en gång


while (!Raylib.WindowShouldClose())
{
    Interface.SetChoice(Choice.None); // Sätter användarens val till inget så att den bara görs en gång.
    Interface.Draw(); // Ritar utt interfacet 
    userInput = Interface.GetChoice(); // Hämtar inputen från användarn, vilket val som har trykts.
    mousePressedPosition = Interface.GetMousePosition(); // hämtar X & Y kordinaten i en vector där användaren trykte. 
    Interface.GetBoardDimentions(out boardSizeY, out boardSizeX);  // Hämtar ramens inskrivnings kordinater, beroende på de övre och undre linijerna.

    if (createOnce)
    { // Görs en gång
        SC.WriteLine("do");
        int cellSize = Cell.GetSize(); // hämtar storleken på cellen
        Board.CreateMatrix(boardSizeX, boardSizeY, cellSize); // Skappar de två matrixerna 2dListor.
        createOnce = false;
    }

    Board.DrawMatrix(); // Rita utt Brädet/Matrixen

    state = Game_Logic.EvaluateUserInput(userInput, mousePressedPosition, out createOnce); // Utför beslutet som spellarn har fattat och returnar statet för simulationen.
    Simulation_Rules.Run(state); // Kör simulationen enligt statet och för in resultatet i nästa matrix.

    Board.UpdateMatrix(state); // För in nästa matrix i nuvarande, görs bara om simulationen körs.
    Thread.Sleep(50); // Liten delay på en 20 dels sekund

}