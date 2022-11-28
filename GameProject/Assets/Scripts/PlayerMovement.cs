
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int lifeCount;
    public Rigidbody2D rb;
    public float speed;
    private Vector2 movement;
    [SerializeField] private Animator animator;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lifeCount = 5;

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
            0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
            180, transform.rotation.z);
        }


        if (rb.velocity != Vector2.zero)
        {
            animator.SetFloat("speedChecker", 1);
        }
        else
        {
            animator.SetFloat("speedChecker", 0);
        }
    }

    public void takeHit(int damage)
    {
        lifeCount -= damage;
        if (lifeCount < 0)
        {
            animator.SetBool("dead", true);
            Destroy(gameObject, 1f);
        }
    }
    public void takeHit()
    {
        lifeCount--;
        if (lifeCount < 0)
        {
            animator.SetBool("dead", true);
            Destroy(gameObject, 1f);
        }

    }

    void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(xAxis, yAxis).normalized;

        movement = dir * speed * Time.deltaTime;
        rb.velocity = movement;

    }

}
