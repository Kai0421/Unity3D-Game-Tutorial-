using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int targetScore;
    Text text;
    public GameObject stuff;
    public Transform[] spawnPoints;

    void Awake ()
    {
        text = GetComponent<Text>();
        score = 0;
        targetScore = score;
    }

    void Update ()
    {
        text.text = "Score: " + score + " - " + targetScore;
    }
}
