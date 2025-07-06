using UnityEngine;

// Controla a exibição em duas fases dos itens finais no inventário.
// Controls a two-phase display of final inventory items.

public class SequenciaFinalItens : MonoBehaviour
{
    public InventarioManager inventarioManager;

    [Header("Itens mostrados primeiro (se tiver os 6)")]
    public GameObject[] primeiraFase; // Arca, Documento, Casaco
                                      // First group of items to show

    [Header("Itens mostrados depois de abanar o casaco")]
    public GameObject[] segundaFase; // Saque, Bule, Espada
                                     // Second group of items to show

    private bool jaMostrouPrimeiraFase = false; // Controla se a primeira fase já foi exibida
                                                // Controls whether first phase has already shown

    void Start()
    {
        // Verifica se todos os 6 itens foram desbloqueados no início
        // Checks if all 6 items are unlocked at start
        if (inventarioManager != null && inventarioManager.TotalItensDesbloqueados() == 6)
        {
            MostrarPrimeiraFase();
        }
    }

    // Exibe apenas os itens da primeira fase e oculta os da segunda
    // Shows only the first phase items and hides the second
    public void MostrarPrimeiraFase()
    {
        foreach (var obj in primeiraFase)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (var obj in segundaFase)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        jaMostrouPrimeiraFase = true;
    }

    // Exibe os itens da segunda fase, mas só se a primeira fase já foi mostrada
    // Shows the second phase items, only if the first phase was shown
    public void MostrarSegundaFase()
    {
        if (jaMostrouPrimeiraFase)
        {
            foreach (var obj in segundaFase)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
