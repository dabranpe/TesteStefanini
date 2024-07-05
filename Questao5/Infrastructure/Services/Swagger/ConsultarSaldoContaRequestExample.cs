using Questao5.Application;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Swagger;

public class ConsultarSaldoContaRequestExample : IExamplesProvider<ConsultarSaldoContaRequest>
{
    public ConsultarSaldoContaRequest GetExamples()
    {
        return new ConsultarSaldoContaRequest
        {
            IdContaCorrente = "5a3cc8a9-27db-4416-860c-1325bf345f7f"
        };
    }
}
