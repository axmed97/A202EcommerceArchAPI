using Core.Utilities.MailHelper;
using Entities.SharedModels;
using MassTransit;

namespace Business.Consumer
{
    public class ReceiveEmailConsumer : IConsumer<SendEmailCommand>
    {
        private readonly IEmailHelper _emailHelper;

        public ReceiveEmailConsumer(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            _emailHelper.SendEmail(context.Message.Email, context.Message.Token, true);
        }
    }
}
