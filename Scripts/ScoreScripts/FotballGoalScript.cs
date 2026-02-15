using UnityEngine;
using System.Collections;

public class FotballGoalScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ballTag) && GameState.Instance.isGoal == false)
            OnGoalScored();
    }

    private void OnGoalScored()
    {
        Debug.Log("GOAL!");
        GameState.Instance.isGoal = true;
        if (isLeftGoal() == true)
            ScoreUI.Instance.scoreAway += 1;
        else
            ScoreUI.Instance.scoreHome += 1;

        if (goalParticles != null)
            goalParticles.Play();

        if (goalAnimator != null)
            goalAnimator.SetTrigger("Goal");

        StartCoroutine(WaitSeconds(5.5f));

        // Sound effect
    }

    private IEnumerator WaitSeconds(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        AfterWaitCommands();
    }

    private void AfterWaitCommands()
    {
        goalParticles.Stop();
        GameState.Instance.isGoal = false;
        FootballSpawnPointScript.Instance.SpawnBall();
        FootballPlayerResetScript.Instance.RepositionPlayers();
    }

    private bool isLeftGoal()
    {
        return transform.parent.position.x < 0;
    }

    [SerializeField] private ParticleSystem goalParticles;
    [SerializeField] private Animator goalAnimator;
    [SerializeField] private string ballTag = "ball";
}