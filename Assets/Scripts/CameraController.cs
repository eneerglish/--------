using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class OverheadCameraController : GameAwareBehaviour
{
    [Header("Target & Center Settings")]
    [Tooltip("カメラが注視する固定の中心点。空欄の場合、All Workersリストの平均位置を追跡します。")]
    [SerializeField] private Transform fixedCenterTarget;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 150f;
    [SerializeField] private float minZoomDistance = 5f;
    [SerializeField] private float maxZoomDistance = 50f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0.2f; // 回転速度を調整
    [SerializeField] private float minVerticalAngle = 10f; // 垂直方向の最小回転角度 (地面に近すぎないように)
    [SerializeField] private float maxVerticalAngle = 80f; // 垂直方向の最大回転角度 (真上に行き過ぎないように)

    private float currentDistance;
    private Vector3 lookAtPoint;
    private Vector2 lastMousePosition;
    private Camera mainCamera;

    // カメラの現在の水平・垂直回転角度
    private float currentYaw = 0f;   // Y軸周りの回転 (水平)
    private float currentPitch = 45f; // X軸周りの回転 (垂直) - 俯瞰なのでデフォルトで傾けておく

    void Awake()
    {
        mainCamera = GetComponent<Camera>();

        // 注視点の初期化
        lookAtPoint = fixedCenterTarget != null ? fixedCenterTarget.position : Vector3.zero;

        // カメラの初期位置から現在の距離と角度を計算
        // カメラの初期状態に基づいて currentDistance と currentPitch/Yaw を設定する
        Vector3 initialDir = (transform.position - lookAtPoint).normalized;
        currentDistance = Vector3.Distance(transform.position, lookAtPoint);
        currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance);

        // 初期回転角度を計算 (現在のカメラの向きから)
        Quaternion initialRotation = transform.rotation;
        currentYaw = initialRotation.eulerAngles.y;
        currentPitch = initialRotation.eulerAngles.x;
        if (currentPitch > 180) currentPitch -= 360; // 0-360度を-180-180度に変換

        // currentPitchをクランプ
        currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);
    }

    void Update()
    {
        if (Mouse.current == null) return;

        HandleZoom();
        HandleRotation(); // 回転処理を呼び出す
    }

    void LateUpdate()
    {
        UpdateLookAtPoint(); // ターゲットが動的に変わる場合、注視点を更新

        // 回転角度に基づいてカメラの方向を計算
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

        // カメラの目標位置を計算: 注視点から回転した方向へ currentDistance 分離れた位置
        Vector3 targetCameraPosition = lookAtPoint + rotation * (Vector3.back * currentDistance);
        
        // カメラの位置と回転を更新
        transform.position = targetCameraPosition;
        transform.LookAt(lookAtPoint); // 常に注視点を見るようにする
    }

    /// <summary>
    /// マウススクロールでのズームイン・アウトを処理します。
    /// </summary>
    private void HandleZoom()
    {
        float scrollInput = Mouse.current.scroll.y.ReadValue();
        if (scrollInput != 0)
        {
            // スクロール量が大きいので、速度を調整する
            currentDistance -= scrollInput * zoomSpeed * Time.deltaTime;
            currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance);
        }
    }

    /// <summary>
    /// マウスの中ボタンドラッグでのカメラ回転を処理します。
    /// </summary>
    private void HandleRotation()
    {
        // マウスの中ボタンが押されているかチェック
        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();

            // 前回マウス位置が記録されていないか、ボタンを押し始めたばかりの場合
            if (lastMousePosition == Vector2.zero)
            {
                lastMousePosition = currentMousePosition;
            }
            else // 前回値が記録されていれば、ドラッグとして処理
            {
                Vector2 delta = currentMousePosition - lastMousePosition;
                
                // 回転速度を調整
                currentYaw += delta.x * rotationSpeed; // マウスのX移動でY軸（水平）回転
                currentPitch -= delta.y * rotationSpeed; // マウスのY移動でX軸（垂直）回転

                // 垂直方向の回転角度を制限 (真上や真横に行き過ぎないように)
                currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);

                // Debug.Log($"Yaw: {currentYaw}, Pitch: {currentPitch}, Delta: {delta}"); // デバッグ用
            }
            lastMousePosition = currentMousePosition;
        }
        else
        {
            // 中ボタンが離されたら、記録をリセット
            lastMousePosition = Vector2.zero;
        }
    }

    /// <summary>
    /// カメラが注視するポイントを更新します。（労働者追跡用）
    /// </summary>
    private void UpdateLookAtPoint()
    {
        if (fixedCenterTarget != null)
        {
            lookAtPoint = fixedCenterTarget.position;
            return;
        }

        if (model.workerManager.workerList.Count > 0)
        {
            Vector3 averagePos = Vector3.zero;
            int validWorkers = 0;
            foreach (GameObject worker in model.workerManager.workerList)
            {
                if (worker != null)
                {
                    averagePos += worker.transform.position;
                    validWorkers++;
                }
            }
            if (validWorkers > 0)
            {
                lookAtPoint = averagePos / validWorkers;
            }
        }
    }
}