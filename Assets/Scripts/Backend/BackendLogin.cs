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
        Debug.Log("�α��� �õ�");
        BackendReturnObject bro = Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����");
        if (bro.IsSuccess())
        {
            Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�");
        }
    }
 }
