using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaController : MonoBehaviour
{
    //Velocidade da flecha
    [SerializeField] private float Velocidade;

    void Update()
    {
        //Move a flecha na direção à sua direita
        //O Time.deltaTime faz a velocidade adaptar à taxa de fps do jogo,
        //assim mantendo a velocidade constante em computadores de desempenho diferentes
        transform.transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    //Ao colidir com o player
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        
        if (collision.collider.tag == "Player")
        {
            //Se sim, avisa o controlador do jogo que ele levou dano
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Dano();
            
        }
    }

    //Ao sair do campo de visão de todas as câmeras
    private void OnBecameInvisible()
    {
        //Destrói a flecha
        Destroy(this.gameObject);
    }
}
