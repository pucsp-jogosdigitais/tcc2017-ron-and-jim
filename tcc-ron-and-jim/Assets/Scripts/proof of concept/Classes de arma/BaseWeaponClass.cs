using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class BaseWeaponClass : MonoBehaviour
{

    public string Nome, Tipo, NomeBala;
    public int Dano, MaximoMunicao, municaoDaArma, indiceArma;
    public Collider colisor;
    public Collider[] childColisor;
    public Rigidbody rBody;
    public ParticleSystem muzzle;


    public abstract void pickUp(Transform transforma);
    public abstract void setFields();
    public abstract void Discard();
    public abstract void SwitchPosition(Transform transforma);
    public abstract void OnSlot(Transform transforma);
    public abstract void Fire();
    //public virtual void Discard()
    //{
    //    this.transform.parent = null;
    //    rBody.useGravity = true;
    //    //rBody.isKinematic = false;
    //}
}
