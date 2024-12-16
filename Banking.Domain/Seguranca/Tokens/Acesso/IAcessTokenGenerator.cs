namespace Banking.Domain.Seguranca.Tokens.Generate
{
    public interface IAcessTokenGenerator
    {
        public string GenerateToken(Guid UserIdentifier);
    }
}
