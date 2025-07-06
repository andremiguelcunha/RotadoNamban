using UnityEngine;

// Script simples para alternar entre dois GameObjects ao clicar num botão, por exemplo.
// Simple script to switch between two GameObjects, e.g., when clicking a button.

public class Abrir : MonoBehaviour
{
    public GameObject objectToShow;   // Objeto que será mostrado (ex: próximo painel, item revelado)
                                      // Object to show (e.g., next panel, revealed item)

    public GameObject objectAtual;    // Objeto atual que será escondido
                                      // Current object to hide

    public void Show()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);   // Mostra o novo objeto
                                            // Show the new object
            objectAtual.SetActive(false);   // Esconde o atual
                                            // Hide the current one
        }
    }
}
