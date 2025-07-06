using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    // Chamar este método para voltar à cena 0
    public void VoltarParaCena0()
    {
        SceneManager.LoadScene(0);
    }

    // Chamar este método para sair do aplicativo
    public void SairDoJogo()
    {
        Application.Quit();

        // Só aparece no Editor (para testes)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
