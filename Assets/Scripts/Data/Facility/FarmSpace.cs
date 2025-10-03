using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
using DG.Tweening;
public class FarmSpace : Facility
{

    public List<Transform> feedposlist = new List<Transform>();
    public List<GameObject> foodPrefab = new List<GameObject>();
    public List<GameObject> storage = new List<GameObject>();
    public Transform storageSpace;

    public override void DoStartProcess(GameObject target)
    {
        int feedCount = Mathf.Min(5, storage.Count);
        if (feedCount <= 0)
        {
            var ev = Simulation.Schedule<ChangeStateEvent>();
            ev.target = target;
            ev.newState = GetActionData(1).followStateType;
            ev.actionData = GetActionData(1);
            return;
        }

        base.DoStartProcess(target);

        for (int i = 0; i < feedCount; i++)
        {
            var ev = Simulation.Schedule<WorkerFeeding>(i);
            ev.target = target;
            ev.facility = this;
        }
    }


    public override void HumanStartProcess(Human human)
    {
        if (human.CheckHaveItem())
        {
            storage.Add(human.takeItem);
            human.PutItem(storageSpace);
        }
        var ev = Simulation.Schedule<MoveEvent>(1);
        ev.target = human.gameObject;
        ev.moveStateType = humanMoveState;
    }
    public GameObject GenerateFood()
    {
        //int randomIndex = Random.Range(0, feedposlist.Count);
        return Instantiate(foodPrefab[0], new Vector3(0, 0f, 0), Quaternion.identity);
    }

    public Transform GetRandomTransform()
    {
        int randomIndex = Random.Range(0, feedposlist.Count);
        return feedposlist[randomIndex];
    }

    public GameObject GetFoodFromStorage()
    {
        if (storage.Count == 0) return null;
        GameObject food = storage[0];
        storage.RemoveAt(0);
        return food;
    }
}