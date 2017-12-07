using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeMenuPrincipal : MonoBehaviour
{

    Image imagem;

    float valor;

    void Start()
    {
        imagem = GetComponent<Image>();
        imagem.enabled = true;

        imagem.CrossFadeAlpha(0.01f, 3.0f, false);
    }

    public void CrossFade()
    {

        imagem.CrossFadeAlpha(1f, 1.5f, false);
    }

}
