using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCon : MonoBehaviour
{
    // Start is called before the first frame update
    public float lookRadius = 10f;
    public Transform target;
    NavMeshAgent agent;
    public Animator g_Animation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        g_Animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            g_Animation.SetBool("isRun", true);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        /*if (agent.remainingDistance <= 5)
        {
            g_Animation.SetBool("isRun", true);
        }*/
        else
        {
            g_Animation.SetBool("isRun", false);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
}
