using SC = System.Console;
using System.Numerics;

namespace Projekt_2_Game_of_life;

public class Game_Logic
{


    public static void EvaluateUserImput(Choice UserInput, Vector2 MousePressedPosition)
    {

        switch (UserInput)
        {
            case Choice.BoardInput:
                SC.WriteLine("BoardIn");
                ChangeCellState(MousePressedPosition);
                break;
            case Choice.Start:
                Simulation_Rules.Run();
                break;
            case Choice.Stop:
                // Stop simulation
                break;
            case Choice.Clear:
                ClearBoard();
                break;
            case Choice.Instructions:
                // skappa en rutta som täker skärmen och inaktiverar alla knappar, förutom den för att stänga istructionerna.
                break;
        }
    }


    public static void ChangeCellState(Vector2 MousePressedPosition)
    {

        foreach (List<Cell> List in Board.CellMatrix)
        {
            foreach (Cell Instence in List)
            {
                Cell.CellState State = Instence.GetState();
                float StartX = Instence.GetXPos();
                float StartY = Instence.GetYPos();

                if (StartX <= MousePressedPosition.X & StartX + Cell.Size >= MousePressedPosition.X)
                {
                    if (MousePressedPosition.Y >= StartY & MousePressedPosition.Y <= StartY + Cell.Size)
                    {
                        switch (State)
                        {
                            case Cell.CellState.Alive:
                                Instence.SetState(Cell.CellState.Dead);
                                break;
                            default:
                                Instence.SetState(Cell.CellState.Alive);
                                break;
                        }
                    }
                }
            }
        }
    }



    public static void ClearBoard()
    {

        foreach (List<Cell> List in Board.CellMatrix)
        {
            foreach (Cell Instence in List)
            {
                Instence.SetState(Cell.CellState.Dead);
            }
        }
    }






} // END OF CLASS






