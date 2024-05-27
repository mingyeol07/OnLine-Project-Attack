using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Block,
    
}

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator animator;
    [SerializeField] private Camera mainCam;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSpeed;
    private float rotationVelocity;
    private Vector2 inputDir;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A, D 
        float vertical = Input.GetAxis("Vertical"); // W, S 

        // ���� ���Ϳ� ���� ����
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;

        // Y�� ���� (���� �̵��� ���)
        forward.y = 0;
        right.y = 0;

        // ���� ����ȭ (���� 1�� �����)
        forward.Normalize();
        right.Normalize();

        // �Է¿� ���� �̵� ���� ���
        Vector3 moveDirection = forward * vertical + right * horizontal;

        // ������ٵ� ����Ͽ� �̵�
        Vector3 velocity = moveDirection * moveSpeed;
        rigid.velocity = new Vector3(velocity.x, rigid.velocity.y, velocity.z);

        // �̵��������� ȸ��
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * turnSpeed);
        }
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
