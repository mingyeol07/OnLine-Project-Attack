using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;
using System;

public class BackendManager : MonoBehaviour
{


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        BackendSetup();
    }

    private void Update()
    {
        if(Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }

        if (SendQueue.IsInitialize == false)
        {
            // SendQueue �ʱ�ȭ
            SendQueue.StartSendQueue(true, ExceptionHandler);
        }

        void ExceptionHandler(Exception e)
        {
            // ���� ó��
        }
    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
        }
    }
}