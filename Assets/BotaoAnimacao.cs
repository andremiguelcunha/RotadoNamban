using UnityEngine;
using UnityEngine.EventSystems;

// Esta classe aplica uma animação de escala quando o botão é pressionado e libertado.
// This class applies a scale animation when the button is pressed and released.

public class BotaoAnimacao : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Guarda a escala original do botão.
    // Stores the original scale of the button.
    private Vector3 escalaOriginal;

    // Fator de escala para aumentar o botão quando pressionado.
    // Scale factor to enlarge the button when pressed.
    public float escalaAumentada = 1.1f;

    void Start()
    {
        // Guarda a escala original ao iniciar.
        // Save the original scale on start.
        escalaOriginal = transform.localScale;
    }

    // Método chamado quando o botão é pressionado.
    // Method called when the button is pressed.
    public void OnPointerDown(PointerEventData eventData)
    {
        // Aumenta a escala do botão.
        // Increases the button scale.
        transform.localScale = escalaOriginal * escalaAumentada;
    }

    // Método chamado quando o botão é libertado.
    // Method called when the button is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        // Restaura a escala original do botão.
        // Restores the original button scale.
        transform.localScale = escalaOriginal;
    }
}
