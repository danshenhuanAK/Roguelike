using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCamera : MonoBehaviour
{
    private UIPanelManager uiPanelManager;

    public float moveSpeedY;                //拖动速度
    public float resetSpeed;                //复位速度
    private Vector3 thisPositionDown;       //下方复位点
    [SerializeField]
    private Vector3 thisPositionUp;         //上方复位点
    private bool resetTargetDown;           //下方复位判断
    private bool resetTargetUp;             //上方复位判断

    private void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
    }
    
    private void Start()
    {
        thisPositionDown = transform.localPosition;
    }

    private void Update()
    {
        if (uiPanelManager.displayPanel != UIPanelType.LevelPanel)
        {
            return;
        }

        if (Input.GetMouseButton(0))                        //按住鼠标左键拖动
        {
            transform.Translate(-1 * Input.GetAxis("Mouse Y") * moveSpeedY * Vector3.up);
        }
        else
        {
            if (resetTargetDown)                                //下方复位
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, thisPositionDown, resetSpeed);
                if (transform.localPosition.y <= thisPositionDown.y)
                {
                    resetTargetDown = false;
                }
            }
            else if (resetTargetUp)                             //上方复位
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
