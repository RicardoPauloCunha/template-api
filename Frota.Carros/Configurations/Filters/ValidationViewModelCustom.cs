using Frota.Carros.DTOs.FiltersErrors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Frota.Carros.Configurations.Filters
{
    public class ValidationViewModelCustom : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCamposViewModel = new ErrorValidationViewModel(context
                    .ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage));

                context.Result = new UnprocessableEntityObjectResult(validaCamposViewModel);
            }
        }
    }
}
