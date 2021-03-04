using System;

using UnityEngine;

using InnerPlayerControl = FFGALNAPKCD;
using ShipStatus = HLBNNHFCNAJ;
using GameData = EGLJNOMOGNP;
using PlayerInfo = EGLJNOMOGNP.DCJMABDDJCF;
using PhysicsHelpers = LBKBHDOOGHL;
using Constants = CAMOCHFAHMA;
using GameOptionsData = KMOGFLPJLLK;
namespace SheriffMod
{
    public class Sheriff : PlayerComponent
    {
        public InnerPlayerControl closestPlayer;
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
            if (InnerPlayerControl.AllPlayerControls.Count > 1)
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
                        if (parent.playerdata.GEBLLBHGHLD)
                        {
                            killTimer = Math.Max(0,killTimer-Time.deltaTime);
                            HudManagerPatch.KillButton.SetCoolDown(SheriffKillTimer(), CustomGameOptions.SheriffKillCD);
                            closestPlayer = FindClosestTarget(InnerPlayerControl.LocalPlayer);
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
                else if (InnerPlayerControl.LocalPlayer.NDGFFHMFGIG.DAPKNDBLKIA)
                {
                    if (InnerPlayerControl.LocalPlayer.NDGFFHMFGIG.DLPCKPBIJOE)
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
       
        public InnerPlayerControl FindClosestTarget(InnerPlayerControl player)
        {
            InnerPlayerControl result = null;
            float num = GameOptionsData.JMLGACIOLIK[Mathf.Clamp(InnerPlayerControl.GameOptions.DLIBONBKPKL, 0, 2)];
            if (!ShipStatus.Instance)
            {
                return null;
            }
            Vector2 truePosition = player.GetTruePosition();
            var allPlayers = GameData.Instance.AllPlayers;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.OMHGJKAKOHO && playerInfo.JKOMCOJCAID != player.PlayerId && !playerInfo.DLPCKPBIJOE && !playerInfo.LAOEJKHLKAI.inVent)
                {
                    InnerPlayerControl obj = playerInfo.LAOEJKHLKAI;
                    if (obj)
                    {
                        Vector2 vector = obj.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.IIPMKCELMED(truePosition, vector.normalized, magnitude, Constants.BBHMKOHHIKI))
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
