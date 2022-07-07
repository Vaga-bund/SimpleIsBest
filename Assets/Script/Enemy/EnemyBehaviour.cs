using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;

    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxcollider;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Right
        if(IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0.0f);
        }
        //Move Left
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0.0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
        
    private void OnTriggerExit2D(Collider2D collider)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }
}
