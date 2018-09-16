using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 15f;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        Run();
        FlipSprite();
       Jump();
	}/// <summary>
    /// khcghadgfkja
    /// </summary>

    private void Run()
    {
        //    transfrom
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);


    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }


    private void Jump()
    {
       // if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

            if (Input.GetButtonDown("Jump")&&myRigidBody.velocity.y==0)
            {
                
                Vector2 playerVelocity = new Vector2(0f, jumpHeight);
           
                myRigidBody.velocity += playerVelocity;
            }
        
    }
}
