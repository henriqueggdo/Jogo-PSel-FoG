using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDoJogo : MonoBehaviour
{
    //Pega o Canvas da UI de morte
    [SerializeField] GameObject Canvas;
    [SerializeField] private int vidas = 3;
    // Intervalo de tempo entre danos
    [SerializeField] private float intervaloDano = 1.0f;
    //Tempo para o próximo dano
    private float tempoDanoPrev = 0f;

    public void Dano()
    {
        if(Time.time - tempoDanoPrev > intervaloDano)
        {
            tempoDanoPrev = Time.time;
            vidas--;
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().UpdateVidas(vidas);
        }
        
        if(vidas <= 0)
        {
            Morreu();
        }
    }
    
    public void Morreu()
    {
        //Para o tempo e ativa o Canvas
        Time.timeScale = 0.0f;
        Canvas.gameObject.SetActive(true);
    }
}
