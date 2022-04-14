using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <Summary>
// Controls the transmuation to the objects
// Requries input from the player, this will control it
//
//
// With no item in hand, hold grip button, gesture in said direction to transmute object
// UI above object will be a Diamond shape, with diamoned icons corresponding to each
// direction. Up, down, left, right
// use Hand velocity + distance traveled minimum to determine wahat to transmute 
/// </Summary>
public class TrasmutationController : MonoBehaviour
{
    [Header("Component refences")]
    private HurricaneVR.Framework.ControllerInput.HVRPlayerInputs HVRInput;

    public Rigidbody leftHand;
    public Rigidbody rightHand;

    public TMP_Text handVelocityDebug;
    
    [Header("Gesture velcocity minimums")]
    // Velocity Direction minimums to transmute
    public float velYDownMin;
    public float velYUpMin;
    public float velZLeftMin;
    public float velZRightMin;

    [Header("SPherecast around player values")]
    // spherecast around player values
    public float radius;
    public LayerMask layerToHit;
    private bool hasHitTransObject;


    // Start is called before the first frame update
    void Start()
    {
        HVRInput = GetComponent<HurricaneVR.Framework.ControllerInput.HVRPlayerInputs>();
        handVelocityDebug.text = "init";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rightHand.velocity;
        handVelocityDebug.text = velocity.ToString();

        // Check for transmutable objects - spherecast
        Collider[] hitCol = Physics.OverlapSphere(transform.position, radius, layerToHit);
        List<TransmutationObject> transObjs = new List<TransmutationObject>();
        if (hitCol.Length > 0)
        {
            hasHitTransObject = true;

            // Get all transmuted objects
            for (int i = 0; i < hitCol.Length; ++i)
            {
                transObjs.Add(hitCol[i].GetComponent<TransmutationObject>());
                Debug.Log("has found transmutable object " + hitCol[i].name);
            }
        }
        else
        {
            hasHitTransObject = false;
        }

        // If player is grabbing, check for velocity
        // right hand
        if (HVRInput.IsRightGripHoldActive && hasHitTransObject)
        {
            // Calculate velocity - 0 = down, left = 1, up = 2, right = 3
            // X = depth, y = vertical - up+  down-, Z = Horizontal - left+ right-
            


            // down
            if(velocity.y < velYDownMin)
            {
                for(int i = 0; i < transObjs.Count; ++i)
                {
                    transObjs[i].transmutateObject(0);
                    Debug.Log("Transmuting Object " + transObjs[i].name);
                }
            }
            
            // Up
            else if(velocity.y > velYUpMin)
            {
                for(int i = 0; i < transObjs.Count; ++i)
                {
                    transObjs[i].transmutateObject(2);
                    Debug.Log("Transmuting Object " + transObjs[i].name);
                }
            }

            // Left
            else if(velocity.z > velZLeftMin)
            {
                for(int i = 0; i < transObjs.Count; ++i)
                {
                    transObjs[i].transmutateObject(1);
                    Debug.Log("Transmuting Object " + transObjs[i].name);
                }
            }

            // Right
            else if(velocity.z < velZRightMin)
            {
                for(int i = 0; i < transObjs.Count; ++i)
                {
                    transObjs[i].transmutateObject(3);
                    Debug.Log("Transmuting Object " + transObjs[i].name);
                }
            }



        }
    }


    
}
