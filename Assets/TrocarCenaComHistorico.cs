using UnityEngine;
using UnityEngine.SceneManagement;

// Esta classe permite trocar de cena com som e guardar o histórico da cena anterior.
// This class allows scene switching with a sound effect and stores the previous scene name.

public class TrocarCenaComHistorico : MonoBehaviour
{
    // Nome da cena para onde queremos mudar.
    // Name of the destination scene.
    public string nomeDaCenaDestino;

    // Fonte de áudio para tocar um som antes da troca.
    // Audio source to play a sound before switching.
    [SerializeField] private AudioSource audioSource;

    // Método público para iniciar a troca de cena.
    // Public method to initiate the scene switch.
    public void Trocar()
    {
        // Inicia a rotina que toca o som e depois troca de cena.
        // Starts the coroutine that plays the sound and then switches the scene.
        StartCoroutine(TocarSomETrocar());
    }

    private System.Collections.IEnumerator TocarSomETrocar()
    {
        // Toca o som, se houver áudio definido.
        // Play the sound if an audio clip is assigned.
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            // Espera até o som terminar antes de continuar.
            // Wait for the sound to finish before proceeding.
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // Guarda o nome da cena atual nos dados persistentes.
        // Save the current scene name in PlayerPrefs.
        PlayerPrefs.SetString("CenaAnterior", SceneManager.GetActiveScene().name);

        // Carrega a cena de destino.
        // Load the destination scene.
        SceneManager.LoadScene(nomeDaCenaDestino);
    }
}
