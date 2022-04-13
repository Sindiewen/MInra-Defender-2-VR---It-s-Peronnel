using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myHVRSwordDamage : HurricaneVR.Framework.Components.HVRDamageProvider
{
    private Rigidbody rb;
    public float attackBuffer;
    private float attackCooldown;
    private bool canAttack;
    public bool canStab;
    public float stabMultiplier;
private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();

        // sets up the attack cooldown
        attackCooldown = 0;

        canAttack = true;

    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        // Debug.LogError("Cur Velocity: " + velocity);

        // attack cooldown timer;
        if (attackCooldown > 0)
        {
            attackCooldown = attackCooldown - Time.deltaTime;

            // Has reached end of cooldown, can ttack again
            if(attackCooldown <= 0)
            {
                canAttack = true;
            }
        }
        
    }

    // When the arrow has collided with enemy

    void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }

    void OnHit(Collision col)
    {
        var damageHandler = col.transform.GetComponent<HurricaneVR.Framework.Components.HVRDamageHandlerBase>();
        // If damage handler has been found, deal damage
        if (damageHandler && canAttack)
        {
            // Differentiate between stabbing vs slicing
            velocity.z = Mathf.Abs(velocity.z);
            velocity.y = Mathf.Abs(velocity.y);

            // Stabber component
            var stabber = GetComponent<HurricaneVR.Framework.Core.Stabbing.HVRStabber>();
            // Player is slicing
            if (velocity.z > velocity.y)
            {
                // Velocity from just 
                DamageProvider.Damage = DamageProvider.Damage * velocity.z;
            }
            else if (stabber.min_IsStabbed && canStab)
            {
                Debug.LogError("Is STabbing");
                DamageProvider.Damage = (DamageProvider.Damage * velocity.y) * stabMultiplier;
            }

            Debug.LogError("Damage to deal on " + col.gameObject.name + " : " + DamageProvider.Damage);
            damageHandler.HandleDamageProvider(DamageProvider, Vector3.zero, Vector3.zero);
            // damageHandler.HandleRayCastHit(DamageProvider, hit);
            
            // Player has hit enemy, enable cooldown
            canAttack = false;
            attackCooldown = attackBuffer;

        }


        // if (AddForceOnHit && col.collider.attachedRigidbody)
        // {
        //     col.collider.attachedRigidbody.AddForceAtPosition(Vector3.back * Force, transform.position, ForceMode.Impulse);
        // }

        // Hit.Invoke(damageHandler);
    }


    public HurricaneVR.Framework.Components.HVRDamageProvider DamageProvider
    {
        get
        {
            return this;
        }
    }
}
