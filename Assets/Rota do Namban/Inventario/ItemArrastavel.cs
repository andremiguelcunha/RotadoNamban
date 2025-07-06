using UnityEngine;
using UnityEngine.EventSystems;

// Permite que um item UI seja arrastado com o rato (drag and drop).
// Allows a UI item to be dragged with the mouse (drag and drop system).

public class ItemArrastavel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;       // Necessário para permitir bloqueio temporário de raycasts
                                           // Needed to allow raycast blocking during drag
    private RectTransform rectTransform;   // Referência à posição e dimensão do objeto na UI
                                           // Reference to the object's position and size in UI
    private Vector3 startPosition;         // Posição inicial antes do arrasto
                                           // Initial position before dragging

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Chamado ao iniciar o arrasto
    // Called when dragging starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false; // Permite que o drop target detete o objeto
                                            // Allows drop targets to detect this object
    }

    // Chamado durante o arrasto
    // Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition; // Segue o cursor
    }

    // Chamado quando o arrasto termina
    // Called when dragging ends
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Restaura a capacidade de bloquear raycasts

        // Opcional: voltar à posição original se necessário
        // Optional: return to original position if needed
        // rectTransform.position = startPosition;
    }
}
