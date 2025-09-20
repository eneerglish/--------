using UnityEngine;
using Platformer.Core;

public class ProductionSpace : GameAwareBehaviour
{
    public Transform spawnPoint;
    public float jumpForce = 5f;

    void Start()
    {
        spawnPoint = this.transform;
    }

    public void ProduceItem()
    {
        GameObject prefab = model.productionList.GetProduction();
        GameObject item = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}