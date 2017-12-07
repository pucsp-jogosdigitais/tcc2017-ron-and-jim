using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponStates { Pistol, Rifle, Shotgun, HandFree }


public class WeaponManager : MonoBehaviour
{
    [Header("Classes")]
    public HudManager hud;
    public MovePersonagem movePersonagem;
    public BaseWeaponClass ArmaAtual;
    //public BaseWeaponClass[] ArmasArmazenadas;

    [Space(5)]


    [Header("Variaveis")]
    public WeaponStates weaponState;
    public Transform[] Slots;
    public Transform pontoDaArma;
    bool pegar, trocar, podeTrocar;
    int quantidadeDeSlots = 2;





    void Start()
    {
        hud = GetComponent<HudManager>();
        movePersonagem = GetComponent<MovePersonagem>();
        SetSlots();
        pontoDaArma = GameObject.FindGameObjectWithTag("Ponto Arma").transform;
        SetMainWeapon();

    }


    void Update()
    {

        pegar = Input.GetKey(KeyCode.E);


        SetCanSwitch();

        if (podeTrocar)
        {
            trocar = Input.GetKeyDown(KeyCode.T);
        }

        trocaArmas();
        SetMainWeapon();

    }

    //void SetSlotWeapons()
    //{
    //    ArmasArmazenadas = new BaseWeaponClass[quantidadeDeSlots];
    //    for (int i = 0; i < ArmasArmazenadas.Length; i++)
    //    {
    //        ArmasArmazenadas[i] = GameObject.Find("Slot" + (i + 1)).GetComponent<BaseWeaponClass>();
    //    }
    //}

    void SetSlots()
    {
        Slots = new Transform[quantidadeDeSlots];

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = GameObject.Find("Slot" + (i + 1)).transform;

        }
    }

    public void SetMainWeapon()
    {
        //pontoDaArma = GameObject.FindGameObjectWithTag("Ponto Arma").transform;
        ArmaAtual = GameObject.FindGameObjectWithTag("Ponto Arma").GetComponentInChildren<BaseWeaponClass>();

        if (ArmaAtual == null)
        {
            movePersonagem.hasWeapon = false;
            //print("There is no weapon");
            weaponState = WeaponStates.HandFree;
        }
        else
        {
            movePersonagem.hasWeapon = true;
            //print("There is a weapon");

            if (ArmaAtual.GetType() == typeof(AssaultRifleAK47) || ArmaAtual.GetType() == typeof(AssaultRifleM4))
            {
                weaponState = WeaponStates.Rifle;
            }
            else if (ArmaAtual.GetType() == typeof(HandGun))
            {
                weaponState = WeaponStates.Pistol;
            }

            //jogar o ponto de grip para a mao esquerda do personagem aqui
            movePersonagem.SetWeaponState(weaponState);

        }
        SetWeaponType();


    }

    void SetCanSwitch()
    {
        if (ArmaAtual != null)//tem arma na mao...
        {       //e tem armas nos slots...
            if (Slots[0].GetComponentInChildren<BaseWeaponClass>() != null || Slots[1].GetComponentInChildren<BaseWeaponClass>() != null)
            {
                podeTrocar = true;
            }
            else//mas se nao tem armas nos slots...
            {
                podeTrocar = false;
            }
        }
    }


    void trocaArmas()
    {
        #region Comentado
        //if (trocar)
        //{
        //    for (int i = 0; i < Slots.Length; i++)
        //    {
        //        //Continuar A partir daqui a troca de armas
        //        if (Slots[i].childCount > 0 && pontoDaArma.childCount < 1)
        //        {

        //            Slots[i].GetComponentInChildren<BaseWeaponClass>().SwitchPosition(pontoDaArma);
        //            SetMainWeapon();
        //            break;
        //            //SwitchPlace(pontoDaArma);
        //        }
        //        else
        //        {
        //            ArmaAtual.SwitchPosition(Slots[i]);
        //            SetMainWeapon();
        //            //SwitchPlace(Slots[i]);

        //            break;



        //        }
        //    }


        //}

        #endregion


        if (trocar)
        {
            if (ArmaAtual != null)
            {
                if (Slots[0].GetComponentInChildren<BaseWeaponClass>() == null && Slots[1].GetComponentInChildren<BaseWeaponClass>() != null)
                {
                    ArmaAtual.SwitchPosition(Slots[0]);

                    Slots[1].GetComponentInChildren<BaseWeaponClass>().SwitchPosition(pontoDaArma);

                }
                else if(Slots[1].GetComponentInChildren<BaseWeaponClass>() == null && Slots[0].GetComponentInChildren<BaseWeaponClass>() != null)
                {
                    ArmaAtual.SwitchPosition(Slots[1]);

                    Slots[0].GetComponentInChildren<BaseWeaponClass>().SwitchPosition(pontoDaArma);

                }
            }

            #region Comentado
            //if (Slots[0].GetComponentInChildren<BaseWeaponClass>() == null && ArmaAtual != null)
            //{
            //    ArmaAtual.SwitchPosition(Slots[0]);
            //}
            //else if (Slots[0].GetComponentInChildren<BaseWeaponClass>() != null && ArmaAtual == null)
            //{
            //    Slots[0].GetComponentInChildren<BaseWeaponClass>().SwitchPosition(pontoDaArma);
            //}
            #endregion

        }

    }



    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.GetComponent<BaseWeaponClass>())
        {

            if (pegar)
            {

                if(ArmaAtual == null)
                {
                    col.gameObject.GetComponent<BaseWeaponClass>().pickUp(pontoDaArma);
                }
                else
                {
                    if(Slots[0].GetComponent<BaseWeaponClass>() == null && Slots[1].GetComponent<BaseWeaponClass>() == null)
                    {
                        ArmaAtual.SwitchPosition(Slots[0]);
                        col.gameObject.GetComponent<BaseWeaponClass>().pickUp(pontoDaArma);
                    }
                }

                //col.gameObject.GetComponent<BaseWeaponClass>().pickUp(pontoDaArma);
                SetMainWeapon();
                //hud.onPickUpForSprites(col.gameObject.GetComponent<BaseWeaponClass>().Nome);

            }
        }



    }

    void SetWeaponType()
    {
        switch (weaponState)
        {

            case WeaponStates.HandFree:
                SetAnimLayer(0, 1.0f);
                SetAnimLayer(1, 0.0f);
                SetAnimLayer(2, 0.0f);
                break;
            case WeaponStates.Pistol:
                SetAnimLayer(0, 0.0f);
                SetAnimLayer(1, 1.0f);
                SetAnimLayer(2, 0.0f);
                break;
            case WeaponStates.Rifle:
            case WeaponStates.Shotgun:
                SetAnimLayer(0, 0.0f);
                SetAnimLayer(1, 0.0f);
                SetAnimLayer(2, 1.0f);
                break;
        }

    }

    void SetAnimLayer(int numero, float pesoDeInfluencia)
    {
        movePersonagem.ActivateAnimLayer(numero, pesoDeInfluencia);
    }

}
