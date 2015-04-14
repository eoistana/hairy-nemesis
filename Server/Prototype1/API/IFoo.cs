namespace API
{
  public interface IFoo
  {
    int Type { get; set; }

    int Calc(ExtensionContext<int> context);
    void Do(ExtensionContext context);
    int Do2(ExtensionContext<int> context, int x, int y);

  }
}