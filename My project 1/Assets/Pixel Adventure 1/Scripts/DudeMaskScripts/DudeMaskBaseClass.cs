using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DudeMaskBaseClass : MonoBehaviour
{

    [SerializeField] protected Transform checkForTurnBackPoint;
    [SerializeField] protected LayerMask layerMaskGround;   
        
    protected Rigidbody2D rigidbody;
    protected Animator animator;

    protected bool dudeMaskIsAlive;
    protected bool rayCastForTurnBack;
    protected bool move;
    protected bool damage;
    protected bool jumping;
    protected bool fall;

    protected int directionForMove;
    protected int moveSpeed;
    protected int jumpForce;

    protected virtual void Move() => rigidbody.velocity = new Vector2(directionForMove * moveSpeed, rigidbody.velocity.y);

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AreaForDamageEnemy")
        {
            GameObject mainPlayer = collision.gameObject; 
            LocalEventsManager.JumpFromDudeMaskSimple?.Invoke();
            Dead(mainPlayer);            
        }                            
    }  

    protected virtual void AnimatorController()
    {
        animator.SetBool("Move", move);
        animator.SetBool("Damage", damage);
        animator.SetBool("Jump", jumping);
        animator.SetBool("Fall", fall);
        if (directionForMove != 0) { move = true; }
        else { move = false; }
    }

    protected virtual void TurnBack()
    {
        rayCastForTurnBack = Physics2D.Raycast(checkForTurnBackPoint.position, Vector2.down, 0.3F, layerMaskGround);
        if (!rayCastForTurnBack)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            switch (directionForMove)
            {
                case -1:
                    directionForMove = 1;
                    break;
                case 1:
                    directionForMove = -1;
                    break;
            }
        }
    }
    
    protected void Dead(GameObject mainPlayer)
    {
        move = false;
        damage = true;       
        dudeMaskIsAlive = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (mainPlayer.transform.position.x > transform.position.x && mainPlayer != null) { rigidbody.velocity = new Vector2(-5, 15F); }
        else if (mainPlayer.transform.position.x < transform.position.x && mainPlayer != null) { rigidbody.velocity = new Vector2(5, 15F); }
    }    

}
