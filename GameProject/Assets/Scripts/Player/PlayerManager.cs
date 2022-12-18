using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView photonView;
    private GameObject player;
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

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }

    void CreateController()
    {
        print("instantiated player controller");
        player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
