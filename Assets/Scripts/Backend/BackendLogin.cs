// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BackendLogin : MonoBehaviour
{
    private static BackendLogin _instance = null;

    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }

    public void CustomSignUp(string id, string pw)
    {
        // Step 2. 회원가입 구현하기 로직
    }

    public void CustomLogin(string id, string pw)
    {
        // Step 3. 로그인 구현하기 로직
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. 닉네임 변경 구현하기 로직
    }
}
