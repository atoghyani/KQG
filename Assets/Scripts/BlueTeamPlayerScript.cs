using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityStandardAssets.CrossPlatformInput;

public class BlueTeamPlayerScript : NetworkBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Collider2D myCollider;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 15f;
    bool isGrounded=true;

    Transform currentItem = null;
    Score scoreLeft;
    Score scoreRight;
    bool hasItem = false;
    Vector2 cl;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        scoreLeft = GameObject.FindWithTag("Chest Left").GetComponent<Score>();
        scoreRight = GameObject.FindWithTag("Chest Right").GetComponent<Score>();
        cl = new Vector2(transform.position.x, transform.position.y + 1);


    }
    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraController>().setTarget(gameObject.transform);
    }
    void Update()
    {
        
        if (!isLocalPlayer)
        {
            CmdFlipSprite();
            return;
        }
        Run();
        CmdFlipSprite();
        Jump();
        cl = new Vector2(transform.position.x, transform.position.y+1);
    


    }/// <summary>
     /// khcghadgfkja
     /// </summary>
     /// 


    public void CmdFlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
        FlipSprite();
    }



    private void Run()
    {
        //    transfrom
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon && myRigidBody.velocity.y == 0f;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);


    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }


    private void Jump()
    {
        //if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            Vector2 playerVelocity = new Vector2(0f, jumpHeight);

            myRigidBody.velocity += playerVelocity;



        }

    }

    void OnCollisionEnter2D(Collision2D Other)
    {
       if(Other.gameObject.tag.Equals("Ground"))
            isGrounded = true;
            // canjump = true;
        
       
    }
    void OnCollisionExit2D(Collision2D Other)
    {
        if(Other.gameObject.tag.Equals("Ground"))
            isGrounded = false;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // pickup if it has tag "Item" and we are not carrying anything
        if (other.CompareTag("Item") && currentItem == null)
        {
            mySpriteRenderer.color = Color.cyan;
            hasItem = true;
           

        }
        if (other.CompareTag("Chest Left") && hasItem)
        {
            mySpriteRenderer.color = Color.white;
            Debug.Log("GOAL");
            scoreLeft.ReduceScore();
            hasItem = false;

        }
        if(other.CompareTag("Border"))
        {
            Debug.Log("On Stage");
            myRigidBody.velocity = new Vector2( -(myRigidBody.velocity.x), myRigidBody.velocity.y);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Border"))
        {
            Debug.Log("Off Stage");
            myRigidBody.velocity = new Vector2( -(myRigidBody.velocity.x), myRigidBody.velocity.y);
        }
            
    }
}