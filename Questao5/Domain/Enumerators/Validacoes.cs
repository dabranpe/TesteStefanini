using System.ComponentModel;

namespace Questao5.Domain.Enumerators;

public enum Validacoes
{
    [Description("Apenas contas correntes cadastradas podem {0}")]
    INVALID_ACCOUNT = 1,
    [Description("Apenas contas correntes ativas podem {0}")]
    INACTIVE_ACCOUNT = 2,
    [Description("Apenas valores positivos podem ser recebidos")]
    INVALID_VALUE = 3,
    [Description("Apenas os tipos “débito” ou “crédito” podem ser aceitos")]
    INVALID_TYPE = 4,
}
