using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxyControllerScript : MonoBehaviour {

    public float maxSpeed = 10f;
    public bool facingRight = true;

    //Ground check
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 70f;

    Rigidbody2D myRigidbody2D;
    Animator anim;

    // Use this for initialization
    void Start () {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D.freezeRotation = true;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", myRigidbody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        myRigidbody2D.velocity = new Vector2(move * maxSpeed, myRigidbody2D.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
	}

    private void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1f;
        transform.localScale = theScale;
    }
}
