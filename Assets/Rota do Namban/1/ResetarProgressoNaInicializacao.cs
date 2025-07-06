using UnityEngine;

public class ResetarProgressoNaInicializacao : MonoBehaviour
{
    void Awake()
    {
        // 🔁 Apaga PlayerPrefs específicos no início da cena
        // 🔁 Delete specific PlayerPrefs on scene start

        // Reseta flags de desbloqueio de itens principais
        // Reset unlock flags for main items
        PlayerPrefs.DeleteKey("CasacoFoiAbanado");
        PlayerPrefs.DeleteKey("Desbloqueado_Casaco");
        PlayerPrefs.DeleteKey("Desbloqueado_Espada");
        PlayerPrefs.DeleteKey("Desbloqueado_Documento");
        PlayerPrefs.DeleteKey("Desbloqueado_Saque");
        PlayerPrefs.DeleteKey("Desbloqueado_Bule");
        PlayerPrefs.DeleteKey("Desbloqueado_Arca");

        // Reseta os autocolantes (stickers) desbloqueados
        // Reset unlocked stickers
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.DeleteKey("Sticker_" + i);
        }

        // Limpa dados temporários da sessão atual
        // Clear temporary data from the current session
        if (StickerSessionData.stickersParaDesbloquear != null)
            StickerSessionData.stickersParaDesbloquear.Clear();

        // Salva alterações no disco
        // Save changes to disk
        PlayerPrefs.Save();

        // Log de depuração
        // Debug log
        Debug.Log("🔄 PlayerPrefs e dados de stickers resetados ao iniciar a cena. / PlayerPrefs and sticker data reset on scene start.");
    }
}
