using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradorController : MonoBehaviour
{
    //Prefab para criar
    [SerializeField] private GameObject FlechaPrefab;
    //Intervalo entre as flehcasflehcas
    [SerializeField] private float Intervalo;
    //Para saber quando pode atirar
    private float UltimoTiro;

    void Start()
    {
        //Salva o momento em que a cena foi carregada, o tempo = 0 da cena
        UltimoTiro = Time.time;
    }

    void Update()
    {
        //Se o tempo é maior que o tempo do último tiro + o intervalo
        if(Time.time >= Intervalo + UltimoTiro)
        {
            //Cria uma rotação
            Quaternion rotacao = new Quaternion();
            //Define a rotação em função de graus (º)
            rotacao.eulerAngles = new Vector3(0, 0, 90);

            //Instancia a flecha apontando pra cima
            Instantiate(FlechaPrefab, transform.position, rotacao);

            //Salva o momento do tiro
            UltimoTiro = Time.time;
        }
    }
}
