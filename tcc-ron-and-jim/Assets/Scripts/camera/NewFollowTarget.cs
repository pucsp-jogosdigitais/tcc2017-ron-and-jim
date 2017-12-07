using UnityEngine;
using System.Collections;

public class NewFollowTarget : MonoBehaviour
{



    public float direita, cima, frente;
    public float direita2, cima2, frente2;
    public int tiltMax, tiltMin;
    [HideInInspector]
    public float TurnXInput, TurnYInput, TurnSpeed, TurnHorizontal, TurnVertical;
    MovePersonagem movepersonagem;
    Camera Camera;
    Transform cam;
    public Transform Target;
    public Transform PivotPrimario, PivotSecundario;// , pivotSemZoom, pivotComZoom;
    public float valorMinimo, valorMaximo;
    public Vector3 posicaoComZoom, posicaoSemZoom;





    void Start()
    {
        MetodoSetTarget();
        MetodoSetPivot();
        TurnSpeed = 2.0f;

        movepersonagem = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePersonagem>();


        //valorMinimo = 30f;//com zoom
        //valorMaximo = 60f;//sem zoom

    }


    void LateUpdate()
    {


        UpdatePosition();


    }




    void MetodoSetTarget()
    {
        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void MetodoSetPivot()
    {
        PivotPrimario = new GameObject().transform;
        PivotPrimario.name = "PivotPrimario";

        PivotSecundario = new GameObject().transform;
        PivotSecundario.name = "PivotSecundario";

        cam = GetComponent<Camera>().transform;
        PivotSecundario.parent = PivotPrimario.transform;
        cam.parent = PivotSecundario.transform;
        cam.rotation = PivotSecundario.rotation;


        PivotPrimario.position = Target.position;
        PivotSecundario.position = PivotPrimario.position;

        //posicaoComZoom = PivotSecundario.position - (PivotSecundario.forward * frente2) + (PivotSecundario.right * direita2) + (PivotSecundario.up * cima2);
        posicaoSemZoom = PivotSecundario.position - (PivotSecundario.forward * frente) + (PivotSecundario.right * direita) + (PivotSecundario.up * cima);

        cam.position = posicaoSemZoom;


        Camera = Camera.main;

        TurnHorizontal = Target.rotation.eulerAngles.y;


    }

    void UpdatePosition()
    {


        #region Comentado
        //if (!movepersonagem.Run)
        //{
        //    TurnXInput = Input.GetAxis("Mouse X");
        //    TurnYInput = Input.GetAxis("Mouse Y");
        //}


        //TurnX += TurnXInput * TurnSpeed;
        //TurnY -= TurnYInput * TurnSpeed;

        //TurnY = Mathf.Clamp(TurnY, tiltMin, tiltMax);


        //if (movepersonagem.Run)
        //{
        //    TurnX += movepersonagem.right * 0.5f;
        //    PivotPrimario.rotation = Quaternion.Euler(0, TurnX, 0);
        //    //Debug.Log(TurnX + movepersonagem.right);
        //    TurnY = ZeraPosicao((int)TurnY);
        //    PivotSecundario.rotation = Quaternion.Euler(TurnY, TurnX, 0);
        //}
        //else
        //{
        //    TurnXInput = Input.GetAxis("Mouse X");
        //    TurnYInput = Input.GetAxis("Mouse Y");

        //    TurnX += TurnXInput * TurnSpeed;
        //    TurnY -= TurnYInput * TurnSpeed;

        //    TurnY = Mathf.Clamp(TurnY, tiltMin, tiltMax);


        //    PivotPrimario.rotation = Quaternion.Euler(0, TurnX, 0);
        //    PivotSecundario.rotation = Quaternion.Euler(TurnY, TurnX, 0);
        //}
        #endregion


        MetodoRotacionaCamera();


        MetodoPosicaoCamera();


    }

    void MetodoRotacionaCamera()
    {
        //if (movepersonagem.Run)
        //{
        //    TurnHorizontal += movepersonagem.right * 0.5f;
        //    PivotPrimario.rotation = Quaternion.Euler(0, TurnHorizontal, 0);
        //    TurnVertical = ZeraPosicao((int)TurnVertical);
        //    PivotSecundario.rotation = Quaternion.Euler(TurnVertical, TurnHorizontal, 0);
        //}
        //else
        //{
            TurnXInput = Input.GetAxis("Mouse X");
            TurnYInput = Input.GetAxis("Mouse Y");

            TurnHorizontal += TurnXInput * TurnSpeed;
            TurnVertical -= TurnYInput * TurnSpeed;

            TurnVertical = Mathf.Clamp(TurnVertical, tiltMin, tiltMax);


            PivotPrimario.rotation = Quaternion.Euler(0, TurnHorizontal, 0);
            PivotSecundario.rotation = Quaternion.Euler(TurnVertical, TurnHorizontal, 0);
        //}
    }


    void MetodoPosicaoCamera()
    {

        #region Comentado
        //cam.position = posicaoSemZoom;

        //Vector3 posicaoDaCamera = PivotSecundario.position - (PivotSecundario.forward * frente) + (PivotSecundario.right * direita) + (PivotSecundario.up * cima);

        //cam.position = posicaoDaCamera;
        #endregion

        PivotPrimario.position = Target.position;
        PivotSecundario.position = PivotPrimario.position;

        //posicaoComZoom = PivotSecundario.position - (PivotSecundario.forward * frente2) + (PivotSecundario.right * direita2) + (PivotSecundario.up * cima2);

        posicaoSemZoom = PivotSecundario.position - (PivotSecundario.forward * frente) + (PivotSecundario.right * direita) + (PivotSecundario.up * cima);

        //MetodoZoomNovo(movepersonagem.mirar, posicaoComZoom, posicaoSemZoom);

        MetodoZoom(movepersonagem.mirar);
        cam.position = posicaoSemZoom;
    }




    public void MetodoZoom(bool zoom)
    {
        float valorZoom = Camera.fieldOfView;
        if (zoom)
        {
            valorZoom -= 5.0f;
        }
        else
        {
            valorZoom += 2.0f;
        }

        valorZoom = clampFloat(valorZoom, valorMinimo, valorMaximo);

        Camera.fieldOfView = valorZoom;
    }

    public void MetodoZoomNovo(bool zoom, Vector3 zoomIn, Vector3 zoomOut)
    {
        if (zoom)
        {

            cam.position = Vector3.Lerp(cam.position, zoomIn, 0.3f);
        }
        else
        {

            cam.position = Vector3.Lerp(cam.position, zoomOut, 0.1f);
        }
    }

    float clampFloat(float valorAtual, float limiteMin, float limiteMax)
    {
        float valorFloat = valorAtual;

        if (valorFloat >= limiteMax) { valorFloat = limiteMax; }
        if (valorFloat <= limiteMin) { valorFloat = limiteMin; }

        return valorFloat;

    }

    int ZeraPosicao(int YInput)
    {
        int valor = YInput;

        valor = (int)Mathf.Lerp(valor, 0.0f, 0.1f);

        //if (valor > 0) { valor --; }
        //else if (valor < 0) { valor ++; }
        //else if(valor == 0) { valor = 0; }

        return valor;

    }

}
