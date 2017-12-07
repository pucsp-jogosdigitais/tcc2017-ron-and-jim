using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HudManager : MonoBehaviour
{
    [Header("Sprites")]

    [SerializeField]
    private Sprite[] HUDHandGun;
    [SerializeField]
    private Sprite[] HUDHandFree;

    [Space(10)]


    //[Header("Concept Aim")]
    //[SerializeField]
    //ConceptAim aim;
    //[Space(10)]

    [Header("textura a ser usado")]
    [SerializeField]
    Sprite[] WeaponSprite;
    [Space(10)]

    [Header("WeaponsClasses")]


    Image imagemArmaSelecionada;
    int indexMunicao = 0;
    // int numeroDeBalas;

    void Start()
    {
        // aim = GetComponent<ConceptAim>();
        SetSprites();
        imagemArmaSelecionada = GameObject.Find("HudArma").GetComponent<Image>();

        // onPickUpForSprites("Handgun", 0);
    }


    void Update()
    {

        /*
         * fazer um metodo que receba o tipo de script anexado a arma 
         * e exiba o hud correspondente a cada frame
         */

        if (WeaponSprite == null)
        {
            //nada
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                indexMunicao++;
                print(indexMunicao);
            }

            if (indexMunicao >= WeaponSprite.Length)
            {
                indexMunicao = 0;
            }
            imagemArmaSelecionada.sprite = WeaponSprite[indexMunicao];
        }
    }

    public void onPickUpForSprites(string nomeArma, int indice)
    {

        WeaponSprite = ReturnSpriteWeapon(nomeArma);
        imagemArmaSelecionada.sprite = WeaponSprite[indexMunicao];
    }

    public void SetHud(BaseWeaponClass tipoArma)
    {
        if (tipoArma.GetType() == typeof(AssaultRifleAK47))
        {
            //exibe o hud da ak
        }
        else if (tipoArma.GetType() == typeof(AssaultRifleM4))
        {
            //exibe o hud da m4
        }
        else if (tipoArma.GetType() == typeof(UziUMP))
        {
            //exibe o hud da uzi
        }
        else if (tipoArma.GetType() == typeof(HandGun))
        {
            //exibe o hud da handgun

        }


    }

    void SetSprites()
    {
        WeaponSprite = null;
        HUDHandGun = Resources.LoadAll<Sprite>("HandgunHudSpriteSheet");
        HUDHandFree = Resources.LoadAll<Sprite>("HandFree");


    }

    Sprite[] ReturnSpriteWeapon(string nome)
    {
        Sprite[] sprt = null;

        switch (nome)
        {

            case "noWeapon":
                sprt = HUDHandFree;
                break;

            case "Handgun":
                sprt = HUDHandGun;
                break;

            default:
                Debug.Log("No weapon exist");
                break;
        }
        return sprt;
    }




}
