using System.Collections.Generic;

namespace SheriffMod
{
    public class Player
    {
        public PlayerControl playerdata;
        public PlayerComponent component;
        public int PlayerId;

        public Player(PlayerControl playerdata)
        {
            this.playerdata = playerdata;
            this.PlayerId = playerdata.PlayerId;
        }

        public void setComponent(PlayerComponent component)
        {
            this.component = component;
        }

        public void Update() {
            if (component != null)
            {
                component.Update();
            }
        }

        public bool isSheriff()
        {
            return component != null && component.name == "Sheriff";
        }

        public bool isImposter()
        {
            return playerdata.Data.IsImpostor;
        }

        public bool isAlive()
        {
            return !playerdata.Data.IsDead;
        }
    }
}
