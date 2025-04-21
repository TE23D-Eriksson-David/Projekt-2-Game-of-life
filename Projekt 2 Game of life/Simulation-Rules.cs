namespace Projekt_2_Game_of_life;


public class Simulation_Rules
{

    public static int AliveCloseCells;
    public static Cell.CellState CurrentCellState;

    public static void Run(SimulationState Condition) 
    { // När simulationen körs så kollas det runt alla celler effter hur många grannar som lever genom två for loopar
        // i varandra, sedan om antalet uppfyller någon av fallen nedan så uppdateras cellens state beroende på det.
        if (Condition == SimulationState.Running)
        {

            for (int Lists = 0; Lists < Board.RowCells; Lists++) // Antalet rader i matrisen, vertikalt
            {
                for (int Cells = 0; Cells < Board.ColumnCells; Cells++) // antalet Cells i en lista, horisontelt
                {

                    AliveCloseCells = 0;

                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++) // Går igenom cellerna runt om
                        {

                            if (y == 0 && x == 0) // hoppar sig själv
                                continue;


                            if (Lists + y <= Board.RowCells - 1 && Lists + y >= 0) 
                            { // Gör så att det inte går uttanför listan
                                if (Cells + x <= Board.ColumnCells - 1 && Cells + x >= 0)
                                {
                                    CurrentCellState = Board.CellCurenMatrix[Lists + y][Cells + x].GetState();
                                    // Hämtar cellens state
                                    if (CurrentCellState == Cell.CellState.Alive)
                                        AliveCloseCells++; // Om den lever, läg den på räknaren
                                }
                            }

                        }
                    }
                    // Hämtar statet av cellen i mitten 
                    CurrentCellState = Board.CellCurenMatrix[Lists][Cells].GetState();
                    
                    // Alla regler som påverkar cellen
                    if (AliveCloseCells <= 1) // Ensam då dör den
                        Board.CellNextMatrix[Lists][Cells].SetState(Cell.CellState.Dead);

                    // Om den ska fortsäta leva
                    if (AliveCloseCells >= 2 && AliveCloseCells < 4 && CurrentCellState == Cell.CellState.Alive)
                        Board.CellNextMatrix[Lists][Cells].SetState(Cell.CellState.Alive);

                    // Dör om det är förmånga runt den
                    if (AliveCloseCells >= 4)
                        Board.CellNextMatrix[Lists][Cells].SetState(Cell.CellState.Dead);

                    // Återuppstår om den är tre runt om den 
                    if (AliveCloseCells == 3 && CurrentCellState == Cell.CellState.Dead)
                        Board.CellNextMatrix[Lists][Cells].SetState(Cell.CellState.Alive);


                }
            }
        }
    }





} // END OF CLASS
