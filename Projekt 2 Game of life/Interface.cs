using SC = System.Console;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Principal;
using Raylib_cs;

namespace Projekt_2_Game_of_life;




public class ButtonTemp
{
public int XStartPosition = 1;
public int YStartPosition = 2; 
public string TextContent = "You forgot to change this!!";
public int FontSize = 600/30;

Rectangle Rec;

    public int Create(){
        int RecLenght = TextContent.Length*FontSize;
        Rec = new Rectangle(XStartPosition,YStartPosition,RecLenght,FontSize*2);
        Raylib.DrawRectangleRec(Rec, Color.Blue);
        Raylib.DrawText(TextContent, XStartPosition+FontSize/2, YStartPosition+FontSize/2, FontSize, Color.Black);
        return RecLenght;
    }
}

public class LineTemp 
{
public int LineWidth = 20;
public int LineEndY = 0;
public int LineStartX = 0;
public int LineStartY = 0;


Vector2 LineStart = new Vector2(0,0);
Vector2 LineStop = new Vector2(0,0);
    public void Create(){
        LineStop.Y = LineEndY;
        LineStop.X = Raylib.GetScreenWidth();
        LineStart.Y = LineStartY;
        LineStart.X = LineStartX;
        Raylib.DrawLineEx(LineStart, LineStop, LineWidth, Color.Black);
    }
}



public class Interface
{

static Vector2 PressedPosition;


public static void Draw(){
Raylib.SetTargetFPS(30);
Raylib.InitWindow(800, 600, "Game Of Life");

ButtonTemp StartButton = new ButtonTemp() {TextContent = "Start", XStartPosition = 200, YStartPosition = 525, };
ButtonTemp EndButton = new ButtonTemp() {TextContent = "Stop", XStartPosition = 350, YStartPosition = 525, };
ButtonTemp ClearButton = new ButtonTemp() {TextContent = "Clear", XStartPosition = 500, YStartPosition = 525, };
LineTemp OverBoardLine = new LineTemp() {LineStartX = 0, LineStartY = 100, LineEndY = 100, LineWidth = 15};
LineTemp UnderBoardLine = new LineTemp() {LineStartX = 0, LineStartY = 500, LineEndY = 500, LineWidth = 15};


while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Gray);
        OverBoardLine.Create();
        UnderBoardLine.Create();
        int LenghtStartButton = StartButton.Create();
        int LenghtEndButton = EndButton.Create();
        int LenghtClearButton = ClearButton.Create();

    Raylib.EndDrawing();






    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
    {
        PressedPosition = Raylib.GetMousePosition();

        if (PressedPosition.X > StartButton.XStartPosition & PressedPosition.X < StartButton.XStartPosition + LenghtStartButton & PressedPosition.Y > StartButton.YStartPosition & PressedPosition.Y < StartButton.YStartPosition + StartButton.FontSize*2)
        {
            SC.WriteLine(PressedPosition.X + "-X-");
            SC.WriteLine(PressedPosition.Y + "-Y-");
            SC.WriteLine("StartButton");
        }
        if (PressedPosition.X > EndButton.XStartPosition & PressedPosition.X < EndButton.XStartPosition + LenghtEndButton & PressedPosition.Y > EndButton.YStartPosition & PressedPosition.Y < EndButton.YStartPosition + EndButton.FontSize*2)
        {
            SC.WriteLine(PressedPosition.X + "-X-");
            SC.WriteLine(PressedPosition.Y + "-Y-");
            SC.WriteLine("StopButton");
        }
        if (PressedPosition.X > ClearButton.XStartPosition & PressedPosition.X < ClearButton.XStartPosition + LenghtClearButton & PressedPosition.Y > ClearButton.YStartPosition & PressedPosition.Y < ClearButton.YStartPosition + ClearButton.FontSize*2)
        {
            SC.WriteLine(PressedPosition.X + "-X-");
            SC.WriteLine(PressedPosition.Y + "-Y-");
            SC.WriteLine("ClearButton");
        }        

    }

    
}

}




}
