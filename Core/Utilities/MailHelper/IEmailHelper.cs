namespace Core.Utilities.MailHelper
{
    public interface IEmailHelper
    {
        bool SendEmail(string mailAddress, string token, bool bodyHtml);
    }
}
