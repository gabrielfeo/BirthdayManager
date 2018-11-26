using System;

namespace Repository.ValidatorNs
{
    internal interface IValidator<T>
    {
        bool Validate(T item);
    }
}