using UnityEngine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public List<Camera> cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // 最初は0番目のカメラだけを有効にする
        SwitchCamera(0);
    }

    public void SwitchCamera(int index)
    {

        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        cameras[index].gameObject.SetActive(true);
    }
}