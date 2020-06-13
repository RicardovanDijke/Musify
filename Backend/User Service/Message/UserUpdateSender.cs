using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using User_Service.Entities;

namespace User_Service.Message
{
    public class UserUpdateSender : IUserUpdateSender
    {
        private string _hostname;
        private string _username;
        private string _password;

        public UserUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }


        public void SendUpdate(string queueName, User user)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);

                var jsonUser = JsonConvert.SerializeObject(user);
                var body = Encoding.UTF8.GetBytes(jsonUser);

                channel.BasicPublish("", queueName, null, body);

            }
        }
    }
}
