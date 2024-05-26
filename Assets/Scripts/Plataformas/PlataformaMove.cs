using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMove : MonoBehaviour
{
    [SerializeField] private float VelocidadePlataforma;

    float velocidadeTransferida;

    // Update is called once per frame
    void Update()
    {
        // Using Mathf.Approximately to compare floating-point values
        if (transform.position.x <= 10f || transform.position.x >= 30f)
        {
            VelocidadePlataforma *= -1;
        }

        // Use Vector3.right to ensure movement is along the global x-axis
        transform.position += Vector3.right * VelocidadePlataforma * Time.deltaTime;
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        if (collision.collider.tag == "Player")
        {
            //calcula normal
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;

            // Verifica se a colisão é vertical (cima ou baixo)
            if (Mathf.Abs(normal.y) != 0)
            { 
                velocidadeTransferida = VelocidadePlataforma;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movimento>().TransferenciaDeVelocidade(velocidadeTransferida);
            } 

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //verifica se o player saiu da plataforma (remove velocidade)
        if (collision.collider.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movimento>().TransferenciaDeVelocidade(0);
        }
    }
}
