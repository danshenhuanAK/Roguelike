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
        if (File.Exists(Application.dataPath + "/Data"))         //这里最好还是判断文件是否存在
        {
            //二进制转换器
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fs = File.Open(Application.dataPath + "/Data", FileMode.Open);
            //用反序列化将文件流转换为对象
            PlayerData playerData = (PlayerData)bf.Deserialize(fs);

            Debug.Log(playerData.maxHealth);
            fs.Close();
        }
    }
}
