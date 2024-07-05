namespace Questao5.Infrastructure;

public class CustomResponse
{
    private const string MensagemSucesso = "Executado com sucesso";

    public CustomResponse() {
        this.messages = new();
    }
    public CustomResponse(dynamic data)
    {
        this.messages = new();
        this.messages.Add(MensagemSucesso);
        this.success = false;        
    }

    public CustomResponse(List<string> messages, bool success)
    {
        this.messages = messages;
        this.success = success;        
    }

    public List<string> messages { get; private set; }
    public bool success { get; private set; }    
}
