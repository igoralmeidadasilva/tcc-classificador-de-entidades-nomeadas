namespace Classificador.Api.SharedKernel.Shared;

public static class ArgumentValidator
{
    public static void ThrowIfNull(object value, string parameterName = null!)
    {
        if ( value == null ) {
            throw new ArgumentNullException( parameterName );
        }
    }
    public static void ThrowIfNullOrEmpty(string value, string parameterName = null!)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
        if (value.Length == 0)
            throw new ArgumentException(string.Format("{0} cannot be an empty string.", parameterName ?? "Value"), parameterName);
    }

    public static void ThrowIfNullOrWhitespace(string value, string parameterName = null!)
    {
        if (value == null) {
            throw new ArgumentNullException(parameterName);
        }

        if (value.Length == 0) {
            throw new ArgumentException(String.Format("{0} cannot be an empty string.", parameterName ?? "Value"), parameterName);
        }

        for ( int i = 0 ; i < value.Length ; i++ ) {
            if ( !Char.IsWhiteSpace( value, i ) ) {
                return;
            }
        }
        throw new ArgumentException(String.Format("{0} cannot consist entirely of whitespace.", parameterName ?? "Value"), parameterName);
    }
    public static void ThrowIfNullOrDefault<T>(T obj, string? paramName = null)
    {
        if (obj == null || obj.Equals(default(T)))
        {
            throw new ArgumentNullException($"{paramName ?? "Argument"} cannot be null or default");
        }
    }

    public static void ThrowIfZero<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument == T.Zero)
        {
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be zero");
        }
    }

    public static void ThrowIfNegative<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument < T.Zero)
        {
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be negative");
        }
    }

    public static void ThrowIfZeroOrNegative<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument <= T.Zero)
        {
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be zero or negative");
        }
    }
}
