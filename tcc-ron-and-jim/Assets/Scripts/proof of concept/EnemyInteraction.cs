using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
 //public GameObject bala;
    //Transform orientacao;
    [SerializeField]
    Transform pivotArma;//incluso via editor
    [SerializeField]
    BaseWeaponClass armaNaMao;//incluso via editor


    RaycastHit CoverHit;

    int intervalo = 100;
    int somaIntervalo = 0;
    Animator anim;
    public float rayPositionOffset, rayLenghtOffset;
    public float PositionOffsetOnCover;
    public int vida;
    [HideInInspector]
    public bool encontrou, alive;
    Transform player;
    bool walk, idle, onCover, aim;
    bool alterna = false;

    float contador = 0;

    public Transform waypoint;

    void Start()
    {
        setUpAnimator();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vida = 10;
        alive = true;
        armaNaMao.pickUp(pivotArma);
        encontrou = false;
        anim.SetLayerWeight(1, 1);

    }


    void Update()
    {


        // EncontraInimigo();
        isAlive();
        AnimatorChange();
        //CoverCheck();
        walkToWaypoint();
        atirarEmCover();
    }


    void setUpAnimator()
    {
        if (!anim)
        {
            anim = GetComponent<Animator>();
        }
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
        if (anim)
        {
            anim.SetBool("Idle", idle);
            anim.SetBool("Walk", walk);
            anim.SetBool("Aim", aim);
            anim.SetBool("OnCover", onCover);
        }
    }

    void isAlive()
    {

        if (!alive)
        {
            print("morto");
            armaNaMao.Discard();
        }


        if (vida > 0)
        {
            alive = true;
        }
        else
        {
            alive = false;
        }
    }

    void EncontraInimigo()
    {
        if (alive)
        {
            if (Vector3.Distance(transform.position, player.position) < 10)
            {

                encontrou = true;
            }

            if (encontrou)
            {
                InteracaoInimigo();
            }

        }

    }

    void InteracaoInimigo()
    {
        //if (alive)
        //{
        if (Vector3.Distance(transform.position, player.position) < 10)
        {
            aim = true;
            Vector3 direction = player.position - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            //print(direction.magnitude);


            if (direction.magnitude > 5)
            {
                walk = true;

                print(direction.magnitude);
                transform.position += transform.forward * Time.deltaTime;
            }
            else
            {
                walk = false;


            }

            if (aim)
            {
                if (somaIntervalo < intervalo)
                {
                    somaIntervalo++;
                }
                else
                {
                    Atira();
                    somaIntervalo = 0;
                }
            }

        }
        else
        {
            aim = false;
            walk = false;
        }
        //}

    }

    void Atira()
    {
        armaNaMao.Fire();
        //GameObject rB = Instantiate(bala, orientacao.transform.position, orientacao.transform.rotation);
        //rB.GetComponent<Rigidbody>().AddForce(orientacao.transform.forward * 2000);
    }

    public void TomaDano(int danoDaArma)
    {
        vida -= danoDaArma;

    }

    void CoverCheck()
    {

        Ray ray = new Ray(transform.position - transform.up * rayPositionOffset, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayLenghtOffset);
        if (Physics.Raycast(ray, out CoverHit, rayLenghtOffset))
        {

            if (CoverHit.collider.gameObject.CompareTag("Cover"))
            {
                walk = false;
                onCover = true;
                StartCoroutine(GetInCover(CoverHit));

            }

        }


       


    }

    IEnumerator GetInCover(RaycastHit hit)
    {
        Vector3 startPosition = transform.position;
        //Vector3 targetPosition = hit.point;
        Vector3 targetPosition = hit.point - transform.forward * PositionOffsetOnCover;
        targetPosition.y = transform.position.y;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }


    void walkToWaypoint()
    {
        if (waypoint)
        {
            if (!onCover)
            {
                Vector3 directionToWaypoint = waypoint.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToWaypoint), 0.1f);
                walk = true;
                CoverCheck();
            }

        }
    }
    void atirarEmCover() 
    {
        if (onCover)
        {
            int contagemMaxima = 120;
            print(1);
            if (alterna)
            {
                print(2);
                aim = true;
                Atira();
            }
            else
            {
                print(3);
                aim = false;
            }

            if (contador < contagemMaxima)
            {
                print(contador);
                contador++;
            }
            else
            {
                print(4);
                alterna = !alterna;
                contador = 0;
            }
        }
    }
}
