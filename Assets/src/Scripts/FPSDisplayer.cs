using TMPro;
using UnityEngine;

public class FPSDisplayer : MonoBehaviour
{
    float fps;
    float updateTimer = .2f;
    [SerializeField] TextMeshProUGUI fpsTitle;

    void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer < 0)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS : " + Mathf.Round(fps);
            updateTimer = .2f;
        }
    }

    void Update()
    {
        UpdateFPSDisplay();
    }
}