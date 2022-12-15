
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private Vector2 movement;
    private bool hasGun;
    [SerializeField] private Animator animator;
    private int lifeCount;
    private Vector2 mousePos;
    private PhotonView photonView;
    [SerializeField] Camera playerCamera;
    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        hasGun = true;
        lifeCount = 5;
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(playerCamera.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimatorParameters();
        if (!photonView.IsMine)
        {
            return;
        }
        CheckLeftRight();


    }
    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Move();
    }

    void CheckLeftRight()
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
    }

    void CheckAnimatorParameters()
    {
        if (rb.velocity != Vector2.zero)
        {
            animator.SetFloat("speedChecker", 1);
        }
        else
        {
            animator.SetFloat("speedChecker", 0);
        }

        if (hasGun)
        {
            animator.SetBool("hasGun", true);
        }
        else
        {
            animator.SetBool("hasGun", false);
        }
    }

    void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(xAxis, yAxis).normalized;


        movement = dir * speed * Time.deltaTime;
        rb.velocity = movement;
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
}
