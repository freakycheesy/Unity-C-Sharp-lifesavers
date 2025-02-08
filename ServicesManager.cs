using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon;
using Unity.Netcode;
using Netcode.Transports.PhotonRealtime;
using System;

/* Used for connecting players over the network using netcode for gameobjects and photon realtime*/

public class ServicesManager : MonoBehaviour, IMatchmakingCallbacks
{
    public static ServicesManager Instance { get; private set;}

    //Server Properties
    public PhotonRealtimeTransport transport;
    public bool ServerPrivate = true;
    public bool ServerModded = false;
    private bool ServerCommunityHosted = Application.platform.Equals(RuntimePlatform.WindowsServer) || Application.platform.Equals(RuntimePlatform.LinuxServer);

    private const string ModdedKey = "md";
    private const string CommunityHostedKey = "ch";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        transport = (PhotonRealtimeTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        HostRoom();
    }

    private void HostRoom()
    {
        NetworkManager.Singleton.StartHost();
        transport.StartServer();
        transport.RoomName = Guid.NewGuid().ToString().Substring(0, 6);
        transport.Client.CurrentRoom.IsVisible = ServerPrivate;
        ExitGames.Client.Photon.Hashtable properties = new();
        properties.Add(ModdedKey, ServerModded);
        properties.Add(CommunityHostedKey, ServerCommunityHosted);
        transport.Client.CurrentRoom.SetCustomProperties(properties);
    }

    private void JoinRoom(string roomCode)
    {
        if (!string.IsNullOrEmpty(roomCode)) {
            EnterRoomParams roomParams = new EnterRoomParams();
            roomParams.RoomName = roomCode;
            transport.Client.OpJoinRoom(roomParams); 
        }
        else
        {
            transport.Client.OpJoinRandomRoom();
        }
    }

    private void StartClient(){
        NetworkManager.Singleton.StartClient();
        transport.StartClient();
    }

    void OnDisconnectedFromServer(){
        SceneManager.LoadScene(0);
        HostRoom();
    }

    public void CreateLobbyCommand() => HostRoom();
    public void JoinLobbyCommand() => JoinRoom("");
    //public void ListLobbiesCommand() => ListLobbies();
    public void ShutdownCommand(){
        NetworkManager.Singleton.Shutdown();
    }

    #region IMatchmakingCallbacks
    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        throw new NotImplementedException();
    }

    public void OnCreatedRoom()
    {
        throw new NotImplementedException();
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        throw new NotImplementedException();
    }

    public void OnJoinedRoom()
    {
        if (!NetworkManager.Singleton.IsServer) StartClient();
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        throw new NotImplementedException();
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        throw new NotImplementedException();
    }

    public void OnLeftRoom()
    {
        throw new NotImplementedException();
    }
    #endregion
}
