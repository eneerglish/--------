using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;

public class Human : Facility
{
    [SerializeField]
    private Transform itemPos;

    public GameObject takeItem;

    void Start()
    {
        var ev = Simulation.Schedule<MoveEvent>(1);
        ev.target = this.gameObject;
        ev.transform = model.positionManager.GetPosition(MoveStateType.生産所へ);
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