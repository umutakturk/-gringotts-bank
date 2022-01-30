namespace GringottsBank.Common.Models
{
    public class ErrorResult
    {
        public ErrorResult(string[] messages)
        {
            Messages = messages;
        }

        public string[] Messages { get; }
    }
}
