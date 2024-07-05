using Questao5.Application;
using Questao5.Domain.Enumerators;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Swagger;

public class CustomResponseExample : IExamplesProvider<CustomResponse>
{
    public CustomResponse GetExamples()
    {
        string complementoMensagem = "receber movimentação";
        List<string> mensagens = new List<string> { string.Format(Validacoes.INVALID_ACCOUNT.GetDescription(), complementoMensagem), string.Format(Validacoes.INACTIVE_ACCOUNT.GetDescription(), complementoMensagem) };

        return new CustomResponse(mensagens, false);       
        
    }
}
