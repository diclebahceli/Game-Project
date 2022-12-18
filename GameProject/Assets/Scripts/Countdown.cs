using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Countdown : MonoBehaviour
{
    public int remainingTime;
    [SerializeField] TMP_Text countDownText;


    private void Awake()
    {
        remainingTime = 10;
    }

    private void Start()
    {
        StartCoroutine(nameof(CountDown));
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator CountDown()
    {
        for (remainingTime = 10; remainingTime >= 0; remainingTime--)
        {
            print(remainingTime);
            countDownText.text = remainingTime.ToString();
            yield return new WaitForSeconds(1f);
        }
    }
}
