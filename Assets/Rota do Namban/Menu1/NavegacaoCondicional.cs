using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Este script permite a navegação condicional entre cenas com base na cena anterior guardada em PlayerPrefs.
// This script allows conditional scene navigation based on the previously visited scene stored in PlayerPrefs.

public class NavegacaoCondicional : MonoBehaviour
{
    [System.Serializable]
    public class Rota
    {
#if UNITY_EDITOR
        // Apenas visível no editor: seleciona cenas através do inspetor.
        // Only visible in the editor: select scenes through the inspector.
        public SceneAsset cenaOrigemAsset;
        public SceneAsset cenaDestinoAsset;
#endif

        // Guardam os nomes das cenas como strings (usadas em runtime).
        // Store the scene names as strings (used at runtime).
        [HideInInspector] public string cenaOrigem;
        [HideInInspector] public string cenaDestino;
    }

    // Lista de rotas possíveis, configurável no Inspetor.
    // List of possible navigation routes, configurable in the Inspector.
    public List<Rota> rotas;

    // Botão que aciona a navegação.
    // Button that triggers the navigation.
    public Button botao;

    private void Start()
    {
        // Liga o evento do botão ao método de navegação.
        // Attach the button's click event to the navigation method.
        if (botao != null)
            botao.onClick.AddListener(Navegar);
    }

    void Navegar()
    {
        // Lê a cena anterior guardada nos dados persistentes.
        // Read the previously saved scene from PlayerPrefs.
        string cenaAnterior = PlayerPrefs.GetString("CenaAnterior", "");

        // Procura uma rota correspondente à cena anterior.
        // Look for a route that matches the previous scene.
        foreach (var rota in rotas)
        {
            if (rota.cenaOrigem == cenaAnterior)
            {
                SceneManager.LoadScene(rota.cenaDestino);
                return;
            }
        }

        // Se nenhuma rota for encontrada, mostra um aviso.
        // If no route is found, log a warning.
        Debug.LogWarning("Nenhuma rota encontrada para: " + cenaAnterior);
    }

#if UNITY_EDITOR
    // Este método corre no editor sempre que algo muda no Inspetor.
    // This method runs in the editor whenever something changes in the Inspector.
    private void OnValidate()
    {
        foreach (var rota in rotas)
        {
            // Atualiza os nomes das cenas com base nos SceneAssets.
            // Update the scene names based on the SceneAssets.
            rota.cenaOrigem = rota.cenaOrigemAsset != null ? rota.cenaOrigemAsset.name : "";
            rota.cenaDestino = rota.cenaDestinoAsset != null ? rota.cenaDestinoAsset.name : "";
        }

        // Garante que o Unity reconhece mudanças no script (salva no Editor).
        // Ensure Unity registers the changes (marks object as dirty in Editor).
        EditorUtility.SetDirty(this);
    }
#endif
}
