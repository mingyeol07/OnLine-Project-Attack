using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
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

        float moveInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = new Quaternion(0, 180, 0, 0);
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            moveInput = 1f;
        }

        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);


        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            Jump();
            jumpCount--;
        }
        Fire();
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 2;
        }
    }
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Destroy(Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation), 1f);
        }
    }
}
