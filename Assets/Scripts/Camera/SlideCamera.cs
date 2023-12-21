using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCamera : MonoBehaviour
{
    public float moveSpeedY;                //�϶��ٶ�
    public float resetSpeed;                //��λ�ٶ�
    private Vector3 thisPositionDown;       //�·���λ��
    [SerializeField]
    private Vector3 thisPositionUp;         //�Ϸ���λ��
    private bool resetTargetDown;           //�·���λ�ж�
    private bool resetTargetUp;             //�Ϸ���λ�ж�

    private void Start()
    {
        thisPositionDown = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))                        //��ס�������϶�
        {
            transform.Translate(Vector3.up * Input.GetAxis("Mouse Y") * -1 * moveSpeedY);
        }
        else
        {
            if (resetTargetDown)                                //�·���λ
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, thisPositionDown, resetSpeed);
                if (transform.localPosition.y <= thisPositionDown.y)
                {
                    resetTargetDown = false;
                }
            }
            else if (resetTargetUp)                             //�Ϸ���λ
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, thisPositionUp, resetSpeed);
                if (transform.localPosition.y >= thisPositionUp.y)
                {
                    resetTargetUp = false;
                }
            }
        }

        if (transform.localPosition.y > thisPositionDown.y && Input.GetMouseButtonUp(0))
        {
            resetTargetDown = true;
        }

        if (transform.localPosition.y < thisPositionUp.y && Input.GetMouseButtonUp(0))
        {
            resetTargetUp = true;
        }
    }
}
