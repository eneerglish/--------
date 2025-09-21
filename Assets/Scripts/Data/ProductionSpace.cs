using UnityEngine;
using Platformer.Core;

public class ProductionSpace : GameAwareBehaviour
{
    //生産場所はこのスクリプトで管理
    public Transform spawnPoint;
    public float jumpForce = 5f;

    void Start()
    {
        spawnPoint = this.transform;
    }

    public void ProduceItem()
    {
        GameObject prefab = model.productionList.GetProduction();
        GameObject item = Instantiate(prefab, spawnPoint.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}