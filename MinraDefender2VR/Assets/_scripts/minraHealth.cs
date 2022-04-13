using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minraHealth : MonoBehaviour
{

    public int minHealth;
    private int minMaxHealth;
    // public GameObject gameoverCanvas;
    // public Slider minHealthSlider;
    // public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // gameoverCanvas.SetActive(false);
        // minHealthSlider.maxValue = minHealth;
        // minHealthSlider.value = minHealth;
        minMaxHealth = minHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (minHealth <= 0)
        {
            // minra died, enable gamemover
            // gameoverCanvas.SetActive(true);
            // mouseLook Mouse = player.GetComponentInChildren<mouseLook>();
            // Mouse.gameOver = true;
            // Mouse.enabled = false;
            // PlayerMovement move = player.GetComponent<PlayerMovement>();
            // move.enabled = false;

            if(Input.GetKeyDown(KeyCode.Return))
            {
                Application.LoadLevel("City");
            }
        }
    }

    public void healMinra(int damage)
    {
        // if we do not go higher than max health, heal minra
        if ((minHealth += damage) >= minMaxHealth)
        {
            minHealth += damage;
        }
    }
    public void takeDamage(int damage)
    {
        minHealth -= damage;
        // minHealthSlider.value = minHealth;
    }

    // Link button here to restart game
    // public void RestartGame()
    // {
    //     Application.LoadLevel("City");
    // }
}
