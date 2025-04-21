using SC = System.Console;
using System.Numerics;
using Raylib_cs;

namespace Projekt_2_Game_of_life;

public enum Choice // Velen/knapparna som spelaren kan tryka.
{
    Start,
    Stop,
    Clear,
    Instructions,
    CloseInstructions,
    BoardInput,
    None,
}


public class ButtonTemplate // Knapp mallen 
{
    public int XStartPosition = 1;
    public int YStartPosition = 2;
    public string TextContent = "Reminder if you forget to change this!!";
    public int FontSize = Raylib.GetScreenHeight() / 30;
    Rectangle Rec;


    public int Create() // Skappa knappen med text. Bakgrunden/Storleken i x led beräknas genom att ta karaktärerna
    {   // I strängen och muliplicera med storleken vilket blir jätte konstigt med långa ord och borde tänkas om.
        int RecLenght = TextContent.Length * FontSize;
        Rec = new Rectangle(XStartPosition, YStartPosition, RecLenght, FontSize * 2);
        Raylib.DrawRectangleRec(Rec, Color.Blue);
        Raylib.DrawText(TextContent, XStartPosition + FontSize / 2, YStartPosition + FontSize / 2, FontSize, Color.Black);
        return RecLenght;
    }
}

public class LineTemplate // Linije mallen vilket är ganska onödig 
{
    public int LineWidth = 20;
    public int LineEndY = 0;
    public int LineStartX = 0;
    public int LineStartY = 0;
    public int LineEndX = 0;

    Vector2 LineStart = new Vector2(0, 0);
    Vector2 LineStop = new Vector2(0, 0);
    public void Create()
    {
        LineStop.Y = LineEndY;
        LineStop.X = LineEndX;
        LineStart.Y = LineStartY;
        LineStart.X = LineStartX;
        Raylib.DrawLineEx(LineStart, LineStop, LineWidth, Color.Black);
    }
}




public class Interface // Hanterar vad som ska ritas utt på fönstret och användarens input.
{

    static Vector2 PressedPosition;
    static Choice UserInput = Choice.None;
    static int OverLineStartY = Raylib.GetScreenHeight() / 6; // Positionen för linijernas höjd
    static int UnderLineStartY = Raylib.GetScreenHeight() * 5 / 6;
    static int LineBeginingX = 0;
    static int LineEndingX = Raylib.GetScreenWidth();
    static int LineWidth = 12;

