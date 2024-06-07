// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

using BackEnd;
using UnityEngine.UI;

public class BackendLogin : MonoBehaviour
{
    [SerializeField] private Button btn_guestLogin;

    private void Start()
    {
       // Login();
        btn_guestLogin.onClick.AddListener(() => Login());   
    }

    private void Update()
    {
       
    }

    private void Login()
    {
        Debug.Log("로그인 시도");
        BackendReturnObject bro = Backend.BMember.GuestLogin("게스트 로그인으로 로그인함");
        if (bro.IsSuccess())
        {
            Debug.Log("게스트 로그인에 성공했습니다");
        }
    }
 }
