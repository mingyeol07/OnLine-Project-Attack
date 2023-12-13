using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Janghukscript : MonoBehaviourPunCallbacks, IPunObservable
{
    Animator anim;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public PhotonView PV;
    public Text nickNameText;
    public Image healthImage;

    private bool isGround;
    Vector3 curPos;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        nickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        nickNameText.color = PV.IsMine ? Color.green : Color.red;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        if (PV.IsMine)
        {
            float axis = Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(axis * 4, rigid.velocity.y);

            if (axis != 0)
            {
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axis);
            }

            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround) PV.RPC("JumpRPC", RpcTarget.All);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(spriteRenderer.flipX ? -0.4f : 0.4f, -0.11f, 0), Quaternion.identity)
                    .GetComponent<PhotonView>().RPC("DirRPC", RpcTarget.All, spriteRenderer.flipX ? -1 : 1);
            }
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    [PunRPC]
    void FlipXRPC(float axis) => spriteRenderer.flipX = axis == -1;

    [PunRPC]
    void JumpRPC()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * 700);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    public void Hit()
    {
        healthImage.fillAmount -= 0.1f;
        if(healthImage.fillAmount <= 0)
        {
            GameObject.Find("Canvas").transform.Find("ReSpawnPanel").gameObject.SetActive(true);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(healthImage.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            healthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack");
        }
    }
}
