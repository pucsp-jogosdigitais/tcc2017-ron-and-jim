using UnityEngine;
using System.Collections;

public class Esfera : MonoBehaviour {
    Vector3 vetor;
    public GameObject personagem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(personagem.transform.position, Vector3.up, 20 * Time.deltaTime);
        //transform.position = personagem.transform.forward;
        //vetor = personagem.transform.position + personagem.transform.forward;
        //transform.position = vetor;

	}
}
