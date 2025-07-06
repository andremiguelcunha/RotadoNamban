using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TirarSom : MonoBehaviour
{
    [Header("Referências")]
    public RawImage iconImage;
    public Texture somOnTexture;
    public Texture somOffTexture;
    public VideoPlayer videoPlayer;

    private bool isMuted = false;

    public void ToggleMute()
    {
        isMuted = !isMuted;

        // Muta ou desmuta o áudio do VideoPlayer
        videoPlayer.SetDirectAudioMute(0, isMuted);

        // Altera o ícone
        iconImage.texture = isMuted ? somOffTexture : somOnTexture;
    }
}
