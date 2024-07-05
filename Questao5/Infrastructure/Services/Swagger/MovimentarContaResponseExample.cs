using Questao5.Application;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Swagger;

public class MovimentarContaResponseExample : IExamplesProvider<MovimentarContaResponse>
{    
    public MovimentarContaResponse GetExamples()
    {
        return new MovimentarContaResponse
        {
            IdMovimento = "5a3cc8a9-27db-4416-860c-1325bf345f7f"
        };
    }
}
