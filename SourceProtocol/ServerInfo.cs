using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceProtocol
{
    public class ServerInfo
    {
        public byte Bots { get; private set; }
        public string GameName { get; private set; }
        public string Map { get; private set; }
        public string Mod { get; private set; }
        public byte PlayerCount { get; private set; }
        public string ServerName { get; private set; }

        public override string ToString()
        {
            string result  = "ServerName:" + ServerName + "\n";
            result += "Mod: " + Mod + "\n";
            result += "GameName: " + GameName + "\n";
            result += "Map: " + Map + "\n";            
            result += "PlayerCount: " + PlayerCount;
            //result += "Bots: " + Bots;

            return result;
        }

        public static ServerInfo Parse(byte[] serverInfo)
        {
            var si = new ServerInfo();
            var sbr = new SourceByteReader(serverInfo);

            sbr.ReadByte();
            sbr.ReadByte();
            sbr.ReadByte();
            sbr.ReadByte();
            sbr.ReadByte();
            sbr.ReadByte();

            si.ServerName = sbr.ReadString();
            si.Map = sbr.ReadString();
            si.Mod = sbr.ReadString();
            si.GameName = sbr.ReadString();

            sbr.ReadByte();
            sbr.ReadByte();

            si.PlayerCount = sbr.ReadByte();
            sbr.ReadByte();
            si.Bots = sbr.ReadByte();

            return si;
        }
    }
}
