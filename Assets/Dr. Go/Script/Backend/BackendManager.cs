using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤끝 SDK namespace 추가
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
            // SendQueue 초기화
            SendQueue.StartSendQueue(true, ExceptionHandler);
        }

        void ExceptionHandler(Exception e)
        {
            // 예외 처리
        }
    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
    }
}