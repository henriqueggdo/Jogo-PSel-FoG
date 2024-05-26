using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnReiniciarController : MonoBehaviour
{
    public void Reiniciar()
    {
        //Volta o tempo e recarrega a fase
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
