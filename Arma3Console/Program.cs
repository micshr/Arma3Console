using SourceProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arma3Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var playersMasterList = new List<PlayerInfo>();

            while (!Console.KeyAvailable)
            {
                try
                {
                    byte[] serverInfoResponse = Request.Send(args[0], 2303, Request.A2S_INFO_REQUEST).Result;

                    ServerInfo si = ServerInfo.Parse(serverInfoResponse);

                    byte[] challengeInfoResponse = Request.Send(args[0], 2303, Request.A2S_SERVERQUERY_GETCHALLENGE_REQUEST).Result;

                    ChallengeInfo ci = ChallengeInfo.Parse(challengeInfoResponse);

                    byte[] playerInfoResponse = Request.Send(args[0], 2303, ci.Challenge).Result;

                    List<PlayerInfo> players = PlayerInfo.Parse(playerInfoResponse);

                    Console.WriteLine(si.ToString());

                    foreach (PlayerInfo pi in players)
                    {
                        bool has = playersMasterList.Any(p => p.Name == pi.Name);

                        if (!has)
                        {
                            // new player play sound.
                            Console.Beep();
                        }
                        Console.WriteLine(pi.ToString());
                    }

                    playersMasterList.Clear();

                    foreach (PlayerInfo pi in players)
                    {
                        playersMasterList.Add(pi);
                    }
                }
                catch (Exception) { }

                Thread.Sleep(10000);

                Console.Clear();
            }

        }
    }
}
