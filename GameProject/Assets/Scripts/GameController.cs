using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public static GameController Instance;

    void Awake()
    {
        //SINGLETON PATTERN, make the object persistent across scenes
        //so object doesn't destroy and be recreated each scene change

        //if there is another instance of game controller
        if (Instance)
        {
            //we destroy this one
            Destroy(gameObject);
            return;
        }
        //if this instance is the only one
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 2)
        { //we're in the game scene
            PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        }

    }
}
