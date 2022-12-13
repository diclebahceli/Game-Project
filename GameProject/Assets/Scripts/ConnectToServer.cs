using UnityEngine.SceneManagement;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
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
        SceneManager.LoadScene("MainMenu");
    }

    void GetUserName()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Username") && result.Data.ContainsKey("Coin"))
        {
            print("we are in ifffff");
            PhotonNetwork.NickName = result.Data["Username"].Value;
            print("nameee" + PhotonNetwork.NickName);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void OnError(PlayFabError error)
    {
    }
}
