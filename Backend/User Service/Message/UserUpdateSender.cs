using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using User_Service.Entities;

namespace User_Service.Message
{
    public class UserUpdateSender : IUserUpdateSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        public UserUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }


        public void SendUpdate(string queueName, User user)
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };

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
