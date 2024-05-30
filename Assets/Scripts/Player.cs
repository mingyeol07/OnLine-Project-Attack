using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSpeed;

    private Vector3 moveDirection;

    private float horizontal;
    private float vertical;

    private readonly int HashMoveAttack = Animator.StringToHash("MoveAttack");

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Inputs();

        SetDirection();

        MoveRotate();
        
        SetAnimatorParameter();
    }

    private void Inputs()
    {
        horizontal = Input.GetAxis("Horizontal"); // A, D 
        vertical = Input.GetAxis("Vertical"); // W, S 
        if (Input.GetKeyDown(KeyCode.E)) MoveAttack();
        if (Input.GetKeyDown(KeyCode.Q)) PowerUp();
    }

    private void PowerUp()
    {

    }

    private void MoveAttack()
    {
        animator.SetTrigger("Trigger");
    }

    private void SetAnimatorParameter()
    {
        if (moveDirection != Vector3.zero) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
    }

    private void MoveRotate()
    {
        // 이동방향으로 회전
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * turnSpeed);
        }
    }

    private void SetDirection()
    {
        // 카메라기준 전방 벡터와 우측 벡터
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;

        // Y값 제거 (수평 이동만 고려)
        forward.y = 0;
        right.y = 0;

        // 벡터 정규화 (길이 1로 만들기)
        forward.Normalize();
        right.Normalize();

        // 입력에 따라 이동 방향 계산
        moveDirection = forward * vertical + right * horizontal;
    }
}
