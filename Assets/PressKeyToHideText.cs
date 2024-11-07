using UnityEngine;
using TMPro;

public class PressKeyToHideText : MonoBehaviour
{
    public TMP_Text pressText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pressText != null)
        {
            pressText.gameObject.SetActive(false);
        }
    }
}