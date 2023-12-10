using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller1 : Player
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private int jumpCount;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform BulletPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            base.Move(rigid, -speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            base.Move(rigid, speed);
        }
        else rigid.velocity = new Vector2(0, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            base.Jump(rigid, jumpPower);
            jumpCount--;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation), 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 2;
        }
    }
}
