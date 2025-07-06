using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Detecta se um objeto arrastado (drag & drop) foi largado sobre este GameObject.
// Detects if a dragged object was dropped onto this GameObject.

public class DropDetector : MonoBehaviour, IDropHandler
{
    [Header("Popup a mostrar se item for espada")]
    // Popup a ativar quando uma "espada" é largada.
    // Popup to show when a "sword" is dropped.
    public GameObject popupEspada;

    // Método chamado automaticamente quando um objeto é largado sobre este.
    // This method is automatically called when something is dropped onto this object.
    public void OnDrop(PointerEventData eventData)
    {
        // Obtém o objeto que foi arrastado.
        // Get the dragged object.
        GameObject itemArrastado = eventData.pointerDrag;

        // Verifica se é válido e se o nome contém "espada".
        // Check if it's valid and contains "espada" in the name.
        if (itemArrastado != null && itemArrastado.name.ToLower().Contains("espada"))
        {
            Debug.Log("Espada foi arrastada e solta!");
            
            // Mostra o popup se estiver definido.
            // Show the popup if it's assigned.
            if (popupEspada != null)
                popupEspada.SetActive(true);

            // Limpa chaves específicas de progresso guardadas em PlayerPrefs.
            // Clears specific saved progress keys from PlayerPrefs.
            PlayerPrefs.DeleteKey("CasacoFoiAbanado");
            PlayerPrefs.DeleteKey("Desbloqueado_Casaco");
            PlayerPrefs.DeleteKey("Desbloqueado_Espada");
            PlayerPrefs.DeleteKey("Desbloqueado_Documento");
            PlayerPrefs.DeleteKey("Desbloqueado_Saque");
            PlayerPrefs.DeleteKey("Desbloqueado_Bule");
            PlayerPrefs.DeleteKey("Desbloqueado_Arca");
        }
    }
}
