using Resume.Entities;
using Resume.Repositories;
using Resume.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Resume.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBaseRepository<Message> messageRepository;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.messageRepository = unitOfWork.MessageRepository;
        }

        public IList<Message> GetAllMessages()
        {
            return this.messageRepository.GetAll();
        }

        public void MarkAsRead(Message message)
        {
            message.IsRead = true;
            this.messageRepository.Update(message);
            this.unitOfWork.SaveChanges();
        }

        public void SaveMessage(Message message)
        {
            this.messageRepository.Add(message);
            this.unitOfWork.SaveChanges();
        }
    }
}
