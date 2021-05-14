using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;
using UnityEngine.SceneManagement;

//This script will take input from player and move and animate character properly
public class playerController : MonoBehaviour
{
    //All variables set inside the editor
    public float moveSpeed;
    public float jumpForce;
    public float blockTime;
    public bool useKeyBoard; //If this is true then Jump is binded to Space and Move is binded to A and D
    public GameObject m_slideDust;
    public Joystick joystick;

    public GameObject BlockButton;
    public GameObject AttackButton;
    public Button PauseButton;
    public GameObject PotionButton;
    public GameObject JumpButton;

    private bool canPotion = false;

    public LayerMask baseLayer;
    public LayerMask enemyLayers;
    public float attackRange;
    public float health = 100f;
    public float damage = 10;
    public float maxJumpTime = 2f;
    public float attackRate = 0.5f;

    public float gold;

    public GameObject isGroundedObject;
    public GameObject rightAttackPoint;
    public GameObject leftAttackPoint;
    public GameObject healthbar;

    //All variables set using code
    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer sprite;
    private CapsuleCollider2D playerCollider;

    public AudioSource jumpAudio;
    public AudioSource blockAudio;
    public AudioSource blockFailAudio;
    public AudioSource attackAudio;
    public AudioSource potionAudio;
    public AudioSource hurtAudio;
    public AudioSource enemyHitAudio;


    //variables used in code to keep track of state
    private bool block = false;
    private int animationState = 0;
    private float timeSinceAttack = 0.0f;
    private int nextAttack = 1;
    
    //misc. variables
    private float wallDistance = 0.5f;

    //Potion Vars
    public float potionHeal;
    private float timeSinceLastPotion = 0.0f;
    public float potionCoolDown;
    private float startHealth;

    //Vars used for setting background
    public SpriteRenderer background;
    public Sprite[] skies;
    
