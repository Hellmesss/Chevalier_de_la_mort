using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D Player;
    SpriteRenderer PlayerRenderer;
    public Animator playerAnim;
    bool facingRight = true;
    public float maxSpeed;
    bool grounded = true;
    float groundCheckEdge = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower;
    bool canMove = true;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    bool isAttacking = false;
    float attackRate = 2f;
    float nextAttackTime = 3f;

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        PlayerRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        //JUMP
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("IsGrounded", false);
            Player.velocity = new Vector2(Player.velocity.x, 0f);
            Player.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        grounded = Physics2D.OverlapBox(groundCheck.position, transform.localScale / 2, groundCheckEdge, groundLayer);
        playerAnim.SetBool("IsGrounded", grounded);
        float move = Input.GetAxis("Horizontal");

        //DEPLACEMENT
        if (canMove == true)
        {
            {
                if (move < 0 && facingRight == true) { Flip(); }
                else if (move > 0 && facingRight == false) { Flip(); }
                Player.velocity = new Vector2(move * maxSpeed, Player.velocity.y);
                playerAnim.SetFloat("MoveSpeed", Mathf.Abs(move));
            }
        }
        else
        {
            Player.velocity = new Vector2(0, Player.velocity.y);
            playerAnim.SetFloat("MoveSpeed", 0);
        }
        Vector3 thePosition = transform.localPosition;


        //ATTACK
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 3f / attackRate;
            }
        }

        toggleMove();

        //STOP CHARACTER IF ATTACKING
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }


        //RESET POS IF FALL
        if (thePosition.y < -15) { comeback(); TakeDamage(101); }

        //DIE
        if (currentHealth<=0) { Die(); }

    }

    //DESTROY BAT
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            if (isAttacking)
            {
                Destroy(other.gameObject);
            }
            else { TakeDamage(20); }

        }
    }

    //FLIP SPRITE
    void Flip()
    {
        facingRight = !facingRight;
        PlayerRenderer.flipX = !PlayerRenderer.flipX;
    }

    //RESET POS
     public void comeback()
    {
        Vector3 thePosition = transform.localPosition;
        thePosition.x = -3;
        thePosition.y = 0;
        transform.localPosition = thePosition;
    }

    //ATTACK
    void Attack()
    {
        playerAnim.SetTrigger("Attack");
    }

    //TAKE DAMAGES
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            playerAnim.SetTrigger("Hurt");
        }
    }

    //DEATH
    public GameObject deathMenu;
    public void Die()
    {
        playerAnim.SetTrigger("Death");
        deathMenu.SetActive(true);
    }

    //HEAL
    public void HealLifePoint(int lifepoint)
    {

            currentHealth = currentHealth + lifepoint;
            healthBar.SetHealth(currentHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    private void toggleMove()
    {
        if (isAttacking == true)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
}