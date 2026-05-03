using UnityEngine;

public class DamageAreas : MonoBehaviour
{
    public Transform respawnPoint;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats hp = collision.GetComponent<PlayerStats>();
            if (hp != null)
                hp.DecreaseHealth(damage);

            collision.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
