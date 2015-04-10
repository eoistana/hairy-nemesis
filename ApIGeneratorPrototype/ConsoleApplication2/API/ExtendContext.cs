namespace API
{
  public class ExtendContext<T> : ExtendContext
  {
    public T LastValue;
  }

  public class ExtendContext
  {
    public bool Override;

    public void SetValue<T1>(T1 result)
    {
      (this as ExtendContext<T1>).LastValue = result;
    }

    public T1 GetValue<T1>()
    {
      return (this as ExtendContext<T1>).LastValue;
    }
  }
}