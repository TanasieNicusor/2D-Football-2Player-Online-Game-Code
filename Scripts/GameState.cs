using UnityEngine;
using TMPro;
using Alteruna;

public class GameState : AttributesSync
{
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static GameState Instance;

    [SynchronizableField]
    public bool isGoal = false;
    [SynchronizableField]
    public bool gameStarted = false;
}