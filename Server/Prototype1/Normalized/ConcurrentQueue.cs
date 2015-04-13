using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Normalized
{
  public class ConcurrentQueue<T>
  {
    private class Container
    {
      public Container Next;
      public Container Prev;
      public T Value;
      public bool HasValue;

      public Container(T value)
      {
        Value = value;
        HasValue = true;
      }
    }

    private Container Head;
    private Container Tail;

    public ConcurrentQueue()
    {
      Head = Tail = new Container(default(T));
      Tail.HasValue = false;
      Head.Next = Tail;
      Tail.Prev = Head;
    }

    public void Push(T value)
    {
      var c = new Container(value);
      lock (Head)
      {
        c.Next = Head;
        Head.Prev = c;
        Head = c;
      }
    }

    public bool TryPop(out T value)
    {
      value = default(T);
      bool found = false;
      lock (Tail)
      {
        if (Tail.HasValue)
        {
          value = Tail.Value;
          Tail.HasValue = false;
          Tail.Value = default(T);
          found = true;
        }
        if(Tail.Prev!=null)
          Tail = Tail.Prev;
      }
      return found;
    }
  }

  
}
