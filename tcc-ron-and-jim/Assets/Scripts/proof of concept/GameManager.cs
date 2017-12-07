using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> inimigos;
    public GameObject[] inimigosArray;
    int numeroInimigos;
    public string Nome;
    // Use this for initialization
    void Start()
    {
       
        inimigosArray = GameObject.FindGameObjectsWithTag("Inimigos");

        for (int i = 0; i < inimigosArray.Length; i++)
        {
            if (inimigosArray[i] == null)
            {
                print("vazio");
            }
            else 
            {
                inimigos.Add(inimigosArray[i]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inimigos.Count; i++) 
        {
            if (i >= inimigos.Count) 
            {
                continue;
            }

            if (inimigos[i] == null) 
            {
                inimigos.RemoveAt(i);
            }
        }


        if (inimigos.Count <= 0) 
        {
            SceneManager.LoadScene(Nome);
        }

    }
}
