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
        public Int32 Score { get; private set; }
        public float Playtime { get; private set; }

        public override string ToString()
        {
            TimeSpan duration = new TimeSpan(0, 0, (int)Playtime);
            return "Name: " + Name + "\n" +
                "Score:" + Score + "\n" +
                "Playtime:" + duration.Hours + "h:"+duration.Minutes +"min:" + duration.Seconds+"s";
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
                pi.Score = sbr.ReadInt32();
                pi.Playtime = sbr.ReadFloat();
                players.Add(pi);
            }


            return players;
        }
    }
}
