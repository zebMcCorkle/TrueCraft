﻿using System;
using TrueCraft.API.Networking;
using System.Net;
using System.Collections.Generic;
using TrueCraft.API.World;
using TrueCraft.API.Logging;
using TrueCraft.API.Logic;

namespace TrueCraft.API.Server
{
    /// <summary>
    /// Called when the given packet comes in from a remote client. Return false to cease communication
    /// with that client.
    /// </summary>
    public delegate void PacketHandler(IPacket packet, IRemoteClient client, IMultiplayerServer server);

    public interface IMultiplayerServer
    {
        event EventHandler<ChatMessageEventArgs> ChatMessageReceived;
        event EventHandler<PlayerJoinedQuitEventArgs> PlayerJoined;
        event EventHandler<PlayerJoinedQuitEventArgs> PlayerQuit;

        IAccessConfiguration AccessConfiguration { get; }
        IPacketReader PacketReader { get; }
        IList<IRemoteClient> Clients { get; }
        IList<IWorld> Worlds { get; }
        IList<IEntityManager> EntityManagers { get; }
        IEventScheduler Scheduler { get; }
        IBlockRepository BlockRepository { get; }
        ICraftingRepository CraftingRepository { get; }
        IItemRepository ItemRepository { get; }
        IPEndPoint EndPoint { get; }
        bool BlockUpdatesEnabled { get; set; }
        bool EnableClientLogging { get; set; }

        void Start(IPEndPoint endPoint);
        void Stop();
        void RegisterPacketHandler(byte packetId, PacketHandler handler);
        void AddWorld(IWorld world);
        void AddLogProvider(ILogProvider provider);
        void Log(LogCategory category, string text, params object[] parameters);
        IEntityManager GetEntityManagerForWorld(IWorld world);
        void SendMessage(string message, params object[] parameters);

        void DisconnectClient(IRemoteClient client);

        bool PlayerIsWhitelisted(string client);
        bool PlayerIsBlacklisted(string client);
        bool PlayerIsOp(string client);
    }
}
