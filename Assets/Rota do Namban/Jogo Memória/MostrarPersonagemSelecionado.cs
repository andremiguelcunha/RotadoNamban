using UnityEngine;
using UnityEngine.UI;

// Este script aplica a textura do personagem selecionado a vários RawImages.
// This script sets the selected character texture on multiple RawImages.

public class MostrarPersonagemSelecionado : MonoBehaviour
{
    [Header("Destinos onde a imagem será mostrada")]
    // RawImages onde será aplicada a textura do personagem escolhido.
    // RawImages where the selected character's texture will be displayed.
    public RawImage[] rawImagesDestino;

    [Header("Texturas disponíveis")]
    // Lista de texturas de personagens disponíveis (deve coincidir com a ordem do seletor).
    // List of available character textures (should match selection order).
    public Texture[] texturasDosPersonagens;

    void Start()
    {
        // Lê o índice do personagem salvo em PlayerPrefs (valor padrão = 0).
        // Read the selected character index from PlayerPrefs (default = 0).
        int indice = PlayerPrefs.GetInt("personagemSelecionado", 0);

        // Garante que o índice está dentro dos limites da lista.
        // Ensure the index is within the valid range.
        if (indice >= 0 && indice < texturasDosPersonagens.Length)
        {
            Texture personagem = texturasDosPersonagens[indice];

            // Aplica a textura em cada destino válido.
            // Apply the selected texture to all target RawImages.
            foreach (RawImage rawImage in rawImagesDestino)
            {
                if (rawImage != null)
                {
                    rawImage.texture = personagem;
                }
            }
        }
    }
}
