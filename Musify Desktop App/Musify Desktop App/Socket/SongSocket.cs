using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;

namespace Musify_Desktop_App.Socket
{
    public static class SongSocket
    {
        //todo remove static
        public static void NewSongSocket(long songID)
        {
            var fileLocation = @$"C:\users\ricar\Desktop\Musify\temp\{songID}.mp3";

            if (!File.Exists(fileLocation))
            {

                int port = 11000;
                var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client connected");

                var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int numberOfBytesRead = 0;
                MemoryStream receivedData = new MemoryStream();
                do
                {
                    numberOfBytesRead = stream.Read(buffer, 0, buffer.Length); //Read from network stream
                    receivedData.Write(buffer, 0, buffer.Length); //Write to memory stream
                } while (stream.DataAvailable);

                // var i = new DirectoryInfo(@"C:\users\ricar\Desktop\temp").GetFiles().Length;

                listener.Stop();
                client.Close();

                File.WriteAllBytes(fileLocation, receivedData.ToArray());
                Debug.WriteLine("Song Received");

            }
            //todo remove file when done playing
        }
    }
}
