using System.Collections.Generic;

namespace Frota.Carros.DTOs.FiltersErrors
{
    public class ErrorValidationViewModel
    {
        public IEnumerable<string> Errors { get; private set; }

        public ErrorValidationViewModel(IEnumerable<string> erros)
        {
            Errors = erros;
        }
    }
}
