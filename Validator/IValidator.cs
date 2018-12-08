namespace Validator
{
    public interface IValidator<T>
    {
        bool Validate(T item);
    }
}