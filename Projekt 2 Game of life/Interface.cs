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
    AdjustWindowSize,
    None,
}


public class ButtonTemplate // Knapp mallen 
{
    public int XStartPosition = 0;
    public int YStartPosition = 0;
    public string TextContent = "Reminder if you forget to change this!!";
    public int FontSize = Raylib.GetScreenHeight() / 30;
    Rectangle Rec;


    public int Create() // Skappa knappen med text. Bakgrunden/Storleken i x led beräknas genom att ta karaktärerna
    {   // I strängen och muliplicera med storleken vilket blir jätte konstigt med långa ord och borde tänkas om.
        int RecLenght = TextContent.Length * FontSize * 3/4;
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
    static int OverLineStartY = 0; // Positionen för linijernas höjd
    static int UnderLineStartY = 0;
    static int LineBeginingX = 0;
    static int LineEndingX = Raylib.GetScreenWidth();
    static int LineWidth = 12;

    public static void Draw() // RIIIIIITAR
    {
        int WindowHeight = Raylib.GetScreenHeight();
        int WindowWidth = Raylib.GetScreenWidth();
        OverLineStartY = Raylib.GetScreenHeight() / 6; // Kollas varge varv det ritas
        UnderLineStartY = Raylib.GetScreenHeight() * 5 / 6;

        // Skappar instncer av knapparna och linijerna som rittas ut.
        ButtonTemplate StartButton = new ButtonTemplate() { TextContent = "Start", XStartPosition = WindowWidth/8, YStartPosition = (WindowHeight/6) *5 + WindowHeight/24, };
        ButtonTemplate EndButton = new ButtonTemplate() { TextContent = "Stop", XStartPosition = (WindowWidth/4) + WindowWidth/32, YStartPosition = (WindowHeight/6) *5 + WindowHeight/24, };
        ButtonTemplate ClearButton = new ButtonTemplate() { TextContent = "Clear", XStartPosition = (WindowWidth/8)*3 + WindowWidth/32, YStartPosition = (WindowHeight/6) *5 + WindowHeight/24, };
        ButtonTemplate InstructionsButton = new ButtonTemplate() { TextContent = "Instructions", XStartPosition = (WindowWidth/8) * 5 + WindowWidth/16, YStartPosition = (WindowHeight/6) *5 + WindowHeight/60, }; 
        ButtonTemplate ChangeWindowSizeButton = new ButtonTemplate() { TextContent = "Change Window Size", XStartPosition = (WindowWidth/8) *5 + WindowWidth/80 + WindowWidth/160, YStartPosition = (WindowHeight/6) *5 + WindowHeight/12 + WindowHeight/150, };

        LineTemplate OverBoardLine = new LineTemplate() { LineStartX = 0, LineStartY = OverLineStartY, LineEndX = WindowWidth, LineEndY = OverLineStartY, LineWidth = LineWidth };
        LineTemplate UnderBoardLine = new LineTemplate() { LineStartX = 0, LineStartY = UnderLineStartY, LineEndX = WindowWidth, LineEndY = UnderLineStartY, LineWidth = LineWidth };


        Raylib.BeginDrawing(); // Ritt tiden =)

        Raylib.ClearBackground(Color.Gray);
        OverBoardLine.Create();
        UnderBoardLine.Create();
        int LenghtStartButton = StartButton.Create();
        int LenghtEndButton = EndButton.Create();   // Hämtar slut x på knapparna 
        int LenghtClearButton = ClearButton.Create();
        int LenghtInstructionsButton = InstructionsButton.Create();
        int LenghtChangeWindowSizeButton = ChangeWindowSizeButton.Create();
        Raylib.DrawText("The Game Of Life", WindowWidth/40, WindowHeight/30, (WindowWidth/40) *3, Color.Black);

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

            if (PressedPosition.X > ChangeWindowSizeButton.XStartPosition & PressedPosition.X < ChangeWindowSizeButton.XStartPosition + LenghtChangeWindowSizeButton & PressedPosition.Y > ChangeWindowSizeButton.YStartPosition & PressedPosition.Y < ChangeWindowSizeButton.YStartPosition + ChangeWindowSizeButton.FontSize * 2)
                UserInput = Choice.AdjustWindowSize;

            if (PressedPosition.Y > OverBoardLine.LineStartY + OverBoardLine.LineWidth / 2 & PressedPosition.Y < UnderBoardLine.LineStartY - OverBoardLine.LineWidth / 2)
                UserInput = Choice.BoardInput;

        }

    }


