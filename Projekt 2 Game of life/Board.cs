using System;
using System.Numerics;
using SC = System.Console;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Board
{

    public static List<List<Cell>> CellMatrix = new List<List<Cell>>();
    public static int ColumnCells = 0;
    public static int RowCells;


    // public static void Draw(Vector2 BoardSizeX, Vector2 BoardSizeY, int CellSize)
    // {

    //     int CellSpace = 14;
    //     Rectangle r1 = new Rectangle(0, 0, CellSize, CellSize);

    //     for (r1.Y = BoardSizeY.X; r1.Y < BoardSizeY.Y; r1.Y += CellSpace)
    //     {
    //         RowCells = 0;
    //         for (r1.X = BoardSizeX.X; r1.X < BoardSizeX.Y-CellSpace; r1.X += CellSpace) 
    //         {
    //             Raylib.DrawRectangleRec(r1, Color.White);
    //             RowCells++;
    //         }
    //         ColumnCells++;
    //     }
    // }




        public static void CerateMatrix(Vector2 BoardSizeX, Vector2 BoardSizeY, int CellSize)
    {
        int CellSpace = 14;
        int CellRows = 0;
        Rectangle r1 = new Rectangle(0, 0, CellSize, CellSize);

        for (r1.Y = BoardSizeY.X; r1.Y < BoardSizeY.Y; r1.Y += CellSpace)
        {
            CellMatrix.Add(new List<Cell>());
            ColumnCells = 0;
            for (r1.X = BoardSizeX.X; r1.X < BoardSizeX.Y-CellSpace; r1.X += CellSpace) 
            {
                Cell NewCell = new Cell();
                NewCell.SetYPos(r1.Y);
                NewCell.SetXPos(r1.X);
                CellMatrix[CellRows].Add(NewCell);

                ColumnCells++;  // räknar rader i x led
            }
            CellRows++; // räknar rader i Y led
        }

        SC.WriteLine(ColumnCells +","+CellRows);

    }


    //     foreach(List<Cell> List in CellMatrix){
    //         foreach (Cell Instnce in List)
    //         {
              
    //         }
    //     }


}
