using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f; // 3 seconds
    public Transform[] spawnPoints; 


    void Start ()
    {
        //Using the spawn method and repeat it, Dont need a timer to repeatedly call the method
        //First Param - call the method by the string
        //Second Param - Wait number of time before doing the method again
        //3rd Params - Waiting number of time before repeating the method
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Find a random spawn point
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        //Instantiate method create a new Spawn enemy
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
