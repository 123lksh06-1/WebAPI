using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Model.Repositories;

namespace WebAPI.Filter.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionsFilterAttribute:ExceptionFilterAttribute // class created to handle exceptions instead of in controller
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirtId= context.RouteData.Values["id"] as string;
            if(int.TryParse(strShirtId,out int shirtId))
            {
                if (!ShirtRepository.ShirtExists(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt doesn't exists anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}
