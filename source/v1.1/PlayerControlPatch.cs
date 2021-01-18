using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;

namespace SheriffMod
{
    enum CustomRPC
    {

        SetSheriff = 40,
        SyncCustomSettings = 41,
        SheriffKill = 42

    }
    enum RPC
    {

        PlayAnimation = 0,
        CompleteTask = 1,
        SyncSettings = 2,
        SetInfected = 3,
        Exiled = 4,
        CheckName = 5,
        SetName = 6,
        CheckColor = 7,
        SetColor = 8,
        SetHat = 9,
        SetSkin = 10,
        ReportDeadBody = 11,
        MurderPlayer = 12,
        SendChat = 13,
        StartMeeting = 14,
        SetScanner = 15,
        SendChatNote = 16,
        SetPet = 17,
        SetStartCounter = 18,
        EnterVent = 19,
        ExitVent = 20,
        SnapTo = 21,
        Close = 22,
        VotingComplete = 23,
        CastVote = 24,
        ClearVote = 25,
        AddVote = 26,
        CloseDoorsOfType = 27,
        RepairSystem = 28,
        SetTasks = 29,
        UpdateGameData = 30,


    }

    [HarmonyPatch]
    public static class PlayerControlPatch
    {
        public static FFGALNAPKCD closestPlayer = null;
        public static FFGALNAPKCD Sheriff;
        public static DateTime lastKilled;
        [HarmonyPatch(typeof(FFGALNAPKCD), "HandleRpc")]
        public static void Postfix(byte HKHMBLJFLMC, MessageReader ALMCIJKELCP)
        {
            try
            {
                switch (HKHMBLJFLMC)
                {

                    case (byte)CustomRPC.SetSheriff:
                        {

                            byte SheriffId = ALMCIJKELCP.ReadByte();
                            foreach (FFGALNAPKCD player in FFGALNAPKCD.AllPlayerControls)
                            {
                                if (player.PlayerId == SheriffId)
                                {
                                    Sheriff = player;
                                    if (CustomGameOptions.showSheriff)
                                    {
                                        player.nameText.Color = new Color(1, (float)(204.0 / 255.0), 0, 1);
                                    }
                                }
                            }
                            break;
                        }
                    case (byte)CustomRPC.SyncCustomSettings:
                        {
                            CustomGameOptions.showSheriff = ALMCIJKELCP.ReadBoolean();
                            CustomGameOptions.SheriffKillCD = System.BitConverter.ToSingle(ALMCIJKELCP.ReadBytes(4).ToArray(), 0);
                            break;
                        }
                    case (byte)CustomRPC.SheriffKill:
                        {
                            FFGALNAPKCD killer = PlayerControlPatch.getPlayerById(ALMCIJKELCP.ReadByte());
                            FFGALNAPKCD target = PlayerControlPatch.getPlayerById(ALMCIJKELCP.ReadByte());
                            if (PlayerControlPatch.isSheriff(killer))
                            {
                                killer.MurderPlayer(target);
                            }
                            break;
                        }
                }
            }
            catch (Exception e) {
                SheriffMod.log.LogInfo("RPC error... possible reasons: Not all players in the lobby have installed the mod or Sheriff mod versions do not match");
            }
        }

        public static bool isSheriff(FFGALNAPKCD player)
        {
            if (Sheriff == null) return false;
            return player.PlayerId == Sheriff.PlayerId;
        }

