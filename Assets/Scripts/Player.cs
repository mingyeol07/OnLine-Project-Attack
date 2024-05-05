using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private void Start()
    {
         rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(hor * moveSpeed, rigid.velocity.y);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
    }
}
