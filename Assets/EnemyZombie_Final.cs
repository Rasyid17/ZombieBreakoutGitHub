﻿using UnityEngine;
using System.Collections;

public class EnemyZombie_Final : MonoBehaviour {

    public float distance;

    Transform target;
    NavMeshAgent nav;
    Transform player;
    //Animator controller;
    public float health = 100;
    //ZombieSpawnPointManager game;
    CapsuleCollider capsuleCollider;
    SphereCollider sphereCollider;
    Animator anim;
    //public float TheDamage = 50;
    RaycastHit hit;
    //public bool isDead = false;
    //public bool isHit = false;
    //public bool isAttack = false;
    public GameObject[] pickups;

    //public float TimetoAttack;
    //float attackTimer;
    //public AudioClip zombieAttackClip;
    //public Text score;
    //public int scoreValue = 10;
    //private int scoring = 1;

    private GameObject GameManagerGO;
    private ScoreManager ScrManager;
    public bool IAmZombieA;
    public bool IAmZombieA1;
    public bool IAmZombieA2;

    public bool IAmZombieB;
    public bool IAmZombieB1;
    public bool IAmZombieB2;

    public bool IAmZombieC;
    public bool IAmZombieC1;
    public bool IAmZombieC2;

    public bool IAmZombieD;
    public bool IAmZombieD1;
    public bool IAmZombieD2;

    public float ZombieADamage = 5;
    public float ZombieA1Damage = 5;
    public float ZombieA2Damage = 5;

    public float ZombieBDamage = 10;
    public float ZombieB1Damage = 10;
    public float ZombieB2Damage = 10;

    public float ZombieCDamage = 20;
    public float ZombieC1Damage = 20;
    public float ZombieC2Damage = 20;

    public float ZombieDDamage = 15;
    public float ZombieD1Damage = 15;
    public float ZombieD2Damage = 15;

    private vp_PlayerEventHandler PlayerEvents = null;

    private GameObject PlayerReference;
    private PlayerHealthNewChar PlayerScriptReferece;


    //public GameObject indicatePain;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        capsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();


