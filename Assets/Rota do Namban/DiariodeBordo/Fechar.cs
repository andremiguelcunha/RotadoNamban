using UnityEngine;

// Este script serve para esconder (fechar) um GameObject, por exemplo, ao clicar num botão "Fechar".
// This script hides (closes) a GameObject, for example when clicking a "Close" button.

public class Fechar : MonoBehaviour
{
    public GameObject objectToShow; // Objeto que será escondido
                                    // Object to hide

    public void Show()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false); // Esconde o objeto
                                           // Hide the object
        }
    }
}
