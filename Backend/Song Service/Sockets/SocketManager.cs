using Song_Service.Entities;

namespace Song_Service.Sockets
{
    public class SocketManager
    {

        //List<Task>

        private readonly SocketCreator _socketCreator;

        public SocketManager()
        {
            _socketCreator = new SocketCreator();
        }

        public void AddSocket(Song song, string clientIp)
        {
            _socketCreator.StreamSong(song,clientIp);
        }
    }
}
