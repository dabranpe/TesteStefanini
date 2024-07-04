namespace Questao5.Infrastructure;

public class CustomErrorException : Exception
{
    public CustomErrorException(string notificacao)
    {
        Notificacoes = new();
        Notificacoes.Add(notificacao);
    }

    public CustomErrorException(List<string> notificacoes) => Notificacoes = notificacoes;

    public List<string> Notificacoes { get; private set; }
}
