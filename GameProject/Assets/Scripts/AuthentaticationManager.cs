using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthentaticationManager : MonoBehaviour
{
    [SerializeField] TMP_Text messageTextLogin;
    [SerializeField] TMP_InputField emailInputLogin;
    [SerializeField] TMP_InputField passwordInputLogin;


    [SerializeField] TMP_Text messageTextRegister;
    [SerializeField] TMP_InputField emailInputRegister;
    [SerializeField] TMP_InputField passwordInputRegister;
    [SerializeField] TMP_InputField UsernameInputRegister;
    private bool loggedIn = false;


    private void Update()
    {
        if (loggedIn)
        {
            StartCoroutine("LoadMainMenu");
        }

    }


    public void RegisterButton()
    {
        if (passwordInputRegister.text.Length < 8)
        {
            messageTextRegister.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInputRegister.text,
            Password = passwordInputRegister.text,
            Username = UsernameInputRegister.text,
            RequireBothUsernameAndEmail = true
        };



        PlayFabClientAPI.RegisterPlayFabUser(request, OnSuccessfullResgister, OnErrorRegister);

    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputLogin.text,
            Password = passwordInputLogin.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnSuccessfullLogin, OnErrorLogin);

    }

    public void ResetPasswordButton()
    {
        if (emailInputLogin.text.Equals(""))
        {
            messageTextLogin.text = "Please type you email and try again";
            return;
        }
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInputLogin.text,
            TitleId = "70C81"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnSuccessfullReset, OnErrorLogin);

    }

    public void SendInitialData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>{
                {"Username", UsernameInputRegister.text},
                {"Coin", "0"}

            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSuccessfullDataSent, OnErrorRegister);

    }



    void OnSuccessfullReset(SendAccountRecoveryEmailResult result)
    {
        messageTextLogin.text = "Password reset mail has sent!";

        var request = new GetAccountInfoRequest
        {

        };

    }
    void OnSuccessfullLogin(LoginResult result)
    {
        messageTextLogin.text = "Logged In!";
        loggedIn = true;
    }
    void OnSuccessfullResgister(RegisterPlayFabUserResult result)
    {
        messageTextRegister.text = "Registered and Logged In";
        SendInitialData();
    }

    void OnSuccessfullDataSent(UpdateUserDataResult result)
    {
        print("entered onSuccessfulDateSent");
        loggedIn = true;


    }

    void OnErrorLogin(PlayFabError error)
    {
        messageTextLogin.text = error.ErrorMessage;

    }
    void OnErrorRegister(PlayFabError error)
    {
        messageTextRegister.text = error.ErrorMessage;

    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Connecting");

    }
}
