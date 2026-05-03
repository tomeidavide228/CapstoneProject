using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerRespawn respawn = collision.GetComponent<PlayerRespawn>();
            respawn.UpdateCheckpoint(transform.position);
        }
    }
}
