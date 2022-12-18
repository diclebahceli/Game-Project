using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomName;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;

    [SerializeField] GameObject startGameButton;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {

        PhotonNetwork.AutomaticallySyncScene = true;
        GetUserName();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.openMenu("TitleMenu");

    }

    void GetUserName()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Username") && result.Data.ContainsKey("Coin"))
        {
            PhotonNetwork.NickName = result.Data["Username"].Value;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void OnError(PlayFabError error)
    {
    }
    public void CreateRoom()
    {
        if (roomNameInputField.text.Length <= 0)
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.openMenu("LoadingMenu");
    }


    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.openMenu("LoadingMenu");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.openMenu("ErrorMenu");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.openMenu("Room");
        roomName.text = PhotonNetwork.CurrentRoom.Name;


        foreach (Transform trans in playerListContent)
        {
            Destroy(trans.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).
                            GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //When host of room quits we need to set start button active for the new host
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }



    public void StartGame()
    {
        PhotonNetwork.LoadLevel(4);

    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.openMenu("LoadingMenu");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.openMenu("TitleMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            //we do this check because when a room is empty photon doesn't
            //actually removes the room instead changes its removed bool value 
            // to true
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).
                            GetComponent<RoomListItem>().setUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).
                        GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
