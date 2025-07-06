using UnityEngine;
using UnityEngine.Video;

// Este script carrega e reproduz um vídeo localizado na pasta StreamingAssets quando o objeto é ativado.
// This script loads and plays a video located in the StreamingAssets folder when the object is enabled.

public class PlayARVideo : MonoBehaviour
{
    // Referência ao componente VideoPlayer atribuído no Inspetor.
    // Reference to the VideoPlayer component assigned in the Inspector.
    public VideoPlayer player;

    void OnEnable()
    {
        // Define o caminho completo para o vídeo na pasta StreamingAssets.
        // Define the full path to the video in the StreamingAssets folder.
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Namban.webm");

        // Define a origem do vídeo como URL e configura o caminho.
        // Set the video source as a URL and assign the path.
        player.source = VideoSource.Url;
        player.url = path;

        // Prepara o vídeo (carrega no fundo antes de tocar).
        // Prepare the video (loads in background before playing).
        player.Prepare();

        // Quando a preparação estiver concluída, inicia automaticamente a reprodução.
        // When preparation is complete, start playing automatically.
        player.prepareCompleted += _ => player.Play();
    }
}
