using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Este script controla uma conta (bead) num ábaco. Permite movê-la entre posições predefinidas.
// This script controls a bead on an abacus, allowing it to move between predefined steps.

public class BeadController : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;

    [Header("Ordem e Grupo")]
    public int index;                          // Índice da conta no grupo
    public BeadController[] group;            // Outras contas do mesmo grupo (coluna)

    [Header("Degraus de Movimento (de baixo para cima)")]
    public List<float> positionsY = new();     // Lista de posições Y possíveis
    public int currentStep = 0;                // Índice da posição atual na lista

    [Header("Som")]
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Obtém automaticamente o AudioSource se não estiver atribuído manualmente
        // Automatically gets the AudioSource if not set in the inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Move a conta para a posição inicial
        // Moves the bead to the initial step position
        if (positionsY.Count > 0)
        {
            MoveTo(positionsY[currentStep]);
        }
    }

    // Chamado quando o utilizador clica na conta
    // Called when the user clicks on the bead
    public void OnPointerClick(PointerEventData eventData)
    {
        if (positionsY == null || positionsY.Count == 0) return;

        // Tenta mover uma posição acima (para cima)
        // Try moving one step up
        if (currentStep < positionsY.Count - 1)
        {
            int upwardStep = currentStep + 1;
            float upwardY = positionsY[upwardStep];

            // Só move se a posição acima não estiver ocupada por outra conta
            // Only move if the position above is free
            if (!PosicaoOcupadaPorOutro(upwardY))
            {
                currentStep = upwardStep;
                MoveTo(upwardY);
                return;
            }
            // Se não puder subir, tenta descer
            else if (currentStep > 0)
            {
                int downwardStep = currentStep - 1;
                float downwardY = positionsY[downwardStep];

                if (!PosicaoOcupadaPorOutro(downwardY))
                {
                    currentStep = downwardStep;
                    MoveTo(downwardY);
                    return;
                }
            }
        }
        // Já está no topo, então tenta descer
        else if (currentStep > 0)
        {
            int downwardStep = currentStep - 1;
            float downwardY = positionsY[downwardStep];

            if (!PosicaoOcupadaPorOutro(downwardY))
            {
                currentStep = downwardStep;
                MoveTo(downwardY);
                return;
            }
        }
    }

    // Move a conta para a posição Y especificada
    // Moves the bead to a specified Y position
    public void MoveTo(float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);

        // Toca som ao mover, se disponível
        // Plays sound when moved, if available
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Retorna a posição Y atual da conta
    // Returns the current Y position of the bead
    public float GetY()
    {
        return rectTransform.anchoredPosition.y;
    }

    // Verifica se outra conta já está na posição desejada
    // Checks if another bead is already at the desired Y position
    private bool PosicaoOcupadaPorOutro(float yDestino)
    {
        foreach (var other in group)
        {
            if (other != this && Mathf.Abs(other.GetY() - yDestino) < 0.1f)
            {
                return true; // posição ocupada
            }
        }
        return false;
    }
}
