
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Engine.Maps;

namespace Engine
{
  public class GameLoop
  {
    public List<Map> Maps = new List<Map>();
    private long time = 0;
    private const long MaxTickLength = 112050;
    bool quit = true;
    private Task task;

    private void Loop()
    {
      var stopwatch = Stopwatch.StartNew();
      while (!quit)
      {
        var start = stopwatch.ElapsedTicks;
        lock(Maps) Parallel.ForEach(Maps, map => map.Tick(time));
        var diff = stopwatch.ElapsedTicks - start;
        while (diff < MaxTickLength)
        {
          Thread.Yield();
          diff = stopwatch.ElapsedTicks - start;
        }
        time += MaxTickLength;
      }

      //Save?
    }

    public void Run()
    {
      if (!quit) return;
      quit = false;
      task = Task.Run((Action) Loop);
    }

    public void Stop()
    {
      quit = true;
      task.Wait();
    }
  }
}
