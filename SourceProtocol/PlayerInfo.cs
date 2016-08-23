using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceProtocol
{
    public class PlayerInfo
    {
        public string Name { get; private set; }
        //public Int32 Score { get; private set; }
        //public Int32 Score { get; private set; }

        public override string ToString()
        {
            return "Name: " + Name;
        }

        public static List<PlayerInfo> Parse(byte[] playerInfo)
        {
            var players = new List<PlayerInfo>();
            var sbr = new SourceByteReader(playerInfo);

            sbr.ReadBytes(5);

            byte playerCount = sbr.ReadByte();
            
            for(byte i=0; i < playerCount; i++)
            {
                sbr.ReadByte();
                var pi = new PlayerInfo();
                pi.Name = sbr.ReadString();
                sbr.ReadBytes(8);
                players.Add(pi);
            }


            return players;
        }
    }
}
