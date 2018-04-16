using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {

    public float maxSpeed = 6f;
    public bool facingRight = true;
    float move = 0.5f;

    //Wall check
    bool walled = false;
    public Transform wallCheck;
    Vector2 wallSize = new Vector2(1.9f,0.2f); //.71f,0.56f
    float angle = 0.0f;
    public LayerMask whatIsWall;

    Rigidbody2D myRigidbody2D;

    // Use this for initialization
    void Start () {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        myRigidbody2D.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        
        walled = Physics2D.OverlapBox(wallCheck.position, wallSize, angle, whatIsWall);
        
        myRigidbody2D.velocity = new Vector2(move * maxSpeed, myRigidbody2D.velocity.y);

        
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float FlashingTime = 0.1f;
        float TimeInterval = 0.1f;

        StartCoroutine(Flash(FlashingTime, TimeInterval));

        if (!facingRight)
        {
            facingRight = true;
            Flip();
        }
        else if (facingRight)
        {
            facingRight = false;
            Flip();
        }


    }

    IEnumerator Flash(float time, float intervalTime)
    {
        //this counts up time until the float set in FlashingTime
        float elapsedTime = 0f;
        //This repeats our coroutine until the FlashingTime is elapsed
        while (elapsedTime < time)
        {
            //This gets an array with all the renderers in our gameobject's children
            Renderer[] RendererArray = GetComponentsInChildren<Renderer>();
            //this turns off all the Renderers
            foreach (Renderer r in RendererArray)
                r.enabled = false;
            //then add time to elapsedtime
            elapsedTime += Time.deltaTime;
            //then wait for the Timeinterval set
            yield return new WaitForSeconds(intervalTime);
            //then turn them all back on
            foreach (Renderer r in RendererArray)
                r.enabled = true;
            elapsedTime += Time.deltaTime;
            //then wait for another interval of time
            yield return new WaitForSeconds(intervalTime);
        }
    }

    void Flip()
    {
        move *= -1;
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1f;
        transform.localScale = theScale;
    }
}
