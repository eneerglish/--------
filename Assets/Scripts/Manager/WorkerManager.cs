using UnityEngine;
using System.Collections.Generic;

public class WorkerManager : MonoBehaviour
{
    public List<GameObject> workerList = new List<GameObject>();
    public GameObject prefab;

    public void DestroyWorker(GameObject worker)
    {
        workerList.Remove(worker);
        Destroy(worker);
    }

    public void InstantiateWorker()
    {
        GameObject workerprefab = Instantiate(prefab);
        workerList.Add(workerprefab);
    }
}
