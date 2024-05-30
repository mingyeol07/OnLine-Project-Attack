using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Move, Block, 
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

    private readonly int HashAttack = Animator.StringToHash("Attack");
    private readonly int HashMoveAttack = Animator.StringToHash("MoveAttack");
    private readonly int HashMoving = Animator.StringToHash("Moving");

    private bool isJumping;
    private bool isMoveing;
    private bool isBlocking;
    private bool isTargeting;

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
        horizontal = Input.GetAxisRaw("Horizontal"); // A, D 
        vertical = Input.GetAxisRaw("Vertical"); // W, S 

        isBlocking =  Input.GetKey(KeyCode.Mouse1);
        isTargeting = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetKey(KeyCode.Mouse1)) Blocking();
        if (Input.GetKey(KeyCode.LeftControl)) Targeting();

        if (Input.GetKeyDown(KeyCode.E)) Skill1();
        if (Input.GetKeyDown(KeyCode.Q)) Skill2();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Attack();
        if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void SetAnimatorParameter()
    {
        isMoveing = moveDirection != Vector3.zero;
        animator.SetBool(HashMoving, isMoveing);
    }

    private void MoveRotate()
    {
        // 이동방향으로 회전
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * turnSpeed);
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

    private void Targeting()
    {

    }

    private void Jump()
    {

    }

    private void Dash()
    {

    }

    private void Skill1()
    {
        // MoveAttakc, RangeAttack
        animator.SetTrigger(HashMoveAttack);
    }

    private void Skill2()
    {
        // power Up
    }

    private void Blocking()
    {

    }

    private void Attack()
    {

    }
}
