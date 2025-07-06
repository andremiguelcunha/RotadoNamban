using UnityEngine;

// Gere os itens visuais no inventário da cena: desbloqueio, contagem e reset.
// Manages scene inventory items: unlocking, counting and resetting.

public class InventarioManager : MonoBehaviour
{
    [Header("Itens na cena")]
    // Lista de itens visuais que podem ser desbloqueados na UI
    // List of visual items that can be unlocked in the UI
    public ItemVisualizador[] itensNaCena;

    // ✅ Desbloqueia um item diretamente pelo nome do GameObject
    // ✅ Unlock an item directly by its GameObject name
    public void DesbloquearItem(string nomeItem)
    {
        // Verifica se o item tem restrições específicas (bule ou saque)
        // Check if the item has special conditions (teapot or loot)
        bool ehItemCondicional = nomeItem == "Item_Bule" || nomeItem == "Item_Saque";

        // Requisitos: arca aberta E estar na fase 4
        // Requirements: chest must be opened AND player must be in phase 4
        bool arcaAberta = PlayerPrefs.GetInt("Item_Arca", 0) == 1;
        bool naFase4 = PlayerPrefs.GetInt("FaseAtual", 0) == 4;

        if (ehItemCondicional && (!arcaAberta || !naFase4))
        {
            Debug.Log($"Item {nomeItem} bloqueado: requer arca aberta e estar na fase 4.");
            return; // Bloqueia a exibição
        }

        // Procura o item na lista e desbloqueia se encontrado
        // Look for the item in the list and unlock if found
        foreach (var item in itensNaCena)
        {
            if (item != null && item.name == nomeItem)
            {
                item.Desbloquear();
                return;
            }
        }

        Debug.LogWarning("Item '" + nomeItem + "' não encontrado no inventário.");
    }

    // ✅ Conta quantos itens foram desbloqueados
    // ✅ Counts how many items are currently unlocked
    public int TotalItensDesbloqueados()
    {
        int count = 0;
        foreach (var item in itensNaCena)
        {
            if (item != null && item.desbloqueado)
                count++;
        }
        return count;
    }

    // ✅ Reset: trava todos os itens (desbloqueio reversível)
    // ✅ Reset: locks all items (reversible)
    public void ResetarInventario()
    {
        foreach (var item in itensNaCena)
        {
            if (item != null)
            {
                item.desbloqueado = false;

                // Desativa botão e oculta o GameObject
                // Disable button and hide the GameObject
                item.GetComponent<UnityEngine.UI.Button>().interactable = false;
                item.gameObject.SetActive(false);
            }
        }
    }
}
