using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Camera cam;
    public RectTransform background;
    public Text text;

    public void Show(string content, Vector3 position)
    {
        text.text = content;
        background.position = cam.WorldToScreenPoint(position);
        background.gameObject.SetActive(true);
    }

    public void Hide()
    {
        background.gameObject.SetActive(false);
    }
}
