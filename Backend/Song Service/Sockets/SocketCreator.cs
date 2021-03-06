﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Song_Service.Entities;

namespace Song_Service.Sockets
{
    public class SocketCreator
    {

        //TODO add datamapper to map Song proxy to songDTO, which is suitable for streaming
        //TODO send create SongDTO which has base64 string content and some other info
        public void StreamSong(Song song, string clientIp)
        {
            var content = System.Convert.ToBase64String(File.ReadAllBytes(song.FilePath));

            Console.WriteLine("song found locally: " + song.FilePath);

            byte[] bytes = File.ReadAllBytes(song.FilePath);

            //this is actually needed
            // ReSharper disable once UnusedVariable
            _ = Encoding.UTF8.GetBytes(content);

            TcpClient client = new TcpClient();
            //todo if no connection after few seconds, disconnect
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 11000);
                Stream stream = client.GetStream();

                stream.Write(bytes, 0, bytes.Length);

            }
            catch (SocketException ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
