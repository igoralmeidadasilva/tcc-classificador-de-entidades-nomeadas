namespace Classificador.Api.Domain.ValueObjects.NamedEntity;

public sealed record WordPosition : IValueObject
{
    private int _startPosition;
    public int StartPosition 
    { 
        get { return _startPosition; }
        init
        {
            ArgumentValidator.ThrowIfNull(value);
            _startPosition = value;
        }
    }

    private int _endPosition;
    public int EndPosition 
    { 
        get { return _endPosition; } 
        init
        {
            ArgumentValidator.ThrowIfNull(value);
            _endPosition = value;
        }
    }

    public static WordPosition Create(int start, int end)
    {
        return new WordPosition 
        { 
            StartPosition = start,
            EndPosition = end
        };
    }

    public static implicit operator WordPosition(int start)
    {
        return new WordPosition();
    }
}