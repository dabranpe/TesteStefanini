using System.Globalization;
using System.Xml.Linq;

namespace Questao1
{
    class ContaBancaria {

        public int NumeroConta { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        const double TarifaSaque = 3.5;

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {            
            Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            NumeroConta = numero;
            Titular = titular;            
        }

        public void Deposito(double valor) => Saldo += valor;

        public void Saque(double valor) => Saldo -= (valor + TarifaSaque);

        public override string ToString() 
          => $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {string.Format("{0:N2}", Saldo) }";
        



    }
}
