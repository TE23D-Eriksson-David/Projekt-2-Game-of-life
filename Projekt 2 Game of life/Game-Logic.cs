using System.Numerics;
namespace Projekt_2_Game_of_life;


public enum SimulationState
{
    Running,
    Stop,
    None,
}


public class Game_Logic
{

    public static SimulationState EvaluateUserInput(SimulationState state, Choice userInput, Vector2 mousePressedPosition, out bool createOnce, out Choice newUserInput, List<List<Cell>> cellCurentMatrix, List<List<Cell>> cellNextMatrix)
    { // tar in vart & vad användaren trök på och utför korisponderande resultat.
        createOnce = false;
        bool entetySizeRecalibration = false;

        switch (userInput)
        {
            case Choice.BoardInput:
                if (state != SimulationState.Running)
                {
                    ChangeCellState(mousePressedPosition, cellCurentMatrix); // Ändrar cellens state på musens positions.
                }
                break;
            case Choice.Start:
                state = SimulationState.Running; // ändrar state så när vi faller ur till den större loopen så körs simulationen.
                break;
            case Choice.Stop:
                state = SimulationState.Stop; // Stoppar simulationen om den körs.
                break;
            case Choice.Clear:
                ClearBoard(cellCurentMatrix); // Sätter alla celler till döda
                break;
            case Choice.Instructions:
                userInput = Interface.PromptInstructionWindow(userInput); // ritar upp ett fönster med instructioner.
                break;
            case Choice.AdjustWindowSize:
                entetySizeRecalibration = Interface.PromptAjdustingWindow(entetySizeRecalibration);
                if (entetySizeRecalibration == true)
                {
                    createOnce = true;
                }
                break;
        }
        newUserInput = userInput;
        return state;
    }


    public static void ChangeCellState(Vector2 mousePressedPosition, List<List<Cell>> cellCurentMatrix)
    { // komplicerad metod som kollar på varge cell i den 2d listan och jämför muspositionen om den är 
        // inanför cellens ramar och sedan sätter dens state till motsatsen av vad den redan är.
        foreach (List<Cell> list in cellCurentMatrix)
        {
            foreach (Cell instence in list)
            {
                Cell.CellState state = instence.GetState();
                float startX = instence.GetXPos();
                float startY = instence.GetYPos();

                if (startX <= mousePressedPosition.X & startX + Cell.size >= mousePressedPosition.X)
                {
                    if (mousePressedPosition.Y >= startY & mousePressedPosition.Y <= startY + Cell.size)
                    {
                        switch (state)
                        {
                            case Cell.CellState.Alive:
                                instence.SetState(Cell.CellState.Dead);
                                break;
                            default:
                                instence.SetState(Cell.CellState.Alive);
                                break;
                        }
                    }
                }
            }
        }
    }



    public static void ClearBoard(List<List<Cell>> cellCurentMatrix) // går igenom varge cell och sätter dem till döda.
    {

        foreach (List<Cell> list in cellCurentMatrix)
        {
            foreach (Cell instence in list)
            {
                instence.SetState(Cell.CellState.Dead);
            }
        }
    }






} // END OF CLASS