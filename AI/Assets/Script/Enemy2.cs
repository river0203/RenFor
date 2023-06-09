using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//Enemy2
//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동하게 한다.
//상태를 만들어서 제어하고 싶다.
// Idle : Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
//Run : 타겟방향으로 이동(요원)
//Attack : 일정 시간마다 공격
//attack -> run이 안됨 수정
//스킬 랜덤 -> 가중치 랜덤
//state가 dead면 코루틴 중지
public class Enemy2 : LivingEntity
{
    public Transform target;
    NavMeshAgent agent;
    private State state;
    private Rigidbody rigid;
    //랜덤 스킬
    private List<string> skill_list = new List<string>() {"skill_1", "skill_2", "skill_3", "skill_4"};

    [SerializeField]
    private float starting_hp = 100.0f;
    private float hp;
    //private float damage;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        
    }

    private void freeze_velocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    private void FixedUpdate() 
    {
        freeze_velocity();
    }

    private void setUp()
    {
        state = State.Idle;
    }

   IEnumerator Attack_Delay()
   {
        _isAttack = true;
        yield return new WaitForSeconds(3f);

        int rand = UnityEngine.Random.Range(0, skill_list.Count); //* 필기 필요
        Debug.Log(skill_list[rand]);
        _isAttack = false;
   }
    private bool _isAttack = false;
    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(state == State.Attack && !_isAttack)
        {
            StartCoroutine("Attack_Delay");
        }
        if (distance > 2)
        {
            state = State.Run;
            //anim.SetTrigger("Run");
            Debug.Log("Run");
        }
    }

    private void UpdateRun()
    {
        //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            if(state == State.Attack)
            {
                StartCoroutine("Attack_Delay");
            }
            //anim.SetTrigger("Attack");
            Debug.Log("attack");
        }

        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;
        Debug.Log("Run");

    }

    private void UpdateIdle()
    {
        agent.speed = 0;
        //생성될때 목적지(Player)를 찿는다.
        target = GameObject.Find("Player").transform;
        //target을 찾으면 Run상태로 전이하고 싶다.
        if (target != null)
        {
            state = State.Run;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
            //anim.SetTrigger("Run");
            Debug.Log("Run");
        }
    }

    private void Die()
    {
        Collider[] enemyColliders = GetComponents<Collider>();
        if(state == State.Dead)
        {
            StopCoroutine("Attack_Delay");
            //움직임 차단
            //애니메이션
            agent.isStopped = true;
            agent.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("onDamage");
            state = State.IsHitting;

            if(hp <= 0) // 현재 바로 실행되는 듯함
            {
                state = State.Dead;
                Die();
            }
        }
    }
}