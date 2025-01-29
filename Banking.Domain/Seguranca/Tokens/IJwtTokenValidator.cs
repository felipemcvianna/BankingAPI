namespace Banking.Domain.Seguranca.Tokens
{
    public interface IJwtTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);

    }
}
