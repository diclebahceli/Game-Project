using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IGun
{
    private Vector3 mousePosition;
    public GameObject cross;
    public GameObject bullet;
    public GameObject player;
    public static Vector3 targetDirection;
    [SerializeField] private GameObject[] spawnPoints;
    private bool canShoot = true;

    [SerializeField] private GameObject reloadIcon;
    // Update is called once per frame
    void Update()
    {
        GetMousePos();
        PutGun();
        PutCross();
        RotateGun();
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
            canShoot = false;
            StartCoroutine("Reload");
        }

    }
    //Creates bullets when you click the mouse
    //Creates bulet on the position that you were in
    public void Shoot()
    {
        Instantiate(bullet, spawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(bullet, spawnPoints[1].transform.position, Quaternion.identity);

    }

    public IEnumerator Reload()
    {
        reloadIcon.SetActive(true);
        //reload time
        yield return new WaitForSeconds(1f);
        canShoot = true;
        reloadIcon.SetActive(false);

    }

    public void RotateGun()
    {
        targetDirection = mousePosition - transform.position;
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
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, transform.position.z));
    }
}
