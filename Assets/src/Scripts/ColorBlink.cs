using UnityEngine;
using System.Collections;

public class ColorBlink : MonoBehaviour
{
    public float blinkSpeed = 1.0f;
    public Color startColor = Color.white;
    public Color endColor = Color.red;

    private Renderer rend;
    private bool isBlinking = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!isBlinking)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        float t = 0.0f;
        
        while (t < 1.0f)
        {
            Color lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * blinkSpeed, 1.0f));
            rend.material.color = lerpedColor;
            t += Time.deltaTime * blinkSpeed;
            yield return null;
        }

        isBlinking = false;
    }
}
