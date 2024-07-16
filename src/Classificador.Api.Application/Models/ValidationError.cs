namespace Classificador.Api.Application.Models;

public sealed record ValidationError : Error
{
    public ICollection<PropertyFailure> Failures { get; set; }
    public ValidationError(string code, string message, ICollection<PropertyFailure> failures) : base(code, message, ErrorType.Validation)
    {
        Failures = failures;
    }
    public sealed record PropertyFailure
    {
        public string Failure { get; set; }
        public string Description {get; set;}
        public PropertyFailure(string failure, string description)
        {
            Failure = failure;
            Description = description;
        }
    }
}

