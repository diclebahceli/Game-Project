using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        print(col.gameObject.tag);
        if (col.gameObject.tag == "Bullet")
        {
           // col.gameObject.takeHit();
            Destroy(col.gameObject);
            
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
