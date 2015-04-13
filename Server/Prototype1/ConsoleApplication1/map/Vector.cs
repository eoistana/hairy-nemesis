using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.map
{
  public class Vector
  {
    public double X;
    public double Y;

    public static double Deg2Rad(double degrees)
    {
      return degrees*Math.PI/180;
    }

    public static Vector Pol2Cart(double distance, double degrees)
    {
      var rad = Deg2Rad(degrees);
      return new Vector { X = distance * Math.Cos(rad), Y = distance * Math.Sin(rad) };
    }
  }
}
