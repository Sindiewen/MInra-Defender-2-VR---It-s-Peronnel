using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerLookAtAttack : MonoBehaviour
{

    // minra
    public minraHealth minra;
    // raycasting stuff
    public float followerNoticeRadius;
    public LayerMask colMask;
    public GameObject chest47;
    public float fireDistance;
    public float Damage;
    public float akFirerate;
    private float akCooldown;
    private Transform minDistTransform = null;
    private AudioSource audioSource;
    [Space(20)]

    // player follow stuff
    public GameObject player;
    public LayerMask playerFoundMask;
    public float followerFoundPlayerRadius;
    public float moveSpeed;
    public float bufferDistance;
    public bool playerFound;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerFound = false;
    }


    // Update is called once per frame
    void Update()
    {
        // akk cooldowntimer
        if(akCooldown > 0)
        {
            akCooldown -= Time.deltaTime;
        }



        // locate plyaer
        if(!playerFound)
        {
            locatePlayer();
        }
        // Follow player
        if(playerFound)
        {
            followPlayer();
        }



        // locate and shoot enemies
        if(playerFound)
        {
            LocateAndSHootEnemies();
        }
    }

    private void LocateAndSHootEnemies()
    {
        // raycasting hitting enemies
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, followerNoticeRadius, transform.position, followerNoticeRadius, colMask);

        if (hits.Length > 0)
        {
            float minDistance = Mathf.Infinity;     // Will store the closes object distance in flaot
            
            // calsulates closest enemy
            for (int i = 0; i < hits.Length; i++)
            {
                // Cheks distance
                float dist = Vector3.Distance(hits[i].transform.position, transform.position);

                // Gets the closest enemy
                if (dist < minDistance)
                {
                    minDistTransform = hits[i].transform;
                    minDistance = dist;
                    //Debug.Log("closest enemy:" + hits[i].transform.name);
                }
            }

            // Look at closest enemy
            transform.LookAt(minDistTransform);

            // fire at enemy - borrow from AK script
            RaycastHit hit;
            Vector3 fireDirection = transform.TransformDirection(Vector3.forward);
            Vector3 firePoint = chest47.transform.position;

            if (Physics.Raycast(firePoint, (fireDirection), out hit, fireDistance))
            {
                if(hit.transform.tag == "Enemy" && akCooldown <= 0)
                {
                    audioSource.Play();
                    Debug.Log(this.gameObject.name + " has hit " + hit.transform.name);
                    //hit.transform.GetComponent<EnemyGameAttributes>().takeDamage(Damage, minra);
                }
            }
        }       
    }

    private void locatePlayer()
    {
        // use spherecast to chech if the player is nearby
        // if nearby, assign player variable when player hits spherecast
        //RaycastHit hit;
        
        //if (Physics.SphereCast(transform.position, followerFoundPlayerRadius, transform.position, out hit, followerFoundPlayerRadius, playerFoundMask))
        Collider[] hitCol = Physics.OverlapSphere(transform.position, followerFoundPlayerRadius);
        for (int i = 0; i < hitCol.Length; i++)
        {
            if (hitCol[i].tag == "Player")
            {
                // found player, now get game object and set flag for following player
                playerFound = true;
                player = hitCol[i].transform.gameObject;
                Debug.Log("player found, now following them");
            }
            

        }

    }
    private void followPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= bufferDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

}


