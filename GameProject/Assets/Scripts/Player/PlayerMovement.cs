using UnityEngine.SceneManagement;
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
    private bool canTakeDamage;



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
        canTakeDamage = true;

    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(usernameText);
            Destroy(healthBar);
            Destroy(playerCamera.gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            canTakeDamage = false;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        CheckAnimatorParameters();
        CheckLeftRight();

        if (lifeCount <= 0)
        {
            playerCamera.GetComponent<CameraMovement>().setPlayerDead(true);
            PV.RPC("Die", RpcTarget.AllBuffered);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 && PhotonNetwork.IsMasterClient && FindObjectOfType<Countdown>().remainingTime <= 0)
        {
            // PhotonNetwork.LoadLevel(3);
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
        if (PV.IsMine && canTakeDamage)
        {
            animator.SetTrigger("hitTrigger");
            healthBar.GetComponent<Slider>().value -= 1;
            lifeCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {

            other.gameObject.GetComponent<PhotonView>().RPC("Die", RpcTarget.AllBuffered);
            // PhotonNetwork.Destroy(other.gameObject);
            PV.RPC("takeDamage", RpcTarget.All);
        }
    }

    IEnumerator killAfter()
    {
        yield return new WaitForSeconds(2f);
        print("GONNNA DIEE NOWWWW");
        gameObject.SetActive(false);
    }


    [PunRPC]
    void Die()
    {
        animator.SetBool("dead", true);
        StartCoroutine("killAfter");
    }
}
