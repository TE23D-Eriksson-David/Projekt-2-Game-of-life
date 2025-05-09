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
    public int xStartPosition = 0;
    public int yStartPosition = 0;
    public string textContent = "Reminder if you forget to change this!!";
    public int fontSize = Raylib.GetScreenWidth() / 40;
    Rectangle Rec;


    public int Create() // Skappa knappen med text. Bakgrunden/Storleken i x led beräknas genom att ta karaktärerna
    {   // I strängen och muliplicera med storleken vilket blir jätte konstigt med långa ord och borde tänkas om.
        int recLenght = textContent.Length * fontSize * 3 / 4;
        Rec = new Rectangle(xStartPosition, yStartPosition, recLenght, fontSize * 2);
        Raylib.DrawRectangleRec(Rec, Color.Blue);
        Raylib.DrawText(textContent, xStartPosition + fontSize / 2, yStartPosition + fontSize / 2, fontSize, Color.Black);
        return recLenght;
    }
}

public class LineTemplate // Linije mallen vilket är ganska onödig 
{
    public int lineWidth = 20;
    public int lineEndY = 0;
    public int lineStartX = 0;
    public int lineStartY = 0;
    public int lineEndX = 0;

    Vector2 lineStart = new Vector2(0, 0);
    Vector2 lineStop = new Vector2(0, 0);
    public void Create()
    {
        lineStop.Y = lineEndY;
        lineStop.X = lineEndX;
        lineStart.Y = lineStartY;
        lineStart.X = lineStartX;
        Raylib.DrawLineEx(lineStart, lineStop, lineWidth, Color.Black);
    }
}




