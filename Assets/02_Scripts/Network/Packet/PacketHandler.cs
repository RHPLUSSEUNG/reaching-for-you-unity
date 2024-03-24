using DummyClient;
using ServerCore;
using System;


class PacketHandler
{
    public static void PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        PlayerInfoReq p = packet as PlayerInfoReq;
        Console.WriteLine($"PlayerInfoReq : {p.playerId} {p.playerName}");

        foreach(PlayerInfoReq.Skill skill in p.skills)
        {
            Console.WriteLine($"Skill : {skill.skillId} {skill.duration} {skill.level}");
        }
    }

    public static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;

        //Console.WriteLine($"{chatPacket.playerId} : {chatPacket.chat}");
    }
}