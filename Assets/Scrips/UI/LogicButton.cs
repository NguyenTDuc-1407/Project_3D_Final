using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicButton : MonoBehaviour
{
    public GameObject SettingPanel;
    public void NewGame()
    {
        SceneManager.LoadScene("Map 1");
    }

    public void Setting()
    {
        SettingPanel.SetActive(true);
    }
    public void ExitSetting()
    {
        SettingPanel.SetActive(false);
    }
}
