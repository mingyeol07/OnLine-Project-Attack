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
        // Step 2. ȸ������ �����ϱ� ����
    }

    public void CustomLogin(string id, string pw)
    {
        // Step 3. �α��� �����ϱ� ����
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. �г��� ���� �����ϱ� ����
    }
}
