using API;

namespace Extension1
{
  [Extension("Extension1", "Bar", "Register")]
  public class Bar : XFoo
  {
    public Bar(IFoo foo) : base(foo)
    {
    }

    public static void Register()
    {
      ClassExtender.Extend<IFoo, Bar>(CreateBar, -500);
    }

    private static Bar CreateBar(IFoo foo)
    {
      if (foo.Type == 0) return null;
      return new Bar(foo);
    }

    public override int Calc(ExtendContext<int> context)
    {
      context.SupressOriginalCall = true;
      return 17;
    }

    public override int Do2(ExtendContext<int> context, int x, int y)
    {
      context.SupressOriginalCall = true;
      return x*y;
    }
  }

  public class Bar2 : XFoo
  {
    public Bar2(IFoo foo) : base(foo)
    {
    }
  }
}
