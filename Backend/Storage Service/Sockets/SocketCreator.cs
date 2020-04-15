﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Model;
using Microsoft.CSharp.RuntimeBinder;

namespace Song_Service.Sockets
{
    public class SocketCreator
    {

        //TODO add datamapper to map Song proxy to songDTO, which is suitable for streaming

        //TODO send create SongDTO which has base64 string content and some other info
        public void StreamSong(Song song, string clientIP)
        {
            //  FileTransfer fileTransfer = new FileTransfer();
            // fileTransfer.Name = "TestFile";
            var content = System.Convert.ToBase64String(File.ReadAllBytes(song.FilePath));

            // var type = (song as IProxyTargetAccessor)?.DynProxyGetTarget().GetType();

            byte[] bytes = System.IO.File.ReadAllBytes(song.FilePath);
            byte[] sendBytes = Encoding.UTF8.GetBytes(content);


            var type2 = ProxyUtil.GetUnproxiedType(song);

            //  var songObject = UnwrapProxy<Song>(song);
            // System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(content.GetType());
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

            }
            finally
            {
                client.Close();
            }


            //

        }

        internal static TType UnwrapProxy<TType>(TType proxy)
        {
            if (!ProxyUtil.IsProxy(proxy))
                return proxy;

            try
            {
                dynamic dynamicProxy = proxy;
                return dynamicProxy.__target;
            }
            catch (RuntimeBinderException)
            {
                return proxy;
            }
        }
    }
}
