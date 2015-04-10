
namespace API
{
  public abstract class XFoo : IFoo
  {
    protected IFoo Parent;

    protected XFoo(IFoo foo)
    {
      Parent = foo;
    }

    public virtual int Type
    {
      get { return Parent.Type; }
      set { Parent.Type = value; }
    }

    public virtual int Calc(ExtendContext<int> context)
    {
      return context.LastValue;
    }

    public virtual void Do(ExtendContext context)
    {
    }

    public virtual int Do2(ExtendContext<int> context, int x, int y)
    {
      return context.LastValue;
    }


    public IFoo GetExtension(string modName, string className)
    {
      return ClassExtender.GetExtensionClass(Parent, modName, className);
    }
  }

}