using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameFightData")]
public class GameFightData_SO : ScriptableObject
{
    public double timer;

    public int second;

    public int minute;

    public int kills;

    public int gold;

    public double DifficultyBoxPosX;                 //难度等级UI的x坐标

    public double KnightArmor;                       //阻挡攻击

    public double Shield;                            //伤害减免

    public double BlueScroll;                        //升级降低冷却时间

    public double GreenScroll;                       //升级提升生命上限

    public double MageHat;                           //升级时概率再升一级

    public double PurpleScroll;                      //升级时提升力量
}
