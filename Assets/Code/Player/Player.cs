using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected virtual void Move(Rigidbody2D controllerRB, float moveSpeed)
    {
        Rigidbody2D rigid = controllerRB.GetComponent<Rigidbody2D>();
        if (moveSpeed < 0) rigid.transform.localRotation = new Quaternion(0, 180, 0, 0);
        else if (moveSpeed > 0) rigid.transform.localRotation = new Quaternion(0, 0, 0, 0);
        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
    }

    protected virtual void Jump(Rigidbody2D controllerRB, float jumpPower)
    {
        Rigidbody2D rigid = controllerRB.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
    }
}
