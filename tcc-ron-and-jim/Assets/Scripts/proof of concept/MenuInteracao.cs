using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuInteracao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,IPointerUpHandler
{
    public string nome;
    Image imagem;
    public bool clicou, CarregaCena;
    fadeMenuPrincipal imagemFade;

    //public RectTransform Transform;
    //public bool clicou;
    //Rect retangulo;

    //public float largura, altura, larguraTela, alturaTela;


    void Start()
    {
        clicou = false;
        imagem = GetComponent<Image>();
        imagemFade = GameObject.Find("imagemFade").GetComponent<fadeMenuPrincipal>();
       
        #region Comentado
        //larguraTela = RectCanvas.rect.width;
        //alturaTela = RectCanvas.rect.height;
        //largura = larguraTela * 0.2f;
        //altura = alturaTela * 0.05f;
        //Transform = GetComponent<RectTransform>();
        //retangulo = new Rect(Transform.position.x, Transform.position.y,imagem.rectTransform.rect.width, imagem.rectTransform.rect.height);
        #endregion

        SetColor(Color.black);
    }

    void Update()
    {

        #region Comentado
        //print(imagem.rectTransform.anchoredPosition);
        //print(imagem.rectTransform.rect.width);

        //if (retangulo.Contains(Input.mousePosition))
        //{
        //    imagem.color = Color.white;

        //    if (Input.GetMouseButton(0))
        //    {
        //        imagem.color = Color.green;
        //        print("clicou");
        //    }
        //}
        //else
        //{

        //    imagem.color = Color.black;
        //}
        #endregion

     

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clicou)
            SetColor(Color.white);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
            SetColor(Color.black);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!clicou)
            SetColor(Color.red);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
        if (!clicou)
        {
            SetColor(Color.white);
            clicou = true;
            imagemFade.CrossFade();
            Invoke("trocaCena", 3);
        }
    }

    public void SetColor(Color color)
    {
            imagem.color = color;
           // Invoke("trocaCena", 3);
    }



    void trocaCena()
    {
        Debug.Log(nome);
        if (CarregaCena)
        {
            SceneManager.LoadScene(nome);
        }
    }
}
