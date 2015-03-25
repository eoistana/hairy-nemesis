using Engine.Maths;

namespace Engine.Maps.Definitions.ShapeDefinitions
{
  public class Edge
  {
    public Vector Vector;

    public double Height;
    public EdgeType Type;

    public Edge(Vector vector, double height)
    {
      Vector = vector;
      Height = height;
    }

    internal Edge Translate(Position position)
    {
      return new Edge(Vector.Rotate(position.Rotation).Translate(position.X, position.Y), Height);
    }

    internal Line ToLine(Edge edge)
    {
      return new Line(this, edge);
    }
  }
}
