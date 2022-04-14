using UnityEngine;

/// How the object will turn into another object

public class TransmutationObject : MonoBehaviour
{
    [Header("UI Sphere stuff")]
    public float radius;
    public LayerMask layerToHit;
    // public GameObject UI;
    public GameObject[] tranmutations;

    // Update is called once per frame
    void Update()
    {
        // When player has entered the radius of this object, show ui
        Collider[] hitCol = Physics.OverlapSphere(transform.position, radius, layerToHit);
        if (hitCol.Length > 0)
        {
            // show ui
        }
        else
        {
            // hide ui
        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     transmutateObject(0);
        // }
    }
    
    public void transmutateObject(int level)
    {
        //create a special transition effect here
        
        // Create clone of this object
        // GameObject trans = Instantiate(tranmutations[level], transform.position, transform.rotation);
        
        // enable object
        // hide current object
        // it's possible because the object isnt in the scnee, we cant instantiate socketable items
        tranmutations[level].SetActive(true);
        Debug.Log("Spawning " + tranmutations[level].name);

        // spawn it here, hide this object
        gameObject.SetActive(false);
    }
}
