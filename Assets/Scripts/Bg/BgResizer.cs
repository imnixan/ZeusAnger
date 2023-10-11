using UnityEngine.UI;
using UnityEngine;

public class BgResizer : MonoBehaviour
{
    public static Vector2 BgSize;
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        Vector2 screenSize = rt.parent.GetComponent<RectTransform>().sizeDelta;
        Debug.Log("Screensize " + screenSize);
        GetComponent<Image>().SetNativeSize();
        float scale = screenSize.y / rt.sizeDelta.y;
        Debug.Log("scale " + scale);
        rt.sizeDelta *= scale;
        BgSize = rt.sizeDelta;
    }
}
