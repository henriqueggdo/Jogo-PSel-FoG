using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject primeiraImagem;
    [SerializeField] private GameObject segundaImagem;
    [SerializeField] private GameObject terceiraImagem;

    public void UpdateVidas(int vidas)
    {
        primeiraImagem.SetActive(vidas > 0);
        segundaImagem.SetActive(vidas > 1);
        terceiraImagem.SetActive(vidas > 2);
    }
}
