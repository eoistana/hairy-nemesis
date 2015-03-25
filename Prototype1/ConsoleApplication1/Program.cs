
using Modules.Engine;
using Normalized.Maths;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace ConsoleApplication1
{
  public class Program
  {
    static void Main(string[] args)
    {
      var g = new GameLoop();
      g.Run();

      var mainmap = new Map(1, 5, 5, "mainmap") { Scale = 10 };
      var leftmap = new Map(2, 10, 10, "leftmap") { Position = new Position(0, 0, 0) { Parent = mainmap } };
      mainmap.Cells[0, 0] = leftmap;
      var rightmap = new Map(3, 5, 10, "rightmap") { Position = new Position(1, 0, 0) { Parent = mainmap } };
      mainmap.Cells[1, 0] = rightmap;

      lock (g.Maps)
      {
        g.Maps.Add(mainmap);
      }
      var player = new Player(2,0,0,"Player1")
      {
        Shape = new Shape(new ShapeDefinition
        {
          Edges = new List<Edge>
          {
            new Edge(new Vector(-0.4, -0.4), 1),
            new Edge(new Vector(0.4, -0.4), 1),
            new Edge(new Vector(0.4, 0.4), 1),
            new Edge(new Vector(-0.4, 0.4), 1)
          }
        }),
        Position = new Position(5,5, 0)
      };

      Console.WriteLine("Use number pad to enter direction\nHit enter after each number\nEnter q to quit\n\nThe Map is 20 x 10");

      mainmap.AddMapEntity(player, 2, 0.5, 0);
      player.Heading = Vector.Deg2Rad(45);
      player.Speed = 0.5;
      bool quit = false;
      while (!quit)
      {
        Thread.Sleep(100);
        Console.WriteLine(player);
        var speedfactor = Math.Round( 1.0 / player.Position.Parent.Scale, 8);
        switch (Console.ReadLine())
        {
          case "q":
          case "Q":
            quit = true;
            break;
          case "2":
            player.RegisterMoveMessage(new MoveMessage(player.Position.X, player.Position.Y + speedfactor, 1));
            break;
          case "4":
            player.RegisterMoveMessage(new MoveMessage(player.Position.X - speedfactor, player.Position.Y, 1));
            break;
          case "6":
            player.RegisterMoveMessage(new MoveMessage(player.Position.X + speedfactor, player.Position.Y, 1));
            break;
          case "8":
            player.RegisterMoveMessage(new MoveMessage(player.Position.X, player.Position.Y - speedfactor, 1));
            break;
          default:
            break;
        }
      }
      //player.RegisterMoveMessage(new MoveMessage(9, 9, 1));
      //Console.WriteLine(player);
      //Thread.Sleep(100);
      //player.RegisterMoveMessage(new MoveMessage(3, 3, 1));
      //Console.WriteLine(player);
      //Thread.Sleep(100);
      //player.RegisterMoveMessage(new MoveMessage(6, 2, 1));
      //Console.WriteLine(player);
      //Thread.Sleep(100);
      //Console.WriteLine(player);
      //Thread.Sleep(100);
      g.Stop();
    }
  }
}
