using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Service.Entities;

namespace User_Service.Message
{
    public interface IUserUpdateSender
    {
        public void SendUpdate(string queueName, User user);
    }
}
