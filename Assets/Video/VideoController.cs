using UnityEngine;
using UnityEngine.Video;

// Esta classe controla a reprodução de vídeo com base no estado do objeto.
// This class controls video playback based on the object's state.

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        // Obtém o componente VideoPlayer de um objeto filho.
        // Get the VideoPlayer component from a child object.
        videoPlayer = GetComponentInChildren<VideoPlayer>();

        if (videoPlayer == null)
        {
            // Mostra um erro se nenhum VideoPlayer for encontrado.
            // Log an error if no VideoPlayer is found.
            Debug.LogError("Nenhum VideoPlayer encontrado no objeto ou seus filhos!");
        }
    }

    private void OnEnable()
    {
        // Reproduz o vídeo automaticamente quando o objeto for ativado.
        // Automatically play the video when the object is enabled.
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    private void OnDisable()
    {
        // Para o vídeo automaticamente quando o objeto for desativado.
        // Automatically stop the video when the object is disabled.
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }

    // Método opcional para iniciar a reprodução manualmente.
    // Optional method to start playback manually.
    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Método opcional para parar o vídeo manualmente.
    // Optional method to stop the video manually.
    public void StopVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}
