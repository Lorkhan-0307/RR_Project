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
        //시작 시 모든 창 꺼놓기(Default)
        SettingWindow.SetActive(false);
        KeyboardWindow.SetActive(false);
        GamepadWindow.SetActive(false);
        GraphicWindow.SetActive(false);
        AudioWindow.SetActive(false);
        StageWindow.SetActive(false);
    }

    //Start 버튼 눌렀을 때 실행
    public void OnClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Continue 버튼 눌렀을 때 실행
    public void OnClickContinue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //Quit 버튼 눌렀을 때 실행
    public void OnClickQuit()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
