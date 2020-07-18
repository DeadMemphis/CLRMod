using System;
using System.Net.Sockets;

namespace ExitGames.Client.Photon
{
    public class PingMono : PhotonPing
    {
        private Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public override bool StartPing(string ip)
        {
            base.Init();
            try
            {
                this.sock.ReceiveTimeout = 5000;
                this.sock.Connect(ip, 5055);
                this.PingBytes[this.PingBytes.Length - 1] = this.PingId;
                this.sock.Send(this.PingBytes);
                this.PingBytes[this.PingBytes.Length - 1] = (byte)(this.PingId - 1);
            }
            catch (Exception value)
            {
                this.sock = null;
                Console.WriteLine(value);
            }
            return false;
        }

        public override bool Done()
        {
            bool result;
            if (this.GotResult || this.sock == null)
            {
                result = true;
            }
            else if (this.sock.Available <= 0)
            {
                result = false;
            }
            else
            {
                int num = this.sock.Receive(this.PingBytes, SocketFlags.None);
                if (this.PingBytes[this.PingBytes.Length - 1] != this.PingId || num != this.PingLength)
                {
                    this.DebugString += " ReplyMatch is false! ";
                }
                this.Successful = (num == this.PingBytes.Length && this.PingBytes[this.PingBytes.Length - 1] == this.PingId);
                this.GotResult = true;
                result = true;
            }
            return result;
        }

        public override void Dispose()
        {
            try
            {
                this.sock.Close();
            }
            catch
            {
            }
            this.sock = null;
        }
    }
}
