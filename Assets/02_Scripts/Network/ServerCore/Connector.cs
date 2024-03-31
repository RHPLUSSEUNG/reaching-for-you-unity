using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    public class Connector
    {
        Func<Session> _sessionFactory;

        public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
        {
            for (int i = 0; i < count; i++) 
            {
                //socket option setting
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                //session factory create
                _sessionFactory = sessionFactory;
                //socket asynchronos event argument setting
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += OnConnectedComplete;
                args.RemoteEndPoint = endPoint;
                args.UserToken = socket;

                RegisterConnect(args);
            }
        }

        void RegisterConnect(SocketAsyncEventArgs args)
        {
            Socket socket = args.UserToken as Socket;
            if(socket == null)
            {
                return;
            }
            //connecting request true => continue pending / false => success
            bool pending = socket.ConnectAsync(args);
            if(pending == false)
            {
                //connect success
                OnConnectedComplete(null, args);
            }
        }

        void OnConnectedComplete(object sender, SocketAsyncEventArgs args)
        {
            if(args.SocketError == SocketError.Success)
            {
                //session create
                Session session = _sessionFactory.Invoke();
                //session start
                session.Start(args.ConnectSocket);
                //session connect event
                session.OnConnected(args.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine($"OnConnectCompleted Fail : {args.SocketError}");
            }
        }
    }
}
