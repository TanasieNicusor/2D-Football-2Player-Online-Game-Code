using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Alteruna;

public class TimeUI : AttributesSync
{
    void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        float timePassed = Time.time - time;

        int minutes = (int)timePassed / 60;
        int seconds = (int)timePassed % 60;

        myTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ResetTime()
    {
        time = Time.time;
    }


    public void OnPlayerJoinedRoom(Multiplayer mp, Room room, User user)
    {
        ResetTime();
    }

    public void OnOtherPlayerJoined(Multiplayer mp, User user)
    {
        ResetTime();
    }

    public void TestFunction()
    {
        Debug.Log("Test");
    }

    public TextMeshProUGUI myTimeText;

    private float time;

    public static TimeUI Instance;

    private Multiplayer multiplayer;
}
