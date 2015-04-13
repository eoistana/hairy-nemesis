using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Engine.Maps;
using Engine.Maps.Cells.CellContainers.Entities.MobileEntities;
using Engine.Maps.Definitions.ShapeDefinitions;
using Engine.Maps.Shapes;
using Engine.Maths;

namespace Engine
{
    public class Class1
    {
      static void Main(string[] args)
      {
        var g = new GameLoop();
        g.Run();

        lock (g.Maps)
        {
          g.Maps.Add(new Map("map", 10, 10));
        }
        var player = new Player
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
          })
        };

        g.Maps[0].AddMapEntity(player, 5, 5);
        Console.WriteLine(player.Position);
        player.Heading = Vector.Deg2Rad(45);
        player.Speed = 0.5;
        player.Move();
        Console.WriteLine(player);
        Thread.Sleep(1);
        player.Move();
        Console.WriteLine(player);
        Thread.Sleep(1);
        player.Move();
        Console.WriteLine(player);

        g.Stop();
      }
    }
}
