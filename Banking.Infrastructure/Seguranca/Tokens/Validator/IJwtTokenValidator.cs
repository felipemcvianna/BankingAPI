namespace Banking.Infrastructure.Seguranca.Tokens.Validator
{
    public interface IJwtTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
        
    }
}
