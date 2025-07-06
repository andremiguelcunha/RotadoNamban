using UnityEngine;

// Exemplo simples de como desbloquear um item no inventário via script externo.
// Simple example of how to unlock an item in the inventory from an external script.

public class ExemploDesbloqueio : MonoBehaviour
{
    // Referência ao InventarioManager, onde está a lógica de desbloqueio.
    // Reference to the InventoryManager where the unlock logic resides.
    public InventarioManager inventarioManager;

    // Método que chama a função de desbloqueio para o item "Casaco".
    // Method that calls the unlock function for the item "Casaco".
    public void DesbloquearCasaco()
    {
        inventarioManager.DesbloquearItem("Casaco");
    }
}
