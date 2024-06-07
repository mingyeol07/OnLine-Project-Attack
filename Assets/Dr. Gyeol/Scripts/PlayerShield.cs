// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
        else if (other.gameObject.CompareTag("Shield"))
        {

        }
        else if (other.gameObject.CompareTag("Sword"))
        {

        }
    }
}
