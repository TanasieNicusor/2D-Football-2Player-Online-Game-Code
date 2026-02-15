using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Alteruna;

public class ScoreUI : AttributesSync
{
    void Start()
    {
        scoreHome = 0;
        scoreAway = 0;
        myScoreText.text = "" + scoreHome.ToString() + " " + scoreAway.ToString();
    }


    void Update()
    {
        if (GameState.Instance.isGoal == true)
            UpdateScore();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ResetScore()
    {
        scoreHome = 0;
        scoreAway = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        myScoreText.text = "" + scoreHome.ToString() + " " + scoreAway.ToString();
    }

    public void OnPlayerJoinedRoom(Multiplayer mp, Room room, User user)
    {
        ResetScore();
    }

    public void OnOtherPlayerJoined(Multiplayer mp, User user)
    {
        ResetScore();
    }

    public TextMeshProUGUI myScoreText;

    [SynchronizableField]
    public int scoreHome = 0;
    [SynchronizableField]
    public int scoreAway = 0;

    private Multiplayer multiplayer;

    public static ScoreUI Instance;
}
