using UnityEngine;
using UnityEngine.UI;

public class mouseLook : MonoBehaviour
{

    // Crosshair related stuff
    public GameObject crosshair;
    private Image crosshairImg;
    public float fireDistance;
    [Space(20)]

    // Public Variables
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool gameOver;


    // private varaiables
    private float xRotation = 0f;
    private Vector3 fireDirection;
    private Vector3 firePoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
    // Get crosshair 
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        crosshairImg = crosshair.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!gameOver)
        {
        // Mouse input camera movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        // Raycast code
        Hit();
        }
        else if (gameOver)
        {
            // game voer
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        
    }

    private void Hit()
    {
			// Raycasting variables:
			RaycastHit hit;
			fireDirection = transform.TransformDirection(Vector3.forward) * 10;
			firePoint = transform.position;

			// Do raycasting:
			if (Physics.Raycast (firePoint, (fireDirection), out hit, fireDistance)) {
				// Change the color based on what object is under the crosshair:
				if (hit.transform.tag == "Friendly" || hit.transform.tag == "Gao") 
                {
					ChangeColor (Color.green);
                    Debug.Log("Friendly found");
				} else if (hit.transform.tag == "Enemy") 
                {
					ChangeColor (Color.red);
                    Debug.Log("Enemy Found");

                    // shoot at enemy
				} 
                else 
                {
					ChangeColor (Color.white);
				}
			} 
            else 
            {
				ChangeColor (Color.white);
			}

			// Debug the ray out in the editor:
			Debug.DrawRay(firePoint, fireDirection, Color.green);
	}

    private void ChangeColor(Color color)
    {
        crosshairImg.color = color;
    }
}
