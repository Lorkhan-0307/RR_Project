using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameState
{
    //private bool escapeInput;
    //private bool GameIsPaused = false;

    public InGameState(Player player, GameManager gameManager, GameStateMachine stateMachine) : base(player, gameManager, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*if (gameManager.isGameover)
        {
            stateMachine.ChangeState(gameManager.GameOverState);
        }

        //Pause Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                Debug.Log("Resume");
            }
            else
            {
                Pause();
                Debug.Log("Pause");
            }
        }*/
        
    }


    #region Pause Fuction
    /*private void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    */
    #endregion
}
