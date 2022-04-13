using UnityEngine;
public class Gun_AK : MonoBehaviour 
{

    private AudioSource audioSource;

    // Raycasting stuff
    public Camera raycastStartPoint;
    public float fireDistance; 
    private Vector3 fireDirection;
    private Vector3 firePoint;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireAK(float akDamage, minraHealth minra)
    {
        // Play audio source
        Debug.Log("Firing AK");
        audioSource.Play();
        

        // Create raycast for hitting
        RaycastHit hit;
        fireDirection = transform.TransformDirection(Vector3.forward) * 10;
        firePoint = raycastStartPoint.transform.position;

        // do raycasting
        if (Physics.Raycast(firePoint, (fireDirection), out hit, fireDistance))
        {
            if(hit.transform.tag == "Friendly")
            {
                // hit friendly
                Debug.Log("Friendly Hit");
            }
            else if (hit.transform.tag == "Enemy")
            {
                // Hit enemy
                Debug.Log("Enemy Hit");
                //hit.transform.GetComponent<EnemyGameAttributes>().takeDamage(akDamage, minra);
            }
            else if (hit.transform.tag == "Gao")
            {
                Debug.Log("Starting game gao");
                hit.transform.GetComponent<startGame>().GameStart();
            }
        }
    }
}
