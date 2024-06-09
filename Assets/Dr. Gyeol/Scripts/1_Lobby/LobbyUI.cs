// # Systems
using System.Collections;
using System.Collections.Generic;
using TMPro;
using BackEnd;

// # Unity
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nickName;
    [SerializeField] private TMP_Text winCount;
    [SerializeField] private TMP_Text level;

    private void Start()
    {
        BackendUserData.Instance.GetUserData();

        BackendRank.Instance.InsertRank(BackendUserData.userData.winCount);
        nickName.text = Backend.UserNickName;
        winCount.text = "Win : " + BackendUserData.userData.winCount.ToString();
        level.text = "Level : " + BackendUserData.userData.level.ToString();
    }
}
