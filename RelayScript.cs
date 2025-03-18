using System.Threading.Tasks;
using FishNet.Managing;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace FishNet.Transporting.UTP {
public class RelayScript : MonoBehaviour
{
    public bool hostOnStart = false;

    void Start()
    {
        if (hostOnStart)
        {
            StartHostWithRelay(5);
        }

        if (hostOnStart && NetworkManager.Instances[0].ServerManager.AnyServerStarted())
        {
            NetworkManager.Instances[0].ClientManager.StartConnection();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static async void ConnectToUnityServices()
    {
        await UnityServices.InitializeAsync();
    }


    public static async Task<string> StartHostWithRelay(int maxConnections = 5, bool supportWebGl = false)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        string connectionType = supportWebGl ? "wss" : "dtls";
        NetworkManager.Instances[0].GetComponent<FishyUnityTransport>().SetRelayServerData(allocation.ToRelayServerData(connectionType));
        NetworkManager.Instances[0].GetComponent<FishyUnityTransport>().UseWebSockets = supportWebGl;
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        return NetworkManager.Instances[0].ServerManager.StartConnection() ? joinCode : null;
    }

    public static async Task<bool> StartClientWithRelay(string joinCode, bool supportWebGl = false)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        string connectionType = supportWebGl ? "wss" : "dtls";
        NetworkManager.Instances[0].GetComponent<FishyUnityTransport>().SetRelayServerData(joinAllocation.ToRelayServerData(connectionType));
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Instances[0].ClientManager.StartConnection();
    }

    // Used for Unity Events
    public void StartHostWithRelay(int maxConnections = 5) => StartHostWithRelay(maxConnections, false);
    public void StartClientWithRelay(string joinCode) => StartClientWithRelay(joinCode, false);
}
}
