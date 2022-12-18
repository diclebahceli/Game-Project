using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerToFollow;
    private PhotonView photonView;

    private bool isPlayerDead;
    void Awake()
    {
        isPlayerDead = false;
        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!isPlayerDead)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        transform.position = new Vector3(playerToFollow.transform.position.x, playerToFollow.transform.position.y, transform.position.z);
    }
    public void setPlayerDead(bool isDead)
    {
        isPlayerDead = isDead;
        this.transform.position = new Vector3(0, 0, -10);
    }
}
