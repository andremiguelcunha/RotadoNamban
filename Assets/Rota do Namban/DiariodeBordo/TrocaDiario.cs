using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Script usado para trocar para uma nova cena ao clicar num botão, com suporte a referência via SceneAsset no Editor.
// Script used to load another scene (e.g. the diary scene) when clicking a button, supporting SceneAsset reference in the Editor.

public class TrocaDiario : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Arraste a cena aqui")]
    public SceneAsset cenaDestinoAsset; // Permite escolher a cena no Editor sem escrever o nome manualmente
                                        // Allows selecting the scene directly in the Inspector (Editor only)
#endif

    [HideInInspector]
    public string nomeCenaDestino; // Nome real da cena a ser carregada
                                   // Actual scene name to be loaded at runtime

    private void Awake()
    {
#if UNITY_EDITOR
        // Copia o nome da cena automaticamente ao iniciar (no editor)
        // Automatically stores the scene name during Awake (Editor only)
        if (cenaDestinoAsset != null)
        {
            nomeCenaDestino = cenaDestinoAsset.name;
        }
#endif
    }

    // Chamada para trocar de cena (ligar num botão, por exemplo)
    // Call this method to load the target scene (e.g., from a button)
    public void TrocarCena()
    {
        if (!string.IsNullOrEmpty(nomeCenaDestino))
        {
            SceneManager.LoadScene(nomeCenaDestino); // Carrega a cena
        }
        else
        {
            Debug.LogWarning("Nome da cena de destino não definido.");
        }
    }
}
