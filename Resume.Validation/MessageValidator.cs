using FluentValidation;
using Resume.Entities;
using System;

namespace Resume.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Body).NotNull().MinimumLength(1).MaximumLength(250);
            RuleFor(message => message.SenderName).NotNull();
            RuleFor(message => message.SenderEmail).NotNull().EmailAddress();
            RuleFor(message => message.Subject).NotNull();
        }
    }
}
