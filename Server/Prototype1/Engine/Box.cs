//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Engine
//{
//  public class Box
//  {
//    public double X1;
//    public double X2;
//    public double Y1;
//    public double Y2;

//    public Box(double x1, double y1, double x2, double y2)
//    {
//      this.X1 = x1;
//      this.X1 = x2;
//      this.X1 = y1;
//      this.X1 = y2;
//    }

//    internal bool Intersects(Box entity2Box)
//    {
//      var yBetween = (Between(entity2Box.Y1, Y1, Y2) || Between(entity2Box.Y2, Y1, Y2) || Between(Y1, entity2Box.Y1, entity2Box.Y2));
//      var yOtherBetween = (Between(Y1, entity2Box.Y1, entity2Box.Y2) || Between(Y2, entity2Box.Y1, entity2Box.Y2) || Between(entity2Box.Y1, Y1,Y2));
//      return
//        (yBetween || yOtherBetween) &&
//        (
//          (Between(entity2Box.X1, X1, X2) && yBetween) ||
//          (Between(entity2Box.X2, X1, X2) && yBetween) ||
//          (Between(X1, entity2Box.X1, entity2Box.X2) && yOtherBetween) ||
//          (Between(X2, entity2Box.X1, entity2Box.X2) && yOtherBetween));
//    }

//    private static bool Between(double otherx, double x1, double x2)
//    {
//      return otherx >= x1 && otherx <= x2;
//    }
//  }
//}
