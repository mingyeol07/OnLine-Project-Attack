using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEditor;

public class BulletScript : MonoBehaviour
{
    public PhotonView PV;
    private int dir;

    void Start() => Destroy(gameObject, 3.5f);

    void Update() => transform.Translate(Vector3.right * 20 * Time.deltaTime * dir);

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        if (!PV.IsMine && collision.gameObject.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
        {
            collision.GetComponent<PlayerScript>().Hit();
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
