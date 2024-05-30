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
        // �̵��������� ȸ��
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * turnSpeed);
        }
    }

    private void SetDirection()
    {
        // ī�޶���� ���� ���Ϳ� ���� ����
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;

        // Y�� ���� (���� �̵��� ���)
        forward.y = 0;
        right.y = 0;

        // ���� ����ȭ (���� 1�� �����)
        forward.Normalize();
        right.Normalize();

        // �Է¿� ���� �̵� ���� ���
        moveDirection = forward * vertical + right * horizontal;
    }
}
