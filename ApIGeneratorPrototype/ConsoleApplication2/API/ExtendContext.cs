namespace API
{
  public class ExtendContext<T> : ExtendContext
  {
    public T LastValue;
  }

  public class ExtendContext
  {
    public bool SupressOriginalCall;

    public void SetValue<T1>(T1 result)
    {
      var extendContext = this as ExtendContext<T1>;
      if (extendContext != null) extendContext.LastValue = result;
    }

    public T1 GetValue<T1>()
    {
      var extendContext = this as ExtendContext<T1>;
      return extendContext != null ? extendContext.LastValue : default(T1);
    }
  }
}