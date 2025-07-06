using UnityEngine;
using UnityEngine.UI;

// Este script mostra um GameObject quando um botão é clicado.
// This script shows a GameObject when a button is clicked.

public class MostrarGameObjectAoClicar : MonoBehaviour
{
    [Header("Botão que será clicado")]
    // Referência ao botão que o utilizador vai clicar.
    // Reference to the button the user will click.
    public Button botao;

    [Header("Objeto a ser mostrado")]
    // Objeto que será ativado (tornado visível).
    // GameObject that will be shown (set active).
    public GameObject objetoParaMostrar;

    void Start()
    {
        // Verifica se o botão e o objeto foram atribuídos.
        // Check if the button and object are assigned.
        if (botao != null && objetoParaMostrar != null)
        {
            // Associa o método MostrarObjeto ao clique do botão.
            // Attach the MostrarObjeto method to the button's onClick event.
            botao.onClick.AddListener(MostrarObjeto);
        }
        else
        {
            Debug.LogWarning("Botão ou Objeto para mostrar não foi atribuído.");
        }
    }

    void MostrarObjeto()
    {
        // Ativa o objeto, tornando-o visível na cena.
        // Activates the object, making it visible in the scene.
        objetoParaMostrar.SetActive(true);
    }
}
