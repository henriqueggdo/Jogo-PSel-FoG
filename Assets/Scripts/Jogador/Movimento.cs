using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float Velocidade;
    [SerializeField] private float VelocidadeCorrendo;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private LayerMask Chao;
    
    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    [SerializeField] private float ForcaPulo;

    //Variaveis do dash
    [SerializeField] private float ForcaDash;
    [SerializeField] private float CooldownDash = 0.5f;
    [SerializeField] private float InputDuploTimer = 0.5f;
    
    [SerializeField] private float coyoteTime = 0.2f;

    //Prefab do tiro
    [SerializeField] private GameObject TiroJogadorPrefab;
    //Intervalo entre os tiros
    [SerializeField] private float TiroTimer;
    //Para saber quando pode atirar
    private float tempoTiroPrev = 0f;
    private bool viradoEsquerda = false;

    //Velocidade transferiada
    private float velocidadeDrag = 0;

    //Para ele não pular infinitamente
    private int numPulos = 0;
    
    //Contador do coyote time
    private float coyoteTimeContador;
    
    //Condições para controlar o doubletap do dash
    private float tempoInputDirecionalPrev = 0f;
    private float tempoDashPrev = -100f;
    private string direcaoPrev = "";

    void Update()
    {
        //Determina a velocidade que será usada
        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift) ? VelocidadeCorrendo : Velocidade;
    
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A função retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna -1 se a seta da esquerda ou A foram pressionados
        //Retorna 0 se nenhum direcional foi pressionado
        if(velocidadeDrag > 0f && velocidadeAtual <= velocidadeDrag) {
            transform.position += transform.right * velocidadeDrag * Time.deltaTime;
        }
        if(velocidadeDrag < 0f && velocidadeAtual >= velocidadeDrag) {
            transform.position += transform.right * velocidadeDrag * Time.deltaTime;
        }
        int direcaoMovimentoHorizontal = (int)Input.GetAxisRaw("Horizontal");
        float movimento_horizontal = velocidadeAtual * direcaoMovimentoHorizontal;
        //Determina se o tiro vai para esquerda
        viradoEsquerda = direcaoMovimentoHorizontal == -1 ? true : false;

        //Neste caso, não se usa Time.deltaTime, porque RigidBody2D.velocity já opera baseado na taxa de frames
        Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);

        //Cria uma caixa, se a caixa colidir com o chao, pode pular
        //Nessa função se passa a posição, tamanho, angulo e distancia(tamanho) em relação a direção
        //Tambem passa um layer mask, pra que somente os layers associados a Chao sejam considerados
        bool PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.01f, Chao);
                
        //Se o acerto tem um resultado não nulo, pode pular
        if(PertoDoChao)
        {
            //Timer coyotetime resetta
            coyoteTimeContador = coyoteTime;
        } else {
            //Timer coyote time decrementa
            coyoteTimeContador -= Time.deltaTime;
        }

        if(coyoteTimeContador > 0f) { //No chao (ou perto)
            numPulos = 2;
        } else {
            if(numPulos == 2) { //No ar
                numPulos = 1;
            }
        }

        //Se a barra de espaço foi pressionada e o jogador pode pular
        if (Input.GetKeyDown(KeyCode.Space) && numPulos > 0)
        {
            coyoteTimeContador = 0f;
            //Adiciona uma força para cima proporcional à ForçaPulo
            Corpo.AddForce(Vector2.up * ForcaPulo, ForceMode2D.Impulse);
            //Decrementa o número de pulos
            numPulos--;
            
        }
        
        //Aperte Z depois do cooldown do tiro para usar
        if(Time.time >= TiroTimer + tempoTiroPrev && Input.GetKeyDown(KeyCode.Z))
        {
            //Cria uma rotação
            Quaternion rotacao = new Quaternion();
            //Define a rotação em função de graus (º)
            if(viradoEsquerda) {
                rotacao.eulerAngles = new Vector3(0, 0, 180);
            } else {
                rotacao.eulerAngles = new Vector3(0, 0, 0);
            }

            //Instancia a flecha apontando pra cima
            Instantiate(TiroJogadorPrefab, transform.position, rotacao);

            //Salva o momento do tiro
            tempoTiroPrev = Time.time;
        }

        Doubletap();
    }
    
    private void Doubletap()
    {
        if (Time.time - tempoDashPrev < CooldownDash)
            return;

        bool dashInputDetected = false;
        string currentDirection = "";

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = "-X";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = "+X";
        }

        if (!string.IsNullOrEmpty(currentDirection))
        {
            if (currentDirection == direcaoPrev && (Time.time - tempoInputDirecionalPrev) < InputDuploTimer)
            {
                dashInputDetected = true;
            }

            tempoInputDirecionalPrev = Time.time;
            direcaoPrev = currentDirection;
        }

        if (dashInputDetected)
        {
            //zera a velocidade
            Corpo.velocity = new Vector2(0, 0);
            
            Dash(currentDirection);
            tempoDashPrev = Time.time;
        }
    }

    private void Dash(string direction)
    {
        Vector2 dashVector = Vector2.zero;

        if (direction == "-X")
        {
            dashVector = Vector2.left * ForcaDash;
        }
        else if (direction == "+X")
        {
            dashVector = Vector2.right * ForcaDash;
        }

        Corpo.AddForce(dashVector, ForceMode2D.Impulse);
    }

    public void TransferenciaDeVelocidade(float velocidadeTransferida) {
        velocidadeDrag = velocidadeTransferida;
    }
}


