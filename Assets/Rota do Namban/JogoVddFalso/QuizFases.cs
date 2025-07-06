using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Controla a lógica de um quiz dividido em fases com afirmações, imagens e feedbacks visuais.
// Controls the logic of a multi-phase quiz with statements, images, and visual feedback.

public class QuizFases : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        // Imagem associada à fase.
        // Image associated with this quiz phase.
        public Texture imagem;

        // Afirmativa apresentada ao jogador.
        // Statement shown to the player.
        public string afirmacao;

        // Define se a afirmação é verdadeira ou falsa.
        // Indicates whether the statement is true or false.
        public bool respostaCorreta;

        // Popups de feedback.
        // Feedback popups.
        public GameObject popupAcerto;
        public GameObject popupErro;
        public GameObject popupDica;
    }

    [Header("Popup Final")]
    public GameObject popupParabens;

    [Header("Popup Recompensa")]
    public GameObject popupEspada;

    [Header("Lista de Fases")]
    public List<Fase> fases;

    [Header("Referências da UI")]
    public RawImage imagemPrincipal;
    public TextMeshProUGUI textoAfirmacao;
    public TextMeshProUGUI textoProgresso;
    public Button botaoVerdadeiro;
    public Button botaoFalso;
    public GameObject[] botoesRawImages;

    [Header("Botão Dica")]
    public Button botaoDica;

    private int faseAtual = 0;

    void Start()
    {
        // Mostra a primeira fase ao iniciar.
        // Show the first phase at start.
        MostrarFase();

        // Liga os botões aos seus métodos.
        // Connect buttons to their methods.
        botaoVerdadeiro.onClick.AddListener(() => VerificarResposta(true));
        botaoFalso.onClick.AddListener(() => VerificarResposta(false));
        botaoDica.onClick.AddListener(MostrarDica);
    }

    void MostrarFase()
    {
        if (faseAtual >= fases.Count) return;

        Fase fase = fases[faseAtual];

        // Atualiza imagem e afirmação.
        // Update image and statement.
        imagemPrincipal.texture = fase.imagem;
        textoAfirmacao.text = fase.afirmacao;
        textoProgresso.text = $"{faseAtual + 1}/{fases.Count}";

        // Garante que todos os popups estão fechados.
        // Ensure all popups are closed.
        foreach (var f in fases)
        {
            if (f.popupErro != null) f.popupErro.SetActive(false);
            if (f.popupAcerto != null) f.popupAcerto.SetActive(false);
            if (f.popupDica != null) f.popupDica.SetActive(false);
        }
    }

    void VerificarResposta(bool respostaDoJogador)
    {
        Fase fase = fases[faseAtual];

        // Mostra popup de acerto ou erro.
        // Show correct or incorrect popup.
        if (respostaDoJogador == fase.respostaCorreta)
        {
            if (fase.popupAcerto != null)
                fase.popupAcerto.SetActive(true);
        }
        else
        {
            if (fase.popupErro != null)
                fase.popupErro.SetActive(true);
        }
    }

    public void FecharPopupEAvancar()
    {
        Fase fase = fases[faseAtual];

        // Fecha todos os popups da fase atual.
        // Close all popups of the current phase.
        if (fase.popupAcerto != null) fase.popupAcerto.SetActive(false);
        if (fase.popupErro != null) fase.popupErro.SetActive(false);
        if (fase.popupDica != null) fase.popupDica.SetActive(false);

        // Se for a última fase, mostra o popup final.
        // If it's the last phase, show the final popup.
        if (faseAtual == fases.Count - 1)
        {
            if (popupParabens != null)
            {
                popupParabens.SetActive(true);
            }
            else
            {
                Debug.LogWarning("PopupParabens não atribuído no Inspector!");
            }
            return;
        }

        // Avança para a próxima fase.
        // Move to the next phase.
        faseAtual++;
        MostrarFase();
    }

    public void FecharPopupParabensEEspada()
    {
        // Fecha o popup de parabéns.
        // Close the congratulatory popup.
        if (popupParabens != null)
            popupParabens.SetActive(false);

        // Mostra o popup da recompensa e salva o progresso.
        // Show the reward popup and save progress.
        if (popupEspada != null)
        {
            popupEspada.SetActive(true);

            PlayerPrefs.SetInt("Desbloqueado_Espada", 1);
            PlayerPrefs.Save();

            // Adiciona os autocolantes desbloqueados à sessão.
            // Add unlocked stickers to session.
            StickerSessionData.stickersParaDesbloquear.Add(0);
            StickerSessionData.stickersParaDesbloquear.Add(1);
            StickerSessionData.stickersParaDesbloquear.Add(2);
            StickerSessionData.stickersParaDesbloquear.Add(3);
            StickerSessionData.stickersParaDesbloquear.Add(4);

            // Marca os autocolantes como desbloqueados.
            // Mark stickers as unlocked.
            PlayerPrefs.SetInt("Sticker_0", 1);
            PlayerPrefs.SetInt("Sticker_1", 1);
            PlayerPrefs.SetInt("Sticker_2", 1);
            PlayerPrefs.SetInt("Sticker_3", 1);
            PlayerPrefs.SetInt("Sticker_4", 1);
            PlayerPrefs.Save();
        }
    }

    public void FecharPopupEspada()
    {
        // Fecha o popup da espada.
        // Close the sword popup.
        if (popupEspada != null)
            popupEspada.SetActive(false);
    }

    public void FecharPopupErro()
    {
        // Fecha o popup de erro da fase atual.
        // Close the error popup of the current phase.
        Fase fase = fases[faseAtual];

        if (fase.popupErro != null)
            fase.popupErro.SetActive(false);
    }

    void MostrarDica()
    {
        // Mostra a dica da fase atual, se existir.
        // Show the hint of the current phase, if it exists.
        Fase fase = fases[faseAtual];

        if (fase.popupDica != null)
            fase.popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        // Fecha o popup de dica da fase atual.
        // Close the hint popup of the current phase.
        Fase fase = fases[faseAtual];

        if (fase.popupDica != null)
            fase.popupDica.SetActive(false);
    }
}
