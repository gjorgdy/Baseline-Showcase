using System.ComponentModel.DataAnnotations;
using Core.Exceptions;

namespace Core.Validation;

public abstract class ValidatedModel
{
    private readonly List<ValidationResult> _validationResults = [];
    private readonly ValidationContext _validationContext;

    protected ValidatedModel()
    {
        _validationContext = new ValidationContext(this);
    }

    public bool Validate() {
        bool res = Validator.TryValidateObject(
            this,
            _validationContext,
            _validationResults,
            true
        );
        _validationResults.ForEach(result => Console.WriteLine(result.ErrorMessage));
        return res;
    }

    public void ValidateOrThrow()
    {
        if (!Validate()) throw new InvalidModelException(
            _validationResults.Select(r => r.ErrorMessage).ToList()
        );
    }

}