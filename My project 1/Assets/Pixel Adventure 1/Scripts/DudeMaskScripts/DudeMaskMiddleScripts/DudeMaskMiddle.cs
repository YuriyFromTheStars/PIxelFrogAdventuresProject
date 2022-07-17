using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMaskMiddle : DudeMaskBaseClass
{

    [SerializeField] private Transform checkingJumpAreaPoint;
    [SerializeField] private Transform checkingJumpAreaPointTwo;
    [SerializeField] private Transform checkGroundPoint;

    private bool rayCastForJump;
    private bool rayCastForJumpTwo;
    private bool onGround;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = 6;
        directionForMove = 1;
        dudeMaskIsAlive = true;
    }


   
    private void Update()
    {
        if (dudeMaskIsAlive)
        {
            JumpController();
            OnGround();
            Move();
            TurnBack();
        }
        AnimatorController();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkGroundPoint.position, new Vector3(1, 0.1F));
    }

    protected void OnGround()
    {
        onGround = Physics2D.OverlapBox(checkGroundPoint.position, new Vector2(1, 0.1F), 0, layerMaskGround);
    }

    private void JumpController()
    {
        rayCastForJump = Physics2D.Raycast(checkingJumpAreaPoint.position, Vector2.down, 0.1F, layerMaskGround);
        rayCastForJumpTwo = Physics2D.Raycast(checkingJumpAreaPointTwo.position, Vector2.down, 0.1F, layerMaskGround);
        if (!rayCastForJump & rayCastForJumpTwo & onGround) { rigidbody.velocity = new Vector2(directionForMove * 3, 15); }
        if (!onGround) { jumping = true; }
        else { jumping = false; }
        if (rigidbody.velocity.y < -0.01F) { fall = true; }
        else { fall = false; }
    }

    protected override void TurnBack()
    {
        rayCastForTurnBack = Physics2D.Raycast(checkForTurnBackPoint.position, Vector2.down, 0.3F, layerMaskGround);
        if (!rayCastForTurnBack & !rayCastForJumpTwo & onGround)
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
}
