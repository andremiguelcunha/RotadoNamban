using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

// Controla a lógica de fases com perguntas visuais, popups de feedback e desbloqueio de recompensas.
// Controls visual question phases, feedback popups, and sticker rewards.

public class FaseManager : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public string textoPergunta;                  // Pergunta textual / Question text
        public RawImage[] imagensBotoes;              // Botões com imagens / Buttons with images
        public int indexCorreto;                      // Índice da resposta certa / Correct answer index
        public GameObject[] popupsErrados;            // Popups para respostas erradas / Wrong answer popups
        public GameObject popupCerto;                 // Popup quando a resposta está correta / Correct answer popup
        public GameObject popupDica;                  // Dica da fase atual / Current phase hint
        public Texture[] imagens;                     // Imagens a mostrar nos botões / Images to show on buttons
    }

    [Header("Configuração Geral")]
    public List<Fase> fases;

    [Header("UI")]
    public TextMeshProUGUI textoPerguntaUI;
    public TextMeshProUGUI progressoText;

    [Header("Popups")]
    public GameObject parabensPopup;
    public GameObject bauPopup;

    [Header("Cena Final")]
    public string nomeCenaFinal; // Nome da próxima cena / Final scene name

    private int faseAtual = 0;

    void Start()
    {
        MostrarFase();
    }

    void MostrarFase()
    {
        // Esconde todos os popups para evitar sobreposição
        // Hide all popups to prevent overlap
        foreach (var fase in fases)
        {
            if (fase.popupDica != null) fase.popupDica.SetActive(false);
            foreach (var p in fase.popupsErrados) p.SetActive(false);
            if (fase.popupCerto != null) fase.popupCerto.SetActive(false);
        }

        var f = fases[faseAtual];

        // Atualiza a pergunta e o progresso atual
        // Update question and progress UI
        textoPerguntaUI.text = f.textoPergunta;
        progressoText.text = $"{faseAtual + 1}/{fases.Count}";

        // Preenche as imagens dos botões e define as ações de clique
        // Set button images and assign click actions
        for (int i = 0; i < f.imagensBotoes.Length; i++)
        {
            f.imagensBotoes[i].texture = f.imagens[i];
            var btn = f.imagensBotoes[i].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            int index = i; // Captura local para evitar erro de referência
            btn.onClick.AddListener(() => CliqueBotao(index));
        }
    }

    void CliqueBotao(int index)
    {
        var f = fases[faseAtual];

        // Verifica se a resposta está correta e ativa o popup correspondente
        // Check if the answer is correct and show appropriate popup
        if (index == f.indexCorreto)
        {
            f.popupCerto.SetActive(true);
        }
        else
        {
            f.popupsErrados[index].SetActive(true);
        }
    }

    public void MostrarDica()
    {
        // Fecha todas as dicas antes de mostrar a da fase atual
        // Close all hints before showing the current one
        foreach (var fase in fases)
            if (fase.popupDica != null)
                fase.popupDica.SetActive(false);

        fases[faseAtual].popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        fases[faseAtual].popupDica.SetActive(false);
    }

    public void ContinuarPosParabens()
    {
        // Fecha o popup de parabéns e mostra o baú
        // Close the congratulations popup and open the chest
        parabensPopup.SetActive(false);
        bauPopup.SetActive(true);
    }

    public void FecharPopup(GameObject popup)
    {
        popup.SetActive(false);

        // Se for o popup de acerto, avança ou mostra o parabéns final
        // If it's the correct popup, move on or show final congratulations
        if (popup == fases[faseAtual].popupCerto)
        {
            if (faseAtual == fases.Count - 1)
            {
                parabensPopup.SetActive(true);
            }
            else
            {
                faseAtual++;
                MostrarFase();
            }
        }
    }

    // ✅ NOVO: Ao fechar o baú, desbloqueia autocolantes e carrega nova cena
    // ✅ NEW: When closing the chest, unlock stickers and load final scene
    public void FecharBau()
    {
        // Desbloqueia autocolantes via sistema de sessão
        // Unlock stickers via session data
        StickerSessionData.stickersParaDesbloquear.Add(0);
        StickerSessionData.stickersParaDesbloquear.Add(1);
        StickerSessionData.stickersParaDesbloquear.Add(2);

        PlayerPrefs.SetInt("Sticker_0", 1);
        PlayerPrefs.SetInt("Sticker_1", 1);
        PlayerPrefs.SetInt("Sticker_2", 1);
        PlayerPrefs.Save();

        // Carrega a próxima cena se definida
        // Load next scene if defined
        if (!string.IsNullOrEmpty(nomeCenaFinal))
        {
            SceneManager.LoadScene(nomeCenaFinal);
        }
        else
        {
            Debug.LogWarning("Nome da cena final não foi definido!");
        }
    }
}
