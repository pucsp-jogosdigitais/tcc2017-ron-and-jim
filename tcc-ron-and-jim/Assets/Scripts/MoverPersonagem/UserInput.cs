using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour
{
    [Header("Variaveis/Inputs")]
    float HorizontalButton, HorizontalMouse, Vertical;
    bool Run, OnCover, HasWeapon, Aim;
    [Space(10)]

    [Header("Acesso a outras classes")]
   
    WeaponManager weaponmanager;
    MovePersonagem movePersonagem;
    HudManager hudManager;




    void Start()
    {

    }

    void FixedUpdate()
    {
        HorizontalButton = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Run = Input.GetKey(KeyCode.LeftShift);



    }


}
