using UnityEngine;
using Platformer.Core;
using Platformer.Events;
using System.Collections.Generic;

public class ProductionSpace : Facility
{
    public Transform spawnPoint;
    public float jumpForce = 5f;
    public List<GameObject> productionList;

    void Start()
    {
        spawnPoint = this.transform;
    }

    public override void DoStartProcess(GameObject target)
    {
        base.DoStartProcess(target);
        for (int i = 1; i <= 5; i++)
        {
            var ev = Simulation.Schedule<ProduceItemEvent>(i);
            ev.facility = this;
        }
    }
    public override void HumanStartProcess(Human human)
    {
        if (productionList.Count > 0)
        {
            GameObject item = productionList[0];
            productionList.RemoveAt(0);
            human.TakeItem(item);
        }
        var ev = Simulation.Schedule<MoveEvent>(0.5f);
        ev.target = human.gameObject;
        ev.moveStateType = humanMoveState;
    }
    public void ProduceItem()
    {
        GameObject prefab = model.productionList.GetProduction();
        GameObject item = Instantiate(prefab, spawnPoint.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        productionList.Add(item);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}