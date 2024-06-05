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

    private void Awake()
    {
        btn_guestLogin.onClick.AddListener(() => Login());   
    }

    private void Login()
    {
        SendQueue.Enqueue(
            Backend.BMember.GuestLogin,
            "게스트 로그인으로 로그인함",
            (callback) => {
                if (callback.IsSuccess())
                {
                    Debug.Log("게스트 로그인에 성공했습니다");
                }
            }
        );
    }
}
