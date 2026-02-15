using UnityEngine;
using System.Collections;

public class GameRestartScript : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;
    }

    public void GameRespawnRoutine()
    {
        GameObject ball = FindBall();

        if (ball != null)
            Destroy(ball);

        Instantiate(ballPrefab, new Vector3(0, ballSpawnY, 0), Quaternion.identity);

        TimeUI.Instance.ResetTime();
        ScoreUI.Instance.ResetScore();
        GameState.Instance.isGoal = false;
    }

    public void BallRespawnRoutine()
    {
        GameObject ball = FindBall();

        if (ball != null)
            Destroy(ball);

        Instantiate(ballPrefab, new Vector3(0, ballSpawnY, 0), Quaternion.identity);

        GameState.Instance.isGoal = false;
    }

    private GameObject FindBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        return ball;
    }

    public static GameRestartScript Instance;

    [SerializeField] private float ballSpawnY = 80f;
    [SerializeField] private GameObject ballPrefab;
}