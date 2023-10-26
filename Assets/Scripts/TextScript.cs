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

    public void startBlink() {
        blinkCoroutine = StartCoroutine(Blink());
        textComponent.enabled = true;
    }

    public void stopBlink()
    {
        StopCoroutine(blinkCoroutine);
        textComponent.enabled = false;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
