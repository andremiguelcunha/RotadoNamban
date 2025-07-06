using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

// Script de gestão de fases com botões, imagens, sons e feedback interativo para quizzes educativos.
// Manages interactive quiz phases with buttons, images, audio, and feedback (ideal for educational games).

public class FaseSomManager : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public string textoPergunta;                   // Texto da pergunta / Question text
        public GameObject[] botoes;                    // Botões de escolha / Choice buttons
        public RawImage[] imagensDosBotoes;            // Imagens ligadas aos botões / Images for each button
        public Texture[] imagens;                      // Texturas a serem mostradas nos botões / Textures to display
        public int indexCorreto;                       // Índice da resposta correta / Index of the correct answer
        public GameObject[] popupsErrados;             // Popups de erro para respostas incorretas / Wrong answer popups
        public GameObject popupCerto;                  // Popup de resposta correta / Correct answer popup
        public GameObject popupDica;                   // Popup com dica / Hint popup
        public GameObject popupExtraDepoisDeCerto;     // Popup extra opcional após resposta correta / Optional extra popup after correct answer
        public AudioClip audioPergunta;                // Áudio da pergunta / Question audio clip
    }

    public List<Fase> fases;
    public TextMeshProUGUI textoPerguntaUI;
    public TextMeshProUGUI progressoText;
    public GameObject parabensPopup;
    public string nomeCenaFinal;

    public Button botaoAudioPergunta;
    public AudioSource audioSource;

    private int faseAtual = 0;

    void Start()
    {
        MostrarFase();
    }

    void MostrarFase()
    {
        // Fecha todos os popups de todas as fases
        // Close all popups from all phases
        foreach (var fase in fases)
        {
            if (fase.popupDica != null)
                fase.popupDica.SetActive(false);
            foreach (var p in fase.popupsErrados)
                p.SetActive(false);
            if (fase.popupCerto != null)
                fase.popupCerto.SetActive(false);
        }

        var f = fases[faseAtual];

        // Atualiza o UI da pergunta e progresso
        // Update question and progress UI
        textoPerguntaUI.text = f.textoPergunta;
        progressoText.text = $"{faseAtual + 1}/3";

        // Configura o botão de áudio da pergunta
        // Set up question audio playback
        botaoAudioPergunta.onClick.RemoveAllListeners();
        botaoAudioPergunta.onClick.AddListener(() => TocarAudioPergunta(f.audioPergunta));

        // Liga os botões de resposta e define as imagens
        // Assign button clicks and set button images
        for (int i = 0; i < f.botoes.Length; i++)
        {
            var btnObj = f.botoes[i];
            var btn = btnObj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            int index = i;
            btn.onClick.AddListener(() => CliqueBotao(index));

            if (f.imagensDosBotoes.Length > i && f.imagens.Length > i && f.imagensDosBotoes[i] != null)
            {
                f.imagensDosBotoes[i].texture = f.imagens[i];
            }
        }

        // Grava a fase atual (útil para continuar depois)
        // Save current phase (useful for resuming later)
        PlayerPrefs.SetInt("FaseAtual", faseAtual + 1);
        PlayerPrefs.Save();
    }

    public void AvancarDoPopupCerto()
    {
        var f = fases[faseAtual];
        f.popupCerto.SetActive(false);

        // Se existir popup extra, mostra-o, senão avança
        // If there's an extra popup, show it; otherwise advance
        if (f.popupExtraDepoisDeCerto != null)
        {
            f.popupExtraDepoisDeCerto.SetActive(true);
        }
        else
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

    public void FecharPopupExtraEAvancar()
    {
        var f = fases[faseAtual];

        if (f.popupExtraDepoisDeCerto != null)
            f.popupExtraDepoisDeCerto.SetActive(false);

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

    void TocarAudioPergunta(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void CliqueBotao(int index)
    {
        var f = fases[faseAtual];

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
        // Fecha todas as outras dicas antes de abrir
        // Close all other hints before opening current
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
        parabensPopup.SetActive(false);

        // Desbloqueia autocolantes após conclusão
        // Unlock stickers upon completion
        StickerSessionData.stickersParaDesbloquear.Add(0);
        StickerSessionData.stickersParaDesbloquear.Add(1);
        StickerSessionData.stickersParaDesbloquear.Add(2);
        StickerSessionData.stickersParaDesbloquear.Add(3);

        PlayerPrefs.SetInt("Sticker_0", 1);
        PlayerPrefs.SetInt("Sticker_1", 1);
        PlayerPrefs.SetInt("Sticker_2", 1);
        PlayerPrefs.SetInt("Sticker_3", 1);
        PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(nomeCenaFinal))
        {
            SceneManager.LoadScene(nomeCenaFinal);
        }
        else
        {
            Debug.LogWarning("Nome da cena final não foi definido!");
        }
    }

    public void FecharPopup(GameObject popup)
    {
        popup.SetActive(false);

        // Desbloqueio duplicado aqui — pode ser movido para um único local se necessário
        // Duplicate sticker unlock logic – consider centralizing this
        StickerSessionData.stickersParaDesbloquear.Add(0);
        StickerSessionData.stickersParaDesbloquear.Add(1);
        StickerSessionData.stickersParaDesbloquear.Add(2);
        StickerSessionData.stickersParaDesbloquear.Add(3);

        PlayerPrefs.SetInt("Sticker_0", 1);
        PlayerPrefs.SetInt("Sticker_1", 1);
        PlayerPrefs.SetInt("Sticker_2", 1);
        PlayerPrefs.SetInt("Sticker_3", 1);
        PlayerPrefs.Save();

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
}
