using System;
using System.Numerics;
using SC = System.Console;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Board
{


    public static void Draw(Vector2 BoardSizeX, Vector2 BoardSizeY, int CellStorlek)
    {

        int CellMellanrum = 14;

        Rectangle r1 = new Rectangle(0, 0, CellStorlek, CellStorlek);

                for (r1.Y = BoardSizeY.X; r1.Y < BoardSizeY.Y; r1.Y += CellMellanrum)
        {
            for (r1.X = BoardSizeX.X; r1.X < BoardSizeX.Y-CellMellanrum; r1.X += CellMellanrum) // BoardSizeX.Y
            {
                Raylib.DrawRectangleRec(r1, Color.White);
            }
        }

    }


}
