using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;                    //跟随的物体
    public Camera mainCamera;

    public Vector2 minPosition;                 //最小跟随范围
    public Vector2 maxPosition;                 //最大跟随范围

    public float smoothing;                     //跟随速度

    public float contentWidth;                  //设计宽度
    public float contentHeight;                 //设计高度

    Vector3 ve;
    Vector3 playerPos;

    private void Start()
    {
        var cameraOrthographicSize = 0.5f * Screen.height * 0.01f;
        var aspectRatio = ((float)Screen.width / (float)Screen.height);

        var cameraWidth = cameraOrthographicSize * 2.0f * aspectRatio;

        if (cameraWidth < contentWidth * 0.01f)
        {
            cameraOrthographicSize = 0.5f * contentWidth * 0.01f / aspectRatio;
        }

        mainCamera.orthographicSize = cameraOrthographicSize;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            playerPos = player.position;

            playerPos.x = Mathf.Clamp(playerPos.x, minPosition.x, maxPosition.x);
            playerPos.y = Mathf.Clamp(playerPos.y, minPosition.y, maxPosition.y);
            playerPos.z = transform.position.z;

            //平滑阻尼移动
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref ve, smoothing);
        }
    }
}
