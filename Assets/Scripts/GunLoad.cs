using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoad : MonoBehaviour
{

    GameplayManager gm;

    private void Awake()
    {
        gm = GameplayManager.GetGameplayManager();
    }

    // Use this for initialization
    void Start()
    {
        int gunPicked = SceneControl.gun;
        GameObject uzi = GameObject.Find("Uzi");
        GameObject M200 = GameObject.Find("M200");
        GameObject SPAS = GameObject.Find("SPAS");
        GameObject weapon = SPAS;

        switch (gunPicked)
        {
            case 0:
                Destroy(M200);
                Destroy(SPAS);
                weapon = uzi;
                break;
            case 1:
                Destroy(uzi);
                Destroy(SPAS);
                weapon = M200;
                break;
            case 2:
                Destroy(uzi);
                Destroy(M200);
                weapon = SPAS;
                break;
        }

        gm.Weapon = weapon;
        gm.SpawnPoint = weapon.transform.Find("GunTip").gameObject;


    }
}
