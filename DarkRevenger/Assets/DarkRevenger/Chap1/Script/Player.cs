using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{
    public Image nowHpbar;
        // Some variables and integers are serialized to facilitate run time Debug in the inspector
        [Header("References")]
    [SerializeField] Animator animator;             // Reference the Animator component  
    [SerializeField] public Rigidbody2D rb;         // Refecenre the Rigidbody component
    [SerializeField] public Transform groundCheck;  // Reference the GrounCheck Object transform
    [SerializeField] LayerMask groundLayer;         // Reference the Ground Layer
    [SerializeField] TrailRenderer tr;              // Reference the Trail Renderer for Dash

    [Header("Movement and Jumping variables")]
    [SerializeField] float horizontalMove;          // Reference the Horizontal values
    [SerializeField] float speed = 8f;              // Reference the Runing Speed
    [SerializeField] float jumpingPower = 16f;      // Reference the Jump force
    public bool isFacingRight = true;     // Reference for checking if the Player is facing Right
    [SerializeField] bool doubleJump;               // Reference for Double Jump
    
    [Header("Dashing variables")]
    [SerializeField] bool canDash = true;           // Reference for the Player if Can Dash
    public bool isDashing;                // Reference if the Player is Dashing
    [SerializeField] float dashingPower = 20f;      // Reference the Dashing force
    [SerializeField] float dashingTime = 0.5f;      // Reference the Dashing time
    [SerializeField] float dashingCooldown = 1f;    // Reference the Dashing cooldown

    bool isSwordManDead = false;
    public bool attacked = false;
    public Status status;
    bool isHit = false;
    public UnitCode unitcode;
    [SerializeField] float hitAnimationDuration = 0.3f; // Duration of hit animation
    int previousHp;

    void Update() {
        if (isSwordManDead || isHit) return;
        horizontalMove = Input.GetAxisRaw("Horizontal");        // Get the Horizontal Input Axis values
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;
        
        if (status.nowHp < previousHp)
        {
            StartCoroutine(PlayHitAnimation());
            previousHp = status.nowHp;
        }
        
        if (Input.GetKeyDown(KeyManager.defaultKeys[0])) {                  // Press "Jump"  for jump function
            if (IsGrounded() || doubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                animator.SetBool("Jump", true);
                doubleJump = !doubleJump;
            }
        }
        if (Input.GetKeyUp(KeyManager.defaultKeys[0]) && rb.velocity.y > 0f) {  // Release "Jump" for short jump of hold for longer jump 
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyManager.defaultKeys[5]) && canDash){    // Press KeyCode to Dash
            StartCoroutine(Dash());
        }
        
        Flip();             // Flip function call
    }

    void FixedUpdate() {
        if (isDashing) {    // Check if IsDashing
            return;
        }
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y); // Move the Player Horizontal
        
        animator.SetFloat("xVelocity", rb.velocity.x);  // Reference the xVelocity for the Animator
        animator.SetFloat("yVelocity", rb.velocity.y);  // Reference the yVelocity for the Animator
    }

    // GroundCheck function
    bool IsGrounded() {     
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Dash function "has its own flipping check to prevent dashing to wrong direction"
    IEnumerator Dash() { 
        canDash = false;
        isDashing = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        if (isFacingRight && horizontalMove > 0f ) {    // Dash to the "Right" if facing right and horizontal > 0
            rb.velocity = Vector2.right * dashingPower;
        }
        if (!isFacingRight && horizontalMove < 0f ) {   // Dash to the "Left" if facing left and horizontal < 0
            rb.velocity = Vector2.left * dashingPower;
        }       

        tr.emitting = true;                             // Enable the Trail Renderer
        yield return new WaitForSeconds(dashingTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        tr.emitting = false;                            // Disable the Trail Renderer
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    // Flip Player function "based on Rotation not LocalScale for shooting Projectiles the right direction"    
    void Flip() {
        if (isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f) {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    // Start is called before the first frame update
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    IEnumerator CheckPlayerDeath()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        while(true)
        {
        // 체력이 0이하일 때
        if (nowHpbar.fillAmount <= 0)
            {
                isSwordManDead = true;
                animator.SetTrigger("Dead");
                yield return new WaitForSeconds(2); // 2초 기다리기
                SceneManager.LoadScene(currentScene.name);
            }
            yield return new WaitForEndOfFrame(); // 매 프레임의 마지막 마다 실행
        }
    }

    public void TakeDamage(int damage){
        status.nowHp -= damage;
    }

    IEnumerator PlayHitAnimation() {
        isHit = true;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(hitAnimationDuration);
        isHit = false;
    }
    void Start()
    {
        StartCoroutine(CheckPlayerDeath());
        status = new Status();
        status = status.SetUnitStatus(unitcode);
        animator = GetComponent<Animator>();
        previousHp = status.nowHp;
    }
}
