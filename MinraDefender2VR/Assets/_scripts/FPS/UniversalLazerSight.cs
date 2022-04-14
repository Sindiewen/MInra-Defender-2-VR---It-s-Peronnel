using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalLazerSight : MonoBehaviour
{
    // public Vector3 lazerOrigin;
    public LineRenderer lr;
    // public Transform dot;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        // lr.startWidth = 10;
        // lr.endWidth = 10;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, hit.distance));
                Vector3 dotPos = hit.point;
                // dot.position = dotPos;
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 1000));
        // dot.position = Vector3.zero;
        }
    }
}
