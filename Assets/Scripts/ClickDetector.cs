using UnityEngine;
// 新しいInput Systemを使用している場合
using UnityEngine.InputSystem;
using Platformer.Core;
using Platformer.Events;
public class ClickDetector : GameAwareBehaviour
{
    // 最後にクリックしてオブジェクトに当たった座標を保存する変数
    private Vector3 lastHitPoint;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private GameObject throwObject;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                lastHitPoint = hitInfo.point;
                GameObject obj = Instantiate(throwObject);
                var ev = Simulation.Schedule<ObjectThrowing>();
                ev.startPos = startPos.position;
                ev.targetPos = lastHitPoint;
                ev.throwObject = obj;
            }
        }
    }
}