using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen1 : MonoBehaviour
{
    public string escenario;
    // Start is called before the first frame update
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(escenario);
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu");
    }
}
