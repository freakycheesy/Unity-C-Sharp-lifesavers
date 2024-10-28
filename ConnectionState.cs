using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConnectionState : MonoBehaviourPunCallbacks
{
    public TMP_Text[] connectionTexts;
    public string connectionState;

    public override void OnConnectedToMaster() {
        connectionState = "Connected To Master";
        ChangeConnectionTexts(Color.green);
        base.OnConnected();
    }

    public override void OnCreatedRoom() {
        connectionState = "Created Room";
        ChangeConnectionTexts(Color.green);
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom() {
        connectionState = "Joined Room";
        ChangeConnectionTexts(Color.green);
        base.OnJoinedRoom();
    }

    public override void OnLeftRoom() {
        connectionState = "Left Room";
        ChangeConnectionTexts(Color.red);
        base.OnLeftRoom();
    }

    public override void OnErrorInfo(ErrorInfo errorInfo) {
        connectionState = $"Error: {errorInfo}";
        base.OnErrorInfo(errorInfo);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        connectionState = $"Creating Room Failed, Return Code: {returnCode}, Message: {message}";
        ChangeConnectionTexts(Color.red);
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnDisconnected(DisconnectCause cause) {
        connectionState = $"Disconnected, Cause: {cause}";
        ChangeConnectionTexts(Color.red);
        base.OnDisconnected(cause);
    }

    public override void OnConnected() {
        connectionState = "Connected";
        ChangeConnectionTexts(Color.green);
        base.OnConnected();
    }


    private void Start() {
        if (PhotonNetwork.IsConnected) {
            connectionState = "Online";
        }
        else
        {
            connectionState = "Offline";
        }
    }

    private void ChangeConnectionTexts(Color newColor) {
        if (connectionTexts == null)
            return;
        foreach (var text in connectionTexts) {
            text.text = connectionState;
            text.color = newColor;
        }
    }
}
