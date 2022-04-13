using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// How the object will turn into another object

public class TransmutationObject : MonoBehaviour
{

    public GameObject[] tranmutations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transmutateObject(0);
        }
    }
    
    void transmutateObject(int level)
    {
        //create a special transition effect here
        
        // Create clone of this object
        GameObject trans = Instantiate(tranmutations[level], transform.position, transform.rotation);
        
        // enable object
        // hide current object
        // it's possible because the object isnt in the scnee, we cant instantiate socketable items
        // tranmutations[level].SetActive(true);

        // spawn it here, hide this object
        gameObject.SetActive(false);
    }
}
