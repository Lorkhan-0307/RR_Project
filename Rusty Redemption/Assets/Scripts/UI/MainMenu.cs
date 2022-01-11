using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingWindow;
    public GameObject KeyboardWindow;
    public GameObject GamepadWindow;
    public GameObject GraphicWindow;
    public GameObject AudioWindow;
    public GameObject StageWindow;

    private void Start()
    {
        //���� �� ��� â ������(Default)
        SettingWindow.SetActive(false);
        KeyboardWindow.SetActive(false);
        GamepadWindow.SetActive(false);
        GraphicWindow.SetActive(false);
        AudioWindow.SetActive(false);
        StageWindow.SetActive(false);
    }

    //Start ��ư ������ �� ����
    public void OnClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Continue ��ư ������ �� ����
    public void OnClickContinue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //Quit ��ư ������ �� ����
    public void OnClickQuit()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
