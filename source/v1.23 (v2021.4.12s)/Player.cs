using System.Collections.Generic;


namespace SheriffMod
{
    public class Player
    {
        public PlayerControl playerdata;
        public List<PlayerComponent> components;
        public int PlayerId;

        public Player(PlayerControl playerdata)
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
            if (playerdata != null)
            {
                return playerdata.FIMGDJOCIGD.FDNMBJOAPFL;
            }
            return false;
        }

        public bool isAlive()
        {
            if (playerdata != null)
            {
                return !playerdata.FIMGDJOCIGD.IAGJEKLJCCI;
            }
            return false;
        }
    }
}
