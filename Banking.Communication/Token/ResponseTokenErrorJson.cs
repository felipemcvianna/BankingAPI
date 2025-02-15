namespace Banking.Communication.Token
{
    public class ResponseTokenErrorJson
    {
        public List<string> Erros { get; set; } = new List<string>();
        public bool TokenExpires { get; set; }

        public ResponseTokenErrorJson(string message)
        {
            Erros.Add(message);
        }

        public ResponseTokenErrorJson(List<string> errorMessages)
        {
            Erros = errorMessages;
        }
    }
}