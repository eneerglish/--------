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

    //新しいキューブを生み出すシステムを考えたい
    public List<Pet> petList = new List<Pet>();

    void Start()
    {
        foreach (Pet pet in petList)
        {
            pet.SetMoveList(feedposlist);
        }
    }

    public override void DoStartProcess(GameObject target)
    {
        int feedCount = Mathf.Min(5, storage.Count);

        if (feedCount <= 0)
        {
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            var ev = scheduler.Schedule<ChangeStateEvent>();
            ev.target = target;
            ev.newState = GetActionData(1).followStateType;
            ev.actionData = GetActionData(1);
            return;
        }

        base.DoStartProcess(target);
        //同時に入ってきたときのエラーを回避したい
        List<GameObject> _foodList = new List<GameObject>();
        for (int i = 0; i < feedCount; i++)
        {
            _foodList.Add(GetFoodFromStorage());
            TaskScheduler scheduler = target.GetComponent<TaskScheduler>();
            var ev = scheduler.Schedule<WorkerFeeding>(i);
            ev.target = target;
            ev.facility = this;
            ev.throwObject = _foodList[i];
        }
    }


    public override void HumanStartProcess(Human human)
    {
        if (human.CheckHaveItem())
        {
            storage.Add(human.takeItem);
            human.PutItem(storageSpace);
        }
        TaskScheduler scheduler = human.GetComponent<TaskScheduler>();
        var ev = scheduler.Schedule<MoveEvent>(1);
        ev.target = human.gameObject;
        ev.transform = model.positionManager.GetPosition(humanMoveState);
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