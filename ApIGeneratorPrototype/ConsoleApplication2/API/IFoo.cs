namespace API
{
  public interface IFoo
  {
    int Type { get; set; }

    int Calc(ExtendContext<int> context);
    void Do(ExtendContext context);
    int Do2(ExtendContext<int> context, int x, int y);


    IFoo GetExtension(string name);
  }
}