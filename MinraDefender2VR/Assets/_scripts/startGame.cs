using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class startGame : MonoBehaviour
{   

    private AudioSource audioSource;
    public AudioClip hellohello;
    public AudioClip gao;
    public GameObject enemies;
    public GameObject[] partners;
    public GameObject startGameText;
    public GameObject haveFunText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        enemies.SetActive(false);
        for (int i = 0; i < partners.Length; i++)
        {
            partners[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        for (int i = 0; i < partners.Length; i++)
        {
            partners[i].SetActive(true);
        }
        enemies.SetActive(true);
        audioSource.clip = gao;
        audioSource.Play();
        audioSource.loop = false;
        startGameText.SetActive(false);
        haveFunText.SetActive(true);

        Invoke("disableThis", 3);
    }

    private void disableThis()
    {
        audioSource.clip = hellohello;
        audioSource.loop = true;
        audioSource.Play();
        this.enabled = false;
    }
}
