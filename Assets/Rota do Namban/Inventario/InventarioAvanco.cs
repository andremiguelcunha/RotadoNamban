using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Gere a transição de cena e manipula o estado do inventário com base numa condição visual (ex: chave visível).
// Manages scene transition and inventory state based on a visual condition (e.g. key visibility).

public class InventarioAvanco : MonoBehaviour
{
    public InventarioManager inventarioManager; // Referência ao gestor de inventário
    public GameObject imagemChave;              // Objeto da chave (exibido quando o casaco é abanado)
    public GameObject areaExibicao;             // Área onde a imagem ampliada é mostrada (RawImage)
    public string proximaCena;                  // Nome da próxima cena a carregar

    public void Avancar()
    {
        // Verifica se a chave está visível (o que significa que o casaco foi interagido)
        // Check if the key is visible (which means the coat was interacted with)
        bool casacoFoiInteragido = imagemChave != null && imagemChave.activeSelf;

        if (casacoFoiInteragido && inventarioManager != null)
        {
            // Reseta o inventário se o casaco foi abanado
            // Reset the inventory if the coat was shaken
            inventarioManager.ResetarInventario();

            // Esconde a chave
            // Hide the key object
            if (imagemChave != null)
                imagemChave.SetActive(false);

            // Limpa a imagem da área de exibição (ex: zoom) e aplica cor base
            // Clear the enlarged image and set background color
            if (areaExibicao != null)
            {
                var raw = areaExibicao.GetComponent<RawImage>();
                if (raw != null)
                {
                    raw.texture = null;
                    raw.color = new Color32(219, 171, 129, 255); // #DBAB81
                }
            }
        }

        // Carrega a próxima cena sempre, independentemente da condição
        // Always load the next scene regardless of condition
        SceneManager.LoadScene(proximaCena);
    }
}
