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
    public Transform starageSpace;

    public override void DoStartProcess(GameObject target, Facility facility)
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }

    public override void HumanStartProcess(Human human)
    {
        if (human.CheckHaveItem())
        {
            storage.Add(human.takeItem);
            human.PutItem(starageSpace);
        }
        human.MoveToOtherPosition(humanMoveState);
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