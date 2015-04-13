using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public class Class4
  {
    public decimal value;
    public class Infinity : Class4
    {
      public Infinity(bool positive)
      {
        value = positive ? decimal.MaxValue : decimal.MinValue;
      }
    }

    /// <remark>Add</remark>
    public static Class4 operator +(Class4 a, Class4 b)
    {
      return new Class4(a.value + b.value);
    }

    /// <remark>Add</remark>
    public static Class4 operator +(Class4 a, Infinity b)
    {
      return b;
    }

    /// <remark>Add</remark>
    public static Class4 operator +(Infinity a, Class4 b)
    {
      return a;
    }

    /// <remark>Substract.</remark>
    public static Class4 operator -(Class4 a, Class4 b)
    {
      return new Class4(a.value - b.value);
    }

    /// <remark>Substract.</remark>
    public static Class4 operator -(Class4 a, Infinity b)
    {
      return b;
    }

    /// <remark>Substract.</remark>
    public static Class4 operator -(Infinity a, Class4 b)
    {
      return a;
    }

    public static readonly Infinity PositiveInfinity = new Infinity(true);
    public static readonly Infinity NegativeInfinity = new Infinity(false);

    private Class4() { }
    public Class4(decimal v)
    {
      value = v;
    }
  }
}
