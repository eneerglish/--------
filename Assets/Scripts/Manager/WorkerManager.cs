using UnityEngine;
using System.Collections.Generic;

public class WorkerManager : MonoBehaviour
{
    public List<GameObject> workerList = new List<GameObject>();
    public GameObject prefab;
    public Transform playerSpawnPoint;
    public GameObject RestButton;
    public bool isReset = false;

    public List<ActionData> actionDataList = new List<ActionData>();

    public void DestroyWorker(GameObject worker)
    {
        workerList.Remove(worker);
        //Destroy(worker);
        /*if (workerList.Count <= 0)
        {
            RestButton.SetActive(true);
            isReset = true;
        }*/
    }

    public GameObject InstantiateWorker(Transform transform = null)
    {
        if (transform == null)
        {
            transform = playerSpawnPoint;
        }
        GameObject workerprefab = Instantiate(prefab, transform.position, Quaternion.identity);
        workerList.Add(workerprefab);
        return workerprefab;
    }

    public GameObject GetWorker(int num = 0)
    {
        if (num >= 0 && num < workerList.Count)
        {
            return workerList[num];
        }
        return null;
    }

    public ActionData GetActionData(FollowStateType stateType)
    {
        foreach (var actionData in actionDataList)
        {
            if (actionData.followStateType == stateType)
            {
                return actionData;
            }
        }
        return null;
    }
}