    // Start is called before the first frame update
    private GameObject entityManager;
    void Start()
    {
        //jumpAudio = GetComponentInChildren<AudioSource>();
        //blockAudio = GetComponentInChildren<AudioSource>();
        //blockFailAudio = GetComponentInChildren<AudioSource>();
        //attackAudio = GetComponentInChildren<AudioSource>();

        



        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        
        entityManager = GameObject.FindGameObjectWithTag("EntityManager");

        //AttackButton.onClick.AddListener(Attack);
        //BlockButton.onClick.AddListener(Block);
        PauseButton.onClick.AddListener(Pause);
        //PotionButton.onClick.AddListener(Potion);
        //JumpButton.onClick.AddListener(Jump);
        EventTrigger.Entry attack = new EventTrigger.Entry();
        attack.eventID = EventTriggerType.PointerDown;
        attack.callback.AddListener((data) => { Attack(); });
        AttackButton.GetComponent<EventTrigger>().triggers.Add(attack);

        EventTrigger.Entry block = new EventTrigger.Entry();
        block.eventID = EventTriggerType.PointerDown;
        block.callback.AddListener((data) => { Block(); });
        BlockButton.GetComponent<EventTrigger>().triggers.Add(block);

        EventTrigger.Entry potion = new EventTrigger.Entry();
        potion.eventID = EventTriggerType.PointerDown;
        potion.callback.AddListener((data) => { Potion(); });
        PotionButton.GetComponent<EventTrigger>().triggers.Add(potion);

        EventTrigger.Entry jump = new EventTrigger.Entry();
        jump.eventID = EventTriggerType.PointerDown;
        jump.callback.AddListener((data) => { Jump(); });
        JumpButton.GetComponent<EventTrigger>().triggers.Add(jump);

        equipItems();
        startHealth = health;
        health = health * GameObject.Find("Game Engine").GetComponent<EngineController>().currHealthPercentage;
        potionHeal = startHealth * .15f + 10f;
        background.sprite = skies[GameObject.Find("Game Engine").GetComponent<EngineController>().floorNum];
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health <= 0) {
            SceneManager.LoadScene("GameOver");
        }
        timeSinceLastPotion += Time.deltaTime;
        timeSinceAttack += Time.deltaTime;
        if(timeSinceLastPotion>potionCoolDown)
        {
            PotionButton.GetComponent<Button>().interactable = true;
            canPotion = true;
        }
        else
        {
            PotionButton.GetComponent<Button>().interactable = false;
            canPotion = false;
        }
        Animations();
    }

    void FixedUpdate()
    {
        Move();
        if (useKeyBoard == true)
        {
            Jump();
        }
    }
    
    void Move()
    {
        float x;
        if(useKeyBoard == true)
        {
            x = Input.GetAxis("Horizontal");
        }
        else
        {
            x = joystick.Horizontal;
        }
        float moveBy = x * moveSpeed;
        rb.velocity = new Vector2(moveBy,rb.velocity.y);
    }
    void Jump()
    {
        if (useKeyBoard == true)
        {
            if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpAudio.Play();
                ani.SetTrigger("Jump");
            }
        }
        else
        {
            if (IsGrounded()) {
                jumpAudio.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                ani.SetTrigger("Jump");
            }
        }
    }
    void Potion()
    {
        if(timeSinceLastPotion >= potionCoolDown && canPotion) {
            potionAudio.Play();
            health += potionHeal;
            if(health >= startHealth) {
                health = startHealth;
            }
            PotionButton.GetComponent<Button>().interactable = false;
            timeSinceLastPotion = 0.0f;
        }
    }
    void Attack()
    {
        if(timeSinceAttack > attackRate){
            ani.SetTrigger("Attack" + nextAttack);
            //attackAudio.Play();
            if(sprite.flipX == false) {
                Collider2D[] rightHitEnemies = Physics2D.OverlapCircleAll(rightAttackPoint.transform.position, attackRange, enemyLayers);
                foreach(Collider2D enemy in rightHitEnemies)
                {
                    //Debug.Log("We hit "+enemy.name + " on right");
                    if (enemy.GetComponent<enemyHP>().getHP() > 0f)
                        enemyHitAudio.Play();
                    enemy.GetComponent<enemyHP>().Hit(damage);
                }
            }
            else if (sprite.flipX == true) {
                Collider2D[] leftHitEnemies = Physics2D.OverlapCircleAll(leftAttackPoint.transform.position, attackRange, enemyLayers);
                foreach(Collider2D enemy in leftHitEnemies)
                {
                    //Debug.Log("We hit "+enemy.name + " on left");
                    if (enemy.GetComponent<enemyHP>().getHP() > 0f)
                        enemyHitAudio.Play();
                    enemy.GetComponent<enemyHP>().Hit(damage);
                }
            }
            nextAttack++;
            timeSinceAttack = 0f;
            if(nextAttack> 3)
                nextAttack = 1;
        }
        
    }
    void Block()
    {
        block = true;
        ani.SetTrigger("Block");
        
        Invoke("BlockOver", blockTime);
    }
    void BlockOver(){
        block = false;
    }
    void Pause()  {
        GameObject.Find("Game Engine").GetComponent<SceneTracker>().updateScene(SceneManager.GetActiveScene().name);
        PauseMenu.PauseGame();
    }
    void Animations() //other than triggers
    {
        
        if(rb.velocity.x > 0) {
            animationState = 1;
            sprite.flipX = false;
        }
        if(rb.velocity.x < 0) {
            animationState = 1;
            sprite.flipX = true;
        }
        
        if(rb.velocity.x == 0) {
                animationState = 0;
                
            }
        WallSlide();
        Roll();
        ani.SetInteger("AnimState", animationState);
        ani.SetFloat("AirSpeedY",rb.velocity.y);
        ani.SetBool("Grounded",IsGrounded());

    }
    void WallSlide()
    {
        
        RaycastHit2D leftray = Physics2D.Raycast(playerCollider.bounds.center,Vector2.left,wallDistance,baseLayer);
        RaycastHit2D rightray = Physics2D.Raycast(playerCollider.bounds.center,Vector2.right,wallDistance,baseLayer);

        Debug.DrawRay(playerCollider.bounds.center,Vector2.left*wallDistance);
        Debug.DrawRay(playerCollider.bounds.center,Vector2.right*wallDistance);
        if(leftray.collider != null && rb.velocity.y <0){
            rb.gravityScale = 1;
            ani.SetBool("WallSlide",true);
            sprite.flipX = true;
        }
        else if(rightray.collider != null  && rb.velocity.y <0) {
            rb.gravityScale = 1;
            ani.SetBool("WallSlide",true);
            sprite.flipX = false;
        }
        else {
            rb.gravityScale = 4;
            ani.SetBool("WallSlide",false);
        }

    }
    void Roll(){
        if (Input.GetKeyDown("left shift")){
            ani.SetTrigger("Roll");
            
        }
    }
    private bool IsGrounded() {
        return isGroundedObject.GetComponent<BoxCollider2D>().IsTouchingLayers(baseLayer);
    }
    //Lets us see the range our attacks will have
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rightAttackPoint.transform.position, attackRange);
        Gizmos.DrawWireSphere(leftAttackPoint.transform.position, attackRange);
    }
    //Enemy will call this method if we are blocking we wont take damage
    public void Hit(float dmg) {
        if(block){
            //Debug.Log("You blocked a knight hit");
            
        }
        else if (!GameObject.Find("Game Engine").GetComponent<EngineController>().cheats)
        {
            //Debug.Log("Knight hit you");
            health = health - dmg;
            
            ani.SetTrigger("Hurt");
            hurtAudio.Play();
            //healthbar.GetComponent<HealthBar>().onTakeDamage(dmg);
        }
        
    }
    public void addGold(float moreGold){
        gold += moreGold;
    }
    void AE_SlideDust()
    {

        if (m_slideDust != null)
        {
            /*
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, player.transform.position, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(1, 1, 1);
            */
        }
    }
    
    void equipItems()
    {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        Item[] equipment = engine.playerEquipment;
        Item item1 = equipment[0];
        Item item2 = equipment[1];
        Item item3 = equipment[2];
        Item item4 = equipment[3];

        //Change base stats here
        moveSpeed = 10 + item1.bonusSpeed + item2.bonusSpeed + item3.bonusSpeed + item4.bonusSpeed;
        jumpForce = 20 + item1.bonusJump + item2.bonusJump + item3.bonusJump + item4.bonusJump;
        damage = 10 + item1.bonusAttack + item2.bonusAttack + item3.bonusAttack + item4.bonusAttack;
        health = 100 + item1.bonusHealth + item2.bonusHealth + item3.bonusHealth + item4.bonusHealth;
        HealthBar healthBar = GameObject.Find("HealthBar").GetComponentInChildren<HealthBar>();
        healthBar.setStartHealth(health);
    }
}
