using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecube : MonoBehaviour
{

    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
    }
}
