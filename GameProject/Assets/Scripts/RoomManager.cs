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
    [SerializeField] GameObject roomListItemPrefab;


    void Awake() {
        Instance = this;   
    }
     void Start()
    {
        GetUserName();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        
    }

    public override void OnJoinedLobby()
    {
        print("Joined lobby...");
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
   public void CreateRoom(){
        if(roomNameInputField.text.Length<=0){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.openMenu("LoadingMenu");
   }


   public override void OnJoinedRoom(){
        MenuManager.Instance.openMenu("Room");
        roomName.text = PhotonNetwork.CurrentRoom.Name;
   }

    
   public override void OnCreateRoomFailed(short returnCode, string message){
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.openMenu("ErrorMenu");
   }

   public void JoinRoom(RoomInfo info){
    PhotonNetwork.JoinRoom(info.Name);
    MenuManager.Instance.openMenu("LoadingMenu");
   }

   public void leaveRoom(){
    PhotonNetwork.LeaveRoom();
    MenuManager.Instance.openMenu("LoadingMenu");
   }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.openMenu("TitleMenu");
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        
        foreach(Transform trans in roomListContent){
                Destroy(trans.gameObject);
            }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab,roomListContent).
                            GetComponent<RoomListItem>().setUp(roomList[i]);
        }
    }
}
