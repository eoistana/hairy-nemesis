
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

    public virtual int Calc(ExtensionContext<int> context)
    {
      return context.LastValue;
    }

    public virtual void Do(ExtensionContext context)
    {
    }

    public virtual int Do2(ExtensionContext<int> context, int x, int y)
    {
      return context.LastValue;
    }


    public IFoo GetExtension(string modName, string className)
    {
      return ClassExtender.GetExtensionClass(Parent, modName, className);
    }
  }

}