
using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

public interface IPacket
    {
        ushort Protocol { get; }
        void Read(ArraySegment<byte> segment);
        ArraySegment<byte> Write();
    }

public enum PacketID
{
    
    PlayerInfoReq = 1,
	
    S_Chat = 2,
	
    C_Chat = 3,
	
}


public class PlayerInfoReq : IPacket
{
    public long playerId;
	public string playerName;
	
	public struct Skill
	{
	    public int skillId;
		public short level;
		public float duration;
	
	public void Read(ReadOnlySpan<byte>span, ref ushort count)
	{
	    
			this.skillId = BitConverter.ToInt32(span.Slice(count, span.Length - count));
			count += sizeof(int);
			
			this.level = BitConverter.ToInt16(span.Slice(count, span.Length - count));
			count += sizeof(short);
			
			this.duration = BitConverter.ToSingle(span.Slice(count, span.Length - count));
			count += sizeof(float);
	}
	
	public bool Write(Span<byte> span, ref ushort count)
	{
	    bool success = true;
	    
			success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.skillId);
			count += sizeof(int);
			
			
			success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.level);
			count += sizeof(short);
			
			
			success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.duration);
			count += sizeof(float);
			
	    return success;
	}
	
	    
	}
	public List<Skill> skills = new List<Skill>();
	
    public ushort Protocol { get { return (ushort)PacketID.PlayerInfoReq; } }
    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
        count += sizeof(ushort);
        count += sizeof(ushort);

        
		this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));
		count += sizeof(long);
		
		ushort playerNameLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
		count += sizeof(ushort);
		this.playerName = Encoding.Unicode.GetString(span.Slice(count, playerNameLen));
		count += playerNameLen;
		
		
		this.skills.Clear();
		ushort skillLen = BitConverter.ToUInt16(span.Slice(count, span.Length-count));
		count += sizeof(ushort);
		for (int i = 0; i < skillLen; i++)
		{
		    Skill skill = new Skill();
		    skill.Read(span, ref count);
		    skills.Add(skill);
		}
		
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);

        bool success = true;
        ushort count = 0;

        Span<byte> span = new Span<byte>(openSegment.Array, openSegment.Offset, openSegment.Count);

        count += sizeof(ushort);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)PacketID.PlayerInfoReq);
        count += sizeof(ushort);

        
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.playerId);
		count += sizeof(long);
		
		
		ushort playerNameLen = (ushort)Encoding.Unicode.GetBytes(this.playerName, 0, this.playerName.Length, openSegment.Array, openSegment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), playerNameLen);
		count += playerNameLen;
		count += sizeof(ushort);
		
		
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)skills.Count);
		count += sizeof(ushort);
		foreach(Skill skill in skills)
		{
		    success &= skill.Write(span, ref count);
		}
		
        
        success &= BitConverter.TryWriteBytes(span, count);

        if (success == false)
        {
            return null;
        }
        return SendBufferHelper.Close(count);
    }
}

public class S_Chat : IPacket
{
    public long playerId;
	public string chat;
    public ushort Protocol { get { return (ushort)PacketID.S_Chat; } }
    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
        count += sizeof(ushort);
        count += sizeof(ushort);

        
		this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));
		count += sizeof(long);
		
		ushort chatLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(span.Slice(count, chatLen));
		count += chatLen;
		
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);

        bool success = true;
        ushort count = 0;

        Span<byte> span = new Span<byte>(openSegment.Array, openSegment.Offset, openSegment.Count);

        count += sizeof(ushort);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)PacketID.S_Chat);
        count += sizeof(ushort);

        
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.playerId);
		count += sizeof(long);
		
		
		ushort chatLen = (ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, openSegment.Array, openSegment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), chatLen);
		count += chatLen;
		count += sizeof(ushort);
		
        
        success &= BitConverter.TryWriteBytes(span, count);

        if (success == false)
        {
            return null;
        }
        return SendBufferHelper.Close(count);
    }
}

public class C_Chat : IPacket
{
    public long playerId;
	public string chat;
    public ushort Protocol { get { return (ushort)PacketID.C_Chat; } }
    public void Read(ArraySegment<byte> segment)
    {
        ushort count = 0;

        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
        count += sizeof(ushort);
        count += sizeof(ushort);

        
		this.playerId = BitConverter.ToInt64(span.Slice(count, span.Length - count));
		count += sizeof(long);
		
		ushort chatLen = BitConverter.ToUInt16(span.Slice(count, span.Length - count));
		count += sizeof(ushort);
		this.chat = Encoding.Unicode.GetString(span.Slice(count, chatLen));
		count += chatLen;
		
    }

    public ArraySegment<byte> Write()
    {
        ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);

        bool success = true;
        ushort count = 0;

        Span<byte> span = new Span<byte>(openSegment.Array, openSegment.Offset, openSegment.Count);

        count += sizeof(ushort);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), (ushort)PacketID.C_Chat);
        count += sizeof(ushort);

        
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), this.playerId);
		count += sizeof(long);
		
		
		ushort chatLen = (ushort)Encoding.Unicode.GetBytes(this.chat, 0, this.chat.Length, openSegment.Array, openSegment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), chatLen);
		count += chatLen;
		count += sizeof(ushort);
		
        
        success &= BitConverter.TryWriteBytes(span, count);

        if (success == false)
        {
            return null;
        }
        return SendBufferHelper.Close(count);
    }
}

