using Microsoft.Extensions.Hosting;

namespace Playlist_Service.Message
{
    public interface IUserUpdateReceiver
    {
        public void SendUpdate(string queueName, object content);
    }
}
