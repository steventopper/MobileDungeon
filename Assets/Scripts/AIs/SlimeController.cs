using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int jumpSpeed;
    public Transform player;
    public BoxCollider2D isGroundedCollider;
    public LayerMask baseLayer;
    public enemyHP hp;
    public float attack = 15;
    public bool isAttacking = false;
    public bool isGrounded = true;
    public float aggroDistance;
    public Animator animator;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = GetComponent<enemyHP>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        hp.health = 75f + 75f * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
        attack += 0.5f * attack * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
    }
    // Update is called once per frame
    void Update()
    {
        //updating animation variables not including triggers
        animator.SetBool("IsGrounded",IsGrounded());
        animator.SetFloat("VelocityY",rb.velocity.y);

        //end of animaton variables
        if (IsGrounded() && !isAttacking && (Mathf.Abs(player.position.x - this.transform.position.x)) < aggroDistance && hp.getHP() >0)
        {
            StartCoroutine(Attack());
            isAttacking = true;
        }
        if(hp.getHit() == true && hp.getHP() > 0)
        {
            animator.SetTrigger("Hit");
            hp.hitReceived();
        }
        if (hp.getHP() <= 0)
        {
            animator.SetBool("Death",true);
            //freeze position and dont interact with collisions
            if (IsGrounded())
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                col.enabled = false;
            }
            
            //GameObject.Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hp.getHP() > 0)
        {
            collision.gameObject.GetComponent<playerController>().Hit(attack);
        }
        if(collision.gameObject.tag == "Player"){
            
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), col);
        }
    }
    IEnumerator Attack()
    {
        
        Vector2 playerDist = this.transform.position - player.position;
        yield return new WaitForSeconds(3f);
        float playerDir = -playerDist.x / Mathf.Abs(playerDist.x);
        rb.velocity = new Vector2(jumpSpeed * playerDir, jumpSpeed);
        isAttacking = false;
    }
    private bool IsGrounded()
    {
        isGrounded = isGroundedCollider.IsTouchingLayers(baseLayer);
        return isGrounded;
    }
}