        public static FFGALNAPKCD getPlayerById(byte id)
        {

            foreach (FFGALNAPKCD player in FFGALNAPKCD.AllPlayerControls)
            {
                if (player.PlayerId == id)
                {
                    return player;
                }
            }
            return null;
        }
        public static float SheriffKillTimer()
        {
            if (lastKilled == null) return 0;
            DateTime now = DateTime.UtcNow;
            TimeSpan diff = now - lastKilled;
            var KillCoolDown = CustomGameOptions.SheriffKillCD * 1000.0f;
            if (KillCoolDown - (float)diff.TotalMilliseconds < 0) return 0;
            else
            {
                return (KillCoolDown - (float)diff.TotalMilliseconds) / 1000.0f;

            }



        }
        public static List<FFGALNAPKCD> getCrewMates(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> infection)
        {

            List<FFGALNAPKCD> CrewmateIds = new List<FFGALNAPKCD>();
            foreach (FFGALNAPKCD player in FFGALNAPKCD.AllPlayerControls)
            {

                bool isInfected = false;
                foreach (EGLJNOMOGNP.DCJMABDDJCF infected in infection)
                {

                    if (player.PlayerId == infected.LAOEJKHLKAI.PlayerId)
                    {
                        isInfected = true;

                        break;
                    }

                }
                if (!isInfected)
                {
                    CrewmateIds.Add(player);
                }


            }
            return CrewmateIds;

        }

        public static FFGALNAPKCD getClosestPlayer(FFGALNAPKCD refplayer)
        {
            double mindist = double.MaxValue;
            FFGALNAPKCD closestplayer = null;
            foreach (FFGALNAPKCD player in FFGALNAPKCD.AllPlayerControls)
            {
                if (player.NDGFFHMFGIG.DLPCKPBIJOE) continue;
                if (player.PlayerId != refplayer.PlayerId)
                {

                    double dist = getDistBetweenPlayers(player, refplayer);
                    if (dist < mindist)
                    {
                        mindist = dist;
                        closestplayer = player;
                    }

                }

            }
            return closestplayer;

        }

        public static double getDistBetweenPlayers(FFGALNAPKCD player, FFGALNAPKCD refplayer)
        {
            var refpos = refplayer.GetTruePosition();
            var playerpos = player.GetTruePosition();

            return Math.Sqrt((refpos[0] - playerpos[0]) * (refpos[0] - playerpos[0]) + (refpos[1] - playerpos[1]) * (refpos[1] - playerpos[1]));
        }

        [HarmonyPatch(typeof(FFGALNAPKCD), "RpcSetInfected")]
        public static void Postfix(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> JPGEIBIBJPJ)
        {


            MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SetSheriff, Hazel.SendOption.None, -1);
            List<FFGALNAPKCD> crewmates = getCrewMates(JPGEIBIBJPJ);

     

            System.Random r = new System.Random();
            Sheriff = crewmates[r.Next(0, crewmates.Count)];
            if (CustomGameOptions.showSheriff)
            {
                Sheriff.nameText.Color = new Color(1, (float)(204.0 / 255.0), 0, 1);
            }
            byte SheriffId = Sheriff.PlayerId;

            writer.Write(SheriffId);

            FMLLKEACGIO.Instance.FinishRpcImmediately(writer);

        }

        [HarmonyPatch(typeof(FFGALNAPKCD), "MurderPlayer")]
        public static bool Prefix(FFGALNAPKCD __instance, FFGALNAPKCD CAKODNGLPDF)
        {
            if (Sheriff != null)
            {
                if (__instance.PlayerId == Sheriff.PlayerId)
                {
                    __instance.NDGFFHMFGIG.DAPKNDBLKIA = true;

                }
            }
            return true;
        }

        [HarmonyPatch(typeof(FFGALNAPKCD), "MurderPlayer")]
        public static void Postfix(FFGALNAPKCD __instance, FFGALNAPKCD CAKODNGLPDF)
        {

            if (Sheriff != null)
            {
                if (__instance.PlayerId == Sheriff.PlayerId)
                {

                    __instance.NDGFFHMFGIG.DAPKNDBLKIA = false;


                }
            }



        }

        [HarmonyPatch(typeof(FFGALNAPKCD), "RpcSyncSettings")]
        public static void Postfix(KMOGFLPJLLK IOFBPLNIJIC)
        {
            if (FFGALNAPKCD.AllPlayerControls.Count > 1)
            {
                MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SyncCustomSettings, Hazel.SendOption.None, -1);

                writer.Write(CustomGameOptions.showSheriff);
                writer.Write(CustomGameOptions.SheriffKillCD);
                FMLLKEACGIO.Instance.FinishRpcImmediately(writer);
            }

        }
    }
}
