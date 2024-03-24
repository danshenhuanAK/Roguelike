using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int roomNum;                                             //�������(levelDataList�����)
    public int roomFloor;                                           //��������¥�����
    public int roomFloorNum;                                        //�����������¥����ķ�����
    public int kind;                                                //��������
    public bool isButton;                                           //�Ƿ��ܵ��
    public double roomPosX;                                         
    public double roomPosY;
    public List<int> lineRoomNum;                                //���������ӵķ������
    public List<int> lineRoomFloorNum;                              //���������ӷ���������¥����ķ�����
}
