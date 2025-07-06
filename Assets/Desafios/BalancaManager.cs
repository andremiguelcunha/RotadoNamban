using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BalancaManager : MonoBehaviour
{
    [Header("Popup")]
    public GameObject popupPanel;
    public TextMeshProUGUI trocaTexto;
    public Button botaoFechar;
    public Button botaoEscolher;

    [Header("Pista Final")]
    public GameObject pistaPanel;
    public TextMeshProUGUI pistaTexto;
    public Button botaoContinuar;

    private int balancaSelecionada = -1;

    private string[] descricoes = {
        "Esta troca contém 1 pimenta e 3 tecidos.",
        "Esta troca contém 1 espelho e 10 sacos de arroz.",
        "Esta troca contém 5 moedas de ouro e 1 chávena de chá."
    };

    private string[] explicacoesErro = {
        "", // correta → sem explicação
        "Nesta época, espelhos eram considerados itens de luxo na Europa e no Japão. Trocar por arroz, um bem comum, era desvantajoso.",
        "5 moedas de ouro por uma chávena de chá era uma troca altamente desigual. O chá era valioso, mas não valia tanto quanto ouro."
    };

    private bool[] respostasCertas = { true, false, false };

    void Start()
    {
        popupPanel.SetActive(false);
        pistaPanel.SetActive(false);
        botaoFechar.onClick.AddListener(FecharPopup);
        botaoEscolher.onClick.AddListener(ConfirmarEscolha);
        botaoContinuar.onClick.AddListener(IrParaCena4);
    }

    public void AbrirPopup(int indice)
    {
        balancaSelecionada = indice;
        trocaTexto.text = descricoes[indice];
        botaoEscolher.gameObject.SetActive(true);
        popupPanel.SetActive(true);
    }

    void FecharPopup()
    {
        popupPanel.SetActive(false);
        balancaSelecionada = -1;
    }

    public void ConfirmarEscolha()
    {
        if (balancaSelecionada < 0) return;

        if (respostasCertas[balancaSelecionada])
        {
            popupPanel.SetActive(false);
            pistaPanel.SetActive(true);
            pistaTexto.text = "Boa! Estás a ficar bom nisto! Clica em continuar para veres qual será a tua recompensa...";
        }
        else
        {
            trocaTexto.text = explicacoesErro[balancaSelecionada];
            botaoEscolher.gameObject.SetActive(false);
        }
    }

    public void IrParaCena4()
    {
        SceneManager.LoadScene(4);
    }
}
