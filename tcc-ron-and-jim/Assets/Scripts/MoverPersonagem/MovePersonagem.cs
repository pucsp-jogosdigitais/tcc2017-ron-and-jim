using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class MovePersonagem : MonoBehaviour
{


    /*
     * offsetRotation with two hand weapons:
        X: -10.5
        Y:  6.75
        Z: -32.01
        W:  39.4

         offsetRotation with one hand weapons:

     * 
     */




    #region outrasPropriedades
    public Animator anim;
    public Rigidbody _rigidBody;

    [HideInInspector]
    public bool Onground, Run, onCover;
    public bool canCrawlRight, canCrawlLeft;


    [HideInInspector]
    public float forward, right;
    public float quebraVelocidade = 65, speed = 0.002f;
    public bool mirar, hasWeapon;
    public Transform pivotTransform;


    public float distanciaChecaCover = 5.0f;


    RaycastHit coverHit;


    public float floatOffset;

    #endregion


    WeaponStates weaponState;
    public Quaternion assaultWeaponOffsetRotation, handgunOffsetRotation;
    //public int offsetY, offsetX, offsetZ;
    Vector3 target;
    public float DistanciaDoChao;



    void Start()
    {
        setUpAnimator();
        _rigidBody = GetComponent<Rigidbody>();

        #region Comentado_Pivot_Tranform
        //if (!pivotTransform)
        //{
        //    pivotTransform = GameObject.Find("PivotPrimario").transform;
        //}
        #endregion
    }

    void Update()
    {

        if (!pivotTransform)
        {
            pivotTransform = GameObject.Find("PivotPrimario").transform;
        }
        Onground = GroundCheck();

        AnimatorChange();
        if (Onground)
        {
            onAnimatorMove();

            TurnSpeed();
            CoverCheck();

        }



    }

    void setUpAnimator()
    {
        anim = GetComponent<Animator>();

        foreach (Animator childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }

    }

    void AnimatorChange()
    {
        anim.applyRootMotion = true;

        forward = Input.GetAxis("Vertical");
        right = Input.GetAxis("Horizontal");

        anim.SetFloat("Forward", forward, .1f, Time.deltaTime);
        anim.SetFloat("Straff", right, .1f, Time.deltaTime);

        Run = Input.GetKey(KeyCode.LeftShift);
        anim.SetBool("Run", Run);

        anim.SetBool("HasWeapon", hasWeapon);
        anim.SetBool("OnCover", onCover);

        anim.SetBool("CanCrawlRight", canCrawlRight);
        anim.SetBool("CanCrawlLeft", canCrawlLeft);

        if (hasWeapon)
        {

           mirar = Input.GetMouseButton(1);
           // mirar = true;
            anim.SetBool("Aim", mirar);
        }
        else
        {
            mirar = false;
        }
    }


    void onAnimatorMove()
    {
        if (!onCover)
        {
            speed = 0.002f;
        }
        else
        {
            speed = 0.008f;

            if (canCrawlLeft && canCrawlRight)
            {
                right = Mathf.Clamp(right, -1, 1);
            }
            if (!canCrawlLeft && canCrawlRight)
            {
                right = Mathf.Clamp(right, 0, 1);
            }
            if (canCrawlLeft && !canCrawlRight)
            {
                right = Mathf.Clamp(right, -1, 0);
            }


            float distancia = Vector3.Distance(transform.position, coverHit.point);

            if (Mathf.Abs(distancia) > 0.5f)
            {
                float valorYOriginal = transform.position.y;
                Vector3 offset = transform.forward;
                Vector3 posicaoCover = coverHit.point - offset * floatOffset;
                posicaoCover.y = valorYOriginal;
                transform.position = posicaoCover;
            }

            //print(distancia);
        }

        Vector3 mover = (forward * transform.forward + right * transform.right) * speed;
        transform.position += mover;
    }

    void TurnSpeed()
    {
        #region Comentado
        /*
        if (!mirar)
        {
            if (forward != 0 || right != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, pivotTransform.rotation, Time.deltaTime * 15);
            }
        }
        else
        {

        }
        */
        #endregion

        if (!onCover)
        {
            if (forward != 0 || right != 0 || mirar)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, pivotTransform.rotation, Time.deltaTime * 10);
            }
        }
    }

    bool GroundCheck()
    {

        Ray ray = new Ray(transform.position, -Vector3.up);

        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * DistanciaDoChao, Color.red);
        if (Physics.Raycast(ray, out hit, DistanciaDoChao))
        {

            if (!hit.collider.isTrigger)
            {
                //_rigidBody.position = Vector3.MoveTowards(_rigidBody.position, hit.point, Time.deltaTime);
                //_rigidBody.position = hit.point - transform.up * DistanciaDoChao;
                return true;
            }

        }
        return false;
    }

    void CoverCheck()
    {
        bool StartGetCover = Input.GetKeyDown(KeyCode.Q);
        Ray ray = new Ray(transform.position - Vector3.up * 0.5f, transform.forward);
        //RaycastHit coverHit;
        Debug.DrawRay(ray.origin, ray.direction * distanciaChecaCover);
        if (Physics.Raycast(ray, out coverHit, distanciaChecaCover))
        {
            if (coverHit.collider.gameObject.name.Contains("Cover")|| coverHit.collider.CompareTag("Cover"))
            {
                //print("pode entrar em cover");
                if (StartGetCover)
                {

                    GetIntoCover(coverHit);
                }
            }
        }
        else
        {
            //print("não pode entrar em cover");

        }

        if (onCover && forward < 0)
        {
            onCover = false;
        }

    }

    void GetIntoCover(RaycastHit hit)
    {
        onCover = true;
        canCrawlLeft = true;
        canCrawlRight = true;
        StartCoroutine(GetCover(hit.point));
    }

    IEnumerator GetCover(Vector3 target)
    {

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = target;
        targetPosition.y = transform.position.y;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }


    }


    public void targetPosition(Vector3 posicao)
    {
        target = posicao;
    }
    public void SetWeaponState(WeaponStates wp)
    {
        weaponState = wp;
        //print(weaponState);
    }

    void OnAnimatorIK()
    {
        //Arrumar depois o sistema de mira
        if (anim)
        {
            if (mirar && hasWeapon)
            {
                if (weaponState == WeaponStates.Rifle || weaponState == WeaponStates.Shotgun)
                {
                    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    Quaternion handRotation = Quaternion.LookRotation(target - transform.position) * assaultWeaponOffsetRotation;
                    anim.SetIKRotation(AvatarIKGoal.RightHand, handRotation);

                }
                else if (weaponState == WeaponStates.Pistol)
                {
                    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    Quaternion handRotation = Quaternion.LookRotation(target - transform.position) * handgunOffsetRotation;
                    anim.SetIKRotation(AvatarIKGoal.RightHand, handRotation);

                }


                //Vector3 vetorDiferenca = target - transform.position;
                //print(vetorDiferenca);
                anim.SetLookAtWeight(1, 1, 1, 1);
                anim.SetLookAtPosition(target);
                //anim.SetLookAtPosition(vetorDiferenca);

            }
        }
    }


    public void ActivateAnimLayer(int layerID, float weight)
    {
        if (anim)
        {
            anim.SetLayerWeight(layerID, weight);
        }
    }


    #region Proposito de teste
    void OnTriggerStay(Collider col)
    {
        if (onCover)
        {
            if (col.gameObject.name == "Direita")
            {

                canCrawlRight = false;
            }
            else if (col.gameObject.name == "Esquerda")
            {
                canCrawlLeft = false;

            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (onCover)
        {
            if (col.gameObject.name == "Direita")
            {

                canCrawlRight = true;
            }
            else if (col.gameObject.name == "Esquerda")
            {
                canCrawlLeft = true;

            }
        }
    }
    #endregion

}
