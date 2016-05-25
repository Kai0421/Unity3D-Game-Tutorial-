using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    //How far a gun can shoot
    public float range = 100f;

    float timer;
    Ray shootRay;

    //Return to us what the play shot at
    RaycastHit shootHit;

    //Shoot only at shootable things
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;

    //How long is the gun effect last before disappeared
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        //Time between Shot before you can shoot again
        timer += Time.deltaTime;

        //If the mouse left key is click and the timer delay is done then shoot
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        //If the effect shooting time is done then disable the shooting effect.
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

    //Disable effect component from Unity UI.
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        //Reset the number latency between shot
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;
        
        //Stop and start again so it wont have it inconsistancy
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;

        //Origin of the gun so is 0, 
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward; // Shoot forward.

        //Physics part. return whatever shoot.
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //It hit something, give object health script that the things player shot at
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            //If the object doesnt have health script them ignore it.
            //If it hit the enemy then show where the point they got hit and how much damage they take.
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

            //Then this is the point where the gun line display. 
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            //But is we dont hit something then the origin then * by the forward direction to draw a line.
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
