using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Platformer.Core;
using Platformer.Events;

public class Enemy : Facility
{
    public NavMeshAgent navMesh;
    public Animator animator;
    private Transform targetWorker;

    private EnemyState currentState;
    public enum EnemyState
    {
        落下中,
        着地,
        追跡,
    }
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMesh.enabled = false;
    }

    void Update()
    {
        if (currentState != EnemyState.追跡) return;

        if (targetWorker != null)
        {
            navMesh.SetDestination(targetWorker.position);
        }

        float speed = navMesh.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void FallAndLand(float fallDuration)
    {

        transform.DOMoveY(0, fallDuration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("着地しました。");
                currentState = EnemyState.着地;
                model.effectManager.InstantiateEffect(1, this.transform, 5);
                var ev = Simulation.Schedule<EnemyChaseEvent>(7);
                ev.enemy = this;
            });
    }

    public override void DoStartProcess(GameObject target)
    {
        base.DoStartProcess(target);
    }

    public void SetTarget(GameObject target)
    {
        if (target != null)
        {
            targetWorker = target.transform;
        }
        else
        {
            targetWorker = null;
        }

    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }
}