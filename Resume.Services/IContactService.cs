using FluentValidation.Results;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Services
{
    public interface IContactService
    {
        IList<Message> GetAllMessages();
        void MarkAsRead(Message message);
        ValidationResult SendMessage(Message message);
        Message GetById(Guid id);
        void UpdateMessage(Message message);
    }
}
