using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class Save : Singleton<Save>
{
    public void SaveFun()
    {
        if(!File.Exists(Application.dataPath + "/Data"))
        {
            PlayerData playerData = new PlayerData();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream fs = File.Create(Application.dataPath + "/Data");

            binaryFormatter.Serialize(fs, playerData);
            fs.Close();
        }
    }

    public void LoadFun()
    {
        if (File.Exists(Application.dataPath + "/Data"))         //������û����ж��ļ��Ƿ����
        {
            //������ת����
            BinaryFormatter bf = new BinaryFormatter();
            //��һ���ļ���
            FileStream fs = File.Open(Application.dataPath + "/Data", FileMode.Open);
            //�÷����л����ļ���ת��Ϊ����
            PlayerData playerData = (PlayerData)bf.Deserialize(fs);

            Debug.Log(playerData.maxHealth);
            fs.Close();
        }
    }
}
