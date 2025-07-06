using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

// Este script controla o comportamento de um item no inventário visual:
// desbloqueio, exibição, interação especial (como o casaco com chave escondida), popups, etc.
// This script handles the behavior of a visual inventory item: unlocking, display, special interactions (e.g., coat with hidden key), popups, etc.

public class ItemVisualizador : MonoBehaviour
{
    [Header("Configuração")]
    public Texture imagemGrande;         // Textura principal a exibir quando o item for clicado
    public RawImage areaExibicao;        // Área onde a imagem será exibida
    public bool desbloqueado = false;    // Define se o item está desbloqueado

    [Header("Objetos a ocultar após a chave")]
    public GameObject[] objetosParaOcultar;

    [Header("Imagem final após entrega")]
    public Texture imagemFinal;

    [Header("Popup ao exibir este item")]
    public GameObject popupAoMostrar;

    [Header("Interação Especial do Casaco")]
    public bool itemEspecialCasaco = false;
    public GameObject chave;
    public InventarioManager inventarioManager;

    private static readonly Color corPadrao = new Color32(219, 171, 129, 255);
    private static readonly Color corSelecionado = Color.white;

    void Start()
    {
        // Ativa ou desativa o botão com base no estado de desbloqueio
        GetComponent<Button>().interactable = desbloqueado;

        if (areaExibicao != null && areaExibicao.texture == null)
            areaExibicao.color = corPadrao;

        // Se for o casaco, adiciona comportamento ao clique na imagem grande
        if (areaExibicao != null && itemEspecialCasaco)
        {
            Button btn = areaExibicao.GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(OnImagemGrandeClicada);
        }

        // Caso o casaco já tenha sido abanado anteriormente
        if (itemEspecialCasaco && PlayerPrefs.GetInt("CasacoFoiAbanado", 0) == 1)
        {
            foreach (var obj in objetosParaOcultar)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            if (chave != null)
                chave.SetActive(false);

            if (areaExibicao != null && imagemFinal != null)
            {
                areaExibicao.texture = imagemFinal;
                areaExibicao.color = Color.white;
                areaExibicao.gameObject.SetActive(true);
            }

            SequenciaFinalItens sequencia = FindObjectOfType<SequenciaFinalItens>();
            if (sequencia != null)
                sequencia.MostrarSegundaFase();
        }

        if (chave != null)
            chave.SetActive(false);
    }

    public void MostrarItem()
    {
        if (!desbloqueado) return;

        if (areaExibicao != null && imagemGrande != null)
        {
            areaExibicao.texture = imagemGrande;
            areaExibicao.color = corSelecionado;
            areaExibicao.gameObject.SetActive(true);

            // Se for a espada, mostra popup associado
            if (popupAoMostrar != null && gameObject.name.ToLower().Contains("espada"))
                popupAoMostrar.SetActive(true);
        }
    }

    public void Desbloquear()
    {
        desbloqueado = true;
        GetComponent<Button>().interactable = true;
        gameObject.SetActive(true); // Torna o item visível no inventário
    }

    public void LimparExibicao()
    {
        if (areaExibicao != null)
        {
            areaExibicao.texture = null;
            areaExibicao.color = corPadrao;
        }
    }

    void OnImagemGrandeClicada()
    {
        // Só executa se for o casaco e se pelo menos 3 itens já estiverem desbloqueados
        if (itemEspecialCasaco && inventarioManager != null && inventarioManager.TotalItensDesbloqueados() >= 3)
        {
            StartCoroutine(AbanarCasacoERevelarChave());

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate(); // Vibração em dispositivos móveis
#endif
        }
    }

    IEnumerator AbanarCasacoERevelarChave()
    {
        // Simula o "abanar" do casaco com pequenas variações de posição
        Vector3 original = areaExibicao.rectTransform.localPosition;

        for (int i = 0; i < 6; i++)
        {
            areaExibicao.rectTransform.localPosition = original + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            yield return new WaitForSeconds(0.04f);
        }

        areaExibicao.rectTransform.localPosition = original;

        // Mostra a chave temporariamente
        if (chave != null)
            chave.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (chave != null)
            chave.SetActive(false);

        // Oculta os objetos definidos (ex: corda, lenço, etc.)
        foreach (var obj in objetosParaOcultar)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Atualiza a imagem final (após interação)
        if (areaExibicao != null && imagemFinal != null)
        {
            areaExibicao.texture = imagemFinal;
            areaExibicao.color = Color.white;
            areaExibicao.gameObject.SetActive(true);
        }

        // Ativa segunda fase da sequência, se existir
        SequenciaFinalItens sequencia = FindObjectOfType<SequenciaFinalItens>();
        if (sequencia != null)
            sequencia.MostrarSegundaFase();

        // Marca que o casaco foi abanado (persistência)
        PlayerPrefs.SetInt("CasacoFoiAbanado", 1);
        PlayerPrefs.Save();
    }
}
