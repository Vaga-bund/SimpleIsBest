using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Damage for Player")]
    private Health playerHealth;
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth = collision.transform.GetComponent<Health>();
            //Debug.Log(playerHealth.currentHealth);
            playerHealth.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
