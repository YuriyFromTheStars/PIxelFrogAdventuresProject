using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

    [SerializeField] private Transform checkGroundPoint;
    [SerializeField] private LayerMask layerMaskGround;    

    private BoxCollider2D boxCollider;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private GameObject child;

    private float jumpForce;
    private float horizontalMove;

    private int moveSpeed;
    private int jumpOption;
   
    private bool onGround;
    private bool somersaultAnimation;
    private bool runAnimation;
    private bool jumpAnimation;
    private bool fall;
    private static bool mainPlayerIsLive;
    private bool finishLevel;
    private bool movementActivation;   
    public static bool PropertyMainPlayerIsLive { get { return mainPlayerIsLive; } }

    private void OnEnable()
    { 
        LocalEventsManager.JumpFromDudeMaskSimple += JumpFromEnemy;
        GlobalEventManager.FinishLevel += DisappearanceMainPlayer;
    }

    private void OnDisable()
    { 
        LocalEventsManager.JumpFromDudeMaskSimple -= JumpFromEnemy;
        GlobalEventManager.FinishLevel -= DisappearanceMainPlayer;
    }  
    
    private void Awake()
    {
        child = transform.Find("AreaForDamageEnemy").gameObject;
        movementActivation = true;
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();      
        animator = GetComponent<Animator>();
        mainPlayerIsLive = true;
        moveSpeed = 7;
        jumpForce = 20;
    }

    private void Update()
    {              
        if (mainPlayerIsLive)
        {            
            Jump();
            Move();
            OnGround();
            CheckOnGround();
            ActivationAreaForHit();
        }
        AnimatorController();      
    }

    private void ActivationAreaForHit() => child.SetActive(rigidbody.velocity.y < -0.01F ? true : false);
       
    private void Move()
    {   
        if (movementActivation)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            switch (horizontalMove)
            {
                case < 0:
                    rigidbody.velocity = new Vector2(horizontalMove * moveSpeed, rigidbody.velocity.y);
                    transform.localScale = new Vector3(-9, transform.localScale.y, transform.localScale.z);
                    break;
                case > 0:
                    rigidbody.velocity = new Vector2(horizontalMove * moveSpeed, rigidbody.velocity.y);
                    transform.localScale = new Vector3(9, transform.localScale.y, transform.localScale.z);
                    break;
            }        
            if (horizontalMove != 0) { runAnimation = true; }
            else
            {
                runAnimation = false;
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }

        }
        
    }

    private void Jump()
    {       
        if (Input.GetKeyDown(KeyCode.Space) && onGround) { rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce); }    
        if (rigidbody.velocity.y < -0.01F) { fall = true; }
        else { fall = false; }
    }

    private void JumpFromEnemy()
    {      
        switch (jumpOption)
        {            
            case 0:
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                jumpOption++;
                break;
            case 1:
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce + 5);
                jumpOption++;
                break;
            case 2:
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce + 10);
                somersaultAnimation = true;
                Invoke(nameof(DeactivatoinSomersault), 0.17F);
                break;
        }  
        
    }

    private void DeactivatoinSomersault() => somersaultAnimation = false;
    
    private void OnGround()
    {        
        onGround = Physics2D.OverlapBox(checkGroundPoint.position, new Vector2(1, 0.1F), 0, layerMaskGround);
        if (!onGround) { jumpAnimation = true; }
        else
        { 
            jumpAnimation = false; 
            jumpOption = 0;
        } 
    }

    private void CheckOnGround()
    {
        if (!onGround) { boxCollider.size = new Vector2(0.119341F, 0.2200678F); }
        else { boxCollider.size = new Vector2(0.1707372F, 0.2200678F); }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkGroundPoint.position, new Vector3(1, 0.1F));
    }

    private void AnimatorController()
    {
        animator.SetBool("Run", runAnimation);
        animator.SetBool("Jump", jumpAnimation);
        animator.SetBool("Fall", fall);
        animator.SetBool("OnGround", onGround);
        animator.SetBool("Live", mainPlayerIsLive);
        animator.SetBool("FinishLevel", finishLevel);
        animator.SetBool("Somersault", somersaultAnimation);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {           
        if (collision.gameObject.CompareTag("DudeMask") || collision.gameObject.CompareTag("Spike"))
        {
            GameObject dudeMask = collision.gameObject;
            Dead(dudeMask);
        }
        
    }

    
    private void Dead(GameObject dudeMask)
    {      
        mainPlayerIsLive = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (dudeMask.transform.position.x > transform.position.x && dudeMask != null) { rigidbody.velocity = new Vector2(-5, 15F); }
        else if (dudeMask.transform.position.x < transform.position.x && dudeMask != null) { rigidbody.velocity = new Vector2(5, 15F); }
        GlobalEventManager.MainPlayerIsDead?.Invoke();        
    }
    private void DisappearanceMainPlayer()
    {
        
        finishLevel = true;
        movementActivation = false;       
        rigidbody.velocity = new Vector2(0, 0);
        StartCoroutine(CoroutineDisappearanceMainPlayer());
    }

    IEnumerator CoroutineDisappearanceMainPlayer()
    {
        yield return new WaitForSeconds(0.5F);        
        gameObject.SetActive(false);
    }
    
                
}
