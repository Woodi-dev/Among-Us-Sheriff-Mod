using System.Collections.Generic;

namespace SheriffMod
{
    public class PlayerController
    {
        public static List<Player> players = new List<Player>();
        public static Player LocalPlayer;

        public static void Update()
        {
            foreach (Player player in players)
            {
                player.Update();
            }
        }

        public static void InitPlayers()
        {
            players.Clear();
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                Player p = new Player(player);
                if (player.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    LocalPlayer = p;
                }
                players.Add(p);
            }
        }

        public static Player getPlayerById(byte id)
        {
            foreach (Player player in players)
            {
                if (player.PlayerId == id)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
