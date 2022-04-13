using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myHVRArrowDamage : HurricaneVR.Framework.Components.HVRDamageProvider
{

    [Tooltip("If true adds force on hit to everything")]
    public bool AddForceOnHit = true;
    private HurricaneVR.Framework.Weapons.Bow.HVRArrow arrow;
    private Rigidbody rb;
    private Vector3 velocity;

    private bool dealDamage = false;

    void Start()
    {
        base.Start();
        arrow = GetComponent<HurricaneVR.Framework.Weapons.Bow.HVRArrow>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the arrow is flying, toggle flag to check to deal damage
        dealDamage = arrow.Flying;
        // Debug.Log("Arrow flying: " + arrow.Flying);
        velocity = rb.velocity;
        Debug.Log("Cur Velocity: " + velocity);
        // Arrow flying forward, it's the -x axis!


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
        if (damageHandler)
        {
            // Velocity from just 
            DamageProvider.Damage = DamageProvider.Damage * Mathf.Abs(velocity.x);
            Debug.LogError("Damage to deal on " + col.gameObject.name + " : " + DamageProvider.Damage);
            damageHandler.HandleDamageProvider(DamageProvider, Vector3.zero, Vector3.zero);
            // damageHandler.HandleRayCastHit(DamageProvider, hit);
            
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
