using UnityEngine;

// Restaura os itens desbloqueados com base nos dados salvos em PlayerPrefs.
// Restores previously unlocked items based on PlayerPrefs data.

public class InventarioRestaurador : MonoBehaviour
{
    // Referência ao InventarioManager que irá aplicar o desbloqueio visual.
    // Reference to the InventarioManager that handles visual unlocking.
    public InventarioManager inventarioManager;

    void Start()
    {
        RestaurarItensDesbloqueados();
    }

    void RestaurarItensDesbloqueados()
    {
        // Lista dos nomes de itens que podem estar desbloqueados.
        // List of item names that may have been unlocked.
        string[] nomesItens = { "Casaco", "Espada", "Documento", "Arca", "Bule", "Saque" };

        foreach (string nome in nomesItens)
        {
            // Se estiver marcado como desbloqueado em PlayerPrefs, desbloqueia no inventário.
            // If marked as unlocked in PlayerPrefs, unlock it in the inventory.
            if (PlayerPrefs.GetInt("Desbloqueado_" + nome, 0) == 1)
            {
                inventarioManager.DesbloquearItem("Item_" + nome); // Garante prefixo usado corretamente
            }
        }
    }
}
