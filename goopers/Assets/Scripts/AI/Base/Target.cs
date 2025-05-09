using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour, IEnemyMoveable
{
    private float enemyHealth;
    public float enemyMovementSpeed;
    private float enemyDamage;
    private float enemyDamageCooldown;
    
    private bool inCoroutine;
    public bool damageTDOn;
    private bool inTDCoroutine;
    public bool damagePlayerOn;
    private bool inPlayerCoroutine;

    
    
   
    private GameObject otherSlime;

    [SerializeField] private GameBoss gameBoss;
    [SerializeField] GameObject gameBossObject;
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    private AudioSource _source;
    [SerializeField] private AudioClip takeDamage;
    [SerializeField] private AudioClip die;

    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _source = GetComponent<AudioSource>();
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        
        enemyHealth = Random.Range(1.0f * (gameBoss.globalDifficulty * 0.2f), 3.0f * (gameBoss.globalDifficulty * 0.15f));
        enemyMovementSpeed = 2f * (gameBoss.globalDifficulty * 0.09f);
        
        
        //Enemy damage calculation
        enemyDamage = 1f;
        // if (gameBoss.globalDifficulty >= 3)
        // {
        //     enemyDamage = 1f * (gameBoss.globalDifficulty * 0.25f);
        // }

        enemyDamageCooldown = 5f;
        // if (gameBoss.globalDifficulty >= 4)
        // {
        //     enemyDamageCooldown = 5f * (1 / (gameBoss.globalDifficulty * 0.25f));
        // }
        
        //Init walk state
        StateMachine.Initialize(WalkState);
    }

    public void EnemyTakeDamage(float damage)
    {
        _source.PlayOneShot(takeDamage, 0.75f);
        _source.pitch = Random.Range(0.85f, 1.2f);
        enemyHealth -= damage;
        if (enemyHealth <= 0.0f)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        if (!inCoroutine)
        {
            _source.PlayOneShot(die, 0.75f);
            _source.pitch = Random.Range(0.85f, 1.2f);
            StartCoroutine("DieSound");
        }

    }

    private IEnumerator DieSound()
    {
        gameBoss.enemiesDefeated++;
        inCoroutine = true;
        yield return new WaitForSeconds(0.5f);
        StopCoroutine("DamagePlayer");
        StopCoroutine("DamageTD");
        Destroy(gameObject);
    }
    
    
    //**********************************
    //************EnemyMovement********
    //**********************************
    
    
    public Rigidbody RB { get; set; }
    public bool IsFacingRight { get; set; }
    public void MoveEnemy(float point)
    {
        if (point == 0) //walk state
        {
            navMeshAgent.speed = enemyMovementSpeed;
            navMeshAgent.angularSpeed = 100 * (gameBoss.globalDifficulty * 0.75f);
            navMeshAgent.destination = GameObject.Find("TDPoint1").transform.position;
        }

        if (point == 2) // attack player
        {
            navMeshAgent.destination = GameObject.Find("Player").transform.position;
        }
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
       
    }

    //**********************************
    //************StateMachine**********
    //**********************************


    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        WalkState = new EnemyWalkState(this, StateMachine);
        AttackPlayerState = new EnemyAttackPlayerState(this, StateMachine);
        AttackTDState = new EnemyAttackTDState(this, StateMachine);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        otherSlime = GameObject.FindWithTag("Slime1");
        Physics.IgnoreCollision(otherSlime.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);

        if (damageTDOn)
        {
            if (!inTDCoroutine)
            {
                StartCoroutine("DamageTD");
            }
        }

        if (damagePlayerOn)
        {
            if (!inPlayerCoroutine)
            {
                StartCoroutine("DamagePlayer");
            }
        }

        if (gameBoss.TDHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (gameBoss.newGame == false)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
            StateMachine.ChangeState(AttackPlayerState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
        
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
    
    public enum AnimationTriggerType
    {
        
    }
    
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyWalkState WalkState { get; set; }
    public EnemyAttackPlayerState AttackPlayerState { get; set; }
    public EnemyAttackTDState AttackTDState { get; set; }
    
    //Walking state

    IEnumerator DamageTD()
    {
        Debug.Log("DamageTD on");
        inTDCoroutine = true;
        gameBoss.TDDamage(enemyDamage);
        yield return new WaitForSeconds(enemyDamageCooldown);
        inTDCoroutine = false;
    }

    IEnumerator DamagePlayer()
    {
        inPlayerCoroutine = true;
        gameBoss.PlayerDamage(enemyDamage);
        yield return new WaitForSeconds(enemyDamageCooldown);
        inPlayerCoroutine = false;
    }
}
