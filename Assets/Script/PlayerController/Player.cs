using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Animator playerAnim;

    [Header("Move")]
    public float speed = 1f;
    public float direction;
    public float xRaw;
    private bool faceRight = true;
    private bool faceNow;
    private bool canMove = true;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int jumpCount;
    public float fallMultiplier = 100f;//Scale of gravity when falling
    [SerializeField] private bool toJump;

    [Header("WallSlide And WallJump")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private bool wallJumped;
    [SerializeField] private bool pushingWall;

    [Header("Position Info")]
    public LayerMask ground;
    public LayerMask grabableWall;
    public Transform groundCheck;
    public Transform wallCheck;
    [SerializeField] private bool onGround;
    [SerializeField] private bool onWall;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 50;
    [SerializeField] private bool hasDashed;
    [SerializeField] private bool toDash;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Position Maintainance
        onGround = Physics2D.OverlapCircle(groundCheck.position, .05f, ground);
        onWall = Physics2D.OverlapCircle(wallCheck.position, .1f, grabableWall);
        playerAnim.SetFloat("SpeedY", RB.velocity.y);
        playerAnim.SetBool("onGround", onGround);

        // Walking || Running
        direction = Input.GetAxis("Horizontal");
        xRaw = Input.GetAxisRaw("Horizontal");
        
        if (onGround)
        {
            if (Mathf.Abs(RB.velocity.y) < 0.1f)
                playerAnim.SetBool("Falling", false);
            canMove = true;
        }

        // Jumping
        if (!onWall && RB.velocity.y < 0)
        {
            RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            playerAnim.SetBool("Falling", true);
        }

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            toJump = true;
        }
        // 重置二段跳及蹬墙跳
        if (onGround || onWall)
        {
            jumpCount = 2;
            wallJumped = false;
            hasDashed = false;
        }
        // 如果走下平台的话，跳跃数-1
        if (!onGround && !onWall && jumpCount == 2)
            jumpCount -= 1;

        // WallJump
        if (Input.GetButtonDown("Jump") && onWall && !wallJumped)
        {
            toJump = true;
        }
        if(!onWall)
        {
            playerAnim.SetBool("WallSliding", false);
        }
        
        // Dash
        if (Input.GetButtonDown("Fire2") && !hasDashed && !onWall)
        {
            toDash = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
            
        if (toJump)
        {
            if(!onWall || onWall && onGround)
            Jump(Vector2.up);
            else if (!onGround)
            WallJump();
        }

        if(onWall && !onGround)
        {
            WallSlide();
        }

        if (toDash)
        {
            Dash(faceRight);
        }
    }

    /*
     * Actions Below
     */
    void Flip()
    {
        // Record NowFacing
        faceRight = !faceRight;
        // Core Function
        transform.Rotate(0f, 180f, 0f);
    }

    void Jump(Vector2 JumpDir)
    {
        // Core function
        if (onGround || onWall)
        {
            playerAnim.SetBool("DoubleJumpped", false);
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.velocity += JumpDir * jumpForce;
            jumpCount--;
            toJump = false;
        }else if (jumpCount > 0)
        {
            playerAnim.SetBool("DoubleJumpped", true);
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.velocity += JumpDir * jumpForce;
            jumpCount --;
            toJump = false;
        }
    }

    void Move()
    {
        if (!canMove)
            return;
        //Remember for walking
        playerAnim.SetFloat("Speed", Mathf.Abs(direction));

        if (Mathf.Abs(direction) > 0.2f)
        {
            // Animator Parameter
            playerAnim.SetBool("Movement", true);

            // Core function
            RB.velocity = new Vector2(direction * speed, RB.velocity.y);

            // Flipping only happens when moving around
            faceNow = (direction > 0.2f);
            if (faceNow != faceRight) { Flip(); }

            if (wallJumped)
            {
                RB.velocity = Vector2.Lerp(RB.velocity, (new Vector2(direction * speed, RB.velocity.y)), .5f * Time.deltaTime);
            }
        }
        else
        {
            playerAnim.SetBool("Movement", false);
            //防止平地上人物因为惯性滑动
            RB.velocity = new Vector2(0, RB.velocity.y); 
        }
        
    }

    void WallSlide()
    {
        // initialize
        pushingWall = false;
        if (!canMove)
            return;
        // Animation
        playerAnim.SetBool("WallSliding", true);

        // Detection
        if (onWall && Mathf.Abs(direction) > 0.2f && RB.velocity.x == 0)
            pushingWall = true;
        // Core function
        float push = pushingWall ? 0 : RB.velocity.x;

        RB.velocity = new Vector2(push, -slideSpeed);
    }
    
    void WallJump()
    {
        playerAnim.SetBool("DoubleJumpped", false);

        // 跳出去之后的0.2s内禁止操作
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector2 wallFront = faceRight ? Vector2.left : Vector2.right;
        
        Jump(Vector2.up  + wallFront);
        Flip();

        wallJumped = true;
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void Dash(bool faceright)
    {
        // Core Function
        hasDashed = true;
        toDash = false;
        RB.velocity = Vector2.zero;
        Vector2 dashDir = faceright ? Vector2.right : Vector2.left;
        RB.velocity += dashDir.normalized * dashSpeed;
        // Dash wait
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        // Setting constraints
        RB.gravityScale = 0;
        wallJumped = true; // disable Dash n walljump

        yield return new WaitForSeconds(.3f);

        RB.gravityScale = 3;
        wallJumped = false;
    }
}
