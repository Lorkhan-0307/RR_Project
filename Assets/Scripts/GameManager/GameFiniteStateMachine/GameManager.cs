using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameover { get; private set; }


    public GameStateMachine gameStateMachine { get; private set; }
    public Player player { get; private set; }
    public GamePlayState GamePlayState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    private void Start()
    {
        //FindObjectOfType<Death>().playerOnDeath += EndGame;
        gameStateMachine.Initialize(GamePlayState);
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        gameStateMachine = new GameStateMachine();
        player = FindObjectOfType<Player>();

        GamePlayState = new GamePlayState(player, this, gameStateMachine);
        GameOverState = new GameOverState(player, this, gameStateMachine);

    }

    private void Update()
    {
        gameStateMachine.currentState.LogicUpdate();
    }


    //Game over Ã³¸®
    public void EndGame()
    {
        isGameover = true;
        Debug.Log(isGameover);
    }
}
