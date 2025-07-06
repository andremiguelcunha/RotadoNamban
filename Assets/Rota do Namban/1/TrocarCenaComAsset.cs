using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrocarCenaComAsset : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar; // Cena selecionada no editor / Scene selected in the editor
#endif

    [SerializeField] private string nomeCena = "";         // Nome da cena para carregar / Scene name to load
    [SerializeField] private AudioSource audioSource;      // Áudio tocado antes da troca / Audio played before scene change

    [ContextMenu("Trocar Cena / Change Scene")]
    public void TrocarCena()
    {
        // Verifica se o nome da cena está definido / Check if the scene name is set
        if (!string.IsNullOrEmpty(nomeCena))
        {
            StartCoroutine(TocarSomETrocarCena()); // Inicia a troca com som / Starts scene change with sound
        }
        else
        {
            Debug.LogWarning("Nome da cena está vazio! / Scene name is empty!");
        }
    }

    private System.Collections.IEnumerator TocarSomETrocarCena()
    {
        // Se houver áudio, toca-o antes de mudar de cena / If there's audio, play it before changing the scene
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length); // Espera o áudio terminar / Wait for audio to finish
        }

        SceneManager.LoadScene(nomeCena); // Carrega a nova cena / Load the new scene
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Atualiza automaticamente o nome da cena com base no asset selecionado / Auto-updates scene name from selected asset
        if (cenaParaCarregar != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaParaCarregar);
            nomeCena = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
