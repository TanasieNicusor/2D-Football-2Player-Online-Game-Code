using System.Collections;
using UnityEngine;
using Alteruna;

public class FootballSpawnPointScript : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        multiplayer = FindObjectOfType<Multiplayer>();
    }

    public void OnPlayerJoin(Multiplayer mp, Room room, User user)
    {
        if (multiplayer == null)
            multiplayer = mp;

        if (multiplayer != null && multiplayer.InRoom)
        {
            int playerCount = multiplayer.CurrentRoom.GetUserCount();

            if (playerCount == 1)
                SpawnBall();
            else if (playerCount == 2)
                StartCoroutine(waitSeconds(timeToWait));
        }
    }

    private IEnumerator waitSeconds(float time)
    {
        yield return new WaitForSeconds(time);

        SpawnBall();
    }

    public void SpawnBall()
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


    public static FootballSpawnPointScript Instance;

    private Multiplayer multiplayer;

    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float ballSpawnY = 80f;
    [SerializeField] private GameObject ballPrefab;
}