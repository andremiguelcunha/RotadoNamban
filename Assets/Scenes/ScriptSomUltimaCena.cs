using UnityEngine;

public class ScriptSomUltimaCena : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    void Start()
    {
        // Inicia a sequência de áudio
        StartCoroutine(PlayAudioSequence());
    }

    private System.Collections.IEnumerator PlayAudioSequence()
    {
        // Toca o primeiro áudio
        audioSource1.Play();

        // Espera 3 segundos
        yield return new WaitForSeconds(1f);

        // Toca o segundo áudio
        audioSource2.Play();
    }
}