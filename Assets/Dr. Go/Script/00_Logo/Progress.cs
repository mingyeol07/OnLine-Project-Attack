// # Systems
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;


// # Unity
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 로딩 정보를 제어하는 스크립트
/// </summary>
public class Progress : MonoBehaviour
{
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private TextMeshProUGUI textProgressData;
    [SerializeField] private float progressTime;

    public void Play(UnityAction action=null)
    {
        StartCoroutine(OnProgress(action));
    }

    private IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / progressTime;

            // text 정보 설정
            textProgressData.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
            // slider 값 설정
            sliderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }
        // action이 null이 아니면 action 메소드 실행
        action?.Invoke();
    }
}
