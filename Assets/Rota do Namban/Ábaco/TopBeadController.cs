using UnityEngine;
using UnityEngine.EventSystems;

// Controla a conta superior do ábaco (que só tem duas posições: cima e baixo)
// Controls the top bead of the abacus (which toggles between two positions)

public class TopBeadController : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;

    [Header("Posições")]
    public float posCima = 209.3f;     // Posição vertical quando está para cima
    public float posBaixo = 133.02f;   // Posição vertical quando está para baixo

    private bool estaBaixo = false;    // Estado atual da conta (inicia em cima)

    [Header("Som")]
    [SerializeField] private AudioSource audioSource; // Som tocado ao clicar

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Tenta obter automaticamente o componente AudioSource se não foi atribuído no Inspector
        // Automatically grabs the AudioSource if not assigned in the Inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Começa sempre na posição de cima
        // Start in the top position
        MoveTo(posCima);
    }

    // Chamado quando o utilizador clica na conta
    // Called when the user clicks the bead
    public void OnPointerClick(PointerEventData eventData)
    {
        estaBaixo = !estaBaixo; // Alterna o estado (toggle)
        float destino = estaBaixo ? posBaixo : posCima; // Decide a nova posição
        MoveTo(destino); // Move a conta

        if (audioSource != null)
        {
            audioSource.Play(); // Toca o som (se existir)
        }
    }

    // Retorna se a conta está em baixo (usado para validação)
    // Returns whether the bead is in the down position (used in verification)
    public bool EstaBaixo()
    {
        return estaBaixo;
    }

    // Move a conta para uma posição Y específica
    // Moves the bead to a specific Y position
    private void MoveTo(float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
    }
}
