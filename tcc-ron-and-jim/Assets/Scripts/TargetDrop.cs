using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDrop : MonoBehaviour
{
    Rigidbody _rigidBody;
    public bool colide;
    public float distancia, forca;
    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (colide)
        {
            _rigidBody.isKinematic = false;
            _rigidBody.AddForce(new Vector3(forca, 0, 0));
        }

        if (NoChao())
        {
            _rigidBody.isKinematic = true;

        }

    }

    bool NoChao()
    {
        Ray raio = new Ray(transform.position, transform.right);
        RaycastHit hit;

        Debug.DrawRay(raio.origin, raio.direction * distancia, Color.blue);
        if (Physics.Raycast(raio, out hit, distancia))
        {
            if (!hit.collider.isTrigger)
            {
                return true;
            }
            //if (hit.collider.gameObject.CompareTag("Chao"))
            //{
            //    return true;
            //}

        }

        return false;


    }
}
