using System;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public class Cell
{

    public static int Size = 12;
    public enum CellState
    {
        Dead,
        Alive
    }
    CellState State = CellState.Dead;

    float Xposition;
    float YPosition;

    public static int Space = 14;








    public static int GetSize()
    {
        return Size;
    }

    public CellState GetState()
    {
        return State;
    }

    public void SetState(CellState NewState)
    {
        State = NewState;
    }

    public void SetSize(int NewSize)
    {
        Size = NewSize;
    }

    public void SetXPos(float NewPos)
    {
        Xposition = NewPos;
    }

    public void SetYPos(float NewPos)
    {
        YPosition = NewPos;
    }

    public float GetXPos()
    {
        return Xposition;
    }

    public float GetYPos()
    {
        return YPosition;
    }

}
