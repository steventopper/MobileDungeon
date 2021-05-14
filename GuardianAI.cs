using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour {
	private SpriteRenderer sprite;
	private Rigidbody2D rigidB;
	private CapsuleCollider2D collide;
	private Animator animation;
	private Vector2 GuardianTransform;
	private Transform playerTransformed;
	private Vector2 playerPointer;
	private Vector2 playerTransform;
	private enemyHP hp;


	public float dmg = 10;
	public float speed;
	public float attackSpeed = 1f;
	public float attackRange;
	public float engageRange;

	private float moveXaxis;
	private float playerDistanceFrom;
	private float timeAfterLastAttack = 0;
	private bool notHit = true;

	// Start is called before the first frame update
	void Start()
	{
		animation = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		rigidB = GetComponent<Rigidbody2D>();
		collide = GetComponent<CapsuleCollider2D>();
		hp = GetComponent<enemyHP>();
		playerTransformed = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		//Timer for after the Guardians last attack, needed so the enemy cant just spam hit the Player.
		timeAfterLastAttack += Time.deltaTime;

		//Calculates the distance to teh Player to set up next move.
		CalculateDistance();


		if (hp.getHP() > 0) {
			if (playerDistanceFrom < engageRange && notHit && playerDistanceFrom > attackRange)
			{
				FollowPlayer();
			}
			else if (notHit)
			{
				moveXaxis = 0;
			}

			if (playerDistanceFrom < attackRange && attackSpeed <= timeAfterLastAttack)
			{
				Hit();
			}
		}
		if (hp.getHit() == true)
		{

			hp.hitRecived();
		}
	}
	void FixedUpdate()
	{
		rigidB.velocity = new Vector2(moveXaxis * speed, ridgidB.velocity.y);
	}

	void FollowPlayer()
	{
		if (playerTransform.x - GuardianTransform.x > 0)
		{
			//Teleport forwards 5 spots on X axis
			moveXaxis = 5;
		}
		else
		{
			//Teleport back three spots on X axis
			moveXaxis = -3;
		}
	}

	void CalculateDistance()
	{
		//Set Player distance from Guardian by passing both positions in as parameters.
		GuardianTransform= new Vector2(transform.position.x, transform.position.y);
		playerTransform= new Vector2(playerTransformed.position.x, playerTrans.position.y);
		playerDistanceFrom = Vector2.Distance(playerTransform, GuardianTransform);
	}
		

	void Hit()
	{
		//Function for when Guardian hits Player
		animation.SetTrigger("Hit");
		notHit = false;
		Invoke("NotHitReset");
	}

	void NotHitReset()
	{
		notHit = true;
	}


} 