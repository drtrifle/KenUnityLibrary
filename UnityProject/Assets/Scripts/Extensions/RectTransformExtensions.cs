using UnityEngine;

public static class RectTransformExtensions
{
    public static float GetWidth(this RectTransform rect)
    {
        return rect.sizeDelta.x;
    }

    public static void SetWidth(this RectTransform rect, float width)
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public static float GetHeight(this RectTransform rect)
    {
        return rect.sizeDelta.y;
    }

    public static void SetHeight(this RectTransform rect, float height)
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
