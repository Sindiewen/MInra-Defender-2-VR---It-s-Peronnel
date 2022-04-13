using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGameAttributes : MonoBehaviour
{


    // Depricating old values here.
    // Take from hvr dmaamge handler to get health to determine when to despawn enemy
    // public float enemyHealth;
    private HurricaneVR.Framework.Components.HVRDamageHandler _HVRDamageHandler;

    public int damageToDeal;
    public float attackRadius;
    public float attackcooldownRate;
    public LayerMask colMask;
    [SerializeField] private minraHealth MinraHealth;
    private bool minraFound;
    private NavMeshAgent agent;
    private CapsuleCollider cc;
    private Rigidbody[] rbs;
    private float attackCooldown;


    private void Start()
    {
        _HVRDamageHandler = GetComponent<HurricaneVR.Framework.Components.HVRDamageHandler>();
        rbs = GetComponentsInChildren<Rigidbody>();
        setRigidbodyState(true);
        setColliderState(true);
        agent = GetComponent<NavMeshAgent>();
        cc = GetComponent<CapsuleCollider>();
        minraFound = false;
    }

    private void Update()
    {

        // attacck cooldown
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if(!minraFound)
        {
            // spherecast
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRadius, transform.position, attackRadius, colMask);
            if (hits.Length > 0)
            {
                // Minra found, start attacking
                minraFound = true;
                MinraHealth = hits[0].transform.GetComponent<minraHealth>();
            }
        }
        else if (minraFound && attackCooldown <= 0)
        {
            // start attacking

            attackCooldown = attackcooldownRate;
            MinraHealth.takeDamage(damageToDeal);
        }

        
        // Determine when the enemy needs to die
        if (_HVRDamageHandler.Life <= 0)
        {
            // for(int i = 0; i < rbs.Length; i++)
            // {
            //     rbs[i].AddForce(Vector3.back * 100, ForceMode.Impulse);
            // }
            // upon death, heal minra for 1
            // minHealth.healMinra(1);
            die();
        }
    }

    private void die()
    {
        setRigidbodyState(false);
        //setColliderState(false);
        agent.enabled = false;
        cc.enabled = false;
        Destroy(gameObject, 3);
    }

    private void setRigidbodyState(bool state)
    {
        //Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rbs)
        {
            rigidbody.isKinematic = state;
        }
    }

    private void setColliderState(bool state)
    {
        Collider[] cols = GetComponentsInChildren<Collider>();

        foreach(Collider col in cols)
        {
            col.enabled = state;
        }
    }
}
