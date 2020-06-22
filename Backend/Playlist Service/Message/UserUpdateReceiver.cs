using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Playlist_Service.Entities;
using Playlist_Service.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Playlist_Service.Message
{
    public class UserUpdateReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IPlaylistService _playlistService;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        public UserUpdateReceiver(IPlaylistService playlistService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _playlistService = playlistService;
            InitializeRabbitMqListener();
        }


        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "User.DisplayName", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "User.Deleted", durable: false, exclusive: false, autoDelete: false, arguments: null);
            //_channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            //todo add multiple consumers & channels, handlemethod for each (1 displaynameupdate 1 deleted)
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(content);

                switch (ea.RoutingKey)
                {
                    case "User.DisplayName":
                        {
                            var userDisplayNameUpdate = JsonConvert.DeserializeObject<UserDisplayNameUpdate>(content);
                            _playlistService.UpdateCreatorName(userDisplayNameUpdate);

                            break;
                        }
                    case "User.Deleted":
                        {
                            var userId = JsonConvert.DeserializeObject<UserDisplayNameUpdate>(content).UserId;

                            _playlistService.DeletePlaylistsByCreatorId(userId);
                            break;
                        }
                }



                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("User.DisplayName", false, consumer);
            /// _channel.
            _channel.BasicConsume("User.Deleted", false, consumer);

            return Task.CompletedTask;
        }

        //private void HandleMessage(UpdateCustomerFullNameModel updateCustomerFullNameModel)
        //{
        //    _customerNameUpdateService.UpdateCustomerNameInOrders(updateCustomerFullNameModel);
        //}

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
