using System;
using System.Numerics;
using System.Security.Principal;
using Raylib_cs;

namespace Projekt_2_Game_of_life;


public class Interface
{

static void Draw(){

Raylib.SetTargetFPS(30);
Raylib.InitWindow(800, 600, "Game Of Life");
int WindowWidht = Raylib.GetScreenWidth();
int WindowHeight = Raylib.GetScreenHeight();

int LineWidth = WindowHeight/20;
int OverHeadLineHeight = WindowHeight/5;
Vector2 OverHeadLineStart = new Vector2(0,OverHeadLineHeight);
Vector2 OverHeadLineEnd = new Vector2(WindowWidht,OverHeadLineHeight);

int UnderBoardLineHeight = WindowHeight - WindowHeight/5;
Vector2 UnderBoardLineStart = new Vector2(0,UnderBoardLineHeight);
Vector2 UnderBoardLineEnd = new Vector2(WindowWidht,UnderBoardLineHeight);

int TextYButtonPosition = WindowHeight - WindowHeight/6;
int TextXButtonPosition = WindowWidht/5;
int FontSize = WindowWidht/30;
Rectangle ButtonPlay = new Rectangle(TextXButtonPosition,TextYButtonPosition,3*FontSize,FontSize*2); 

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Gray);
        Raylib.DrawLineEx(OverHeadLineStart, OverHeadLineEnd, LineWidth, Color.Black);
        Raylib.DrawLineEx(UnderBoardLineStart, UnderBoardLineEnd, LineWidth, Color.Black);

        Raylib.DrawRectangleRec(ButtonPlay, Color.Blue);
        Raylib.DrawText("Filler Start", TextXButtonPosition+FontSize/2, TextYButtonPosition+FontSize/2, FontSize, Color.Black);


        Vector2 MousePosition = Raylib.GetMousePosition();
        bool MouseButtonPressed = Raylib.IsMouseButtonPressed(MouseButton.Left);

    Raylib.EndDrawing();
}

}





}
