using System.Numerics;
namespace Projekt_2_Game_of_life;


public enum SimulationState{
    Running,
    Stop,
    None,
}


public class Game_Logic
{
    static SimulationState State = SimulationState.None;

    public static SimulationState EvaluateUserInput(Choice UserInput, Vector2 MousePressedPosition, out bool CreateOnce)
    { // tar in vart & vad användaren trök på och utför korisponderande resultat.
        CreateOnce = false;

        switch (UserInput)
        {
            case Choice.BoardInput:
                if (State != SimulationState.Running){
                    ChangeCellState(MousePressedPosition); // Ändrar cellens state på musens positions.
                }
                break;
            case Choice.Start:
                State = SimulationState.Running; // ändrar state så när vi faller ur till den större loopen så körs simulationen.
                break;
            case Choice.Stop: 
                State = SimulationState.Stop; // Stoppar simulationen om den körs.
                break;
            case Choice.Clear:
                ClearBoard(); // Sätter alla celler till döda
                break;
            case Choice.Instructions:
                Interface.PromptInstructionWindow(); // ritar upp ett fönster med instructioner.
                if (Interface.EntetySizeRecalibration == true){
                    CreateOnce = true;
                }
                break;
            case Choice.AdjustWindowSize:
                Interface.PromptAjdustingWindow();
            break;
        }
        return State;
    }


    public static void ChangeCellState(Vector2 MousePressedPosition)
    { // komplicerad metod som kollar på varge cell i den 2d listan och jämför muspositionen om den är 
        // inanför cellens ramar och sedan sätter dens state till motsatsen av vad den redan är.
        foreach (List<Cell> List in Board.CellCurenMatrix)
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



    public static void ClearBoard() // går igenom varge cell och sätter dem till döda.
    {

        foreach (List<Cell> List in Board.CellCurenMatrix)
        {
            foreach (Cell Instence in List)
            {
                Instence.SetState(Cell.CellState.Dead);
            }
        }
    }






} // END OF CLASS






