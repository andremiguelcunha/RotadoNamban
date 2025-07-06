using UnityEngine;

// Este script desativa um GameObject específico (como um baú) quando chamado.
// This script disables a specific GameObject (like a chest) when triggered.

public class DesativarBauAntes : MonoBehaviour
{
    // Objeto que será desativado.
    // Target GameObject to deactivate.
    public GameObject alvo;

    // Método público que desativa o objeto se estiver atribuído.
    // Public method to deactivate the object if it's assigned.
    public void Desativar()
    {
        if (alvo != null)
        {
            alvo.SetActive(false);
        }
    }
}
