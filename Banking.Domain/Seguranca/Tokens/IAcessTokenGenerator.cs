namespace Banking.Domain.Seguranca.Tokens
{
    public interface IAcessTokenGenerator
    {
        public string GenerateToken(Guid UserIdentifier);
    }
}
