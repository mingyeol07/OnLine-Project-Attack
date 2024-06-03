using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    Idle, Blocking, invincibility
}

public class Player : MonoBehaviour
{
    public PlayerState state;

    private Rigidbody rigid;
    private Animator animator;
    [SerializeField] private Camera mainCam;

    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private BoxCollider swordCollider;
    [SerializeField] private BoxCollider shieldCollider;
    private Vector3 moveDirection;

    private float horizontal;
    private float vertical;

    private bool isAttack;
    private bool isComboAttack;
    private bool isJump;
    private bool isMove;
    private bool isBlock;
    private bool isDash;

    #region hashs
    private readonly int HashMoving = Animator.StringToHash("Moving");
    private readonly int HashBlock = Animator.StringToHash("Blocking");
    private readonly int HashDash = Animator.StringToHash("Dash");

    private readonly int HashAttack = Animator.StringToHash("Attack");
    private readonly int HashComboAttack = Animator.StringToHash("ComboAttack");
    private readonly int HashComboAttack2 = Animator.StringToHash("ComboAttack2");
    private readonly int HashMoveAttack = Animator.StringToHash("MoveAttack");
    private readonly int HashPowerUpAttack = Animator.StringToHash("PowerUpAttack");
    private readonly int HashBlockAttack = Animator.StringToHash("BlockAttack");
    private readonly int HashRushAttack = Animator.StringToHash("RushAttack");
    #endregion

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Inputs();

        SetDirection();

        SetIdleState();

        Move();
        
        SetAnimatorParameter();
    }

    private void Inputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // A, D 
        vertical = Input.GetAxisRaw("Vertical"); // W, S 

        isBlock =  Input.GetKey(KeyCode.Mouse1);

        if (Input.GetKeyDown(KeyCode.E) && !isAttack) Skill1();
        if (Input.GetKeyDown(KeyCode.Q) && !isAttack) Skill2();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isBlock) Attack();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash) Dash();
        //if (Input.GetKeyDown(KeyCode.Space) && !isJump) Jump();
    }

    private void SetAnimatorParameter()
    {
        animator.SetBool(HashMoving, isMove);
        animator.SetBool(HashBlock, isBlock);
    }

    private void Move()
    {
        if (isDash)
        {
            rigid.AddForce(transform.forward * dashSpeed);
        }
        else if (isMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * turnSpeed);
            rigid.velocity = moveDirection * moveSpeed;
        }
        else if (!isMove)
        {
            rigid.velocity = Vector3.zero;
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
        isMove = moveDirection != Vector3.zero && !isDash && !isAttack;
    }

    private void SetIdleState()
    {
        if(isDash)
        {
            SetState(PlayerState.invincibility);
        }
        else if (!isBlock)
        {
            SetState(PlayerState.Idle);
            shieldCollider.enabled = false;
        }
    }

    private void Dash()
    {
        isDash = true;
        SetState(PlayerState.invincibility);
        animator.SetTrigger(HashDash);
        rigid.velocity = Vector3.zero;
    }

    #region Skills
    private void Skill1()
    {
        isAttack = true;

        if (!isBlock)
        {
            Skill_MoveAttack();
        }
        else
        {
            Skill_RushAttack();
        }
    }

    private void Skill2()
    {
        isAttack = true;

        if (!isBlock)
        {
            Skill_PowerUp();
        }
        else
        {
            Skill_BlockAttack();
        }
    }

    private void Skill_MoveAttack()
    {
        animator.SetTrigger(HashMoveAttack);
    }

    private void Skill_PowerUp()
    {
        animator.SetTrigger(HashPowerUpAttack);
    }

    private void Skill_RushAttack()
    {
        animator.SetTrigger(HashRushAttack);
    }

    private void Skill_BlockAttack()
    {
        animator.SetTrigger(HashBlockAttack);
    }
    #endregion

    private void Attack()
    {
        if (isComboAttack)
        {
            animator.SetTrigger(HashComboAttack2);
        }
        else if (isAttack)
        {
            isComboAttack = true;
            animator.SetTrigger(HashComboAttack);
        }
        else
        {
            isAttack = true;
            animator.SetTrigger(HashAttack);
        }
    }

    #region animation event
    private void SetBlockState()
    {
        SetState(PlayerState.Blocking);
        shieldCollider.enabled = true;
    }
    private void AttackExit()
    {
        isAttack = false;
        isComboAttack = false;

        swordCollider.enabled = false;
        animator.ResetTrigger(HashComboAttack2);
    }
    private void DashExit()
    {
        isDash = false;
        SetState(PlayerState.Idle);
        rigid.velocity = Vector3.zero;
    }
    private void SetSwordColliderActive(int _bool)
    {
        swordCollider.enabled = _bool == 1 ? true : false;
    }
    private void SetShieldColliderActive(int _bool)
    {
        shieldCollider.enabled = _bool == 1 ? true : false;
    }
    #endregion

    private void SetState(PlayerState setState)
    {
        state = setState;
    }
}
