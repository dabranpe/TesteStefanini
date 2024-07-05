using Questao5.Application;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Swagger;

public class ConsultarSaldoContaResponseExample : IExamplesProvider<ConsultarSaldoContaResponse>
{
    public ConsultarSaldoContaResponse GetExamples()
    {
        return new ConsultarSaldoContaResponse
        {
            NumeroConta = 123,
            NomeTitularConta = "João Neves",
            DataHoraConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            Saldo = 500
        };
    }
}
