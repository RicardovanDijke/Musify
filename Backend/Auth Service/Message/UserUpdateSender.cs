using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using User_Service.Entities;

namespace User_Service.Message
{
    public class UserUpdateSender : IUserUpdateSender
    {
        private string _hostName;
        private string _username;
        private string _password;
        private string _queueName;

        public void SendUser(User user)
        {
            var factory = new ConnectionFactory() { HostName = _hostName, UserName = _username, Password = _password };

            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, false, false, false, null);

                var jsonUser = JsonConvert.SerializeObject(user);
                var body = Encoding.UTF8.GetBytes(jsonUser);

                channel.BasicPublish("", _queueName, null, body);

            }
        }
    }
}
