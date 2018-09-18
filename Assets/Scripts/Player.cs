using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : NetworkBehaviour
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


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        scoreLeft = GameObject.FindWithTag("Chest Left").GetComponent<Score>();
        scoreRight = GameObject.FindWithTag("Chest Right").GetComponent<Score>();
        Vector2 cl = new Vector2(transform.position.x, transform.position.y + 1);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            CmdFlipSprite();
            return;
        }
        Run();
        CmdFlipSprite();
        Jump();
        Vector2 cl = new Vector2(transform.position.x, transform.position.y+1);
    


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
       
            isGrounded = true;
            // canjump = true;
            Debug.Log("COLL-Enter" + "  -  Ground:" + isGrounded);
       
    }

    void OnCollisionExit2D(Collision2D Other)
    {
       
            isGrounded = false;

            Debug.Log("COLL-Exit" + "  -  Ground:" + isGrounded);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // pickup if it has tag "Item" and we are not carrying anything
        if (other.CompareTag("Item") && currentItem == null)
        {
            mySpriteRenderer.color = Color.blue;
        }
        if (other.CompareTag("Chest Right") && currentItem == null)
        {
            mySpriteRenderer.color = Color.white;
            Debug.Log("GOAL");
            scoreRight.ReduceScore();

        }
        if (other.CompareTag("Chest Left") && currentItem == null)
        {
            mySpriteRenderer.color = Color.white;
            Debug.Log("GOAL");
            scoreLeft.ReduceScore();

        }
    }
}