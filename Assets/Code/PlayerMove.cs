using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveMaxSpeed;
    [SerializeField] private float moveAddSpeed;

    private bool isSpeedUp;

    private void Update()
    {
        Move();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(x, y);
        moveDirection.Normalize();

        rigid.velocity = moveDirection * moveSpeed;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isSpeedUp = true;
            if (moveSpeed <= moveMaxSpeed) moveSpeed += (Time.deltaTime + moveAddSpeed);
        }
        else isSpeedUp = false;
        if (!isSpeedUp && moveSpeed >= 1) moveSpeed -= (Time.deltaTime + moveAddSpeed);
    }
}