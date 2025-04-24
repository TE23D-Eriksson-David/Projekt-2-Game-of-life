using System.Numerics;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Board
{
    // Min aledning för varför jag använder listor istället för arrayer är för att jag tror man inte kan skappa en
    // 2D array med obejekt i sig. Jag kunde kanshe ha gjort det med en array men när jag skrev min metod tänkte jag 
    // att jag behövde skappa lägga in cellerna när jag skappade dem och de var så längden av listan bestämdes vilket
    // Inte skulle fungerat med en array.
    public static List<List<Cell>> CellCurenMatrix = new List<List<Cell>>(); // Mina två dimetionela listor
    public static List<List<Cell>> CellNextMatrix = new List<List<Cell>>(); 
    public static int ColumnCells = 0;
    public static int RowCells = 0;



    public static void DrawMatrix() // Ritar ut brädet inom de två svarta linijerna på interfacet.
    {

        int CellSize = Cell.GetSize(); // Cellernas storlek
        Rectangle RectangleTemplate = new Rectangle(0, 0, CellSize, CellSize);

        foreach (List<Cell> List in CellCurenMatrix)
        {
            foreach (Cell Instence in List) // Deta görs för varge cell
            {
                Cell.CellState State = Instence.GetState();
                RectangleTemplate.X = Instence.GetXPos(); // Hämtar positionen av cellen
                RectangleTemplate.Y = Instence.GetYPos();
                Color CellCollor = Color.DarkGreen;

                switch (State)
                {
                    case Cell.CellState.Alive:
                        CellCollor = Color.White;
                        break;
                    case Cell.CellState.Dead:
                        CellCollor = Color.Beige;
                        break;
                }

                Raylib.DrawRectangleRec(RectangleTemplate, CellCollor); // Ritar ut min cell

            }
        }

    }

 

    public static void CreateMatrix(Vector2 BoardSizeX, Vector2 BoardSizeY, int CellSize)
    { // Skappar mina celler och lägger in dem i mina två listor.
        int CellSpace = Cell.Space; // mellanrumet mellan cellerna.
        Rectangle RectangleTemplate = new Rectangle(0, 0, CellSize, CellSize);
        CellCurenMatrix.Clear(); // Om listan redan är full så måste den tömas anars problem.
        CellNextMatrix.Clear(); // Deta görs för att skärmstorleken kan ändras.
        ColumnCells = 0;
        RowCells = 0;

        for (RectangleTemplate.Y = BoardSizeY.X; RectangleTemplate.Y < BoardSizeY.Y; RectangleTemplate.Y += CellSpace)
        { // Inom ramarna för det två linijerna i yled, görs till en cell som skappas har des slut y position utanför den svarat linijen.
            CellCurenMatrix.Add(new List<Cell>()); // Läggs till en lista med cell obejekt som data typ.
            CellNextMatrix.Add(new List<Cell>());
            ColumnCells = 0; // Räknare för rader x led som sätts till 0

            for (RectangleTemplate.X = BoardSizeX.X; RectangleTemplate.X < BoardSizeX.Y - CellSpace; RectangleTemplate.X += CellSpace)
            {  // räknar i x led och lägger till cell storleken och mellanrumet för varge varv och samma som i den övre loppen.
                Cell CurentMatrixCell = new Cell(); 
                CurentMatrixCell.SetYPos(RectangleTemplate.Y);
                CurentMatrixCell.SetXPos(RectangleTemplate.X);
                CellCurenMatrix[RowCells].Add(CurentMatrixCell);
                // Skppar en cell för varge lista och lägger till nuvarande position i dem. Läger in dem i listan.
                Cell NextMatrixCell = new Cell();
                NextMatrixCell.SetYPos(RectangleTemplate.Y);
                NextMatrixCell.SetXPos(RectangleTemplate.X);
                CellNextMatrix[RowCells].Add(NextMatrixCell);

                ColumnCells++;  // räknar rader i x led  
            }
            RowCells++; // räknar rader i Y led    
        }
    }


    public static void UpdateMatrix(SimulationState State) 
    { // Tar cellen state från nya och lägger in i nuvarande.

        if (State == SimulationState.Running)
        {
            for (int y = 0; y < RowCells - 1; y++)
            {
                for (int x = 0; x < ColumnCells - 1; x++)
                {
                    Cell.CellState state = CellNextMatrix[y][x].GetState();
                    CellCurenMatrix[y][x].SetState(state);
                }
            }
        }

    }



} // END OF CLASS








