using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float collderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //reference
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        //cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
        else
        {
            cooldownTimer = 0;
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if(PlayerInSight())
            playerHealth.TakeDamage(damage);
        Debug.Log(playerHealth.currentHealth);

    }
}