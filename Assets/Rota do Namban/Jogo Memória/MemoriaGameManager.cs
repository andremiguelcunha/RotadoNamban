using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Gere o fluxo de jogo de um Jogo da Memória com pares, popups explicativos e recompensas visuais.
// Manages the game flow of a Memory Game with matching pairs, explanatory popups and visual rewards.

public class MemoriaGameManager : MonoBehaviour
{
    [System.Serializable]
    public class ParInfo
    {
        public string nome;             // Nome do par (identificador)
        public string descricao;        // Texto explicativo quando o par é encontrado
        public GameObject imagemGO;     // GameObject com imagem associada ao par (mostrado no popup)
    }

    public List<ParInfo> paresInfo;     // Lista com a info de todos os pares possíveis
    public string sceneToLoad;          // Nome da próxima cena a carregar após o jogo

    [Header("UI Popups")]
    public GameObject popupExplicacao;
    public TextMeshProUGUI tituloExplicacao;
    public TextMeshProUGUI descricaoExplicacao;
    public GameObject popupParabens;
    public GameObject popupBau;
    public GameObject popupDica;

    private int paresEncontrados = 0;   // Contador de pares encontrados
    private int totalPares;             // Total de pares possíveis (definido no Start)

    private CartaMemoria primeiraCarta; // Primeira carta clicada
    private CartaMemoria segundaCarta;  // Segunda carta clicada
    private bool bloqueado = false;     // Bloqueia interação enquanto compara cartas

    void Start()
    {
        totalPares = paresInfo.Count;
    }

    public void MostrarParEncontrado(string nome)
    {
        // Procura o par correspondente pelo nome
        ParInfo par = paresInfo.Find(p => p.nome == nome);
        if (par != null)
        {
            // Esconde todas as imagens dos pares
            foreach (var item in paresInfo)
            {
                if (item.imagemGO != null)
                    item.imagemGO.SetActive(false);
            }

            // Mostra a imagem correspondente ao par atual
            if (par.imagemGO != null)
                par.imagemGO.SetActive(true);

            // Atualiza título e descrição
            tituloExplicacao.text = par.nome;
            descricaoExplicacao.text = par.descricao;

            // Ajuste específico se o par for "Cadeira"
            if (par.nome == "Cadeira")
            {
                descricaoExplicacao.fontSize = 59.64f;
                descricaoExplicacao.enableAutoSizing = false;

                RectTransform rt = descricaoExplicacao.GetComponent<RectTransform>();
                if (rt != null)
                {
                    Vector2 pos = rt.anchoredPosition;
                    pos.y = -194.6f;
                    rt.anchoredPosition = pos;
                }
            }

            // Mostra o popup explicativo
            popupExplicacao.SetActive(true);
        }
    }

    public void ConfirmarPar()
    {
        popupExplicacao.SetActive(false);

        paresEncontrados++;

        // Verifica se terminou o jogo (todos os pares encontrados)
        if (paresEncontrados == totalPares)
        {
            popupParabens.SetActive(true);
        }

        // Limpa referências para próximas seleções
        primeiraCarta = null;
        segundaCarta = null;
        bloqueado = false;
    }

    public void FecharParabens()
    {
        // Fecha o popup de parabéns e abre o baú
        popupParabens.SetActive(false);
        popupBau.SetActive(true);
    }

    public void FecharBau()
    {
        // Marca o autocolante como desbloqueado
        StickerSessionData.stickersParaDesbloquear.Add(0);
        PlayerPrefs.SetInt("Sticker_0", 1);
        PlayerPrefs.Save();

        // Carrega a próxima cena
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena definida para carregar.");
        }
    }

    public void MostrarDica()
    {
        popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        popupDica.SetActive(false);
    }

    public void SelecionarCarta(CartaMemoria carta)
    {
        if (primeiraCarta == null)
        {
            primeiraCarta = carta;
        }
        else if (segundaCarta == null && carta != primeiraCarta)
        {
            segundaCarta = carta;
            bloqueado = true; // Impede que selecione mais cartas durante a comparação
            StartCoroutine(CompararCartas());
        }
    }

    public bool PodeSelecionarCarta()
    {
        return !bloqueado && (primeiraCarta == null || segundaCarta == null);
    }

    private IEnumerator CompararCartas()
    {
        // Espera 1 segundo antes de verificar
        yield return new WaitForSeconds(1f);

        if (primeiraCarta.nomeCarta == segundaCarta.nomeCarta)
        {
            // Se são iguais, bloqueia ambas e mostra info
            primeiraCarta.Bloquear();
            segundaCarta.Bloquear();
            MostrarParEncontrado(primeiraCarta.nomeCarta);
        }
        else
        {
            // Se são diferentes, vira de novo para trás
            primeiraCarta.VirarParaTrás();
            segundaCarta.VirarParaTrás();

            // Liberta para nova tentativa
            primeiraCarta = null;
            segundaCarta = null;
            bloqueado = false;
        }
    }
}
