using UnityEngine;
using UnityEngine.UI;

// Gere o sistema de autocolantes (stickers) que podem ser desbloqueados e visualizados.
// Manages the collectible sticker system (locked/unlocked UI and interactions).

public class StickerManager : MonoBehaviour
{
    [System.Serializable]
    public class Sticker
    {
        public Button button;               // Botão clicável do autocolante
                                            // Clickable button for the sticker

        public GameObject lockedImage;      // Imagem visível quando está bloqueado
                                            // Image shown when sticker is locked

        public GameObject unlockedImage;    // Imagem visível quando está desbloqueado
                                            // Image shown when sticker is unlocked

        public bool isUnlocked;             // Estado de desbloqueio
                                            // Current unlock state

        public GameObject popupCanvas;      // Popup que aparece ao clicar no autocolante desbloqueado
                                            // Popup shown when unlocked sticker is clicked
    }

    public Sticker[] stickers;

    [Header("Popup para quando o autocolante está bloqueado")]
    public GameObject popupOops; // Mensagem de aviso quando clica em sticker bloqueado

    void Start()
    {
        // Carrega o estado de cada sticker a partir do PlayerPrefs
        // Loads saved state of each sticker from PlayerPrefs
        for (int i = 0; i < stickers.Length; i++)
        {
            stickers[i].isUnlocked = PlayerPrefs.GetInt("Sticker_" + i, 0) == 1;
        }

        // Aplica desbloqueios registados durante a sessão (memória temporária)
        // Applies temporary unlocks from current session
        foreach (int index in StickerSessionData.stickersParaDesbloquear)
        {
            UnlockSticker(index);
        }

        // Atualiza o aspeto visual dos autocolantes na UI
        // Updates the visual appearance of stickers in the UI
        UpdateStickersUI();

        // Limpa a lista de desbloqueios temporários
        // Clears session-based unlocks
        StickerSessionData.stickersParaDesbloquear.Clear();
    }

    void Update()
    {
        // Permite atualizar a UI manualmente ao pressionar a tecla U
        // Pressing U key forces manual UI refresh (useful for debug)
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Atualização manual chamada.");
            UpdateStickersUI();
        }
    }

    // Desbloqueia um autocolante pelo índice
    // Unlocks a sticker by its index
    public void UnlockSticker(int index)
    {
        if (index >= 0 && index < stickers.Length)
        {
            stickers[index].isUnlocked = true;
            SaveUnlockedStickers();   // Guarda no PlayerPrefs
            UpdateStickersUI();       // Atualiza o visual
        }
    }

    // Atualiza o estado visual e funcional dos botões e imagens
    // Updates button listeners and locked/unlocked visuals
    void UpdateStickersUI()
    {
        for (int i = 0; i < stickers.Length; i++)
        {
            Sticker sticker = stickers[i];

            // Esconde o popup inicialmente
            if (sticker.popupCanvas != null)
                sticker.popupCanvas.SetActive(false);

            // Mostra imagem certa com base no estado
            if (sticker.lockedImage != null)
                sticker.lockedImage.SetActive(!sticker.isUnlocked);

            if (sticker.unlockedImage != null)
                sticker.unlockedImage.SetActive(sticker.isUnlocked);

            // Remove listeners antigos para evitar sobreposição
            if (sticker.button != null)
                sticker.button.onClick.RemoveAllListeners();

            // Ligações de clique consoante desbloqueado ou não
            if (sticker.isUnlocked)
            {
                // Abre o popup do sticker desbloqueado
                sticker.button.onClick.AddListener(() =>
                {
                    if (sticker.popupCanvas != null)
                        sticker.popupCanvas.SetActive(true);
                });
            }
            else
            {
                // Mostra o popup "Oops" se estiver bloqueado
                sticker.button.onClick.AddListener(() =>
                {
                    Debug.Log("Autocolante bloqueado.");
                    if (popupOops != null)
                        popupOops.SetActive(true);
                });
            }
        }
    }

    // Guarda o estado de desbloqueio de todos os stickers
    // Saves all sticker unlock states to PlayerPrefs
    void SaveUnlockedStickers()
    {
        for (int i = 0; i < stickers.Length; i++)
        {
            PlayerPrefs.SetInt("Sticker_" + i, stickers[i].isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // Fecha qualquer popup de sticker
    // Closes any open sticker popup
    public void ClosePopup(GameObject popup)
    {
        if (popup != null)
            popup.SetActive(false);
    }
}
