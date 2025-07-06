using UnityEngine;

// Script associado a uma carta individual num jogo da memória (Memory Game).
// Script attached to a single card in a memory game.

public class CartaMemoria : MonoBehaviour
{
    public string nomeCarta;          // Nome identificador da carta (usado para comparação).
                                     // Identifier name of the card (used for matching).

    public GameObject frente;        // Objeto visual da frente da carta (imagem visível ao virar).
                                     // Front visual of the card (shown when flipped).

    public GameObject tras;          // Objeto visual da parte de trás da carta.
                                     // Back visual of the card.

    public bool bloqueada = false;   // Define se a carta está bloqueada (já foi combinada).
                                     // Whether the card is blocked (already matched).

    // Referência estática ao GameManager (evita chamadas repetidas ao Find).
    // Static reference to the GameManager (optimizes by finding only once).
    private static MemoriaGameManager gameManager;

    private void Awake()
    {
        // Inicializa a referência ao GameManager se ainda não estiver atribuída.
        // Initialize GameManager reference if not already set.
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<MemoriaGameManager>();
        }
    }

    public void OnClick()
    {
        // Ignora o clique se a carta já estiver virada ou bloqueada.
        // Ignore click if the card is already flipped or blocked.
        if (bloqueada || frente.activeSelf)
        {
            Debug.Log("Carta ignorada: bloqueada ou já está virada");
            return;
        }

        // Verifica se o GameManager existe.
        // Check if GameManager is present.
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager não encontrado!");
            return;
        }

        // Verifica com o GameManager se ainda pode selecionar cartas.
        // Ask GameManager if card selection is currently allowed.
        if (!gameManager.PodeSelecionarCarta())
        {
            Debug.Log("Não pode selecionar mais cartas agora.");
            return;
        }

        // Mostra feedback do clique.
        // Log the card click.
        Debug.Log("Carta clicada: " + nomeCarta);

        // Vira a carta (mostra a frente).
        // Flip the card (show front).
        frente.SetActive(true);
        tras.SetActive(false);

        // Informa o GameManager que esta carta foi selecionada.
        // Notify GameManager that this card was selected.
        gameManager.SelecionarCarta(this);
    }

    // Vira a carta para trás (volta a esconder).
    // Flip the card back (hide front).
    public void VirarParaTrás()
    {
        frente.SetActive(false);
        tras.SetActive(true);
    }

    // Marca esta carta como bloqueada (já foi combinada corretamente).
    // Mark this card as blocked (successfully matched).
    public void Bloquear()
    {
        bloqueada = true;
    }
}
