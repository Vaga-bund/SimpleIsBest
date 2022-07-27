using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    enum Directions
    { left, down, right, up }

    const float NONE = 0.0f;
    const float RA = 90.0f;

    [Header("Basic Parameter")]
    public float range;
    public float force;
    private bool dectected = false;

    [Header("Target")]
    [SerializeField] private Transform target;
    private Vector2 targetPos;
    private RaycastHit2D rayInfo;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;
    [SerializeField] private float fireCoolTime;
    private float fireRate = 0.0f;

    [Header("Direction")]
    [SerializeField] Directions direction;
    private Vector3 endPos;
    private Vector3 bulletDirection;

    void Awake()
    {
        bulletDirection = new Vector3(NONE, NONE, (float)direction * RA);
    }

    void Update()
    {
        targetPos = target.position;
        endPos = transform.position + ShootDircetion() * range;
        
        rayInfo = Physics2D.Linecast(transform.position, endPos);

        if(rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if(!dectected)
                {
                    // 발사 경고 스프라이트 켜기
                    dectected = true;
                }
            }
            else
            {
                if (dectected)
                {
                    // 발사 경고 스프라이트 끄기
                    dectected = false;
                }
            }
        }

        if(dectected)
        {
            if(fireRate > fireCoolTime)
            {
                fireRate = 0.0f;
                Shoot();
            }
        }

        fireRate += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletIns = Instantiate(bullet, shootPos.position, Quaternion.Euler(bulletDirection), transform);
        
        bulletIns.GetComponent<Rigidbody2D>().AddForce(ShootDircetion() * force);
    }

    Vector3 ShootDircetion()
    {
        Vector3 shootingDir = Vector3.zero;

        switch(direction)
        {
            case Directions.up:
                shootingDir = Vector3.up;
                break;
            case Directions.down:
                shootingDir = Vector3.down;
                break;
            case Directions.right:
                shootingDir = Vector3.right;
                break;
            case Directions.left:
                shootingDir = Vector3.left;
                break;
        }

        return shootingDir;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, endPos);
    }
}
