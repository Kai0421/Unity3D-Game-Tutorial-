using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer; // So that the enemy is not attacking too fast or too slow


    void Awake ()
    {
        // Locate the player, purpose is to improve performance. call it once when spawned
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    // Activated when the trigger that set in the UI is trigger, Which is the sphere collider.
    void OnTriggerEnter (Collider other)
    {
        // If the object that the enemy is interacted is player then attack
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    //Inverse of the above function. When the player out of the range so dont attack
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update ()
    {
        //Find the time between attack 
        timer += Time.deltaTime;

        //If it long enough between attack then attack and if the player is in range and enemy is not dead.
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) 
        {
            Attack ();
        }

        // If player dead, dont attack
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }
    
    void Attack ()
    {
        //Reset the timer for recalculation purpose
        timer = 0f;

        //If player is not dead then Attack.
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
