
namespace SheriffMod
{
    public abstract class PlayerComponent
    {
        public string name;
        public Player parent;

        protected PlayerComponent(Player player)
        {
            this.parent = player;
        }

        public abstract void Update();

        public virtual void Destroy()
        {
            //
        }
    }
}
