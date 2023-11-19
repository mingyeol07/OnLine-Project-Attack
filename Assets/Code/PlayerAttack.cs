using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private Image gaugeImage;
    [SerializeField] private GameObject attackRange;

    [Header("Gauge")]
    [SerializeField] private float maxGauge;
    [SerializeField] private float gaugePower;
    [SerializeField] private float currentGauge;

    [Header("Attack")]
    [SerializeField] private float currentAttackCool;
    [SerializeField] private float maxAttackCool = 0.3f;
    [SerializeField] private int attackDamage;

    private void Update()
    {
        Click();
        Attack();
    }

    // 마우스의 위치를 감지하고 그 방향으로 공격하는 함수
    private void Click()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - attackRange.transform.position.y, mouse.x - attackRange.transform.position.x) * Mathf.Rad2Deg;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            attackRange.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    // 마우스를 누른시간만큼 게이지를 채워주는 함수
    private void Attack()
    {
        gaugeImage.fillAmount = currentGauge / maxGauge;
        if (Input.GetKey(KeyCode.Mouse0) && currentGauge < maxGauge) currentGauge += gaugePower;
        else if (currentGauge > 0) currentGauge -= gaugePower; 

        if(Input.GetKeyUp(KeyCode.Mouse0)) StartCoroutine(AttackActive());
    }

    private IEnumerator AttackActive()
    {
        attackRange.SetActive(true);
        currentGauge = 0f;
        attackDamage = (int)Mathf.Round(currentGauge);
        yield return new WaitForSeconds(0.1f);
        attackRange.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Attack!");
        }
    }
}
