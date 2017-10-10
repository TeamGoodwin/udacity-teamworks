using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoad : MonoBehaviour
{

    GameplayManager gm;

    private void Awake()
    {
        gm = GameplayManager.GetGameplayManager();

        int gunPicked = SceneControl.gun;
        GameObject uzi = GameObject.Find("Uzi");
        GameObject M200 = GameObject.Find("M200");
        GameObject SPAS = GameObject.Find("SPAS");

        SPAS.SetActive(false);
        M200.SetActive(false);
        uzi.SetActive(false);

        GameObject weapon = M200;

        switch (gunPicked)
        {
            case 0:
                weapon = uzi;
                break;
            case 1:
                weapon = M200;
                break;
            case 2:
                weapon = SPAS;
                break;
        }

        weapon.SetActive(true);
        gm.Weapon = weapon;
        gm.SpawnPoint = weapon.transform.Find("GunTip").gameObject;
    }

}
