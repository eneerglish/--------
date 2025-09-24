using UnityEngine;
using UnityEngine.AI;

public class Enemy : Facility
{
    public NavMeshAgent navMesh;
    public Animator animator;
    private Transform targetWorker;


    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        targetWorker = model.workerManager.workerList[0].transform;
    }

    void Update()
    {
        if (targetWorker != null)
        {
            navMesh.SetDestination(targetWorker.position);
        }

        float speed = navMesh.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public override void DoStartProcess(GameObject target, Facility facility)
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }
}