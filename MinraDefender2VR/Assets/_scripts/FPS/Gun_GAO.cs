using UnityEngine;

public class Gun_GAO : MonoBehaviour
{
    private AudioSource audioSource;

    // Raycasting stuff
    public Camera raycastStartPoint;
    public float fireDistance; 
    private Vector3 fireDirection;
    private Vector3 firePoint;
    public LayerMask colMask;
    private EnemyGameAttributes[] enemiesHit;
    private Vector3 debugSpherePos;
    private float DebugSphereRadius;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void fireGao(AudioClip gao, float gaoDamage, float gaoRadius, minraHealth minra)
    {
        // Play sound
        Debug.Log("GAAAAAOOOOOOOOO");
        audioSource.clip = gao;
        audioSource.Play();


        // Create raycast for hitting
        RaycastHit hit;
        fireDirection = transform.TransformDirection(Vector3.forward) * 10;
        firePoint = raycastStartPoint.transform.position;



        // do raycasting
        if (Physics.Raycast(firePoint, (fireDirection), out hit, fireDistance))
        {
             // Hits enemy, create explosion around them
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(hit.transform.position, gaoRadius, transform.position, 0, colMask);
            debugSpherePos = hit.transform.position;
            DebugSphereRadius = gaoRadius;

            // have successfully hit enemy
            if (hits.Length > 0)
            {
                // createse array of length same as hits
                enemiesHit = new EnemyGameAttributes[hits.Length];

                // loops through each one getting each enemy attribute
                for (int i = 0; i < hits.Length; ++i)
                {
                    enemiesHit[i] = hits[i].transform.GetComponent<EnemyGameAttributes>();
                }

                // Deal damage to each enemy
                for (int i = 0; i < enemiesHit.Length; ++i)
                {
                    if(hits[i].transform.tag == "Friendly")
                    {
                        // hit friendly
                        Debug.Log("Friendly Hit");
                    }
                    else if (hits[i].transform.tag == "Enemy")
                    {
                        
                        // Hit enemy
                        Debug.Log("Enemy Hit");
                        //hits[i].transform.GetComponent<EnemyGameAttributes>().takeDamage(gaoDamage, minra);
                    }
                    else if (hit.transform.tag == "Gao")
                    {
                        Debug.Log("Starting game gao");
                        hit.transform.GetComponent<startGame>().GameStart();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(debugSpherePos, DebugSphereRadius);
    }
}
