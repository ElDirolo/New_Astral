using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    enum State
    {
        Patrolling,
        PatrolWait,
        Chasing,
        Attacking,
        WaitingAttack,
    }
    State currentState;
    UnityEngine.AI.NavMeshAgent porcuxAgent;

    public Transform player;
    private Animator anim;
    int locationIndex = 0;
    public Transform[] locationPoints;

    public float visionRange;
    public float attackRange;

    public float startWaitTime;
    private float waitTime;

    public float atackTime;
    public float attackWaitTime;


    public Transform AibulletSpawn;
    public int powerType;


    public Transform quieta;


    public AudioSource PorcuxAudio;
    public AudioClip walkingPorcux;
    public AudioClip porcuxShoot;

    void Awake()
    {
        porcuxAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }


    void Start()
    {
        currentState = State.Patrolling;
        locationIndex = Random.Range(0, locationPoints.Length);
        waitTime = startWaitTime;
        PorcuxAudio.clip = walkingPorcux;
    }


    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();               
                break;
            case State.PatrolWait:
                Wait();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.WaitingAttack:
                WaitAttack();
                break;
            default:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        porcuxAgent.destination = locationPoints[locationIndex].position;
        anim.SetBool("Run", true);

        if (Vector3.Distance(transform.position, locationPoints[locationIndex].position) < 1)
        {
            currentState = State.PatrolWait;
        }

        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

    void Wait()
    {
        porcuxAgent.destination = locationPoints[locationIndex].position;
        anim.SetBool("Run", false);
        anim.SetBool("Atack", false);
        PorcuxAudio.Pause();

        if (waitTime <= 0)
        {
            locationIndex = Random.Range(0, locationPoints.Length);
            waitTime = startWaitTime;
            currentState = State.Patrolling;
            PorcuxAudio.Play();

        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
            PorcuxAudio.Play();
        }
    }

    void Chase()
    {
        porcuxAgent.destination = player.position;
        

        if (Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            currentState = State.Attacking;
        }
    }

    void Attack()
    {
        porcuxAgent.destination = player.position;
        if (atackTime <= 0)
        {
            anim.SetBool("Atack", true);
            atackTime = 1;
            GameObject AIPlantBall = PoolManager.Instance.GetPooledPower(powerType, AibulletSpawn.position, AibulletSpawn.rotation);
            AIPlantBall.SetActive(true);
            currentState = State.WaitingAttack;
            PorcuxAudio.PlayOneShot(porcuxShoot);

        }
        else
        {
            atackTime -= Time.deltaTime;
            anim.SetBool("Atack", false);
        }
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chasing;
        }



    }
    void WaitAttack()
    {
        porcuxAgent.destination = quieta.position;
        PorcuxAudio.Pause();

        if (attackWaitTime <= 0)
        {
            attackWaitTime = 1;
            currentState = State.Chasing;
            PorcuxAudio.Play();
            
        }
        else
        {
            attackWaitTime -= Time.deltaTime;
        }

    }


    void OnDrawGizmos()
    {
        foreach (Transform point in locationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnTriggerEnter(Collider collider)
    {

        if (this.gameObject.tag == "PorcuxFuego" && collider.gameObject.tag == "Mojado")
        {
            Destroy(this.gameObject);
        }

        if (this.gameObject.tag == "PorcuxPlanta" && collider.gameObject.tag == "Caliente")
        {
            Destroy(this.gameObject);
        }
    }


}