    public static void PromptInstructionWindow() // EN egen metod för att rita utt instruktions rutan.
    {   
        int WindowHeight = Raylib.GetScreenHeight();
        int WindowWidth = Raylib.GetScreenWidth();
        
        // Väldigt lång instruktion som jag inte orkade skriva själv ChatGPT =)
        string VeryLongTextContent = "Conway's Game of Life is a grid-based \nsimulation where cells live or die based \non simple rules. Each cell checks its 8 \nneighbors: It lives on with 2 or 3 neighbors,\nDies from loneliness or overcrowding,\nA dead cell is reborn with exactly 3 neighbors\nYou can click to toggle cells, press run\nto simulate, stop to stop simulating, and clear \nto clear the grid. There's no goal — just \npatterns that grow, move, or fade. It's a \nsandbox where complexity emerges \nfrom simplicity.";

        ButtonTemplate CloseInstructionsButton = new ButtonTemplate() { TextContent = "Close", XStartPosition = (WindowWidth/4)*3 + (WindowWidth/40) *3, YStartPosition = WindowHeight/15, };

        while (UserInput == Choice.Instructions) // Så länge användaren inte trykt på stäng knappen
        {
            Raylib.BeginDrawing(); // Rittar ut rutan med text och stäng knappen.

            Raylib.DrawRectangle(WindowWidth/16, WindowHeight/30, (WindowWidth/8)*7, (WindowHeight/6)*5, Color.LightGray);
            Rectangle r2 = new Rectangle(WindowWidth/16, WindowHeight/30, (WindowWidth/8)*7, (WindowHeight/6)*5);
            Raylib.DrawRectangleLinesEx(r2, 5, Color.Black);
            int LenghtCloseInstructionButton = CloseInstructionsButton.Create();
            Raylib.DrawText("Istructioner", (WindowWidth/40)*4, (WindowHeight/60)*4, WindowWidth/10, Color.White);
            Raylib.DrawText(VeryLongTextContent, (WindowWidth/40)*4, (WindowHeight/24)*5, WindowWidth/40 + WindowWidth/100, Color.White);

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







    public static bool EntetySizeRecalibration;
    public static bool Close = false;
    public static string StringUserInput = "";
    public static string ErrorMesage = "";

    public static void PromptAjdustingWindow()
    {
        int WindowHeight = Raylib.GetScreenHeight();
        int WindowWidth = Raylib.GetScreenWidth();

        Close = false;
        while (Close == false)
        {
            if (Raylib.WindowShouldClose()) // Gör så att man kan stänga fönstret 
            { // Gör så att den går ur whille loppen och sedan stänger.
                Close = true;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                Close = true; // Stänger fönstret
                StringUserInput = "";
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Enter)) // Enter
            { 
                ErrorMesage = AdjustSizeAfterInput(); // Tar strängen och fixar den sedan ändrar storlek på fönstret
                if (ErrorMesage == "")
                    Close = true;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Backspace)) // Baksteg
            { 
                if (StringUserInput.Length > 0)
                    StringUserInput = StringUserInput.Substring(0, StringUserInput.Length - 1);
            } // Skppar en sträng av strängen men kan ta emot ett länd arhument vilket gör att jag kan 
              // korta ner strängen vilket jag vill =)

            int key = Raylib.GetCharPressed();


            while (key > 0) // Om användare trök på något
            {
                if (key > 31 && key < 127) //Vanliga tecken
                    StringUserInput += (char)key; // Konverterar sifra till karaktär

                key = Raylib.GetCharPressed(); // Tar nästa tangent i kön om användaren tryker på en frame.
            }

            Raylib.BeginDrawing(); // Rittar ut rutan med användarens text/ userinput.

            Raylib.DrawRectangle(0, 0, WindowWidth, WindowHeight, Color.LightGray);
            Rectangle r2 = new Rectangle(0, 0, WindowWidth, WindowHeight);
            Raylib.DrawRectangleLinesEx(r2, 10, Color.Black);

            Raylib.DrawText("Här kan du ändra stroleken på skärmen genom att \nskriva den storleken som du vill ha i x och y led! \nDet finns bara två restriktioner. 1. Formatet för din \ninput måste se utt på deta sätt tex '800x800' med \nett x imellan bräden och höjden. 2. Skärmstorlekn få \ninte överstiga 1000x1000 eller understiga 400x400 \nanars får jag problem. Du kan skriva genom att tryka \npå tangenterna och ta bort med baksteg, för att \nsedan bekräfta/submita din storlek tryker du på \nEnter! ", 30, 100, 28, Color.White);
            Raylib.DrawText("Skärm Storleks Ändraren", (WindowWidth/80)*3, WindowHeight/20, (WindowWidth/160)*11, Color.White);
            Raylib.DrawText($"Enter size: {StringUserInput}", (WindowWidth/80)*3, (WindowHeight/6)*5, (WindowWidth/20), Color.White);
            Vector2 StartLinePos = new Vector2((WindowWidth/80)*3,  (WindowHeight/6)*5 + WindowWidth/15 - WindowWidth/100); Vector2 EndLinePos = new Vector2(WindowWidth/2 + (WindowWidth/40)*3, (WindowHeight/6)*5 + WindowWidth/15 - WindowWidth/100);
            Raylib.DrawLineEx(StartLinePos, EndLinePos, 5, Color.White);

            if (ErrorMesage.Length != 0) // Skriver bara utt om det finns något i.
            {
                Raylib.DrawText(ErrorMesage, (WindowWidth/80)*3, (WindowWidth/3)*2 + (WindowHeight/30), WindowWidth/40 + WindowWidth/100, Color.Red);
            }

            Raylib.EndDrawing();

        }
    }



