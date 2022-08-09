using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Animator Anim;

    [Header("Position Info")]
    public LayerMask ground;
    public Transform groundCheck;
    [SerializeField] private bool onGround;

    [Header("Move")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float Hdirection;
    [SerializeField] private Vector2 MoveVector;
    private bool faceRight = true;
    private bool faceNow;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int jumpCount;
    public float fallMultiplier = 3f;//Scale of gravity when falling

    [Header("Stealth")]
    [SerializeField] private float stealthSpeed = 2f;
    [SerializeField] private bool toStealth = false; // 按键来翻转它，控制是否进入潜行状态
    [SerializeField] private bool canStealth; // 判断是否在潜行区域内
    public LayerMask StealthArea;
    public Transform StealthCheck;

    [Header("Dash")]
    [SerializeField] private bool canDash = true; // 是否可以冲刺
    [SerializeField] private bool isDashing; // 正在冲刺中
    [SerializeField] private float dashingPower = 20f; //冲刺的力量
    [SerializeField] private float dashingTime = 0.1f; //冲刺的时间
    [SerializeField] private float dashCooldown = 2f; //冲刺的冷却时间
    [SerializeField] private TrailRenderer dashTrail;
    private float DashDir;

    // Setting InputMap to Gameplay Map
    // to make sure that the methods below are conducted;
    void OnEnable()
    {
        playerInput.onMove += Move;
        playerInput.onStopMove += StopMove;
        playerInput.onJump += Jump;
        playerInput.onStealth += Stealth;
        playerInput.onDash += toDash;
    }
    void OnDisable()
    {
        playerInput.onMove -= Move;
        playerInput.onStopMove -= StopMove;
        playerInput.onJump -= Jump;
        playerInput.onStealth -= Stealth;
        playerInput.onDash -= toDash;
    }
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        playerInput.EnableGameplayInput();
    }

    void Update()
    {
        // Position Maintainance
        onGround = Physics2D.OverlapCircle(groundCheck.position, .05f, ground);
        canStealth = Physics2D.OverlapCircle(StealthCheck.position, .05f, StealthArea);
        // Animation
        Anim.SetFloat("SpeedY", RB.velocity.y);
        Anim.SetBool("onGround", onGround);

        // Setting states
        if (onGround)
        {
            // Animation
            if (Mathf.Abs(RB.velocity.y) < 0.1f)
                Anim.SetBool("Falling", false);
            // Setting value
            jumpCount = 2;
            
        }

        if (!onGround)
        {   
            if(RB.velocity.y < 0)
            {
                // Animation
                Anim.SetBool("Falling", true);
            }

            if (jumpCount == 2)
                jumpCount -= 1;
        }

    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        // Continue Moving
        RB.velocity = new Vector2(Hdirection * Speed, RB.velocity.y);
    }

    void Move(Vector2 dir)
    {
        Hdirection = dir.x;
        // Animation
        Anim.SetFloat("Speed", Mathf.Abs(Hdirection)); // Remember for walking
        Anim.SetBool("Movement", true);

        if(RB.velocity.y < 0)
            // Multiplying Gravity when falling
            RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        faceNow = (Hdirection > 0.1f);
        if (faceNow != faceRight) { Flip(); }
    }
    void Flip()
    {
        // Record NowFacing
        faceRight = !faceRight;
        // Core Function
        transform.Rotate(0f, 180f, 0f);
    }

    void StopMove()
    {
        Anim.SetFloat("Speed", 0f);
        Anim.SetBool("Movement", false);
        Hdirection = 0f;
    }

    void Jump()
    {
        if (onGround)
        {
            // Animation
            Anim.SetBool("DoubleJumpped", false);
            // Ordinary Jump
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.velocity += Vector2.up * jumpForce;
            jumpCount--;
        }else if (jumpCount > 0)
        {
            // Animation
            Anim.SetBool("DoubleJumpped", true);
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.velocity += Vector2.up * jumpForce;
            jumpCount--;
        }
    }


    void toDash()
    {
        if (canDash)
            StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = RB.gravityScale;
        RB.gravityScale = 0;

        // Dash
        DashDir = faceRight ? 1 : -1;
        RB.velocity = new Vector2(DashDir * dashingPower, 0f);
        // dashTrail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        RB.gravityScale = originalGravity;
        // dashTrail.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    void Stealth()
    {
        if (!onGround || !canStealth)
            return;
        toStealth = !toStealth; // 点按切换
        // TODO
    }
}
