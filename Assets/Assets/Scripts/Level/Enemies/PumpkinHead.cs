using System.Collections;
using UnityEngine;

public class PumpkinHead : Health
{
    
    public Vector2 velocity;
    public float speed;
    public float DistanceForTele;
    public float fullHp;
    //private Animator animator;
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
        //State = Attack
        while (state == State.Attack)
         {
           
          


            Collider2D hit = Physics2D.OverlapCircle(transform.position, DistanceForTele, playerMask);
            if (hit )
            {
               
               
                transform.position = new Vector2(hit.transform.position.x - 1, hit.transform.position.y);






                yield return new WaitForSeconds(delayForAttack);

                animator.SetTrigger("AttackTrigger");
            }

            //actions
          //yield return null; 
            state = State.Summon;
            shouldSwapState = true;

            // shouldSwapState = true;

        }
        // NextState();
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

        //NextState();
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
            //state = State.Patrol;
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
        //StartCoroutine(AttackState(), PatrolState(), DieState())
        StateCheck();
         WhenHit();
       // if (Input.GetKeyDown(KeyCode.Space))
       // {
       //NextState();
       //     Debug.Log(state.ToString());
       //  }
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
            EffectsManager.e_Instance.BloodHitEffect(transform);
            isHit = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceForTele);
    }
    /* void NextState()
     {
         string methodName = state.ToString() + "State";
         System.Reflection.MethodInfo info =
             GetType().GetMethod(methodName,
                                 System.Reflection.BindingFlags.NonPublic |
                                 System.Reflection.BindingFlags.Instance);
         StartCoroutine((IEnumerator)info.Invoke(this, null));
     }*/

}
