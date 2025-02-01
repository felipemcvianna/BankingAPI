namespace Banking.Application.Services.Encryption
{
    public class PasswordEncryptor
    {
        public string Encript(string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash.ToString();
        }
        public  bool Verify(string passwordRequest, string senhaCliente)
        {
            return BCrypt.Net.BCrypt.Verify(passwordRequest, senhaCliente);
        }
    }
}
