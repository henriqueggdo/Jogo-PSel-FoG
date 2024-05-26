using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroJogadorController : MonoBehaviour
{
    //Velocidade da flecha
    [SerializeField] private float Velocidade;

    void Update()
    {
        //Move a flecha na dire��o � sua direita
        //O Time.deltaTime faz a velocidade adaptar � taxa de fps do jogo,
        //assim mantendo a velocidade constante em computadores de desempenho diferentes
        transform.transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    //Ao colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se colidiu com o inimigo
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
          
        }
    }

    //Ao sair do campo de vis�o de todas as c�meras
    private void OnBecameInvisible()
    {
        //Destr�i a flecha
        Destroy(this.gameObject);
    }
}
