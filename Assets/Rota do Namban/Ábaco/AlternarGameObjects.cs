using UnityEngine;

// Script simples para alternar visibilidade entre dois GameObjects.
// Simple script to toggle visibility between two GameObjects.

public class AlternarGameObjects : MonoBehaviour
{
    public GameObject esconderEste; // Objeto a esconder
    public GameObject mostrarEste;  // Objeto a mostrar

    // Este método deve ser chamado por um botão ou outro evento
    // This method should be called via a button or other event
    public void Trocar()
    {
        // Esconde o primeiro GameObject, se estiver definido
        // Hides the first GameObject, if assigned
        if (esconderEste != null)
            esconderEste.SetActive(false);

        // Mostra o segundo GameObject, se estiver definido
        // Shows the second GameObject, if assigned
        if (mostrarEste != null)
            mostrarEste.SetActive(true);
    }
}
