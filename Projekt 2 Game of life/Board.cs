using System;
using System.Numerics;
using SC = System.Console;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Board
{

    public static List<List<Cell>> CellCurenMatrix = new List<List<Cell>>();
    public static List<List<Cell>> CellNextMatrix = new List<List<Cell>>();
    public static int ColumnCells = 0;
    public static int RowCells = 0;





    public static void DrawMatrix()
    {

        int CellSize = Cell.GetSize();
        Rectangle r1 = new Rectangle(0, 0, CellSize, CellSize);

        foreach (List<Cell> List in CellCurenMatrix)
        {
            foreach (Cell Instence in List)
            {

                Cell.CellState State = Instence.GetState();
                r1.X = Instence.GetXPos();
                r1.Y = Instence.GetYPos();
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

                Raylib.DrawRectangleRec(r1, CellCollor);

            }
        }

    }



    public static void CreateMatrix(Vector2 BoardSizeX, Vector2 BoardSizeY, int CellSize)
    {
        int CellSpace = Cell.Space;
        Rectangle r1 = new Rectangle(0, 0, CellSize, CellSize);

        for (r1.Y = BoardSizeY.X; r1.Y < BoardSizeY.Y; r1.Y += CellSpace)
        {
            CellCurenMatrix.Add(new List<Cell>());
            CellNextMatrix.Add(new List<Cell>());
            ColumnCells = 0;
            for (r1.X = BoardSizeX.X; r1.X < BoardSizeX.Y - CellSpace; r1.X += CellSpace)
            {
                Cell CurentMatrixCell = new Cell();
                CurentMatrixCell.SetYPos(r1.Y);
                CurentMatrixCell.SetXPos(r1.X);
                CellCurenMatrix[RowCells].Add(CurentMatrixCell);

                Cell NextMatrixCell = new Cell();
                NextMatrixCell.SetYPos(r1.Y);
                NextMatrixCell.SetXPos(r1.X);
                CellNextMatrix[RowCells].Add(NextMatrixCell);

                ColumnCells++;  // räknar rader i x led  
            }
            RowCells++; // räknar rader i Y led    
        }

        SC.WriteLine(ColumnCells + "," + RowCells); //  Ska TAS BORT!!

    }





    public static void DrawNextMatrix(SimulationState State)
    {
        if (State == SimulationState.Runing)
        {

            int CellSize = Cell.GetSize();
            Rectangle r1 = new Rectangle(0, 0, CellSize, CellSize);

            foreach (List<Cell> List in CellNextMatrix)
            {
                foreach (Cell Instence in List)
                {

                    Cell.CellState Stat = Instence.GetState();
                    r1.X = Instence.GetXPos();
                    r1.Y = Instence.GetYPos();
                    Color CellCollor = Color.DarkGreen;

                    switch (Stat)
                    {
                        case Cell.CellState.Alive:
                            CellCollor = Color.White;
                            break;
                        case Cell.CellState.Dead:
                            CellCollor = Color.Beige;
                            break;
                    }

                    Raylib.DrawRectangleRec(r1, CellCollor);

                }
            }
        }


    }


    public static void UpdateMatrix(SimulationState State)
    {

        if (State == SimulationState.Runing)
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



}








