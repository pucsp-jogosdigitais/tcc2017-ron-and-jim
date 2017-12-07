using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeout : MonoBehaviour
{

    Image imagem;
    public string nome;
    void Start()
    {
        imagem = GetComponent<Image>();
        imagem.enabled = true;
        imagem.CrossFadeAlpha(0.01f, 3f, false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            imagem.CrossFadeAlpha(1f, 1.5f, false);

            Invoke("trocaCena", 3);
        }
    }

    void trocaCena()
    {
        Debug.Log("Troca Cena");
        SceneManager.LoadScene(nome);
    }
}
