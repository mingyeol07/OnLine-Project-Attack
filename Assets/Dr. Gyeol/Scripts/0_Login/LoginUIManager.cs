// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


// # Unity
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField_ID;
    [SerializeField] private TMPro.TMP_InputField inputField_PW;

    [SerializeField] private string txt_ID;
    [SerializeField] private string txt_PW;

    [SerializeField] private Button btn_login;
    [SerializeField] private Button btn_signUp;

    private void Awake()
    {
        btn_login.onClick.AddListener(()=> { OnClickLogin(); });
        btn_signUp.onClick.AddListener(() => { OnClickSignUp(); });
    }

    private void Update()
    {
        txt_ID = inputField_ID.text;
        txt_PW = inputField_PW.text;
    }

    private void OnClickLogin()
    {
        BackendLogin.Instance.CustomLogin(txt_ID, txt_PW);
    }

    private void OnClickSignUp()
    {
        BackendLogin.Instance.CustomSignUp(txt_ID, txt_PW);
    }
}
