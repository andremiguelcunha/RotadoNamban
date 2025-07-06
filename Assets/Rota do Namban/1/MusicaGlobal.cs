using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicaGlobal : MonoBehaviour
{
    public static MusicaGlobal instancia;

    [Header("Música")]
    public AudioSource musica;

    [Header("Cenas onde a música não deve tocar")]
    public List<string> cenasSemMusica = new List<string>();

    void Awake()
    {
        // Garante que exista apenas uma instância persistente da música
        // Ensures only one persistent music instance exists
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // Destroi duplicatas
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject); // Não destruir ao trocar de cena
        SceneManager.sceneLoaded += OnSceneLoaded; // Escuta carregamento de cena
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Se a cena atual está na lista de exclusão, pausa a música
        // If current scene is excluded, pause music
        if (cenasSemMusica.Contains(cena.name))
        {
            if (musica.isPlaying)
                musica.Pause();
        }
        else
        {
            // Caso contrário, garante que a música está tocando
            // Otherwise, make sure the music is playing
            if (!musica.isPlaying)
                musica.Play();
        }
    }

    private void OnDestroy()
    {
        // Remove o listener ao destruir o objeto
        // Remove the scene load listener when object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
