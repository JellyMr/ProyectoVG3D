using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
   public void QuitGame(){
    Application.Quit();
    Debug.Log("Saliste del Juego");
   }
}
