using SC = System.Console;
namespace Projekt_2_Game_of_life;



public class Simulation_Rules
{

    public static int AliveCloseCells;
    public static Cell.CellState CurentCellState;

    public static void Run(SimulationState Condition)
    {
        
        if (Condition == SimulationState.Runing)
        {

            for (int Listor = 0; Listor < Board.RowCells; Listor++) // Antalet rader i matrisen, vertikalt
            {
                for (int Celler = 0; Celler < Board.ColumnCells; Celler++) // antalet celler i en lista, horisontelt
                {

                    AliveCloseCells = 0;

                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {

                            if (y == 0 && x == 0)
                                continue;


                            if (Listor + y <= Board.RowCells-1 && Listor + y >= 0)
                            {
                                if (Celler + x <= Board.ColumnCells-1 && Celler + x >= 0){
                                    CurentCellState = Board.CellCurenMatrix[Listor + y][Celler + x].GetState();
                                
                                if (CurentCellState == Cell.CellState.Alive)
                                AliveCloseCells++;
                                }
                            }

                        }
                    }

                    // SC.WriteLine(AliveCloseCells+":" +Celler);

                    CurentCellState = Board.CellCurenMatrix[Listor][Celler].GetState();


                    if (AliveCloseCells <= 1)
                        Board.CellNextMatrix[Listor][Celler].SetState(Cell.CellState.Dead);


                    if (AliveCloseCells >= 2 && AliveCloseCells < 4 && CurentCellState == Cell.CellState.Alive)
                        Board.CellNextMatrix[Listor][Celler].SetState(Cell.CellState.Alive);


                    if (AliveCloseCells >= 4)
                        Board.CellNextMatrix[Listor][Celler].SetState(Cell.CellState.Dead);


                    if (AliveCloseCells >= 3 && CurentCellState == Cell.CellState.Dead)
                        Board.CellNextMatrix[Listor][Celler].SetState(Cell.CellState.Alive);


                }
                // SC.WriteLine("R");
            }
        }
    }



} // END
