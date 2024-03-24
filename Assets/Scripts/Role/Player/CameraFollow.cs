using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;                    //���������
    public Camera mainCamera;

    public Vector2 minPosition;                 //��С���淶Χ
    public Vector2 maxPosition;                 //�����淶Χ

    public float smoothing;                     //�����ٶ�

    public float contentWidth;                  //��ƿ��
    public float contentHeight;                 //��Ƹ߶�

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

            //ƽ�������ƶ�
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref ve, smoothing);
        }
    }
}
