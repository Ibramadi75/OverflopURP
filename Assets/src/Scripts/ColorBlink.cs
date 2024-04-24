using System.Collections;
using UnityEngine;

public class ColorBlink : MonoBehaviour
{
    public float blinkSpeed = 1.0f;
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    private bool isBlinking;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!isBlinking) StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        var t = 0.0f;

        while (t < 1.0f)
        {
            var lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * blinkSpeed, 1.0f));
            rend.material.color = lerpedColor;
            t += Time.deltaTime * blinkSpeed;
            yield return null;
        }

        isBlinking = false;
    }
}