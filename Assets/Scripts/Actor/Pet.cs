using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Platformer.Core;
using Platformer.Events;
using DG.Tweening;
public class Pet : GameAwareBehaviour
{
    enum State
    {
        待機,
        移動,
        空腹,
        繁殖
    }
    NavMeshAgent navMesh;
    TaskScheduler scheduler;

    List<Transform> moveList = new List<Transform>();

    public int foodCount = 0;


    private State currentState = State.待機;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        scheduler = GetComponent<TaskScheduler>();
    }

    void Update()
    {
        if (foodCount >= 2)
        {
            var ev = scheduler.Schedule<SpawnWorker>();
            ev.startPos = this.transform;
            foodCount = 0;
        }
    
        if (navMesh.remainingDistance < 0.01f && !navMesh.pathPending)
            {
                var ev = scheduler.Schedule<MoveEvent>();
                ev.target = this.gameObject;
                ev.transform = GetRandomTransform();
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Production"))
        {
            if (foodCount < 2)
            {
                foodCount++;
                other.gameObject.transform.DOKill();
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("お腹がいっぱいです。");
            }
        }
    }

    public void SetMoveList(List<Transform> list)
    {
        moveList = list;
    }

    Transform GetRandomTransform()
    {
        int num = Random.Range(0, moveList.Count);
        return moveList[num];
    }
}
