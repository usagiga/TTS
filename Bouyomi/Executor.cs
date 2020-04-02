using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Usagiga.TTS.Bouyomi
{
    public class Executor : IDisposable
    {
        const string defaultHost = "127.0.0.1";
        const int defaultPort = 50001;

        /// <summary>
        /// Create `Executor`.
        /// Prepare connection to BouyomiChan at the same time.
        /// </summary>
        /// <param name="host">BouyomiChan host.</param>
        /// <param name="port">BouyomiChan port.</param>
        public Executor(string host = defaultHost, int port = defaultPort)
        {
            Connect(host, port);
        }

        /// <summary>
        /// Dispose `Executor` safely.
        /// </summary>
        public void Dispose()
        {
            if (client == null) return;
            client.Close();
        }

        private TcpClient client;

        /// <summary>
        /// Establish connection to BouyomiChan over TCP.
        /// </summary>
        /// <param name="host">BouyomiChan host.</param>
        /// <param name="port">BouyomiChan port.</param>
        public void Connect(string host = defaultHost, int port = defaultPort)
        {
            client = new TcpClient(host, port);
        }

        /// <summary>
        /// Play specific voice through BouyomiChan.
        /// </summary>
        /// <param name="ctx">Parameters of voice.</param>
        public void Play(Context ctx)
        {
            if (client == null) throw new Exception();

            var wordBytes = Encoding.UTF8.GetBytes(ctx.Word);
            Int32 wordLen = wordBytes.Length;

            using (var ns = client.GetStream())
            {
                using (var bw = new BinaryWriter(ns))
                {
                    bw.Write((Int16)0x0001); // Command (Reading message)
                    bw.Write((Int16)ctx.Speed);
                    bw.Write((Int16)ctx.Pitch);
                    bw.Write((Int16)ctx.Volume);
                    bw.Write((Int16)ctx.VoiceType);
                    bw.Write((byte)0); // UTF-8
                    bw.Write(wordBytes);
                    bw.Write(wordLen);
                }
            }
        }

        /// <summary>
        /// Record specific voice through BouyomiChan.
        /// The method is NOT implemented
        /// because there's no record feature in BouyomiChan API.
        /// </summary>
        /// <param name="ctx">Parameters of voice.</param>
        /// <param name="dest">Exporting path.</param>
        public void Record(Context ctx, string dest) => throw new NotImplementedException();
    }
}
