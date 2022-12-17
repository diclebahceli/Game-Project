using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : MonoBehaviour, IGun
{
    private Vector3 mousePosition;
    public GameObject cross;
    public GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject spawnPoint;

    private bool canShoot = true;
    [SerializeField] private GameObject reloadIcon;

    private PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();

    }
    private void Start()
    {
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
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
            StartCoroutine("Reload");
        }

    }
    //Creates bullets when you click the mouse
    //Creates bulet on the position that you were in
    public void Shoot()
    {
        PhotonNetwork.Instantiate("bullet", spawnPoint.transform.position, Quaternion.identity);
        canShoot = false;
    }

    public IEnumerator Reload()
    {
        reloadIcon.SetActive(true);
        //reload time
        yield return new WaitForSeconds(0.8f);
        canShoot = true;
        reloadIcon.SetActive(false);
    }

    //rotate gun based on the mouse mosition
    public void RotateGun()
    {
        Vector3 targetDirection = mousePosition - transform.position;
        float rotateZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(this.transform.rotation.x, transform.rotation.y, rotateZ);

        // to make the gun straight in every angle
        Vector3 localScale = new Vector3(1f, 1f, 1);
        if (rotateZ > 90 || rotateZ < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        this.transform.localScale = localScale;
    }

    public void PutGun()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (0.6f), player.transform.position.z);

    }

    //Change the crosshair position belongs to the mouse position
    public void PutCross()
    {
        cross.transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    public void GetMousePos()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, transform.position.z));
    }

}
