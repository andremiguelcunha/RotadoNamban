using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SelecaoPersonagem : MonoBehaviour
{
    [Header("Personagens (RawImages ou GameObjects)")]
    public GameObject[] personagens; // Lista dos personagens disponíveis

    [Header("Botões")]
    public GameObject botaoAnterior;
    public GameObject botaoProximo;

    private int indexAtual = 0; // Índice do personagem selecionado atualmente

    [Header("Cena para seguir")]
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar; // Permite selecionar cena no Editor
#endif
    [SerializeField] private string nomeCena = "Cena4"; // Nome da cena a carregar

    void Start()
    {
        AtualizarVisual(); // Mostra o personagem inicial
    }

    // Vai para o próximo personagem na lista
    public void Proximo()
    {
        indexAtual = (indexAtual + 1) % personagens.Length;
        AtualizarVisual();
    }

    // Vai para o personagem anterior
    public void Anterior()
    {
        indexAtual = (indexAtual - 1 + personagens.Length) % personagens.Length;
        AtualizarVisual();
    }

    // Atualiza o que está visível, deixando apenas o personagem selecionado ativo
    void AtualizarVisual()
    {
        for (int i = 0; i < personagens.Length; i++)
            personagens[i].SetActive(i == indexAtual);
    }

    // Confirma a escolha e carrega a próxima cena
    public void ConfirmarEscolha()
    {
        PlayerPrefs.SetInt("personagemSelecionado", indexAtual); // Guarda a escolha feita
        SceneManager.LoadScene(nomeCena); // Carrega a cena definida
    }

#if UNITY_EDITOR
    // Quando o campo "cenaParaCarregar" muda no inspector, guarda o nome da cena
    void OnValidate()
    {
        if (cenaParaCarregar != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaParaCarregar);
            nomeCena = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
