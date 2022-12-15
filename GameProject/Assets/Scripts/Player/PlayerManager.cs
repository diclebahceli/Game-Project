using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();
        }

    }

    void CreateController()
    {
        print("instantiated player controller");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
