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
            "�Խ�Ʈ �α������� �α�����",
            (callback) => {
                if (callback.IsSuccess())
                {
                    Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�");
                }
            }
        );
    }
}
