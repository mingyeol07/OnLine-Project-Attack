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

        // 전방 벡터와 우측 벡터
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;

        // Y값 제거 (수평 이동만 고려)
        forward.y = 0;
        right.y = 0;

        // 벡터 정규화 (길이 1로 만들기)
        forward.Normalize();
        right.Normalize();

        // 입력에 따라 이동 방향 계산
        Vector3 moveDirection = forward * vertical + right * horizontal;

        // 리지드바디를 사용하여 이동
        Vector3 velocity = moveDirection * moveSpeed;
        rigid.velocity = new Vector3(velocity.x, rigid.velocity.y, velocity.z);

        // 이동방향으로 회전
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
