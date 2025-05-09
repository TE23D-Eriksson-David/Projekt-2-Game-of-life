namespace Projekt_2_Game_of_life;

public class Cell
{

    public static int space = 14;
    public static int size = 12;
    float xPosition;
    float yPosition;



    public enum CellState
    {
        Dead,
        Alive
    }

    CellState state = CellState.Dead;






    public static int GetSize()
    {
        return size;
    }

    public CellState GetState()
    {
        return state;
    }

    public void SetState(CellState newState)
    {
        state = newState; // Sätter om den lever eller om den är död.
    }

    public static void SetSize(int newSize)
    {
        size = newSize;
    }

    public void SetXPos(float newPos)
    {
        xPosition = newPos;
    }

    public void SetYPos(float newPos)
    {
        yPosition = newPos;
    }

    public float GetXPos()
    {
        return xPosition;
    }

    public float GetYPos()
    {
        return yPosition;
    }

} // END OF CLASS