using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Vector3 mousePosition;
    public GameObject cross;
    public GameObject bullet;
    public GameObject player;
    [SerializeField] private GameObject child;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, transform.position.z));
        //Change the crosshair position belongs to the mouse position
        cross.transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }

        Vector3 targetDirection = mousePosition - transform.position;
        float rotateZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(this.transform.rotation.x,transform.rotation.y,rotateZ);
        
        this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y-(0.7f),player.transform.position.z);
        
        
        Vector3 localScale = new Vector3(0.8f,0.8f,1);
        if(rotateZ>90 || rotateZ<-90){
            localScale.y = -0.8f;
        }else{
            localScale.y = +0.8f;
        }
        this.transform.localScale = localScale;


    }
    //Creates bullets when you click the mouse
    //Creates bulet on the position that you were in
    private void shoot()
    {
        Instantiate(bullet, child.transform.position, Quaternion.identity);
    }
}
