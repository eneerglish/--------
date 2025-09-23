using UnityEngine;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Events;
using DG.Tweening;
public class FarmSpace : Facility
{

    public List<Transform> feedposlist = new List<Transform>();
    public List<GameObject> foodPrefab = new List<GameObject>();

    public override void DoStartProcess(GameObject target, Facility facility)
    {
        target.GetComponent<WorkerState>().ChangeFollowState(startstate, facility);
    }

    public GameObject GenerateFood(Vector3 workerpos)
    {
        //int randomIndex = Random.Range(0, feedposlist.Count);
        return Instantiate(foodPrefab[0], workerpos + new Vector3(0, 0.2f, 0), Quaternion.identity);
    }

    public Transform GetRandomTransform()
    {
        int randomIndex = Random.Range(0, feedposlist.Count);
        return feedposlist[randomIndex];
    }

    public void Feding(GameObject worker, Transform targetpos, GameObject target)
    {
            Vector3 startPosition = worker.transform.position + new Vector3(0, 0.2f, 0);

            // 2. 軌道の頂点（中間地点の真上に高さを加える）
            Vector3 peakPosition = (startPosition + targetpos.position) / 2f;
            peakPosition.y += 1f;

            // 3. 最終的な目標地点
            // (targetPositionは引数で受け取っているのでそのまま使う)

            // ウェイポイントの配列を作成
            Vector3[] path = { peakPosition, targetpos.position };

            // --- DOTweenを実行 ---
            target.transform.DOPath(
                path, 
                2, 
                PathType.CatmullRom // 滑らかな曲線を描くためのパスタイプ
            ).SetEase(Ease.Linear); 
    }
}