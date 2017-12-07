using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleAK47 : BaseWeaponClass
{


    // Use this for initialization
    void Awake()
    {
        setFields();
        colisor = GetComponent<Collider>();
        rBody = GetComponent<Rigidbody>();
        childColisor = GetComponentsInChildren<Collider>();
    }



    public override void pickUp(Transform transforma)
    {
        this.transform.parent = transforma;
        transform.position = transforma.position;
        transform.rotation = transforma.rotation;
        rBody.useGravity = false;
        rBody.isKinematic = true;
        rBody.detectCollisions = false;
        foreach (Collider col in childColisor)
        {
            col.enabled = false;
        }
    }

    public override void setFields()
    {
        Nome = "AK47";
        Tipo = "AK47";
        this.gameObject.name = Nome;
        this.gameObject.tag = Tipo;
        municaoDaArma = 60;
        indiceArma = 2;
    }
    public override void Discard()
    {
        this.transform.parent = null;
        rBody.useGravity = true;
        rBody.isKinematic = false;
    }

    public override void SwitchPosition(Transform transforma)
    {
        this.transform.parent = null;
        this.transform.parent = transforma;
        transform.position = transforma.position;
        transform.rotation = transforma.rotation;
        rBody.useGravity = false;
        rBody.isKinematic = true;
        rBody.detectCollisions = false;

    }

    public override void OnSlot(Transform transforma)
    {
        Debug.LogError("Eu nao implementei o metodo OnSlot na classe ak47");
    }


    public override void Fire()
    {
        if (muzzle)
            muzzle.Emit(1);
    }

}
