using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;
using SC = System.Console;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(800, 600, "Game Of Life");
SC.Title = "My Game";

Choice userInput; // enum för spellarens vall
SimulationState state = SimulationState.None; ; // enum om simulationen körs
Vector2 boardSizeY;
Vector2 boardSizeX;
Vector2 pressedPosition; // Musens trykta position

List<List<Cell>> curentCellMatrix = new List<List<Cell>>(); // Mina två dimetionela listor
List<List<Cell>> nextCellMatrix = new List<List<Cell>>();
bool createOnce = true; // En engångsvariabel för att skappa Matrixarna en gång
int columnCells = 0;
int rowCells = 0;


while (!Raylib.WindowShouldClose())
{
    int overlineStartY = Raylib.GetScreenHeight() / 6; // Positionen för linijernas höjd
    int underlineStartY = Raylib.GetScreenHeight() * 5 / 6;
    userInput = Choice.None;

    userInput = Interface.Draw(out pressedPosition, userInput, overlineStartY, underlineStartY); // Ritar utt interfacet 
    Interface.GetBoardDimentions(out boardSizeY, out boardSizeX, overlineStartY, underlineStartY);  // Hämtar ramens inskrivnings kordinater, beroende på de övre och undre linijerna.

    if (createOnce)
    { // Görs en gång
        int cellSize = Cell.GetSize(); // hämtar storleken på cellen
        Board.CreateMatrix(boardSizeX, boardSizeY, cellSize, out curentCellMatrix, out nextCellMatrix, out columnCells, out rowCells); // Skappar de två matrixerna 2dListor.
        createOnce = false;
    }

    Board.DrawMatrix(curentCellMatrix); // Rita utt Brädet/Matrixen

    state = Game_Logic.EvaluateUserInput(state, userInput, pressedPosition, out createOnce, curentCellMatrix, nextCellMatrix); // Utför beslutet som spellarn har fattat och returnar statet för simulationen.
    Simulation_Rules.Run(state, curentCellMatrix, nextCellMatrix, columnCells, rowCells); // Kör simulationen enligt statet och för in resultatet i nästa matrix.

    Board.UpdateMatrix(state, curentCellMatrix, nextCellMatrix, columnCells, rowCells); // För in nästa matrix i nuvarande, görs bara om simulationen körs.
    Thread.Sleep(50); // Liten delay på en 20 dels sekund

}