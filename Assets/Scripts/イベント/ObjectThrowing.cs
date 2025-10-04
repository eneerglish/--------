using UnityEngine;
using Platformer.Core;
using DG.Tweening;
using UnityEngine.AI;

namespace Platformer.Events
{
    public class ObjectThrowing : Simulation.Event<ObjectThrowing>
    {
        public Vector3 targetPos;
        public Vector3 startPos;
        public GameObject throwObject;

        public override void Execute()
        {
            Feding(startPos, targetPos, throwObject);
        }

        public void Feding(Vector3 startPos, Vector3 targetPos, GameObject throwObject)
        {
            throwObject.transform.SetParent(null);

            // 2. 軌道の頂点（中間地点の真上に高さを加える）
            Vector3 peakPosition = (startPos + targetPos) / 2f;
            peakPosition.y += 1f;

            // 3. 最終的な目標地点
            // (targetPositionは引数で受け取っているのでそのまま使う)

            // ウェイポイントの配列を作成
            Vector3[] path = { peakPosition, targetPos };
            throwObject.transform.position = startPos;

            //投げる前に色々無効化
            if (throwObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.useGravity = false;
            }
            if (throwObject.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var navMesh))
            {
                navMesh.enabled = false;
            }


            // --- DOTweenを実行 ---
            throwObject.transform.DOPath(
                path,
                2,
                PathType.CatmullRom // 滑らかな曲線を描くためのパスタイプ
            ).SetEase(Ease.Linear)
            .OnComplete(() => {
                // 2. アニメーション完了後、重力を再度有効にする
                if (throwObject.TryGetComponent<Rigidbody>(out var rb))
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.useGravity = true;
                    }
                //NavMeshも
                if (throwObject.TryGetComponent<NavMeshAgent>(out var navMesh))
                {
                    navMesh.enabled = true;
                }
                }); 
        }
    }
}