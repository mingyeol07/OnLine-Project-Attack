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
        BackendGameData.Instance.GameDataGet();
        nickName.text = Backend.UserNickName;
        winCount.text = "Win : " + BackendGameData.userData.winCount.ToString();
        level.text = "Level : " + BackendGameData.userData.level.ToString();
    }
}
