//using System;
using UnityEngine;
using TMPro;

public class FPSGunplayController : MonoBehaviour
{

    // Public variables

    // Enums
    public enum GunSelection
    {
        AK47 = 0,
        AutoShotty = 1,
        GaoGun = 2
    };

    public enum handedness
    {
        Lefty = 0,
        Righty = 1
    };

    // minra health thingy
    public minraHealth minra;

    // Gun Variables
    public GunSelection gunSelection;       // Determines selected gun
    public handedness curHand;              // current handedness option
    public Vector3[] handedPos;             // 0 = lefty, 1 = righty
    public GameObject[] guns;               // 0 = ak, 1 = shotty, 2 = gao

    [Space(20)]
    public TMP_Text ammo;
    [Space(10)]


    public int ammoAKCurrent;               // current ammo count
    public int ammoAKMagCurrent;            // current ammo inside magazine
    public int ammoAKMagMax;                // max ammo can be inside magazine
    public float akFirerate;
    public float akReloadTime;
    public float akDamage;
    
    
    [Space(10)]

    public int ammoAutoShottyCurrent;
    public int ammoAutoShottyMagCurrent;
    public int ammoAutoShottyMagMax;
    [Space(10)]

    public AudioClip[] gaos;
    public float gaoCooldownResetTimer; // when gao, cooldown timer gets set to this so you have to wait befor eyou can gao again
    public float gaoDamage;
    public float gaoRadius;
    


    // Private variables

    // get the input component
    private PlayerMovement pmInput;           // Player input
    private handedness lasthand;            // last handedness option chose
    [SerializeField] private Gun_AK ak;
    [SerializeField] private Gun_Autoshotty auto;
    [SerializeField] private Gun_GAO gao;
    private float AKCooldownTimer;
    private float gaoCooldownTimer;

    // Protected Variables


    // Start is called before the first frame update
    void Start()
    {
        // get component
        pmInput = GetComponent<PlayerMovement>();

        // sets the last handedness to the current handedness
        if (curHand == handedness.Lefty)
            lasthand = handedness.Righty;
        else
            lasthand = handedness.Lefty;
        setHandedness();

        // Sets selected gun
        ammo.text = ammoAKMagCurrent.ToString();
        

        // reset all wepons at start
        gaoCooldownTimer = 0;

        
    }

    // Update is called once per frame
    void Update()
    {   
        // sets player handedness
        setHandedness();

        // ak cooldown timer
        if(AKCooldownTimer > 0)
        {
            AKCooldownTimer -= Time.deltaTime;

            if (AKCooldownTimer <= 0)
            {
                ammo.text = ammoAKMagCurrent.ToString();
            }
        }

        // update the gao cooldown timer
        if(gaoCooldownTimer > 0)
        {
            // counts down cooldown timer in seconds
            gaoCooldownTimer -= Time.deltaTime;
        }   


        // determines the gun selected
        gunSelectionInputManager();


        // Weapon firing stuffs
        gunFiring();





    }

    private void setHandedness()
    {
        // update player handedness   
        if (lasthand != curHand)
        {
            // handedness has changed
            lasthand = curHand;

            // move gun position
            for(int i = 0; i < guns.Length; ++i)
            {
                // set new handedness
                guns[i].transform.localPosition = new Vector3(handedPos[(int)lasthand].x, handedPos[(int)lasthand].y, handedPos[(int)lasthand].z);
            }
        }
    }

    // selects gun
    private void gunSelected()
    {
        // disables all guns jsut in case
        for (int i = 0; i < guns.Length; ++i)
        {
            guns[i].SetActive(false);
        }

        // enables new gun
        guns[(int)gunSelection].SetActive(true);
        
        switch(gunSelection)
        {
            case GunSelection.AK47: 
                ammo.text = ammoAKMagCurrent.ToString();
            break;
        }
    }


    // determines seleted gun
    private void gunSelectionInputManager()
    {
        // if we press input for selecting the weapon, change to said weapon
        if (pmInput.input_SelAK)
        {
            gunSelection = GunSelection.AK47;
            gunSelected();
        }

        // if (pmInput.input_selAuto)
        // {
        //     gunSelection = GunSelection.AutoShotty;
        //     gunSelected();
        // }
        if (pmInput.input_selGao)
        {
            gunSelection = GunSelection.GaoGun;
            gunSelected();
        }
    }

    // for firing guns
    private void gunFiring()
    {
        // if player fires gun
        if(pmInput.input_FireWeapon)
        {
            // switch to gun based on what's equipet and fire it
            switch(gunSelection)
            {
                case GunSelection.AK47:
                    // Fire AK
                    if (AKCooldownTimer <= 0)
                    {
                        // sets the cooldown timer of the ak to be the firerate
                        AKCooldownTimer = akFirerate;

                        // subtract 1 from the magazine
                        // ammoAKMagCurrent--;

                        // update magazine
                        ammo.text = ammoAKMagCurrent.ToString();

                        // fire's ak
                        ak.FireAK(akDamage, minra);
                    }
                    // Magazine empty
                    else if (ammoAKMagCurrent <= 0)
                    {
                        // relad the ak
                        AKCooldownTimer = akReloadTime;
                        // subtracts ammo pool by the magazine max
                        ammoAKCurrent -= ammoAKMagMax;
                        // reload current magazine
                        ammoAKMagCurrent = ammoAKMagMax;
                        
                        ammo.text = "reloading...";
                    }

                break;

                case GunSelection.AutoShotty:
                    // fire autoshotty
                    auto.fireShotty();
                break;

                case GunSelection.GaoGun:
                // gao
                    if(gaoCooldownTimer <= 0)
                    {
                        // sets gao cooldown
                        gaoCooldownTimer = gaoCooldownResetTimer;

                        // fire gao
                        gao.fireGao(gaos[Random.Range(0, gaos.Length)], gaoDamage, gaoRadius, minra);
                    }
                break;
            }
        }
        
    }

}
