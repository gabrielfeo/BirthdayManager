using System;
using Entities;

namespace Repository.ValidatorNs
{
    internal class ValidatorFactory
    {
        public IValidator<TData> NewValidator<TData>()
        {
            if (typeof(TData) == typeof(Person)) 
                return new PersonValidator() as IValidator<TData>;
            
            else 
                throw new ArgumentException($"There is no Validator available for {typeof(TData)}");
        }
    }
}