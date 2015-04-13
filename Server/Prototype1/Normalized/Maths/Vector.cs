using System;

namespace Normalized.Maths
{
  public class Vector
  {
    public double X;
    public double Y;

    public Vector(double x, double y)
    {
      X = x;
      Y = y;
    }

    public double LengthSquared {
      get { return X*X + Y*Y; }
    }

    public static double Deg2Rad(double degrees)
    {
      return degrees*Math.PI/180;
    }

    public static Vector Pol2Cart(double distance, double rad)
    {
      return new Vector(distance * Math.Cos(rad), distance * Math.Sin(rad));
    }

    public static Vector Pol2CartDeg(double distance, double degrees)
    {
      var rad = Deg2Rad(degrees);
      return Pol2Cart(distance, rad);
    }

    public Vector Rotate(double rad)
    {
      var cos = Math.Cos(rad);
      var sin = Math.Sin(rad);
      return new Vector
      (
        X*cos - Y*sin,
        X*sin + Y*cos
      );
    }

    public Vector Translate(double x, double y)
    {
      return new Vector (X + x, Y + y);
    }

    public static Vector operator +(Vector a, Vector b)
    {
      return new Vector(a.X + b.X, a.Y + b.Y);
    }

    public static Vector operator -(Vector a, Vector b)
    {
      return new Vector(a.X - b.X, a.Y - b.Y);
    }

    public static Vector operator *(Vector a, double d)
    {
      return new Vector(a.X*d, a.Y*d);
    }
  }
}
