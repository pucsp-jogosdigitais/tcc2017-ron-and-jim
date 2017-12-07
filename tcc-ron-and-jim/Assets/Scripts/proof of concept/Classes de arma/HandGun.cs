using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandGun : BaseWeaponClass
{


    // Use this for initialization
    void Awake()
    {
        setFields();
        colisor = GetComponent<Collider>();
        rBody = GetComponent<Rigidbody>();

    }

   

    public override void pickUp(Transform transforma)
    {
        this.transform.parent = transforma;
        transform.position = transforma.position;
        transform.rotation = transforma.rotation;
        rBody.useGravity = false;
        rBody.isKinematic = true;
        rBody.detectCollisions = false;
    }


    public override void setFields()
    {
        Nome = "Handgun";
        Tipo = "Pistola";
        this.gameObject.name = Nome;
        this.gameObject.tag = Tipo;
        municaoDaArma = 15;
        indiceArma = 0;
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

    //public override void Discard()
    //{
    //    this.transform.parent = null;
    //    rBody.useGravity = true;
    //    // base.Discard();
    //}

    public override void OnSlot(Transform transforma)
    {
        Debug.LogError("Eu nao implementei o metodo OnSlot na classe handgun");
    }

    public override void Fire()
    {
        if (muzzle)
            muzzle.Emit(1);
    }

}
