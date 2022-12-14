using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private bool hasGun;
    private int lifeCount;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hasGun = true;
        lifeCount = 5;

    }

    // Update is called once per frame
    void Update()
    {

        if (hasGun)
        {
            animator.SetBool("hasGun", true);
        }
        else
        {
            animator.SetBool("hasGun", false);
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
}
