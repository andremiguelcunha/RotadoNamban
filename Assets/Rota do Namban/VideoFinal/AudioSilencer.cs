using UnityEngine;
using UnityEngine.SceneManagement;

// Este script silencia todos os sons de outras cenas que ainda estão carregadas.
// This script mutes all audio sources from other scenes that are still loaded.

public class AudioSilencer : MonoBehaviour
{
    void Start()
    {
        // Obtém a cena atual onde este script está a correr.
        // Get the currently active scene.
        Scene cenaAtual = SceneManager.GetActiveScene();

        // Encontra todos os AudioSources na hierarquia, incluindo objetos inativos.
        // Find all AudioSources in the hierarchy, including inactive ones.
        AudioSource[] todosAudios = FindObjectsOfType<AudioSource>(true);

        foreach (AudioSource audio in todosAudios)
        {
            // Verifica se o objeto com o AudioSource pertence a outra cena.
            // Check if the object with the AudioSource belongs to another scene.
            if (audio.gameObject.scene.name != cenaAtual.name)
            {
                audio.mute = true; // Silencia o áudio.
                // Mute the audio.
            }
        }
    }
}
