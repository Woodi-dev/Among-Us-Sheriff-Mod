using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace SheriffMod
{
    public class Player
    {
        public FFGALNAPKCD playerdata;
        public bool isSheriff;

        public Player(FFGALNAPKCD playerdata)
        {
            this.playerdata = playerdata;
            isSheriff = false;


        }
        public void Update()
        {
            if (isSheriff & (CustomGameOptions.showSheriff | this == PlayerController.getLocalPlayer()))
            {
                playerdata.nameText.Color = new Color(48 / 255.0f, 223 / 255.0f, 48 / 255.0f);

            }
        }
    }
}
