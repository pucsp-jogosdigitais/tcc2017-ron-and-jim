using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFire : MonoBehaviour
{


    [SerializeField]
    MovePersonagem movePersonagem;
    WeaponManager weaponManager;
    Transform cam;
   [SerializeField] BaseWeaponClass Arma;
    public bool atirar;

    public Transform alvo;
    public Transform cubo;

    public float alcance = 30f;
    public float novaDistancia;




    // Use this for initialization
    void Awake()
    {
        movePersonagem = GetComponent<MovePersonagem>();
        weaponManager = GetComponent<WeaponManager>();
        cam = Camera.main.transform;
        alvo = GameObject.Find("pontodeFuga").transform;
        //Arma = weaponManager.ArmaAtual;

    }

   
    void Update()
    {
        Arma = weaponManager.ArmaAtual;
        atirar = Input.GetMouseButtonDown(0);
        MirarAtirar();
        PosicionarAlvo();


    }


    private void PosicionarAlvo()
    {
        //alvo.position = cam.position + cam.transform.forward * alcance;
        //cubo.position = cam.position + cam.transform.forward *novaDistancia;
        alvo.position = cam.position + cam.transform.forward * novaDistancia;
        movePersonagem.targetPosition(alvo.position);
    }

    void MirarAtirar()
    {

        Ray ray;
        RaycastHit hit;

        ray = new Ray(cam.position, cam.forward);

        Debug.DrawRay(ray.origin, ray.direction * alcance, Color.green);

        //if (Physics.Raycast(cam.position, cam.forward, out hit, alcance))
        ////if (Physics.Raycast(ray, out hit, alcance * 3))
        //{

        if (movePersonagem.mirar & atirar)
        {
            Arma.Fire();
            if (Physics.Raycast(cam.position, cam.forward, out hit, alcance))
            {
                if (hit.collider.GetComponent<EnemyInteraction>())
                {
                    // hit.collider.gameObject.SendMessage("TomaDano", Arma.Dano, SendMessageOptions.DontRequireReceiver);
                    hit.collider.gameObject.GetComponent<EnemyInteraction>().TomaDano(Arma.Dano);
                    //print(Arma.name);
                }

                if (hit.collider.GetComponent<TargetDrop>())
                {
                    hit.collider.GetComponent<TargetDrop>().colide = true;
                }
               
            }
        }
        //else if (movePersonagem.mirar && !atirar)
        //{
        //    if (Physics.Raycast(cam.position, cam.forward, out hit, alcance))
        //    {
        //        if (hit.collider.GetComponent<ConceptEnemy>())
        //        {

        //        }
        //        else if (!hit.collider.GetComponent<ConceptEnemy>())
        //        {

        //        }
        //    }
        //}
    }
}

