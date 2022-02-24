using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPoint;
    public Transform checkPoint;
    public GameObject playerPrefab;

    private Transform respawnPoint;

    private void Awake()
    {
        instance = this;
        respawnPoint = startPoint;
    }

    public void Respawn()
    {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
