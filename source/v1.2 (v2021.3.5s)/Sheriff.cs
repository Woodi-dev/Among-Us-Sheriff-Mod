using System;

using UnityEngine;




using PlayerInfo = GameData.GOOIGLGKMCE;
using PhysicsHelpers = BFDDHAKIDJF;
using Constants = DPHADHMAPCB;
using GameOptionsData = PAMOPBEDCNI;
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
        public override void Update()
        {
            if (PlayerControl.AllPlayerControls.Count > 1)
            {
                if(parent==PlayerController.LocalPlayer || CustomGameOptions.showSheriff)
                {
                   parent.playerdata.nameText.Color = color;

                }

                if (parent == PlayerController.LocalPlayer)
                {

                    if (!parent.isAlive())
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(false);
                        HudManagerPatch.KillButton.isActive = false;
                    }
                    else 
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(true);
                        HudManagerPatch.KillButton.isActive = true;
                        if (parent.playerdata.MPEOHLJNPOB)
                        {
                            killTimer = Math.Max(0,killTimer-Time.deltaTime);
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
                else if (PlayerControl.LocalPlayer.PKMHEDAKKHE.LGEGJEHCFOG)
                {
                    if (PlayerControl.LocalPlayer.PKMHEDAKKHE.AKOHOAJIHBE)
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(false);
                        HudManagerPatch.KillButton.isActive = false;
                    }
                    else
                    {
                        HudManagerPatch.KillButton.gameObject.SetActive(true);
                        HudManagerPatch.KillButton.isActive = true;
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
            float num = GameOptionsData.EEPBOJKJCAJ[Mathf.Clamp(PlayerControl.GameOptions.GEMCDKBIFGG, 0, 2)];
            if (!ShipStatus.Instance)
            {
                return null;
            }
            Vector2 truePosition = player.GetTruePosition();
            var allPlayers = GameData.Instance.AllPlayers;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.HGCENMAGBJO && playerInfo.FMAAJCIEMEH != player.PlayerId && !playerInfo.AKOHOAJIHBE && !playerInfo.IBJBIALCEKB.inVent)
                {
                    PlayerControl obj = playerInfo.IBJBIALCEKB;
                    if (obj)
                    {
                        Vector2 vector = obj.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.OEEHJJNGMLJ(truePosition, vector.normalized, magnitude, Constants.EOJPPJKOKFH))
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
