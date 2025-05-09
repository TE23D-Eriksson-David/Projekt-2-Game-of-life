namespace Projekt_2_Game_of_life;


public class Simulation_Rules
{

    public static void Run(SimulationState condition, List<List<Cell>> cellCurentMatrix, List<List<Cell>> cellNextMatrix, int columnCells, int rowCells)
    { // När simulationen körs så kollas det runt alla celler effter hur många grannar som lever genom två for loopar
        // i varandra, sedan om antalet uppfyller någon av fallen nedan så uppdateras cellens state beroende på det.
        if (condition == SimulationState.Running)
        {
            int aliveCloseCells;
            Cell.CellState currentCellState;

            for (int lists = 0; lists < rowCells; lists++) // Antalet rader i matrisen, vertikalt
            {
                for (int cells = 0; cells < columnCells; cells++) // antalet Cells i en lista, horisontelt
                {

                    aliveCloseCells = 0;

                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++) // Går igenom cellerna runt om
                        {

                            if (y == 0 && x == 0) // hoppar sig själv
                                continue;


                            if (lists + y <= rowCells - 1 && lists + y >= 0)
                            { // Gör så att det inte går uttanför listan
                                if (cells + x <= columnCells - 1 && cells + x >= 0)
                                {
                                    currentCellState = cellCurentMatrix[lists + y][cells + x].GetState();
                                    // Hämtar cellens state
                                    if (currentCellState == Cell.CellState.Alive)
                                        aliveCloseCells++; // Om den lever, läg den på räknaren
                                }
                            }

                        }
                    }
                    // Hämtar statet av cellen i mitten 
                    currentCellState = cellCurentMatrix[lists][cells].GetState();

                    // Alla regler som påverkar cellen
                    if (aliveCloseCells <= 1) // Ensam då dör den
                        cellNextMatrix[lists][cells].SetState(Cell.CellState.Dead);

                    // Om den ska fortsäta leva
                    if (aliveCloseCells >= 2 && aliveCloseCells < 4 && currentCellState == Cell.CellState.Alive)
                        cellNextMatrix[lists][cells].SetState(Cell.CellState.Alive);

                    // Dör om det är förmånga runt den
                    if (aliveCloseCells >= 4)
                        cellNextMatrix[lists][cells].SetState(Cell.CellState.Dead);

                    // Återuppstår om den är tre runt om den 
                    if (aliveCloseCells == 3 && currentCellState == Cell.CellState.Dead)
                        cellNextMatrix[lists][cells].SetState(Cell.CellState.Alive);


                }
            }
        }
    }


} // END OF CLASS