using System.Numerics;
namespace Projekt_2_Game_of_life;


public enum SimulationState{
    Running,
    Stop,
    None,
}


public class Game_Logic
{
    static SimulationState state = SimulationState.None;

    public static SimulationState EvaluateUserInput(Choice UserInput, Vector2 mousePressedPosition, out bool createOnce)
    { // tar in vart & vad användaren trök på och utför korisponderande resultat.
        createOnce = false;

        switch (UserInput)
        {
            case Choice.BoardInput:
                if (state != SimulationState.Running){
                    ChangeCellState(mousePressedPosition); // Ändrar cellens state på musens positions.
                }
                break;
            case Choice.Start:
                state = SimulationState.Running; // ändrar state så när vi faller ur till den större loopen så körs simulationen.
                break;
            case Choice.Stop: 
                state = SimulationState.Stop; // Stoppar simulationen om den körs.
                break;
            case Choice.Clear:
                ClearBoard(); // Sätter alla celler till döda
                break;
            case Choice.Instructions:
            Interface.PromptInstructionWindow(); // ritar upp ett fönster med instructioner.
                break;
            case Choice.AdjustWindowSize:
                Interface.PromptAjdustingWindow();
                if ( Interface.entetySizeRecalibration == true){
                    createOnce = true;
                }
            break;
        }
        return state;
    }


    public static void ChangeCellState(Vector2 mousePressedPosition)
    { // komplicerad metod som kollar på varge cell i den 2d listan och jämför muspositionen om den är 
        // inanför cellens ramar och sedan sätter dens state till motsatsen av vad den redan är.
        foreach (List<Cell> List in Board.CellCurenMatrix)
        {
            foreach (Cell Instence in List)
            {
                Cell.CellState state = Instence.GetState();
                float startX = Instence.GetXPos();
                float startY = Instence.GetYPos();

                if (startX <= mousePressedPosition.X & startX + Cell.size >= mousePressedPosition.X)
                {
                    if (mousePressedPosition.Y >= startY & mousePressedPosition.Y <= startY + Cell.size)
                    {
                        switch (state)
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
