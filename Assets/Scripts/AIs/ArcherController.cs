using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator ani;
    private Transform playerTrans;
    private Vector2 archerTransformVector;
    private Vector2 playerPointer;
    private Vector2 playerTransformVector;
    private enemyHP hp;

    public GameObject projectile;
    public float projectileSpeed = 20f;
    public float damage = 20;
    public LayerMask enemyLayers;
    public float attackRange;
    public float aggroDistance;
    public float speed;
    public float attackSpeed = .5f;
    public float timeBeforeShooting;
    public float knockBackTime = .5f;
    public float dropGold;

    private float moveX;
    public float playerDistance;
    private float timeSinceLastAttack = 0;
    private bool notHit = true;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        ani = GetComponent<Animator>();
        hp = GetComponent<enemyHP>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hp.health = 50f + 50f * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
        damage += 0.5f * damage * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        CalculateDistance();
        //Animations here

        if (hp.getHP() > 0) {
            PointTowardsPlayer();
            if (playerDistance < aggroDistance && notHit && playerDistance > attackRange)
            {
                FollowPlayer();
            }
            else if (notHit)
            {
                moveX = 0;
            }

            if (playerDistance < attackRange && attackSpeed <= timeSinceLastAttack)
            {
                Shoot();
            }
        }
        if (hp.getHit() == true)
        {
            Hit();
            hp.hitReceived();
        }
        if (hp.getHP() <= 0) {
            moveX = 0;
            ani.SetBool("Death",true);
        }
    }
    void PointTowardsPlayer()
    {
        if (playerTransformVector.x - archerTransformVector.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), col);
        }
    }
    void FollowPlayer()
    {
        if (playerTransformVector.x - archerTransformVector.x > 0)
        {
            moveX = 1;
        }
        else
        {
            moveX = -1;
        }
    }
    void CalculateDistance()
    {
        playerTransformVector = new Vector2(playerTrans.position.x, playerTrans.position.y);
        archerTransformVector = new Vector2(transform.position.x, transform.position.y);

        playerDistance = Mathf.Abs(playerTrans.position.x - transform.position.x);

        //If vertical distance is too large, ignore
        if (Mathf.Abs(playerTrans.position.y - transform.position.y) > 1)
        {
            playerDistance = 10000;
        }
    }

    void Shoot()
    {
        timeSinceLastAttack = 0;
        ani.SetTrigger("Shoot");
        Invoke("CreateProjectile", timeBeforeShooting);
        
    }

    void CreateProjectile()
    {
        float direction = playerTransformVector.x - archerTransformVector.x;
        direction = direction / Mathf.Abs(direction);
        GameObject proj = Object.Instantiate(projectile, new Vector3(this.transform.position.x + (direction * .5f), this.transform.position.y, 0), new Quaternion());
        proj.GetComponent<ProjectileController>().damage = this.damage;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * direction, 3f);

    }
    void Hit()
    {
        notHit = false;
        ani.SetTrigger("Hit");
        Knockback();
        Invoke("NotHitReset", knockBackTime);
    }

    void NotHitReset()
    {
        notHit = true;
    }

    void Knockback()
    {
        if (playerTransformVector.x - archerTransformVector.x > 0)
        {
            moveX = -1;
        }
        else
        {
            moveX = 1;
        }
    }
}