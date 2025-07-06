using System.Collections.Generic;

// Classe estática que guarda os autocolantes desbloqueados temporariamente durante a sessão.
// Static class that holds temporarily unlocked stickers during the session.

public static class StickerSessionData
{
    // Lista de índices de autocolantes a desbloquear quando a cena iniciar.
    // List of sticker indices to unlock when the scene loads.

    public static List<int> stickersParaDesbloquear = new List<int>();
}
