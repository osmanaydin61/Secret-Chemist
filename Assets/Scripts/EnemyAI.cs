using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanseToTarget = Mathf.Infinity;
    bool isProvoked;
    EnemyHealth health;
    void Start()
    {   
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

   
    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanseToTarget = Vector3.Distance(target.position,transform.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanseToTarget<= chaseRange)
        {
            isProvoked = true;
            
        }
       
        
    }
    public void OnDamageTaken() //uzakta olsa bile hasar aldığında düşmanın saldırmasını sağlar
    {
        isProvoked = true;
        Debug.Log("Enemy took damage!");
    }
    private void EngageTarget()// düşman alanına girdiğinde düşmanın seni kovalamasını ve durma mesafesine geldiğinde saldırmasını sağlar kodlarla birlikte animasyonlar da çalışır
    {
        FaceTarget();
        if(distanseToTarget>=navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distanseToTarget<= navMeshAgent.stoppingDistance)
        {
            AttactTarget();
        }
        
    }

    private void AttactTarget()
    {
        GetComponent<Animator>().SetBool("Attack",true);
        
        
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack",false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    
    private void FaceTarget()// düşmanın yüzünün sürekli bize dönük olmasını sağlar
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation= Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*turnSpeed);
    }
    void OnDrawGizmosSelected()// düşman tepki alanını renklendirir
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, chaseRange);
    }
}
