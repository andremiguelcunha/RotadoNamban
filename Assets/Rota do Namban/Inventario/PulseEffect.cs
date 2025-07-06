using UnityEngine;

// Aplica um efeito visual de "palpitação" (pulsar) a um GameObject.
// Applies a pulsing visual effect to a GameObject.

public class PulseEffect : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;       // Velocidade da palpitação (quanto mais alto, mais rápido)
                                       // Speed of pulsing (higher value = faster)
    public float pulseAmount = 0.1f;    // Variação máxima de escala (ex: 0.1 = 10% maior)
                                       // Maximum scale variation (e.g. 0.1 = 10% larger)

    private Vector3 initialScale;       // Guarda a escala original do objeto
                                       // Stores the original scale of the object

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calcula um modificador de escala oscilante com base no tempo
        // Calculate an oscillating scale modifier based on time
        float scaleModifier = 1 + Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);

        // Aplica a nova escala ao objeto, mantendo proporção
        // Apply the new scale to the object, preserving proportions
        transform.localScale = initialScale * scaleModifier;
    }
}
