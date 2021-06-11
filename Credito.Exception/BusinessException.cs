using System;
using System.Collections.Generic;
using System.Linq;

namespace Credito.Exception
{
    public class BusinessException : System.Exception
    {
        public List<BusinessValidationFailure> Errors { get; private set; } = new List<BusinessValidationFailure>();
        
        public void AddError(BusinessValidationFailure error)
        {
            this.Errors.Add(error);
        }

        public void ValidateAndThrow()
        {
            if (this.Errors.Any())
                throw this;
        }

    }

    public class BusinessValidationFailure
    {
        public string ErrorName { get; set; } = "violações";
        public string ErrorMessage { get; set; }
    }
}
