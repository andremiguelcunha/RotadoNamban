using UnityEngine;
using UnityEngine.UI;

// Controla a exibição de um botão de "Skip" após um determinado tempo.
// Controls the display of a "Skip" button after a set amount of time.

public class SceneSkipController : MonoBehaviour
{
    [Header("Configurações")]
    // Tempo de espera antes de mostrar o botão (em segundos).
    // Time delay before showing the button (in seconds).
    public float delayAntesDeMostrarBotao = 60f;

    [Header("Referências")]
    // Referência ao botão de Skip.
    // Reference to the Skip button.
    public Button botaoSkip;

    // GameObject (geralmente um canvas) que será ativado quando o botão for clicado.
    // GameObject (usually a canvas) that will be activated when the button is clicked.
    public GameObject canvasParaAtivar;

    // Flag para garantir que o botão só seja mostrado uma vez.
    // Flag to ensure the button is shown only once.
    private bool botaoMostrado = false;

    void Start()
    {
        // Garante que o botão comece invisível.
        // Ensure the button starts hidden.
        if (botaoSkip != null)
            botaoSkip.gameObject.SetActive(false);

        // Agenda a chamada do método que mostrará o botão após o tempo definido.
        // Schedule the method that will show the button after the delay.
        Invoke(nameof(MostrarBotao), delayAntesDeMostrarBotao);
    }

    void MostrarBotao()
    {
        // Mostra o botão e associa o evento de clique.
        // Show the button and attach the click event.
        if (botaoSkip != null)
        {
            botaoSkip.gameObject.SetActive(true);
            botaoSkip.onClick.AddListener(AoClicarBotao);
        }
        botaoMostrado = true;
    }

    void AoClicarBotao()
    {
        // Ativa o canvas (ou outro objeto) quando o botão for clicado.
        // Activate the canvas (or another object) when the button is clicked.
        if (canvasParaAtivar != null)
            canvasParaAtivar.SetActive(true);

        // Opcionalmente desativa o botão após o clique.
        // Optionally deactivate the button after being clicked.
        botaoSkip.gameObject.SetActive(false);
    }
}
