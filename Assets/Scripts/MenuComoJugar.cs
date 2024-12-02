using UnityEngine;

public class MenuComoJugar : MonoBehaviour
{
    // Referencia al Panel que deseas controlar
    public GameObject panelToControl;

    private void Start()
    {
        // Asegúrate de que el panel está desactivado al inicio si es necesario
        if (panelToControl != null)
        {
            panelToControl.SetActive(false);
        }
    }

    // Método para activar el panel
    public void ShowPanel()
    {
        if (panelToControl != null)
        {
            panelToControl.SetActive(true);
        }
    }

    // Método para desactivar el panel
    public void HidePanel()
    {
        if (panelToControl != null)
        {
            panelToControl.SetActive(false);
        }
    }
}
