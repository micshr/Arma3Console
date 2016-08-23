using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceProtocol
{
    public class ChallengeInfo
    {
        public byte[] Challenge { get; private set; }

        public static ChallengeInfo Parse(byte[] challengeInfo)
        {
            var ci = new ChallengeInfo();

            var sbr = new SourceByteReader(challengeInfo);

            sbr.ReadBytes(5);

            byte[] result = new byte[5+4];
            Array.Copy(Request.A2S_SERVERQUERY_GETCHALLENGE_REQUEST, 0, result, 0, 5);

            Array.Copy(sbr.ReadBytes(4), 0, result, 5, 4);

            ci.Challenge = result;

            return ci;
        }
    }
}
