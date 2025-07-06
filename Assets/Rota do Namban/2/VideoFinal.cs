using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VideoFinal : MonoBehaviour
{
    [Header("Referências / References")]
    public VideoPlayer videoPlayer;          // Componente de vídeo / Video component
    public GameObject botaoProximaCena;      // Botão para ir à próxima cena / Next scene button

    [Header("Cena para Carregar / Scene to Load")]
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar;      // Usado apenas no Editor / Used only in Editor
#endif
    [SerializeField] private string nomeCena = "Cena3"; // Nome da cena a ser carregada / Scene to load at runtime

    private bool jaAssistiuUmaVez = false;   // Verifica se o vídeo já foi assistido uma vez / Checks if video has been watched once

    private void Start()
    {
        botaoProximaCena.SetActive(false);   // Esconde o botão no início / Hide button at start
        videoPlayer.loopPointReached += OnVideoFinished; // Evento disparado ao fim do vídeo / Triggered when video ends
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (!jaAssistiuUmaVez)
        {
            botaoProximaCena.SetActive(true); // Mostra o botão após primeira reprodução / Show button after first play
            jaAssistiuUmaVez = true;
        }

        // Após 5 segundos, o vídeo reinicia automaticamente / Replay the video after 5 seconds
        StartCoroutine(ReplayDepoisDeSegundos(5f));
    }

    IEnumerator ReplayDepoisDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        videoPlayer.Play(); // Dá replay no vídeo / Replay video
        // O botão continua visível após o replay / Button remains visible
    }

    public void IrParaProximaCena()
    {
        // Carrega a cena definida se o nome não estiver vazio / Load next scene if name is valid
        if (!string.IsNullOrEmpty(nomeCena))
        {
            SceneManager.LoadScene(nomeCena);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Atualiza o nome da cena automaticamente no Editor / Automatically update scene name in editor
        if (cenaParaCarregar != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaParaCarregar);
            nomeCena = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
