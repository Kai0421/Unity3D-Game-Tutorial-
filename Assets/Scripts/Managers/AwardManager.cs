using UnityEngine;

public class AwardManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject gameToken;
    public Transform[] spawnPoints;
    bool isSpawn = false, isSinking = false;
    float timeStay = 0.2f, timer;

    public void SpawnAwards()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(gameToken, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        //Change isSpawn Status after the initiation.
        isSpawn = true;  
    }

    //Sink to destroy the Award Object
    public void Sink()
    {
        //When move the collider, Kinematic prevents recalculating the static geometry.
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        //Destroy the game object after 2 secs, after player no longer sinking.
        Destroy(gameObject, 2f);
    }
}
