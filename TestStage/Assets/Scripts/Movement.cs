using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Variables pour mouvement

    private Rigidbody2D rb;
    public float speed = 10f;
    public float rollSpeed = 1f;
    public float rollStartTime = 1f;
    public float rollTime;
    private bool faceRight;
    

    //Variables pour saut

    [Range(1, 10)]
    public float jumpVelocity;
    bool isJumping = false;
    int jumpCount = 0;

    //Variables de collision

    public CircleCollider2D ground;
    public CircleCollider2D Left;
    public CircleCollider2D Right;
    public Animator playerCollider;

    //Variables d'animation

    public Animator player;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rollTime = rollStartTime;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);

        //Fonction animation

        AnimateCharacter();
        player.SetBool("onGround", Collision.canJump);
        player.SetBool("isRoll", playerCollider.GetBool("isRoll"));
        player.SetBool("faceRight", faceRight);
        player.SetBool("onWall", Collision.onWall); 
        //Debug.Log(faceRight);

        //Fonction de mouvement


        if (Collision.canJump && jumpCount == 0)
        {
            Jump();
        }
        else if (!Collision.canJump && Collision.onWall && jumpCount == 0 && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D)))
        {
            Jump();
        }

        Move(direction);

        if (player.GetBool("isJumping"))
        {
            jumpCount = 1;
        }
        else if (Collision.canJump || Collision.onWall)
        {
            jumpCount = 0;
        }

        

    }

    public void FixedUpdate()
    {
        if (isJumping)
        {
            //rb.velocity += Vector2.up * jumpVelocity;
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            isJumping = false;
            player.SetBool("isJumping", false);
        }
        if (playerCollider.GetBool("isRoll"))
        {
            if (faceRight)
            {
                rb.AddForce(Vector2.right * rollSpeed, ForceMode2D.Impulse);
            }
            else if (!faceRight)
            {
                rb.AddForce(Vector2.left * rollSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        rollTime -= Time.deltaTime;
        if (Input.GetAxis("Horizontal") > 0)
        {
            faceRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            faceRight = false;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (rollTime < 0)
            {
                playerCollider.SetBool("isRoll", true);
                rollTime = rollStartTime;
            }
        }
        else
        {
            if(rollTime < 0)
            {
                playerCollider.SetBool("isRoll", false);

            }
            rb.velocity = (new Vector2(direction.x * speed, rb.velocity.y));
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.SetBool("isJumping", true);
            isJumping = true;
        }
    }

    private void AnimateCharacter()
    {
        if (Input.GetKey(KeyCode.D) && Collision.canJump)
        {
            player.SetBool("isRunningLeft", true);
        }
        if (Input.GetKey(KeyCode.Q) && Collision.canJump)
        {
            player.SetBool("isRunningRight", true);
        }
        if ((!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D)) || !Collision.canJump)
        {
            player.SetBool("isRunningLeft", false);
            player.SetBool("isRunningRight", false);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            player.SetBool("isRunningRight", false);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            player.SetBool("isRunningLeft", false);
        }
    }
}
