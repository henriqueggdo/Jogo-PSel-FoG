using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Velocidade do inimigo
    [SerializeField] private float Velocidade;

    // Update is called once per frame
    void Update()
    {
        //Move a flecha na direção à sua direita
        //O Time.deltaTime faz a velocidade adaptar à taxa de fps do jogo,
        //assim mantendo a velocidade constante em computadores de desempenho diferentes
        transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    //Ao colidir
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        if (collision.collider.tag == "Player")
        {
            //Se sim, avisa o controlador do jogo que ele morreu
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Dano();
        }
    }

    //Ao colidir com o tiro do player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        if (collision.collider.tag == "TiroAliado")
        {
            //Se sim, avisa o controlador do jogo que ele morreu
            Destroy(gameObject);
            
        }

        if (collision.collider.tag == "Plataforma" || collision.collider.tag == "Parede")
        {
            
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;

            // Verifica se a colisão é lateral (esquerda ou direita)
            if (Mathf.Abs(normal.x) != 0)
            { 
                Velocidade *= -1;
            }
            
        }

    }
}