    public static string AdjustSizeAfterInput()
    {
        
        StringUserInput = StringUserInput.Trim().ToLower(); // Tar bort mellanslag och sätter alla bokstäver som inte bör vara där och bör till des mindre form.

        // användare behöver skriva så här "800x600" !!!
        string[] InputPart = StringUserInput.Split('x'); // Dellar strängen där x förekommer och tar bort den av någon anledning jag trode först att den hamnade i någon av strängarna tills jag testade och kunnde inte hitta den vilket var lite roligt när jag funderade i typ en menut för att sedan lässa nogrant vad metoden gör och slå handflatan i ansiktet.

        if (InputPart.Length == 2)
        {
            if (int.TryParse(InputPart[0], out int NewWidth) && int.TryParse(InputPart[1], out int NewHeight))
            { // Try parsar användarens input, Uppmärksamma att jag använder TRYPARSE.
                if (NewWidth <= 1000 && NewWidth >= 400 && NewHeight <= 1000 && NewHeight >= 400)
                {
                    Raylib.SetWindowSize(NewWidth, NewHeight);
                    EntetySizeRecalibration = true;
                }
                else
                {
                    StringUserInput = ""; // sätter den till inget för nästa gång
                    return "För stora eller små x eller y parametrar. Testa \natt skriva en input inom de rekommenderade ramarna";
                }
            }
            else
            {
                StringUserInput = ""; // sätter den till inget för nästa gång
                return "Skärmstorleken tar bara emot sifror i din input, \nframför och bakom ditt 'x'";
            }
        }
        else
        {
            StringUserInput = ""; // sätter den till inget för nästa gång
            return "Du kan bara använda dig av 1 x i din input och \nden måste vara mellan höden och längden";
        }

        StringUserInput = ""; // sätter den till inget för nästa gång
        return "";
    }



    public static Choice GetChoice()
    {
        return UserInput;
    }

    public static void SetChoice(Choice InChoice)
    {
        UserInput = InChoice;
    }

    public static Vector2 GetMousePosition()
    {
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
