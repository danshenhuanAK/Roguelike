using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameChoose : MonoBehaviour
{
    #region
    public void NewGame()                   //新游戏点击事件
    {
        SceneManager.LoadScene(2);
    }

    public void ContinueGame()              //继续游戏点击事件
    {

    }

    public void QuitGame()                  //退出点击事件
    {

    }
    #endregion
}
