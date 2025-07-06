using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuControlador : MonoBehaviour
{
    [Header("Referências de UI / UI References")]
    public GameObject fundoEscuro;           // Fundo escurecido / Darkened background
    public GameObject menuAberto;            // Menu principal / Main menu panel
    public RawImage botaoSomImage;           // Imagem do botão de som / Sound toggle button image
    public Texture somLigadoTexture;         // Ícone de som ligado / Sound on icon
    public Texture somDesligadoTexture;      // Ícone de som desligado / Sound off icon

    [Header("Objetos de Sobre / About Section Objects")]
    public GameObject canvasCenaAtual;       // Canvas principal da cena / Main scene canvas
    public GameObject painelSobre;           // Painel com informações "Sobre" / "About" panel

    [Header("Cena Sobre (não será usada) / About Scene (not used)")]
#if UNITY_EDITOR
    public SceneAsset cenaSobre;             // Referência à cena no editor / Editor scene reference
#endif
    [SerializeField] private string nomeCenaSobre = "Sobre";  // Nome da cena / Scene name

    private bool somAtivo = true;            // Estado atual do som / Sound state (on/off)

    void Start()
    {
        // Verifica se o estado do som foi salvo anteriormente / Check if sound state was saved
        somAtivo = PlayerPrefs.GetInt("somAtivo", 1) == 1;
        AtualizarSomVisual();  // Atualiza ícone / Update icon
        AplicarSom();          // Aplica volume global / Apply global volume
    }

    public void AbrirMenu()
    {
        // Abre o menu e ativa o fundo escuro / Show menu and dark background
        fundoEscuro.SetActive(true);
        menuAberto.SetActive(true);
    }

    public void FecharMenu()
    {
        // Fecha o menu e desativa o fundo escuro / Hide menu and background
        fundoEscuro.SetActive(false);
        menuAberto.SetActive(false);
    }

    public void Sair()
    {
        // Fecha a aplicação / Exit application
        Application.Quit();

#if UNITY_EDITOR
        // Se estiver no editor, para o modo Play / Stop Play Mode in Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleSom()
    {
        // Alterna o estado do som e salva / Toggle sound and save preference
        somAtivo = !somAtivo;
        PlayerPrefs.SetInt("somAtivo", somAtivo ? 1 : 0);
        PlayerPrefs.Save();

        AplicarSom();          // Aplica o novo volume / Apply new volume
        AtualizarSomVisual();  // Atualiza o ícone / Update icon
    }

    public void VoltarDoSobre()
    {
        // Volta do painel "Sobre" para o menu principal / Return from "About" panel
        if (canvasCenaAtual != null)
            canvasCenaAtual.SetActive(true);

        if (painelSobre != null)
            painelSobre.SetActive(false);
    }

    void AplicarSom()
    {
        // Define o volume global / Set global audio volume
        AudioListener.volume = somAtivo ? 1f : 0f;
    }

    void AtualizarSomVisual()
    {
        // Atualiza o ícone do botão com base no estado atual / Update button icon based on sound state
        if (botaoSomImage != null)
        {
            botaoSomImage.texture = somAtivo ? somLigadoTexture : somDesligadoTexture;
        }
    }

    public void IrParaSobre()
    {
        // Alterna do canvas principal para o painel "Sobre" / Switch from main canvas to "About" panel
        if (canvasCenaAtual != null)
            canvasCenaAtual.SetActive(false);

        if (painelSobre != null)
            painelSobre.SetActive(true);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Atualiza o nome da cena com base no asset selecionado / Automatically update scene name based on selected SceneAsset
        if (cenaSobre != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaSobre);
            nomeCenaSobre = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
