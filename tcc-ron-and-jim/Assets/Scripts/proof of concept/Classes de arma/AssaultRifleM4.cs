using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleM4 : BaseWeaponClass
{


    // Use this for initialization
    void Awake()
    {
        //print("teste");
        colisor = GetComponent<Collider>();
        rBody = GetComponent<Rigidbody>();

        setFields();
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
        Nome = "M4A1";
        Tipo = "M4A1";
        this.gameObject.name = Nome;
        this.gameObject.tag = Tipo;
        municaoDaArma = 90;
        indiceArma = 1;
    }
    public override void Discard()
    {
        foreach (Collider col in childColisor)
        {
            col.enabled = true;
        }
        this.transform.parent = null;
        rBody.useGravity = true;
        rBody.isKinematic = false;
        rBody.detectCollisions = true;
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
        Debug.LogError("Eu nao implementei o metodo OnSlot na classe m4a1");
    }

    public override void Fire()
    {
        if (gameObject.transform.parent != null)
        {
            if (muzzle)
                muzzle.Emit(1);
        }
    }
}
