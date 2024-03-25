namespace Classificador.Api.SharedKernel;

public static class ArgumentValidator
{
    public static void ThrowIfNullOrEmpty(string value, string parameterName = null!)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
        if (value.Length == 0)
            throw new ArgumentException(string.Format("{0} cannot be an empty string.", parameterName ?? "Value"), parameterName);
    }
}