        PlayerEvents = player.transform.GetComponent<vp_PlayerEventHandler>();
    }

    void Start()
    {

        PlayerReference = GameObject.Find("PlayerOVR");
        PlayerScriptReferece = PlayerReference.GetComponent<PlayerHealthNewChar>();


        GameManagerGO = GameObject.Find("GameManager");
        ScrManager = GameManagerGO.GetComponent<ScoreManager>();

        if (IAmZombieA)
            MainDamage = ZombieADamage;
        if (IAmZombieA1)
            MainDamage = ZombieA1Damage;
        if (IAmZombieA2)
            MainDamage = ZombieA2Damage;

        if (IAmZombieB)
            MainDamage = ZombieBDamage;
        if (IAmZombieB1)
            MainDamage = ZombieB1Damage;
        if (IAmZombieB2)
            MainDamage = ZombieB2Damage;

        if (IAmZombieC)
            MainDamage = ZombieCDamage;
        if (IAmZombieC1)
            MainDamage = ZombieC1Damage;
        if (IAmZombieC2)
            MainDamage = ZombieC2Damage;

        if (IAmZombieD)
            MainDamage = ZombieDDamage;
        if (IAmZombieD1)
            MainDamage = ZombieD1Damage;
        if (IAmZombieD2)
            MainDamage = ZombieD2Damage;

        //anim.SetBool("PlayerInRange", false);

    }

    public float currentValue;
    public bool canAttack = true;

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, player.position);

        if (PlayerScriptReferece.PlayerIsDead == true)
        {
            anim.SetBool("PlayerIsDead", true);
            nav.SetDestination(this.transform.position);
        }
        else
            anim.SetBool("PlayerIsDead", false);

        if (anim.GetBool("EnemyStillAlive") == true)
        {
            if (canAttack == true)
            {
                //Debug.Log(nav.remainingDistance);



                if (distance <= 10)
                {
                    anim.SetBool("PlayerInRange", true);
                    anim.SetBool("EnemyWalk", true);
                    nav.SetDestination(player.position);
                    RotateTowards(player.transform);
                }
                else if (distance > 10 && distance < 15)
                {
                    anim.SetBool("PlayerInRange", false);
                    anim.SetBool("EnemyWalk", true);
                    nav.SetDestination(player.position);
                }
                else if (distance >= 15)
                {
                    anim.SetBool("PlayerInRange", false);
                    anim.SetBool("EnemyWalk", false);
                    nav.SetDestination(this.transform.position);
                }

                if (distance <= 3)
                {
                    anim.SetBool("EnemyAttacking", true);
                    anim.SetBool("EnemyWalk", false);
                }
                else 
                    anim.SetBool("EnemyAttacking", false);

                anim.SetFloat("PlayerStillThere", currentValue);

                if (anim.GetBool("PlayerInRange") == true)
                {
                    currentValue += 0.01f;
                    if (currentValue >= 1.1f)
                        currentValue = 0;
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("anger"))
                    nav.SetDestination(this.transform.position);

            }
        }

        if (distance <= 10)
        {
            canAttack = true;
            //nav.stoppingDistance = 2;
            //gameObject.GetComponent<Patrol>().enabled = false;
        }

    }



    public bool InRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            InRange = true;
  

        //if (other.CompareTag("Dead"))
        //{
        //    InRange = false;
        //    anim.SetBool("PlayerIsDead", true);
        //    canAttack = false;
        //    nav.enabled = false;
        //}

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            InRange = false;

   
    }

    private GameObject Player;
    private PlayerHealthNewChar Phealth;
    private float MainDamage = 5;
    private vp_FPPlayerDamageHandler hp;

    public void Attack()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Phealth = Player.GetComponent<PlayerHealthNewChar>();

        if (InRange)
        {
            if (Phealth != null)
            {

                Phealth.remove(MainDamage);
                PlayerEvents.TakeDamage.Send();
            }

            AudioSource noise = GetComponent<AudioSource>();
            noise.Play();

        }
    }



    void ApplyDamage(float damage)
    {
        health -= damage;

        if (IAmZombieA || IAmZombieA1 || IAmZombieA2 || IAmZombieC || IAmZombieC1 || IAmZombieC2 || IAmZombieB || IAmZombieB1 || IAmZombieB2 || IAmZombieD || IAmZombieD1 || IAmZombieD2)
        {
            anim.SetBool("EnemyGotHit", true);
            StartCoroutine(DamageCoolDown());

        }





        if (health <= 0)
            Death();
    }

    IEnumerator DamageCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("EnemyGotHit", false);
    }


    void Death()
    {

        nav.Stop();
        capsuleCollider.enabled = false;
        sphereCollider.enabled = false;

        if (IAmZombieA || IAmZombieA1 || IAmZombieA2 || IAmZombieC || IAmZombieC1 || IAmZombieC2 || IAmZombieB || IAmZombieB1 || IAmZombieB2 || IAmZombieD || IAmZombieD1 || IAmZombieD2)
        {
            anim.SetBool("EnemyStillAlive", false);

        }


        StartCoroutine(SpawnMedicine());

        CheckWhichZombieIAm();
        Destroy(gameObject, 4f);
    }

    IEnumerator SpawnMedicine()
    {
        yield return new WaitForSeconds(2);
        int randomDrop = Random.Range(0, 10);
        int randomPickup = Random.Range(0, pickups.Length - 1);
        if (randomDrop < 2)
        {
            Instantiate(pickups[randomPickup], transform.position + new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
        }
    }

    void CheckWhichZombieIAm()
    {
        if (IAmZombieA)
            ScrManager.KilledZombieA();
        if (IAmZombieA1)
            ScrManager.KilledZombieA1();
        if (IAmZombieA2)
            ScrManager.KilledZombieA2();

        if (IAmZombieB)
            ScrManager.KilledZombieB();
        if (IAmZombieB1)
            ScrManager.KilledZombieB1();
        if (IAmZombieB2)
            ScrManager.KilledZombieB2();

        if (IAmZombieC)
            ScrManager.KilledZombieC();
        if (IAmZombieC1)
            ScrManager.KilledZombieC1();
        if (IAmZombieC2)
            ScrManager.KilledZombieC2();

        if (IAmZombieD)
            ScrManager.KilledZombieD();
        if (IAmZombieD1)
            ScrManager.KilledZombieD1();
        if (IAmZombieD2)
            ScrManager.KilledZombieD2();
    }

    protected virtual void OnEnable()
    {
        if (PlayerEvents != null)
            PlayerEvents.Unregister(this);
    }

    protected virtual void OnDisable()
    {
        if (PlayerEvents != null)
            PlayerEvents.Unregister(this);
    }

    protected virtual void SendMessage()
    {
        PlayerEvents.TakeDamage.Send();
    }
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

}

