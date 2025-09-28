using UnityEngine;
// ↓↓↓ 新しいInput Systemを使うために必要 ↓↓↓
using UnityEngine.InputSystem; 

public class MoveCamera : MonoBehaviour // スクリプト名がMoveCameraだと仮定
{
    public float moveSpeed = 3.0f;
    public float rotationSensitivity = 2.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // ... Startメソッドなど ...

    void LateUpdate()
    {
        // --- 1. カメラの回転（右クリック中）---
        // Mouse.currentがnullでないことを確認
        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            // マウスの移動量を取得
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            
            rotationX += mouseDelta.x * rotationSensitivity * Time.deltaTime; // Time.deltaTimeをかけると滑らかに
            rotationY += mouseDelta.y * rotationSensitivity * Time.deltaTime;
            
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(-rotationY, Vector3.right);
        }

        // --- 2. カメラの移動（WASDキー）---
        // Keyboard.currentがnullでないことを確認
        if (Keyboard.current != null)
        {
            Vector3 moveDirection = Vector3.zero;
            if (Keyboard.current.wKey.isPressed) moveDirection += transform.forward;
            if (Keyboard.current.sKey.isPressed) moveDirection -= transform.forward;
            if (Keyboard.current.aKey.isPressed) moveDirection -= transform.right;
            if (Keyboard.current.dKey.isPressed) moveDirection += transform.right;

            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
    }
}