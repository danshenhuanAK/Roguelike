using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameChoose : MonoBehaviour
{
    #region
    public void NewGame()                   //����Ϸ����¼�
    {
        SceneManager.LoadScene(2);
    }

    public void ContinueGame()              //������Ϸ����¼�
    {

    }

    public void QuitGame()                  //�˳�����¼�
    {

    }
    #endregion
}
