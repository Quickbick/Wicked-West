using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Collectable item during the nighttime events
// Player will have to stand in the radius of the item for a certain amount of time (15 seconds?)

public class NightObjective : MonoBehaviour
{
    GameObject player;
    [SerializeField] float radius;

    // this is just local for now, but i'm heavily considering putting it in the gamemanager
    float score = 0;
    [SerializeField] float maxScore = 500;

    // the bar to be displayed when actively near objective
    public GameObject display;
    public Image scoreBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkRadius();
        checkScore();
    }

    // if player is close, add to objective meter
    void checkRadius()
    {
        // Get the distance between the player and the monster
        float distance = Vector3.Distance(player.transform.position, transform.position);

        scoreBar.fillAmount = score / maxScore;

        // add score if player is close enough, show progress bar
        if (distance < radius && score < maxScore)
        {
            score += 1;
            //Debug.Log(score);

            // set bar display to active
            display.SetActive(true);
        } else
        {
            display.SetActive(false);
        }
    }

    // Updating the player "score" (time near objective)
    void checkScore()
    {
        if (score >= maxScore)
        {
            GameManager.Instance.objComplete = true;
        }
    }

    public void Reset()
    {
        score = 0;
        display.SetActive(false);
    }

    // Used for moving objective around each night
    public void Teleport(Transform target)
    {
        transform.position = target.position;
    }
}

