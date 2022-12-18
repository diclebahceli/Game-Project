using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed;
    private Vector2 movement;
    private bool hasGun;
    [SerializeField] private int lifeCount;
    private Vector2 mousePos;



    private Animator animator;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TMP_Text usernameText;
    private PhotonView PV;

    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        hasGun = true;
        lifeCount = 5;
        animator = GetComponent<Animator>();
        usernameText.text = PhotonNetwork.NickName;

    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(usernameText);
            Destroy(healthBar);
            Destroy(playerCamera.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimatorParameters();
        if (!PV.IsMine)
        {
            return;
        }
        CheckLeftRight();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PV.RPC(nameof(doubleSize), RpcTarget.AllBuffered);
        }


    }
    void FixedUpdate()
    {
        if (!PV.IsMine)
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

    public void takeHit()
    {
        PV.RPC("takeDamage", RpcTarget.All);
    }

    [PunRPC]
    public void takeDamage()
    {
        animator.SetTrigger("hitTrigger");
        healthBar.GetComponent<Slider>().value -= 1;
        lifeCount--;
        if (lifeCount <= 0)
        {
            playerCamera.GetComponent<CameraMovement>().setPlayerDead(true);
            healthBar.SetActive(false);
            usernameText.gameObject.SetActive(false);
            animator.SetBool("dead", true);
            StartCoroutine("killAfter");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {

            PhotonNetwork.Destroy(other.gameObject);
            print("AAAAAAAAAAAAA TAKING DAMAGE");
            PV.RPC("takeDamage", RpcTarget.All);
        }
    }

    IEnumerator killAfter()
    {
        yield return new WaitForSeconds(2f);
        print("GONNNA DIEE NOWWWW");
        gameObject.SetActive(false);
    }


    //rpc ile setactive i control etmeye calis
    [PunRPC]
    void doubleSize()
    {
        this.transform.localScale = new Vector3(2, 2, 2);
    }
}
