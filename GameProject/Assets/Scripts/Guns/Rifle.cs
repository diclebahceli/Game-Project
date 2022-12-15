using System;
using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IGun
{
    private Vector3 mousePosition;
    public GameObject cross;
    public GameObject player;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Camera playerCamera;

    private int bulletCount = 0;

    [SerializeField] private GameObject reloadIcon;
    private PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cross.SetActive(photonView.IsMine);

    }
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        GetMousePos();
        PutGun();
        PutCross();
        RotateGun();
        if (Input.GetMouseButtonDown(0) && bulletCount < 3)
        {
            Shoot();
            if (bulletCount == 3)
            {
                StartCoroutine("Reload");
            }
        }

    }
    //Creates bullets when you click the mouse
    //Creates bulet on the position that you were in
    public void Shoot()
    {
        PhotonNetwork.Instantiate("bullet", spawnPoint.transform.position, Quaternion.identity);
        bulletCount = bulletCount + 1;
        print("new " + bulletCount);
    }

    public IEnumerator Reload()
    {
        reloadIcon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        bulletCount = 0;
        reloadIcon.SetActive(false);
    }

    public void RotateGun()
    {
        Vector3 targetDirection = mousePosition - transform.position;
        float rotateZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(this.transform.rotation.x, transform.rotation.y, rotateZ);

        Vector3 localScale = new Vector3(0.8f, 0.8f, 1);
        if (rotateZ > 90 || rotateZ < -90)
        {
            localScale.y = -0.8f;
        }
        else
        {
            localScale.y = +0.8f;
        }
        this.transform.localScale = localScale;
    }

    public void PutGun()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (0.6f), player.transform.position.z);

    }

    public void PutCross()
    {
        //Change the crosshair position belongs to the mouse position
        cross.transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    public void GetMousePos()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, transform.position.z));
    }
}
