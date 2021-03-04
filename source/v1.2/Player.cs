using System.Collections.Generic;

using InnerPlayerControl = FFGALNAPKCD;
namespace SheriffMod
{
    public class Player
    {
        public InnerPlayerControl playerdata;
        public List<PlayerComponent> components;
        public int PlayerId;

        public Player(InnerPlayerControl playerdata)
        {
            this.playerdata = playerdata;
            components = new List<PlayerComponent>();
            this.PlayerId = playerdata.PlayerId;

        }
        public void Update() {
            foreach(PlayerComponent component in components)
            {

                component.Update();

            }
        }
        public void RemoveComponent(PlayerComponent comp)
        {
            comp.Destroy();
            components.Remove(comp);
        }
        public PlayerComponent GetComponentByName(string name)
        {

            if (components == null) return null;
            foreach (PlayerComponent component in components)
            {
                if (component.name == name)
                {
                    return component;
                }
            }
            return null;
        }
        public bool hasComponent(string name)
        {
            foreach (PlayerComponent component in components)
            {
                if (component != null)
                {
                    if (component.name == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isImpostor()
        {
            return playerdata.NDGFFHMFGIG.DAPKNDBLKIA;

        }

        public bool isAlive()
        {
            if (playerdata != null)
            {
                return !playerdata.NDGFFHMFGIG.DLPCKPBIJOE;
            }
            return false;
        }
    }
}
