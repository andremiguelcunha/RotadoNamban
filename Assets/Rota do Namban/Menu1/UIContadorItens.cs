using UnityEngine;
using TMPro;

// Este script atualiza os contadores de itens desbloqueados no inventário e no diário (autocolantes).
// This script updates the counters for unlocked inventory items and diary stickers.

public class UIContadorItens : MonoBehaviour
{
    [Header("Referências de Texto")]
    // Referência ao campo de texto para o inventário.
    // Reference to the inventory text field.
    public TextMeshProUGUI textoInventario;

    // Referência ao campo de texto para o diário.
    // Reference to the diary text field.
    public TextMeshProUGUI textoDiario;

    [Header("Configuração")]
    // Número total de itens no inventário.
    // Total number of inventory items.
    public int totalInventario = 6;

    // Número total de autocolantes disponíveis.
    // Total number of stickers available.
    public int totalAutocolantes = 5;

    // Lista das chaves usadas para verificar se cada item foi desbloqueado (inventário).
    // List of PlayerPrefs keys used to check if each inventory item is unlocked.
    private string[] nomesDosItens = new string[]
    {
        "Desbloqueado_Casaco",
        "Desbloqueado_Documento",
        "Desbloqueado_Arca",
        "Desbloqueado_Saque",
        "Desbloqueado_Bule",
        "Desbloqueado_Espada"
    };

    void Start()
    {
        // Atualiza os contadores no início da cena.
        // Update the counters when the scene starts.
        AtualizarContadores();
    }

    public void AtualizarContadores()
    {
        // INVENTÁRIO — Conta quantos itens foram desbloqueados com base nas chaves.
        // INVENTORY — Count how many items have been unlocked using the keys.
        int desbloqueadosInventario = 0;
        foreach (string chave in nomesDosItens)
        {
            if (PlayerPrefs.GetInt(chave, 0) == 1)
                desbloqueadosInventario++;
        }
        textoInventario.text = $"{desbloqueadosInventario}/{totalInventario}";

        // DIÁRIO — Conta quantos autocolantes foram desbloqueados com base nas chaves "Sticker_0", "Sticker_1", etc.
        // DIARY — Count how many stickers have been unlocked using keys like "Sticker_0", "Sticker_1", etc.
        int desbloqueadosDiario = 0;
        for (int i = 0; i < totalAutocolantes; i++)
        {
            if (PlayerPrefs.GetInt("Sticker_" + i, 0) == 1)
                desbloqueadosDiario++;
        }
        textoDiario.text = $"{desbloqueadosDiario}/{totalAutocolantes}";
    }
}
