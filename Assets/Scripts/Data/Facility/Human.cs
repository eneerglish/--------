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
    [SerializeField]
    private Transform itemPos;

    public GameObject takeItem;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveFarmOrProduction();
    }

    void Update()
    {
        float speed = navMesh.velocity.magnitude;
        animator.SetFloat("speed", speed);
    }

    public void MoveToOtherPosition(MoveStateType moveStateType)
    {
        navMesh.SetDestination(model.positionManager.GetPosition(moveStateType).position);
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
        navMesh.SetDestination(model.positionManager.GetPosition(curentMoveState).position);
    }

    public override void DoStartProcess(GameObject target, Facility facility)
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }

    public void TakeItem(GameObject item)
    {
        if(CheckHaveItem())
        {
            return;
        }
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.SetParent(itemPos);
        item.transform.localPosition = Vector3.zero;
        takeItem = item;
    }
    public bool CheckHaveItem()
    {
        if(takeItem != null)
        {
            return true;
        }
        return false;
    }

    public void PutItem(Transform pos)
    {
        if (CheckHaveItem())
        {
            takeItem.transform.SetParent(pos);
            takeItem.transform.localPosition = Vector3.zero;
            takeItem.GetComponent<Rigidbody>().isKinematic = false;
            takeItem = null;
        }
    }
}