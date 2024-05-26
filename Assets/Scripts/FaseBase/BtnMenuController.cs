using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMenuController : MonoBehaviour
{
    public void IrMenu()
    {
        //Volta o tempo e vai pra primeira cena
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
