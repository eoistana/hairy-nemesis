using System.Collections.Generic;

namespace ConsoleApplication1
{
  public class Lando
  {
    public Dictionary<int, Dictionary<int, Dictionary<int, Plot>>> OwnedPlots =
      new Dictionary<int, Dictionary<int, Dictionary<int, Plot>>>();

    public bool AddPlot(int worldId, int x, int y, PlotOwner owner)
    {
      if (!OwnedPlots.ContainsKey(worldId)) OwnedPlots[worldId] = new Dictionary<int, Dictionary<int, Plot>>();
      if (!OwnedPlots[worldId].ContainsKey(x)) OwnedPlots[worldId][x] = new Dictionary<int, Plot>();
      if (OwnedPlots[worldId][x].ContainsKey(y)) return false;
      OwnedPlots[worldId][x][y] = new Plot {Owner = owner};
      return true;
    }

    public bool RemovePlot(int worldId, int x, int y, PlotOwner owner)
    {
      if (!OwnedPlots.ContainsKey(worldId)) return false;
      if (!OwnedPlots[worldId].ContainsKey(x)) return false;
      if (!OwnedPlots[worldId][x].ContainsKey(y)) return false;
      if (OwnedPlots[worldId][x][y].Owner.Name != owner.Name) return false;
      OwnedPlots[worldId][x].Remove(y);
      return true;
    }

    public Plot this[int worldId, int x, int y]
    {
      get
      {
        if (!OwnedPlots.ContainsKey(worldId))
          return null;
        if (!OwnedPlots[worldId].ContainsKey(x))
          return null;
        return OwnedPlots[worldId][x].ContainsKey(y) ? null : OwnedPlots[worldId][x][y];
      }
      set
      {
        if (!OwnedPlots.ContainsKey(worldId))
          OwnedPlots[worldId] = new Dictionary<int, Dictionary<int, Plot>>();
        if (!OwnedPlots[worldId].ContainsKey(x))
          OwnedPlots[worldId][x] = new Dictionary<int, Plot>();
        OwnedPlots[worldId][x][y] = value;
      }
    }

    public PlotOwner GetOwner(int worldId, int x, int y)
    {
      if (!OwnedPlots.ContainsKey(worldId))
        return null;
      if (!OwnedPlots[worldId].ContainsKey(x))
        return null;
      return OwnedPlots[worldId][x].ContainsKey(y) ? null : OwnedPlots[worldId][x][y].Owner;
    }

    public void SetOwner(int worldId, int x, int y, PlotOwner owner)
    {
      if (!OwnedPlots.ContainsKey(worldId))
        OwnedPlots[worldId] = new Dictionary<int, Dictionary<int, Plot>>();
      if (!OwnedPlots[worldId].ContainsKey(x))
        OwnedPlots[worldId][x] = new Dictionary<int, Plot>();
      if (!OwnedPlots[worldId][x].ContainsKey(y))
        OwnedPlots[worldId][x][y] = new Plot {Owner = owner};
    }


  }

}