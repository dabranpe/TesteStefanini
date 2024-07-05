namespace Questao5.Application;

public class ConsultarSaldoContaResponse
{
    public int NumeroConta { get; set; }
    public string NomeTitularConta { get; set; }
    public string DataHoraConsulta { get; set; }
    public decimal Saldo { get; set; }
}
