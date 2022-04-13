using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController cc;


    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    // Player inpuyt values
    private float x;
    private float z;
    private bool Fire;
    private bool input_AK;
    private bool input_AUtoshotty;
    private bool input_gaogun;


    bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if were grounded
        isGrounded = Physics.CheckSphere(groundCheck.position,  groundDistance, groundMask);

        // If the player is grounded and the player's downward velocity means it was falling before but now landed, reset velocity
        if(isGrounded == true && velocity.y < 0)  
        {
            // if set to zero, might register before player actually hits ground. 
            // small enough to forve player to the ground far enough
            velocity.y = -2f;
        }


        // Define player input
        playerInput();

        // move player
        MovePlayer();
    }


    // Gets player input
    private void playerInput()
    {
        // horizontal and vertical movement
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Fire = Input.GetButton("Fire1");
        input_AK = Input.GetKeyDown(KeyCode.Alpha1);
        input_AUtoshotty = Input.GetKeyDown(KeyCode.Alpha2);
        input_gaogun = Input.GetKeyDown(KeyCode.Alpha3);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    // Moves player
    private void MovePlayer()
    {
        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }



    // Getters/Setters
    public bool input_FireWeapon
    {
        get { return Fire; }
    }

    public bool input_SelAK
    {
        get { return input_AK; }
    }

    public bool input_selAuto
    {
        get { return input_AUtoshotty; }
    }

    public bool input_selGao
    {
        get { return input_gaogun; }
    }
}
