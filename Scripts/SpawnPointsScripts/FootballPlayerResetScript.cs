using UnityEngine;
using Alteruna;

public class FootballPlayerResetScript : MonoBehaviour
{
    [Header("Player Spawn Settings")]
    [SerializeField] private float SpawnX = 100f;
    [SerializeField] private float SpawnY = 20f;

    private Multiplayer multiplayer;

    public static FootballPlayerResetScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        multiplayer = FindObjectOfType<Multiplayer>();
    }

    public void OnPlayerJoinedRoom(Multiplayer mp, Room room, User user)
    {
        if (multiplayer == null)
            multiplayer = mp;

        StartCoroutine(RepositionAfterDelay(0.5f));
    }

    System.Collections.IEnumerator RepositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (multiplayer != null && multiplayer.CurrentRoom.GetUserCount() == 2)
            RepositionPlayers();
    }

    public void RepositionPlayers()
    {
        Alteruna.Avatar[] avatars = FindObjectsOfType<Alteruna.Avatar>();

        Debug.Log("Found " + avatars.Length + " avatars to reposition");

        foreach (var avatar in avatars)
        {
            int playerIndex = avatar.Possessor.Index;

            if (playerIndex == 0)
            {
                avatar.transform.position = new Vector3(-SpawnX, SpawnY, 0);
                Debug.Log("Player 1 repositioned to left: " + avatar.transform.position);
            }
            else if (playerIndex == 1)
            {
                avatar.transform.position = new Vector3(SpawnX, SpawnY, 0);
                Debug.Log("Player 2 repositioned to right: " + avatar.transform.position);
            }
        }
    }
}