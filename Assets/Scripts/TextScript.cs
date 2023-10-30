using System.Collections;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    [SerializeField]public float blinkTime = 0.3f; // Tiempo en segundos que tarda en aparecer y desaparecer
    private TMP_Text textComponent;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }
    
    // Start the text blinking
    public void startBlink() {
        blinkCoroutine = StartCoroutine(Blink());
        textComponent.enabled = true;
    }

    // Stop the text blinking
    public void stopBlink()
    {
        StopCoroutine(blinkCoroutine);
        textComponent.enabled = false;
    }

    // Makes the text blink according to the blinkTime variable
    IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
