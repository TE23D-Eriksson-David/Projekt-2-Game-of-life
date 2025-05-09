using System.Numerics;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Board
{
    public static void DrawMatrix(List<List<Cell>> cellCurentMatrix) // Ritar ut brädet inom de två svarta linijerna på interfacet.
    {

        int cellSize = Cell.GetSize(); // Cellernas storlek
        Rectangle Rec4 = new Rectangle(0, 0, cellSize, cellSize);

        foreach (List<Cell> list in cellCurentMatrix)
        {
            foreach (Cell instence in list) // Deta görs för varge cell
            {
                Cell.CellState state = instence.GetState();
                Rec4.X = instence.GetXPos(); // Hämtar positionen av cellen
                Rec4.Y = instence.GetYPos();
                Color cellCollor = Color.DarkGreen;

                switch (state)
                {
                    case Cell.CellState.Alive:
                        cellCollor = Color.White;
                        break;
                    case Cell.CellState.Dead:
                        cellCollor = Color.Beige;
                        break;
                }

                Raylib.DrawRectangleRec(Rec4, cellCollor); // Ritar ut min cell

            }
        }

    }


    // Skappar mina celler och lägger in dem i mina två listor.
    public static void CreateMatrix(Vector2 boardSizeX, Vector2 boardSizeY, int cellSize, out List<List<Cell>> cellCurentMatrix, out List<List<Cell>> cellNextMatrix, out int columnCells, out int rowCells)
    {
        // Min aledning för varför jag använder listor istället för arrayer är för att jag tror man inte kan skappa en
        // 2D array med obejekt i sig. Jag kunde kanshe ha gjort det med en array men när jag skrev min metod tänkte jag 
        // att jag behövde skappa lägga in cellerna när jag skappade dem och de var så längden av listan bestämdes vilket
        // Inte skulle fungerat med en array.
        // Deta var tidigare och dumare jag, men nu har jag en vettigare anledninging vilket är att den måste vara en lista 
        // efftersom den måste ändras dynamiskt beroende på skärm storleken vilket en array inte kan.

        cellCurentMatrix = new List<List<Cell>>(); // Mina två dimetionela listor
        cellNextMatrix = new List<List<Cell>>();
        cellCurentMatrix.Clear(); // Om listan redan är full så måste den tömas anars problem.
        cellNextMatrix.Clear(); // Deta görs för att skärmstorleken kan ändras.
        columnCells = 0;
        rowCells = 0;
        int CellSpace = Cell.space; // mellanrumet mellan cellerna.
        Rectangle Rec4 = new Rectangle(0, 0, cellSize, cellSize);
        boardSizeX.Y = Raylib.GetScreenWidth();


        for (Rec4.Y = boardSizeY.X; Rec4.Y < boardSizeY.Y; Rec4.Y += CellSpace)
        { // Inom ramarna för det två linijerna i yled, görs till en cell som skappas har des slut y position utanför den svarat linijen.
            cellCurentMatrix.Add(new List<Cell>()); // Läggs till en lista med cell obejekt som data typ.
            cellNextMatrix.Add(new List<Cell>());
            columnCells = 0; // Räknare för rader x led som sätts till 0

            for (Rec4.X = boardSizeX.X; Rec4.X < boardSizeX.Y - CellSpace; Rec4.X += CellSpace)
            {  // räknar i x led och lägger till cell storleken och mellanrumet för varge varv och samma som i den övre loppen.
                Cell curentMatrixCell = new Cell();
                curentMatrixCell.SetYPos(Rec4.Y);
                curentMatrixCell.SetXPos(Rec4.X);
                cellCurentMatrix[rowCells].Add(curentMatrixCell);
                // Skppar en cell för varge lista och lägger till nuvarande position i dem. Läger in dem i listan.
                Cell nextMatrixCell = new Cell();
                nextMatrixCell.SetYPos(Rec4.Y);
                nextMatrixCell.SetXPos(Rec4.X);
                cellNextMatrix[rowCells].Add(nextMatrixCell);

                columnCells++;  // räknar rader i x led  
            }
            rowCells++; // räknar rader i Y led    
        }
    }


    public static void UpdateMatrix(SimulationState state, List<List<Cell>> cellCurentMatrix, List<List<Cell>> cellNextMatrix, int columnCells, int rowCells)
    { // Tar cellen state från nya och lägger in i nuvarande.

        if (state == SimulationState.Running)
        {
            for (int y = 0; y < rowCells - 1; y++)
            {
                for (int x = 0; x < columnCells - 1; x++)
                {
                    Cell.CellState newState = cellNextMatrix[y][x].GetState();
                    cellCurentMatrix[y][x].SetState(newState);
                }
            }
        }

    }


} // END OF CLASS