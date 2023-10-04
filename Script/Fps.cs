using UnityEngine;
using System.Collections;
using TMPro;

public class Fps : MonoBehaviour
{
    private float count;
    public TMP_Text fps;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        fps.text = "FPS: " + Mathf.Round(count);
    }
}
