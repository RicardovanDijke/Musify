using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;

namespace Song_Service.Sockets
{
    public class SocketManager
    {

        //List<Task>

        private SocketCreator socketCreator;

        public SocketManager()
        {
            socketCreator = new SocketCreator();
        }

        public void AddSocket(Song song, string clientIP)
        {
            socketCreator.StreamSong(song,clientIP);
        }
    }
}
