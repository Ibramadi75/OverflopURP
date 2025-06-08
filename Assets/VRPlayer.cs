using System.Collections;
using TMPro;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem rain;
    public ParticleSystem Rain => rain;

    [SerializeField] private TMP_Text anotherNight;

    [SerializeField] private Persistent persistent;
    public Persistent Persistent => persistent;

    [SerializeField] private GameObject rainOffset;
    
    void Start()
    {
        if (anotherNight != null)
            StartCoroutine(FadeInOutText());
    }

    void Update()
    {
        if (rain == null) return;
        rain.transform.position = rainOffset.transform.position + Vector3.up * 10f;
    }
    
    private IEnumerator FadeInOutText()
    {
        yield return StartCoroutine(FadeText(0, 1, 1));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(FadeText(1, 0, 1));
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        Color textColor = anotherNight.color;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            textColor.a = Mathf.Lerp(startAlpha, endAlpha, normalizedTime);
            anotherNight.color = textColor;

            yield return null;
        }

        textColor.a = endAlpha;
        anotherNight.color = textColor;
    }
}
