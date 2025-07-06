using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Script responsável por verificar se as contas do ábaco estão nas posições corretas.
// Script responsible for verifying if abacus beads are in the correct positions.

public class AbacoVerificador : MonoBehaviour
{
    [Header("Contas Superiores / Top Beads")]
    public TopBeadController contaTopoDireita;

    [Header("Contas Inferiores / Bottom Beads")]
    public BeadController contaBaixoEsquerda;
    public BeadController contaBaixoDireita;

    [Header("Texturas Amarelas (Highlight) / Yellow Highlight Textures")]
    public RawImage imagemTopoDireita;
    public RawImage imagemBaixoEsquerda;
    public RawImage imagemBaixoDireita;
    public Texture texturaAmarela;

    [Header("Popups")]
    public GameObject popupCanvas; // Popup de sucesso (ex: "Parabéns")
    public GameObject popupErrado; // Popup de erro (ex: "Tenta novamente")

    // Método principal que faz a verificação das posições
    // Main method that checks the bead positions
    public void VerificarAbaco()
    {
        Debug.Log("VerificarAbaco chamado!");

        // Posições corretas esperadas
        float posTopoDireita = 133.02f;
        float posBaixo = -39.7f;
        float margem = 0.5f; // margem de tolerância

        // Verifica se a conta superior direita está na posição correta
        bool topoOk = contaTopoDireita != null &&
                      Mathf.Abs(contaTopoDireita.GetComponent<RectTransform>().anchoredPosition.y - posTopoDireita) < margem;

        // Verifica se a conta inferior esquerda está correta
        bool baixoEsquerdaOk = contaBaixoEsquerda != null &&
                      Mathf.Abs(contaBaixoEsquerda.GetY() - posBaixo) < margem;

        // Verifica se a conta inferior direita está correta
        bool baixoDireitaOk = contaBaixoDireita != null &&
                      Mathf.Abs(contaBaixoDireita.GetY() - posBaixo) < margem;

        Debug.Log("Topo OK: " + topoOk);
        Debug.Log("Esquerda OK: " + baixoEsquerdaOk);
        Debug.Log("Direita OK: " + baixoDireitaOk);

        // Se todas as posições estiverem corretas, mostra o popup de parabéns
        if (topoOk && baixoEsquerdaOk && baixoDireitaOk)
        {
            StartCoroutine(MostrarParabensComDelay());
        }
        else
        {
            if (popupErrado != null)
                popupErrado.SetActive(true);
        }
    }

    // Mostra o popup de parabéns com um atraso e destaca as contas corretas
    // Shows success popup after a short delay and highlights correct beads
    private IEnumerator MostrarParabensComDelay()
    {
        // Guarda desbloqueio de autocolantes
        StickerSessionData.stickersParaDesbloquear.Add(0);
        StickerSessionData.stickersParaDesbloquear.Add(1);

        PlayerPrefs.SetInt("Sticker_0", 1);
        PlayerPrefs.SetInt("Sticker_1", 1);
        PlayerPrefs.Save();

        // Aplica textura amarela para indicar acerto
        if (imagemTopoDireita != null)
            imagemTopoDireita.texture = texturaAmarela;

        if (imagemBaixoEsquerda != null)
            imagemBaixoEsquerda.texture = texturaAmarela;

        if (imagemBaixoDireita != null)
            imagemBaixoDireita.texture = texturaAmarela;

        // Espera antes de mostrar popup
        yield return new WaitForSeconds(1.5f);

        if (popupCanvas != null)
            popupCanvas.SetActive(true);
    }
}
