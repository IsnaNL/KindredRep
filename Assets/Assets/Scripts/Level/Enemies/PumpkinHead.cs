using System.Collections;
using UnityEngine;

public class PumpkinHead : Health
{
    
    public Vector2 velocity;
    public float speed;
    public float DistanceForTele;
    public float fullHp;
    public LayerMask playerMask;
    public float timeSinceStateStart = 0f;
    public float timeBetweenTele = 5f;
    public GameObject Ghost;
    public Vector3[] teleportPosArray = new Vector3[6];
    public enum State
    {
        Attack,
        Summon,
        Patrol,
        Die,
    }
    public State state;
    public bool shouldSwapState;
    public float delayForAttack;
    public float delayForSummon;
    public float delayForBackTele;
    public float DelayFromPatrolToAttack;
    public bool SecondBossPhaseActive;
    IEnumerator AttackState()
     {
        shouldSwapState = false;
     
        while (state == State.Attack)
         {
           
          


            Collider2D hit = Physics2D.OverlapCircle(transform.position, DistanceForTele, playerMask);
            if (hit )
            {
               
               
                transform.position = new Vector2(hit.transform.position.x - 1, hit.transform.position.y);






                yield return new WaitForSeconds(delayForAttack);

                animator.SetTrigger("AttackTrigger");
            }

        
            state = State.Summon;
            shouldSwapState = true;

         

        }
        
    }
    IEnumerator PatrolState()
     {
        
            shouldSwapState = false;


        while (state == State.Patrol)
         {
            yield return  new WaitForSeconds(DelayFromPatrolToAttack);
            state = State.Attack;
            shouldSwapState = true;




        }
    }
    IEnumerator SummonState()
    {
        shouldSwapState = false;

        while (state == State.Summon)
        {
            yield return new WaitForSeconds(delayForBackTele);
            transform.position = teleportPosArray[Random.Range(0, 5)];
            yield return new WaitForSeconds(delayForSummon);
            Instantiate(Ghost, transform.position,Quaternion.identity);
            GhostMovement ghostref = Ghost.GetComponent<GhostMovement>();
            ghostref.pumkinHead = this;
            state = State.Patrol;
            shouldSwapState = true;
        }
        
    }
    IEnumerator DieState()
     {
         while (state == State.Die)
         {
            yield return null;
           
         }
    }
    public override void Start()
    {
        shouldSwapState = true;
        isVulnerable = true;
        fullHp = health;
    }
    void Update()
     {
        if (health <= fullHp * 0.5f)
        {
            SecondBossPhaseActive = true;
        }
        StateCheck();
         WhenHit();
    }
    void StateCheck() {
        if (shouldSwapState) { 
        switch (state)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Summon:
                StartCoroutine(SummonState());
                break;
            case State.Die:
                StartCoroutine(DieState());
                break;

        }
        }
       
    }
    void WhenHit()
    {
        if (isHit)
        {
            EffectsManager.instance.CreateEffect(effects.blood, transform);
            EffectsManager.instance.CreateEffect(effects.hit, transform);

            isHit = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceForTele);
    }
   

}
