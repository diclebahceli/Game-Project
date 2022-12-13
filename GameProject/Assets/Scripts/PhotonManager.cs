using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    private string currentUserName;
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private GameObject lobbyPanel;
    // Start is called before the first frame update
    void Start()
    {
        currentUserName = PhotonNetwork.NickName;
        print("current user name " + currentUserName);
    }

    // Update is called once per frame
    public void OnClickCreate()
    {
        if (roomName.text.Length > 0)
        {
            PhotonNetwork.CreateRoom(roomName.text,
             new RoomOptions() { MaxPlayers = 2 });
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);

    }

}
