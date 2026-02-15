using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class FootballScript : AttributesSync
{
    [SerializeField] private float maxForce = 75f; 

    void Start()
    {
        Vector2 kickDir = new Vector2(Random.Range(-1f, 1f), 2f);
        kickDir.Normalize();
        float force = 0f;
        force = Random.Range(0f, maxForce);
        Rigidbody2D ballRb = GetComponent<Rigidbody2D>();

        ballRb.AddForce(kickDir * force, ForceMode2D.Impulse);
    }
}