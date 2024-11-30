using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerCore
{
    public abstract class PacketSession : Session
    {
        //packet header size
        public static readonly int HeaderSize = 2;
        // Onreceive event
        public sealed override int OnReceive(ArraySegment<byte> buffer)
        {
            int processLen = 0;
            int packetCount = 0;
            while (true)
            {
                if (buffer.Count < HeaderSize)
                    break;

                ushort dataSize = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
                if (buffer.Count < dataSize)
                    break;

                OnReceivePacket(new ArraySegment<byte>(buffer.Array, buffer.Offset, dataSize));
                packetCount++;

                processLen += dataSize;
                buffer = new ArraySegment<byte>(buffer.Array, buffer.Offset + dataSize, buffer.Count - dataSize);
            }
            if(packetCount > 1)
            {
                Console.WriteLine($"Flushed : {packetCount}");
            }
            return processLen;
        }

        public abstract void OnReceivePacket(ArraySegment<byte> buffer);
    }

    public abstract class Session
    {
        Socket _socket;
        //connecting state 0 => connect / 1 => disconnect
        int _disconnected = 0;

        RecvBuffer _recvBuffer = new RecvBuffer(65535);

        object _lock = new object();
        Queue<ArraySegment<byte>> _sendQueue = new Queue<ArraySegment<byte>>();
        List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs _recvArgs = new SocketAsyncEventArgs();

        void Clear()
        {
            lock(_lock)
            {
                _sendQueue.Clear();
                _pendingList.Clear();
            }
        }

        //session interface
        public abstract void OnConnected(EndPoint endPoint);
        public abstract int OnReceive(ArraySegment<byte> buffer);
        public abstract void OnSend(int byteOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);

        public void Start(Socket socket)
        {
            _socket = socket;

            _recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
            _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

            RegisterRecv();
        }
        public void Send(ArraySegment<byte> sendBuff)
        {
            lock (_lock)
            {
                //sendBuff -> sendQueue
                _sendQueue.Enqueue(sendBuff);
                //pendingList empty (anything pending)
                if (_pendingList.Count == 0)
                {
                    RegisterSend();
                }
            }
        }

        public void Send(List<ArraySegment<byte>> sendBuffList)
        {
            lock (_lock)
            {
                if(sendBuffList.Count == 0)
                {
                    return;
                }
                foreach(ArraySegment<byte> sendBuff in sendBuffList)
                {
                    //sendBuff -> sendQueue
                    _sendQueue.Enqueue(sendBuff);
                }
                //pendingList empty (anything pending)
                if (_pendingList.Count == 0)
                {
                    RegisterSend();
                }
            }
        }

        public void Disconnect()
        {
            //connecting state confirm
            if (Interlocked.Exchange(ref _disconnected, 1) == 1)
            {
                return;
            }
            OnDisconnected(_socket.RemoteEndPoint);
            //socket closing
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            Clear();
        }
        #region Network Send
        void RegisterSend()
        {
            //disconnect while send
            if (_disconnected == 1)
            {
                return;
            }

                //exist waiting send
                while (_sendQueue.Count > 0)
            {
                //sendQueue -> pendingList
                ArraySegment<byte> buff = _sendQueue.Dequeue();
                _pendingList.Add(buff);
            }
            try
            {
                //argument Bufferlist setting pendingList
                _sendArgs.BufferList = _pendingList;
                //sending buffer in pendingList
                bool pending = _socket.SendAsync(_sendArgs);
                if (pending == false)
                {
                    //sending complete event
                    OnSendCompleted(null, _sendArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register Send Failed : {ex}");
            }
        }

        void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            lock (_lock)
            {
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    try
                    {
                        //reset list
                        _sendArgs.BufferList = null;
                        _pendingList.Clear();
                        //sending complete event
                        OnSend(_sendArgs.BytesTransferred);
                        //exist waiting sendBuff
                        if (_sendQueue.Count > 0)
                        {
                            //restart sending
                            RegisterSend();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"On Receive Data Failed {e}");
                    }
                }
                else
                {
                    //error expire
                    Disconnect();
                }
            }
        }
        #endregion
        #region Network Receive
        void RegisterRecv()
        {
            if (_disconnected == 1)
            {
                return;
            }
            //buffer clear
            _recvBuffer.Clear();
            //arguemnt buffer setting
            ArraySegment<byte> segment = _recvBuffer.FreeSegment;
            _recvArgs.SetBuffer(segment.Array, segment.Offset, segment.Count);
            try
            {
                bool pending = _socket.ReceiveAsync(_recvArgs);
                if (pending == false)
                {
                    //receive complete event
                    OnRecvCompleted(null, _recvArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register Recv Failed : {ex}");
            }
            //receive asynchronous  true => pending / false => success
            
        }
        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                //TODO
                try
                {
                    if(_recvBuffer.OnWrite(args.BytesTransferred) == false)
                    {
                        Disconnect();
                        return;
                    }
                    //receive process length
                    int processLen = OnReceive(_recvBuffer.DataSegment);
                    if (processLen < 0 || processLen > _recvBuffer.DataSize) 
                    {
                        Disconnect();
                        return;
                    }

                    if(_recvBuffer.OnRead(processLen) == false)
                    {
                        Disconnect();
                        return;
                    }
                    RegisterRecv();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Receive Data Failed {e}");
                }
            }
            else
            {
                Disconnect();
            }
        }
        #endregion
    }
}
