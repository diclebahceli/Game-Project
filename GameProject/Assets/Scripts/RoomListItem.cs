using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
   [SerializeField] TMP_Text text;
   RoomInfo info;
    public void setUp(RoomInfo _info){
        info =_info;
        text.text =_info.Name;
    }

    public void OnClick(){
        RoomManager.Instance.JoinRoom(info);
    }
}