public class Interface // Hanterar vad som ska ritas utt på fönstret och användarens input.
{
    public static Choice Draw(out Vector2 pressedPosition, Choice userInput, int overlineStartY, int underlineStartY) // RIIIIIITAR
    {
        pressedPosition.X = 0;
        pressedPosition.Y = 0;
        int windowHeight = Raylib.GetScreenHeight();
        int windowWidth = Raylib.GetScreenWidth();

        // Skappar instncer av knapparna och linijerna som rittas ut.
        ButtonTemplate StartButton = new ButtonTemplate() { textContent = "Start", xStartPosition = windowWidth / 8, yStartPosition = (windowHeight / 6) * 5 + windowHeight / 24, };
        ButtonTemplate EndButton = new ButtonTemplate() { textContent = "Stop", xStartPosition = (windowWidth / 4) + windowWidth / 32, yStartPosition = (windowHeight / 6) * 5 + windowHeight / 24, };
        ButtonTemplate ClearButton = new ButtonTemplate() { textContent = "Clear", xStartPosition = (windowWidth / 8) * 3 + windowWidth / 32, yStartPosition = (windowHeight / 6) * 5 + windowHeight / 24, };
        ButtonTemplate InstructionsButton = new ButtonTemplate() { textContent = "Instructions", xStartPosition = (windowWidth / 8) * 5 + windowWidth / 16, yStartPosition = (windowHeight / 6) * 5 + windowHeight / 60, };
        ButtonTemplate ChangeWindowSizeButton = new ButtonTemplate() { textContent = "Change Window Size", xStartPosition = (windowWidth / 8) * 5 + windowWidth / 80 + windowWidth / 160, yStartPosition = (windowHeight / 6) * 5 + windowHeight / 12 + windowHeight / 150, };

        LineTemplate OverBoardLine = new LineTemplate() { lineStartX = 0, lineStartY = overlineStartY, lineEndX = windowWidth, lineEndY = overlineStartY, lineWidth = 12 };
        LineTemplate UnderBoardLine = new LineTemplate() { lineStartX = 0, lineStartY = underlineStartY, lineEndX = windowWidth, lineEndY = underlineStartY, lineWidth = 12 };


        Raylib.BeginDrawing(); // Ritt tiden =)

        Raylib.ClearBackground(Color.Gray);
        OverBoardLine.Create();
        UnderBoardLine.Create();
        int lenghtStartButton = StartButton.Create();
        int lenghtEndButton = EndButton.Create();   // Hämtar slut x på knapparna 
        int lenghtClearButton = ClearButton.Create();
        int lenghtInstructionsButton = InstructionsButton.Create();
        int lenghtChangeWindowSizeButton = ChangeWindowSizeButton.Create();
        Raylib.DrawText("The Game Of Life", windowWidth / 40, windowHeight / 30, (windowWidth / 40) * 3, Color.Black);

        Raylib.EndDrawing(); // Slut på ritt tiden =( 


        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) // kollar om mussen tryks ner sedan vart
        {   // Om den är inom en av områderna nedan så ändras enumet för anvädarens val.
            pressedPosition = Raylib.GetMousePosition();

            if (pressedPosition.X > StartButton.xStartPosition & pressedPosition.X < StartButton.xStartPosition + lenghtStartButton & pressedPosition.Y > StartButton.yStartPosition & pressedPosition.Y < StartButton.yStartPosition + StartButton.fontSize * 2)
                userInput = Choice.Start;

            if (pressedPosition.X > EndButton.xStartPosition & pressedPosition.X < EndButton.xStartPosition + lenghtEndButton & pressedPosition.Y > EndButton.yStartPosition & pressedPosition.Y < EndButton.yStartPosition + EndButton.fontSize * 2)
                userInput = Choice.Stop;

            if (pressedPosition.X > ClearButton.xStartPosition & pressedPosition.X < ClearButton.xStartPosition + lenghtClearButton & pressedPosition.Y > ClearButton.yStartPosition & pressedPosition.Y < ClearButton.yStartPosition + ClearButton.fontSize * 2)
                userInput = Choice.Clear;

            if (pressedPosition.X > InstructionsButton.xStartPosition & pressedPosition.X < InstructionsButton.xStartPosition + lenghtInstructionsButton & pressedPosition.Y > InstructionsButton.yStartPosition & pressedPosition.Y < InstructionsButton.yStartPosition + InstructionsButton.fontSize * 2)
                userInput = Choice.Instructions;

            if (pressedPosition.X > ChangeWindowSizeButton.xStartPosition & pressedPosition.X < ChangeWindowSizeButton.xStartPosition + lenghtChangeWindowSizeButton & pressedPosition.Y > ChangeWindowSizeButton.yStartPosition & pressedPosition.Y < ChangeWindowSizeButton.yStartPosition + ChangeWindowSizeButton.fontSize * 2)
                userInput = Choice.AdjustWindowSize;

            if (pressedPosition.Y > OverBoardLine.lineStartY + OverBoardLine.lineWidth / 2 & pressedPosition.Y < UnderBoardLine.lineStartY - OverBoardLine.lineWidth / 2)
                userInput = Choice.BoardInput;

        }
        return userInput;
    }


    public static Choice PromptInstructionWindow(Choice userInput) // EN egen metod för att rita utt instruktions rutan.
    {
        int windowHeight = Raylib.GetScreenHeight();
        int windowWidth = Raylib.GetScreenWidth();

        // Väldigt lång instruktion som jag inte orkade skriva själv ChatGPT =)
        string veryLongtextContent = "Conway's Game of Life is a grid-based \nsimulation where cells live or die based \non simple rules. Each cell checks its 8 \nneighbors: It lives on with 2 or 3 neighbors,\nDies from loneliness or overcrowding,\nA dead cell is reborn with exactly 3 neighbors\nYou can click to toggle cells, press run\nto simulate, stop to stop simulating, and clear \nto clear the grid. There's no goal — just \npatterns that grow, move, or fade. It's a \nsandbox where complexity emerges \nfrom simplicity.";

        ButtonTemplate CloseInstructionsButton = new ButtonTemplate() { textContent = "Close", xStartPosition = (windowWidth / 8) * 6 + (windowWidth / 20), yStartPosition = windowHeight / 12, };

        while (userInput == Choice.Instructions) // Så länge användaren inte trykt på stäng knappen
        {
            Raylib.BeginDrawing(); // Rittar ut rutan med text och stäng knappen.

            Raylib.DrawRectangle(windowWidth / 16, windowHeight / 30, (windowWidth / 8) * 7, (windowHeight / 6) * 5, Color.LightGray);
            Rectangle Rec2 = new Rectangle(windowWidth / 16, windowHeight / 30, (windowWidth / 8) * 7, (windowHeight / 6) * 5);
            Raylib.DrawRectangleLinesEx(Rec2, 5, Color.Black);
            int lenghtCloseInstructionButton = CloseInstructionsButton.Create();
            Raylib.DrawText("Istructioner", (windowWidth / 40) * 4, (windowHeight / 60) * 4, windowWidth / 10, Color.White);
            Raylib.DrawText(veryLongtextContent, (windowWidth / 40) * 4, (windowHeight / 24) * 5, windowWidth / 40 + windowWidth / 100, Color.White);

            Raylib.EndDrawing();

            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) // Kollar om musen är ned trykt sedan om knappen tryktes.
            {
                Vector2 pressedPosition = Raylib.GetMousePosition();
                if (pressedPosition.X > CloseInstructionsButton.xStartPosition & pressedPosition.X < CloseInstructionsButton.xStartPosition + lenghtCloseInstructionButton & pressedPosition.Y > CloseInstructionsButton.yStartPosition & pressedPosition.Y < CloseInstructionsButton.yStartPosition + CloseInstructionsButton.fontSize * 2)
                    userInput = Choice.CloseInstructions;
            }

            if (Raylib.WindowShouldClose()) // Gör så att man kan stänga fönstret 
            { // Gör så att den går ur whille loppen och sedan stänger.
                userInput = Choice.CloseInstructions;
            }

        }
        return userInput;
    }






    public static bool PromptAjdustingWindow(bool entetySizeRecalibration)
    {
        int windowHeight = Raylib.GetScreenHeight();
        int windowWidth = Raylib.GetScreenWidth();
        string errorMesage = "";
        bool close = false;
        string stringuserInput = "";

        while (close == false)
        {
            if (Raylib.WindowShouldClose()) // Gör så att man kan stänga fönstret 
            { // Gör så att den går ur whille loppen och sedan stänger.
                close = true;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                close = true; // Stänger fönstret
                stringuserInput = "";
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Enter)) // Enter
            {
                stringuserInput = AdjustSizeAfterInput(stringuserInput, out errorMesage); // Tar strängen och fixar den sedan ändrar storlek på fönstret
                if (errorMesage == "")
                {
                    close = true;
                    entetySizeRecalibration = true;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Backspace)) // Baksteg
            {
                if (stringuserInput.Length > 0)
                    stringuserInput = stringuserInput.Substring(0, stringuserInput.Length - 1);
            } // Skppar en sträng av strängen men kan ta emot ett länd arhument vilket gör att jag kan 
              // korta ner strängen vilket jag vill =)

            int key = Raylib.GetCharPressed();


            while (key > 0) // Om användare trök på något
            {
                if (key > 31 && key < 127) //Vanliga tecken
                    stringuserInput += (char)key; // Konverterar sifra till karaktär

                key = Raylib.GetCharPressed(); // Tar nästa tangent i kön om användaren tryker på en frame.
            }

            Raylib.BeginDrawing(); // Rittar ut rutan med användarens text/ userInput.

            Raylib.DrawRectangle(0, 0, windowWidth, windowHeight, Color.LightGray);
            Rectangle Rec3 = new Rectangle(0, 0, windowWidth, windowHeight);
            Raylib.DrawRectangleLinesEx(Rec3, 10, Color.Black);

            Raylib.DrawText("Här kan du ändra stroleken på skärmen genom att \nskriva den storleken som du vill ha i x och y led! \nDet finns bara två restriktioner. 1. Formatet för din \ninput måste se utt på deta sätt tex orginal storleken \n'800x600' med ett x imellan bräden och höjden. \n2. Skärmstorlekn få inte överstiga 1000x1000 eller \nunderstiga 400x400 annars får jag problem. Du kan \nskriva genom att tryka på tangenterna och ta bort \nmed baksteg, för att sedan bekräfta/submita din \nstorlek tryker du på Enter! ", (windowWidth / 80) * 3, windowHeight / 6, (windowWidth / 200) * 7, Color.White);
            Raylib.DrawText("Skärm Storleks Ändraren", (windowWidth / 80) * 3, windowHeight / 20, (windowWidth / 160) * 11, Color.White);
            Raylib.DrawText($"Enter size: {stringuserInput}", (windowWidth / 80) * 3, (windowHeight / 6) * 5, (windowWidth / 20), Color.White);
            Vector2 startLinePos = new Vector2((windowWidth / 80) * 3, (windowHeight / 6) * 5 + windowWidth / 15 - windowWidth / 100); Vector2 endLinePos = new Vector2(windowWidth / 2 + (windowWidth / 40) * 3, (windowHeight / 6) * 5 + windowWidth / 15 - windowWidth / 100);
            Raylib.DrawLineEx(startLinePos, endLinePos, 5, Color.White);

            if (errorMesage.Length != 0) // Skriver bara utt om det finns något i.
            {
                Raylib.DrawText(errorMesage, (windowWidth / 80) * 3, (windowHeight / 3) * 2 + (windowHeight / 30), windowWidth / 40 + windowWidth / 100, Color.Red);
            }

            Raylib.EndDrawing();

        }
        return entetySizeRecalibration;
    }



    public static string AdjustSizeAfterInput(string stringuserInput, out string errorMesage)
    {
        errorMesage = "";
        stringuserInput = stringuserInput.Trim().ToLower(); // Tar bort mellanslag och sätter alla bokstäver som inte bör vara där och bör till des mindre form.

        // användare behöver skriva så här "800x600" !!!
        string[] InputPart = stringuserInput.Split('x'); // Dellar strängen där x förekommer och tar bort den av någon anledning jag trode först att den hamnade i någon av strängarna tills jag testade och kunnde inte hitta den vilket var lite roligt när jag funderade i typ en menut för att sedan lässa nogrant vad metoden gör och slå handflatan i ansiktet.

        if (InputPart.Length == 2)
        {
            if (int.TryParse(InputPart[0], out int newWidth) && int.TryParse(InputPart[1], out int newHeight))
            { // Try parsar användarens input, Uppmärksamma att jag använder TRYPARSE.
                if (newWidth <= 1000 && newWidth >= 400 && newHeight <= 1000 && newHeight >= 400)
                {
                    Raylib.SetWindowSize(newWidth, newHeight);
                }
                else
                {
                    stringuserInput = ""; // sätter den till inget för nästa gång
                    errorMesage = "För stora eller små x eller y parametrar. Testa \natt skriva en input inom de rekommenderade ramarna";
                }
            }
            else
            {
                stringuserInput = ""; // sätter den till inget för nästa gång
                errorMesage = "Skärmstorleken tar bara emot sifror i din input, \nframför och bakom ditt 'x'";
            }
        }
        else
        {
            stringuserInput = ""; // sätter den till inget för nästa gång
            errorMesage = "Du kan bara använda dig av 1 x i din input och \nden måste vara mellan höden och längden";
        }
        return stringuserInput;
    }


    public static void GetBoardDimentions(out Vector2 ySize, out Vector2 xSize, int overlineStartY, int underlineStartY)
    {
        ySize.X = overlineStartY + 6; // start y för board
        ySize.Y = underlineStartY - 6; // slut y 

        xSize.X = 0; // start x för board
        xSize.Y = Raylib.GetScreenWidth(); ; // slut x 
    }

}