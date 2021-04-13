using System;

using UnityEngine;




using PlayerInfo = GameData.LGBOMGHJELL;
using PhysicsHelpers = FJFJIDCFLDJ;
using Constants = LNCOKMACBKP;
using GameOptionsData = CEIOGGEDKAN;
namespace SheriffMod
{
    public class Sheriff : PlayerComponent
    {
        public PlayerControl closestPlayer;
        public static Sheriff instance;
        public static Color color = new Color(1, (float)(204.0 / 255.0), 0, 1);
        public float killTimer;
        public Sheriff(Player player) : base(player)
        {
            name = "Sheriff";
            instance = this;
            killTimer = CustomGameOptions.SheriffKillCD - 10;
        }
        //handle Sheriff killbutton, nameText behaviour.
        public override void Update()
        {
            if (PlayerControl.AllPlayerControls.Count > 1)
            {
                if(parent==PlayerController.LocalPlayer || CustomGameOptions.showSheriff)
                {
                   parent.playerdata.nameText.color = color;

                }

                if (parent == PlayerController.LocalPlayer)
                {

                    if (!parent.isAlive() || MeetingHud.Instance)
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(false);
                        HudManagerPatch.KillButton.isActive = false;
                    }
                    else 
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(true);
                        HudManagerPatch.KillButton.isActive = true;
                        if (parent.playerdata.POECPOEKKNO) //canMove
                        {
                            killTimer = Math.Max(0,killTimer-Time.deltaTime); //reduce Cooldown
                            HudManagerPatch.KillButton.SetCoolDown(SheriffKillTimer(), CustomGameOptions.SheriffKillCD);
                            closestPlayer = FindClosestTarget(PlayerControl.LocalPlayer);
                            if (closestPlayer != null)
                            {
                                HudManagerPatch.KillButton.SetTarget(closestPlayer);
                            }
                            if (Input.GetKeyInt(KeyCode.Q))
                            {
                                HudManagerPatch.KillButton.PerformKill();
                            }
                        }
                    }
                }


            }
        }

        public float SheriffKillTimer()
        {
            if (killTimer <= 0) killTimer = 0;
            return killTimer;
        }
       
        public PlayerControl FindClosestTarget(PlayerControl player)
        {
            PlayerControl result = null;
            float num = GameOptionsData.DCAJHEGBLDD[Mathf.Clamp(PlayerControl.GameOptions.OCPGKHJJAHL, 0, 2)]; //killrange
            if (!ShipStatus.Instance)
            {
                return null;
            }
            Vector2 truePosition = player.GetTruePosition();
            var allPlayers = GameData.Instance.AllPlayers;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.MFFAGDHDHLO && playerInfo.FNPNJHNKEBK != player.PlayerId && !playerInfo.IAGJEKLJCCI && !playerInfo.GJPBCGFPMOD.inVent)
                {
                    PlayerControl obj = playerInfo.GJPBCGFPMOD;
                    if (obj)
                    {
                        Vector2 vector = obj.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.HLIEDNLNBBH(truePosition, vector.normalized, magnitude, Constants.LEOCDMEJGPA)) //in range and prevent killing through walls
                        {
                            result = obj;
                            num = magnitude;
                        }
                    }
                }
            }
            return result;


        }
    }
}
