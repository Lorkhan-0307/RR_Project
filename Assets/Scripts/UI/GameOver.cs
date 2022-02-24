using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] public PlayerMovement playermovement;
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void CheckpointRestartButton()
    {

        LevelManager.instance.Respawn();
        gameObject.SetActive(false);
    }

    public void StageRestartButton()
    {

        SceneManager.LoadScene("Stage001");
    }

    public void MainMenutButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
