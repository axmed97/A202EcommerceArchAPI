namespace Entities.SharedModels
{
    public class SendEmailCommand
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }
    }
}
