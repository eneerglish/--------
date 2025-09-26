using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;

public class Human : Facility
{
    public NavMeshAgent navMesh;
    public Animator animator;
    //Worker用のenumを再利用するというずるしてるかも
    public MoveStateType curentMoveState = MoveStateType.牧場へ;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveFarmOrProduction();
    }

    void Update()
    {
        if(!navMesh.pathPending && navMesh.remainingDistance < 0.1f )
        {
            MoveFarmOrProduction();
        }
        float speed = navMesh.velocity.magnitude;
        animator.SetFloat("speed", speed);
    }

    void MoveFarmOrProduction()
    {
        switch (curentMoveState)
        {
            case MoveStateType.牧場へ:
                curentMoveState = MoveStateType.生産所へ;

                break;
            case MoveStateType.生産所へ:
                curentMoveState = MoveStateType.牧場へ;
                break;
            default:
                break;
        }
        navMesh.SetDestination(model.positionManager.posList[(int)curentMoveState].position);
    }

    public override void DoStartProcess(GameObject target, Facility facility)
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }

}