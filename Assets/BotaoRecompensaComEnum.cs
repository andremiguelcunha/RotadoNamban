using UnityEngine;
using UnityEngine.UI;

// Esta classe gere um botão de recompensa que desbloqueia um item com base numa enumeração.
// This class manages a reward button that unlocks an item based on an enumeration.

public class BotaoRecompensaComEnum : MonoBehaviour
{
    // Enumeração dos possíveis itens de recompensa.
    // Enumeration of possible reward items.
    public enum ItemRecompensa
    {
        Casaco,
        Espada,
        Documento,
        Arca,
        Bule,
        Saque
    }

    [Header("Escolher item da recompensa")]
    // Seleciona no inspetor qual item será atribuído ao clicar no botão.
    // Select in the inspector which item is awarded on button click.
    public ItemRecompensa itemSelecionado;

    private Button botao;

    void Start()
    {
        // Obtém o componente Button associado a este GameObject.
        // Get the Button component attached to this GameObject.
        botao = GetComponent<Button>();

        // Se o botão existir, associa o método SalvarRecompensa ao evento de clique.
        // If the button exists, attach the SaveReward method to the click event.
        if (botao != null)
            botao.onClick.AddListener(SalvarRecompensa);
        else
            Debug.LogWarning("BotaoRecompensa: Nenhum componente Button encontrado!");
    }

    void SalvarRecompensa()
    {
        // Converte o item selecionado em string para usar como chave.
        // Convert selected item to string to use as a key.
        string nomeItem = itemSelecionado.ToString();

        // Marca o item como desbloqueado nos dados persistentes do jogador.
        // Mark the item as unlocked in PlayerPrefs.
        PlayerPrefs.SetInt("Desbloqueado_" + nomeItem, 1);
        PlayerPrefs.Save();

        // Verifica se todas as partes foram completadas.
        // Check if all parts have been completed.
        VerificarProgresso();

        Debug.Log("Item " + nomeItem + " marcado como desbloqueado.");
    }

    void VerificarProgresso()
    {
        // Parte 1: Verifica se os três primeiros itens foram desbloqueados.
        // Part 1: Check if the first three items have been unlocked.
        if (
            PlayerPrefs.GetInt("Desbloqueado_Casaco", 0) == 1 &&
            PlayerPrefs.GetInt("Desbloqueado_Documento", 0) == 1 &&
            PlayerPrefs.GetInt("Desbloqueado_Arca", 0) == 1)
        {
            PlayerPrefs.SetInt("Entregues_Parte1", 1);
        }

        // Parte 2: Verifica se os três últimos itens foram desbloqueados.
        // Part 2: Check if the last three items have been unlocked.
        if (
            PlayerPrefs.GetInt("Desbloqueado_Saque", 0) == 1 &&
            PlayerPrefs.GetInt("Desbloqueado_Bule", 0) == 1 &&
            PlayerPrefs.GetInt("Desbloqueado_Espada", 0) == 1)
        {
            PlayerPrefs.SetInt("Entregues_Parte2", 1);
        }

        PlayerPrefs.Save();
    }
}
