using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class GameLoop
  {
    private const long MaxTickLength = 112050;
    private Task task;

    partial void OnGameLoopInit()
    {
      Maps = new List<Map>();
    }

    private void Loop()
    {
      var stopwatch = Stopwatch.StartNew();
      while (!quit)
      {
        var start = stopwatch.ElapsedTicks;
        Tick(new TickEventParameters(time) { Continue = true });
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
      task = Task.Run((Action)Loop);
    }

    public void Stop()
    {
      quit = true;
      task.Wait();
    }
  }
}
