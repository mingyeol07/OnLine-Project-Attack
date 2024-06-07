// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

/// <summary>
/// �ΰ� ���� �����ϴ� ��ũ��Ʈ
/// </summary>
public class LogoScenario : MonoBehaviour
{
    [SerializeField] private Progress progress;

    private void Awake()
    {
        SystemSetup();
    }

    private void SystemSetup()
    {
        // Ȱ��ȭ���� ���� ���¿����� ������ ��� ����
        Application.runInBackground = true;

        // �ػ� ���� (9:18.5, 1440x2960)
        int width = Screen.width;
        int height = (int)(Screen.height * 18.5f / 9);
        Screen.SetResolution(width, height, true);

        // ȭ���� ������ �ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // �ε� �ִϸ��̼� ����, ��� �Ϸ�� OnAfterProgress() �޼ҵ� ����
        progress.Play(OnAfterProgress);
    }

    private void OnAfterProgress()
    {

    }
}
