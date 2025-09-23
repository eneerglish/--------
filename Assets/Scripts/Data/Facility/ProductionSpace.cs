using UnityEngine;
using Platformer.Core;
using Platformer.Events;

public class ProductionSpace : Facility
{
    public Transform spawnPoint;
    public float jumpForce = 5f;

    void Start()
    {
        spawnPoint = this.transform;
    }

    public override void DoStartProcess(GameObject target, Facility facility )
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }
    public void ProduceItem()
    {
        GameObject prefab = model.productionList.GetProduction();
        GameObject item = Instantiate(prefab, spawnPoint.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}