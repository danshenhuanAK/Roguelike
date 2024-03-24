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

    public double DifficultyBoxPosX;                 //�Ѷȵȼ�UI��x����

    public double KnightArmor;                       //�赲����

    public double Shield;                            //�˺�����

    public double BlueScroll;                        //����������ȴʱ��

    public double GreenScroll;                       //����������������

    public double MageHat;                           //����ʱ��������һ��

    public double PurpleScroll;                      //����ʱ��������
}
