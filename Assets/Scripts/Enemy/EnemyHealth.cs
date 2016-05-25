using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    //sink Speed is used then the enemy dies, make them sink them through the floor.
    //and this is the speed thatthey sink through
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10; // keep score
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking; // check is they are sinking


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();

        //Get hit particle system from the parent 
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        //If it's sink then sink it negative per second.
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        // Play audio hurt effect
        enemyAudio.Play ();

        //Loose the current health
        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        // If the heath is less than = 0 Die.
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    // This is an animation event. Will be used in Unity 
    public void StartSinking ()
    {
        //Turn off the nav mash. Usually turning of nav mash game object uses setActive(false). who;e game object
        //But to turn off Nav mash of one particular component use enable to false
        GetComponent <NavMeshAgent> ().enabled = false;

        //When move the collider, Kinematic prevents recalculating the static geometry.
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;

        //Destroy the game object after 2 secs, after player no longer sinking.
        Destroy (gameObject, 2f);
    }
}
