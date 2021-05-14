using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKnightMotor : MonoBehaviour
{
    //Set in code
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator ani;
    private Transform playerTrans;
    private Vector2 knightTransformVector;
    private Vector2 playerPointer;
    private Vector2 playerTransformVector;
    private enemyHP hp;

    //set in editor
    //public float health = 100f;
    public GameObject rightAttackPoint;
    public GameObject leftAttackPoint;
    public float damage = 10;
    public LayerMask enemyLayers;
    public float attackRange;
    public float speed;
    public float agroDistance;
    public float dontMoveDistance;
    public float attackSpeed = .5f;//in seconds
    public float timeBeforeLethalStab;
    public float knockBackTime = .2f;
    public float attackDistance = 1f;

    //helper variables
    private float moveX;
    public float playerDistance;
    private float timeSinceLastAttack = 0;
    private bool notHit = true;
    //I would like to add a veritcal component to the knockback as well as its own independent speed
    //private float moveY;
    //private float knockBackSpeed;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        ani = GetComponent<Animator>();
        hp = GetComponent<enemyHP>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hp.health = 100f + 100f * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
        damage += 0.5f * damage * GameObject.Find("GameEngine").GetComponent<EngineController>().floorNum;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack+=Time.deltaTime;
        CalculateDistance();
        Animations();
        if(playerDistance < agroDistance && notHit && hp.getHP()>0 && playerDistance>dontMoveDistance){

            FollowPlayer();
        }
        else if (notHit){
            moveX = 0;
        }

        if(playerDistance< attackDistance && attackSpeed <= timeSinceLastAttack&& hp.getHP()>0){
            Stab();
        }
        if(hp.getHit() == true)
        {
            Hit();
            hp.hitReceived();
        }
        
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX*speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), col);
        }
    }
    void Animations()
    {
        if(playerDistance < agroDistance){
            ani.SetBool("Wake",true);
        }
        else{
            ani.SetBool("Wake",false);
        }

        if(moveX != 0){
            ani.SetBool("Run",true);
        }
        if(moveX < 0 && notHit){
            sprite.flipX = true;
        }
        if(moveX > 0 && notHit){
            sprite.flipX = false;
        }
        if(hp.getHP() <= 0){
            ani.SetBool("Death",true);
        }
    }

    void FollowPlayer(){
        if(playerTransformVector.x-knightTransformVector.x > 0){
            // you know that the player is to the right of you
            moveX = 1;
        }
        else if(playerTransformVector.x-knightTransformVector.x < 0){
            moveX = -1;
        }
    }
    void CalculateDistance(){
        playerTransformVector = new Vector2(playerTrans.position.x,playerTrans.position.y);
        knightTransformVector = new Vector2(transform.position.x,transform.position.y);

        playerDistance = Vector2.Distance(playerTransformVector,knightTransformVector); // This shows the distance from the player
    }
    void Stab(){
        timeSinceLastAttack = 0;
        ani.SetTrigger("Stab");
        Invoke("lethalStab",timeBeforeLethalStab);
    }
    void lethalStab(){
        if(sprite.flipX == false) {
            Collider2D[] rightHitEnemies = Physics2D.OverlapCircleAll(rightAttackPoint.transform.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in rightHitEnemies)
            {
                //Debug.Log("We hit "+enemy.name + " on left");
                enemy.GetComponent<playerController>().Hit(damage);
            }
        }
        else if (sprite.flipX == true) {
            Collider2D[] leftHitEnemies = Physics2D.OverlapCircleAll(leftAttackPoint.transform.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in leftHitEnemies)
            {
                //Debug.Log("We hit "+enemy.name + " on right");
                enemy.GetComponent<playerController>().Hit(damage);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rightAttackPoint.transform.position, attackRange);
        Gizmos.DrawWireSphere(leftAttackPoint.transform.position, attackRange);

        //Gizmos.DrawWireSphere(transform.position, playerDistance);
    }
    
    void Hit()
    {
        notHit = false;
        ani.SetTrigger("Hit");
        KnockBack();
        Invoke("NotHitReset",knockBackTime);
        
    }
    
    void NotHitReset(){
        notHit = true;
    }
    void KnockBack()
    {
        if(playerTransformVector.x-knightTransformVector.x > 0){
            // you know that the player is to the right of you
            moveX = -1;
        }
        else if(playerTransformVector.x-knightTransformVector.x < 0){
            moveX = 1;
        }
    }
}