    public static void Draw() // RIIIIIITAR
    {
        // Skappar instncer av knapparna och linijerna som rittas ut.
        ButtonTemplate StartButton = new ButtonTemplate() { TextContent = "Start", XStartPosition = 100, YStartPosition = 525, };
        ButtonTemplate EndButton = new ButtonTemplate() { TextContent = "Stop", XStartPosition = 250, YStartPosition = 525, };
        ButtonTemplate ClearButton = new ButtonTemplate() { TextContent = "Clear", XStartPosition = 400, YStartPosition = 525, };
        ButtonTemplate InstructionsButton = new ButtonTemplate() { TextContent = "Instructions", XStartPosition = 550, YStartPosition = 525, };
        LineTemplate OverBoardLine = new LineTemplate() { LineStartX = 0, LineStartY = OverLineStartY, LineEndX = Raylib.GetScreenWidth(), LineEndY = OverLineStartY, LineWidth = LineWidth };
        LineTemplate UnderBoardLine = new LineTemplate() { LineStartX = 0, LineStartY = UnderLineStartY, LineEndX = Raylib.GetScreenWidth(), LineEndY = 500, LineWidth = LineWidth };


        Raylib.BeginDrawing(); // Ritt tiden =)

        Raylib.ClearBackground(Color.Gray);
        OverBoardLine.Create();
        UnderBoardLine.Create();
        int LenghtStartButton = StartButton.Create();
        int LenghtEndButton = EndButton.Create();   // Hämtar slut x på knapparna 
        int LenghtClearButton = ClearButton.Create();
        int LenghtInstructionsButton = InstructionsButton.Create();
        Raylib.DrawText("The Game Of Life", 20, 20, 60, Color.Black);

        Raylib.EndDrawing(); // Slut på ritt tiden =( 


        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) // kollar om mussen tryks ner sedan vart
        {   // Om den är inom en av områderna nedan så ändras enumet för anvädarens val.
            PressedPosition = Raylib.GetMousePosition();

            if (PressedPosition.X > StartButton.XStartPosition & PressedPosition.X < StartButton.XStartPosition + LenghtStartButton & PressedPosition.Y > StartButton.YStartPosition & PressedPosition.Y < StartButton.YStartPosition + StartButton.FontSize * 2)
                UserInput = Choice.Start;

            if (PressedPosition.X > EndButton.XStartPosition & PressedPosition.X < EndButton.XStartPosition + LenghtEndButton & PressedPosition.Y > EndButton.YStartPosition & PressedPosition.Y < EndButton.YStartPosition + EndButton.FontSize * 2)
                UserInput = Choice.Stop;

            if (PressedPosition.X > ClearButton.XStartPosition & PressedPosition.X < ClearButton.XStartPosition + LenghtClearButton & PressedPosition.Y > ClearButton.YStartPosition & PressedPosition.Y < ClearButton.YStartPosition + ClearButton.FontSize * 2)
                UserInput = Choice.Clear;

            if (PressedPosition.X > InstructionsButton.XStartPosition & PressedPosition.X < InstructionsButton.XStartPosition + LenghtInstructionsButton & PressedPosition.Y > InstructionsButton.YStartPosition & PressedPosition.Y < InstructionsButton.YStartPosition + InstructionsButton.FontSize * 2)
                UserInput = Choice.Instructions;

            if (PressedPosition.Y > OverBoardLine.LineStartY + OverBoardLine.LineWidth / 2 & PressedPosition.Y < UnderBoardLine.LineStartY - OverBoardLine.LineWidth / 2)
                UserInput = Choice.BoardInput;

        }

    }


    public static void PromptInstructionWindow() // EN egen metod för att rita utt instruktions rutan.
    {   // Väldigt lång instruktion som jag inte orkade skriva själv ChatGPT =)
        string VeryLongTextContent = "Conway's Game of Life is a grid-based \nsimulation where cells live or die based \non simple rules. Each cell checks its 8 \nneighbors: It lives on with 2 or 3 neighbors,\nDies from loneliness or overcrowding,\nA dead cell is reborn with exactly 3 neighbors\nYou can click to toggle cells, press run\nto simulate, stop to stop simulating, and clear to \nclear the grid. There's no goal — just \npatterns that grow, move, or fade. It's a \nsandbox where complexity emerges \nfrom simplicity.";

        ButtonTemplate CloseInstructionsButton = new ButtonTemplate() { TextContent = "Close", XStartPosition = 640, YStartPosition = 60, };

        while (UserInput == Choice.Instructions) // Så länge användaren inte trykt på stäng knappen
        {
            Raylib.BeginDrawing(); // Rittar ut rutan med text och stäng knappen.

                Raylib.DrawRectangle(50, 50, 700, 500, Color.LightGray);
                Rectangle r2 = new Rectangle(50, 50, 700, 500);
                Raylib.DrawRectangleLinesEx(r2, 5, Color.Black);
                int LenghtCloseInstructionButton = CloseInstructionsButton.Create();
                Raylib.DrawText("Istructioner", 80, 70, 40, Color.White);
                Raylib.DrawText(VeryLongTextContent, 80, 125, 28, Color.White);

            Raylib.EndDrawing();

            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) // Kollar om musen är ned trykt sedan om knappen tryktes.
            {
                PressedPosition = Raylib.GetMousePosition();
                if (PressedPosition.X > CloseInstructionsButton.XStartPosition & PressedPosition.X < CloseInstructionsButton.XStartPosition + LenghtCloseInstructionButton & PressedPosition.Y > CloseInstructionsButton.YStartPosition & PressedPosition.Y < CloseInstructionsButton.YStartPosition + CloseInstructionsButton.FontSize * 2)
                    UserInput = Choice.CloseInstructions;
            }

            if (Raylib.WindowShouldClose()) // Gör så att man kan stänga fönstret 
            { // Gör så att den går ur whille loppen och sedan stänger.
                UserInput = Choice.CloseInstructions;
            }

        }


    }


    public static Choice GetChoice(){
        return UserInput;
    }

    public static void SetChoice(Choice InChoice){
        UserInput = InChoice;
    }

    public static Vector2 GetMousePosition(){
        return PressedPosition;
    }

    public static void GetBoardDimentions(out Vector2 YSize, out Vector2 XSize)
    {
        YSize.X = OverLineStartY + LineWidth / 2;
        YSize.Y = UnderLineStartY - LineWidth / 2;

        XSize.X = LineBeginingX;
        XSize.Y = LineEndingX;
    }

}
