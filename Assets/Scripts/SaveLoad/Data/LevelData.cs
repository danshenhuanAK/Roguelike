using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int roomNum;                                             //房间序号(levelDataList的序号)
    public int roomFloor;                                           //房间所处楼层序号
    public int roomFloorNum;                                        //房间所处这个楼层里的房间编号
    public int kind;                                                //房间类型
    public bool isButton;                                           //是否能点击
    public double roomPosX;                                         
    public double roomPosY;
    public List<int> lineRoomNum;                                //房间所连接的房间序号
    public List<int> lineRoomFloorNum;                              //房间所连接房间在所在楼层里的房间编号
}
