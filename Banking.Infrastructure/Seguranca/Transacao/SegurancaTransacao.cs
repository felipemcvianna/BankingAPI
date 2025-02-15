using System.Security.Cryptography;
using System.Text;
using Banking.Domain.Seguranca.Transacoes;

namespace Banking.Infrastructure.Seguranca.Transacao
{
    public class SegurancaTransacao : ISegurancaTransacao
    {
        private readonly string _dados;
        private readonly string _chaveSegura;

        public SegurancaTransacao(string dados, string chaveSegura)
        {
            _dados = dados;
            _chaveSegura = chaveSegura;
        }

        public string GerarNumeroTransacao()
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_chaveSegura));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_dados));
            return BitConverter.ToString(hash).Replace("-", "").Substring(0, 16).ToUpper();
        }
    }
}