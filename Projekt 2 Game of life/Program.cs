using SC = System.Console;
using Raylib_cs;
using Projekt_2_Game_of_life;
using System.Numerics;

Raylib.SetTargetFPS(30);
Raylib.InitWindow(800, 600, "Game Of Life");
Choice UserInput = Choice.None;
Vector2 MoucePressedPosition;

while (true)
{
    Interface.Draw();
    //Interface.SetChoice(Choice.None);
    UserInput = Interface.GetChoice();
    MoucePressedPosition = Interface.GetMousePosition();
}



    