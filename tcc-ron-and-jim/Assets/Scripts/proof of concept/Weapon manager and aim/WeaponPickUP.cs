using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponPickUP : MonoBehaviour///////esta sendo utilizado pra nada
{

    public Transform pontoDaArma;
    bool pegar;
    
    HudManager hud;



    void Start()
    {
        hud = GetComponent<HudManager>();
        pontoDaArma = GameObject.Find("pontoDaArma").transform;
    }

    void Update()
    {
        pegar = Input.GetKey(KeyCode.E);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("ArmaDeMao"))// || col.gameObject.GetComponent<BaseWeaponClass>().GetType() == typeof(AssaualtRifleM4))
        {
          

            if (pegar)
            {
                col.gameObject.GetComponent<BaseWeaponClass>().pickUp(pontoDaArma);
                //aim.Arma = col.gameObject;
                //hud.onPickUpForSprites(col.gameObject.GetComponent<BaseWeaponClass>().Nome);
            }
        }



    }
}
