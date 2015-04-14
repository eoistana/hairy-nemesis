namespace API
{
  public class ExtensionContext<T> : ExtensionContext
  {
    public T LastValue;
  }

  public class ExtensionContext
  {
    public bool SupressOriginalCall;

    public void SetValue<T1>(T1 result)
    {
      var extensionContext = this as ExtensionContext<T1>;
      if (extensionContext != null) extensionContext.LastValue = result;
    }

    public T1 GetValue<T1>()
    {
      var extensionContext = this as ExtensionContext<T1>;
      return extensionContext != null ? extensionContext.LastValue : default(T1);
    }
  }
}