using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public static class Util
{
    // Transform helpers
    public static void SetPosition(this Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    public static void SetPosX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetPosY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetLocalPosX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }
    public static void SetLocalPosY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
    public static void SetRotation(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public static void AddRotation(this Transform transform, float angle)
    {
        transform.rotation *= Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// Returns a list containing the transforms of children objects without their own children
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<Transform> GetChildTransforms(this Transform root)
    {
        List<Transform> list = new List<Transform>();
        StoreTransforms(root, list);
        return list;
    }
    private static void StoreTransforms(Transform root, List<Transform> list)
    {
        if (root.childCount == 0)
        {
            list.Add(root);
        }
        else
        {
            for (int i = 0; i < root.childCount; i++)
            {
                StoreTransforms(root.GetChild(i), list);
            }
        }
    }
    public static void TransferTransformData(this Transform transform, TransformData data)
    {
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.rotation = Quaternion.Euler(new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]));
        transform.localScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);
    }
    // RectTransform helpers
    public static void SetPosX(this RectTransform rectTransform, float x)
    {
        rectTransform.localPosition = new Vector3(x, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }
    public static void SetPosY(this RectTransform rectTransform, float y)
    {
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, y, rectTransform.localPosition.z);
    }
    // SpriteRenderer helpers
    public static void SetAlpha(this SpriteRenderer spriteRenderer, float opacity)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
    }
    public static void AddAlpha(this SpriteRenderer spriteRenderer, float amount)
    {
        spriteRenderer.color += new Color(0, 0, 0, amount);
    }
    public static IEnumerator<SpriteRenderer> FadeAlpha(SpriteRenderer spriteRenderer, float targetOpacity, float duration)
    {
        if (spriteRenderer.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = spriteRenderer.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            spriteRenderer.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        spriteRenderer.SetAlpha(targetOpacity);
        yield break;
    }
    // Image helpers
    public static void SetAlpha(this Image image, float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }
    // Tilemap helpers
    public static void SetAlpha(this Tilemap tilemap, float opacity)
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, opacity);
    }
    public static IEnumerator<Tilemap> FadeAlpha(Tilemap tilemap, float targetOpacity, float duration)
    {
        if (tilemap.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = tilemap.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            tilemap.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        tilemap.SetAlpha(targetOpacity);
        yield break;
    }
    // Camera helpers
    public static IEnumerator<Camera> FadeColor(Camera camera, Color targetColor, float duration)
    {
        if (camera.backgroundColor == targetColor) { yield break; }
        float currentTime = 0;
        Color start = camera.backgroundColor;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            camera.backgroundColor = Color.Lerp(start, targetColor, currentTime / duration);
            yield return null;
        }
        camera.backgroundColor = targetColor;
        yield break;
    }
    // TMPro helpers
    public static void SetAlpha(this TextMeshPro text, float opacity)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
    public static IEnumerator<TextMeshPro> FadeAlpha(TextMeshPro text, float targetOpacity, float duration)
    {
        if (text.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = text.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        text.SetAlpha(targetOpacity);
        yield break;
    }
    public static void SetAlpha(this TextMeshProUGUI text, float opacity)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
    public static IEnumerator<TextMeshProUGUI> FadeAlpha(TextMeshProUGUI text, float targetOpacity, float duration)
    {
        if (text.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = text.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        text.SetAlpha(targetOpacity);
        yield break;
    }
    // Vector helpers
    public static Vector3Int ToVector3Int(Vector3 vector3)
    {
        return new Vector3Int((int)vector3.x, (int)vector3.y, (int)vector3.z);
    }
    public static Vector3 ToVector3(this Vector2 vector2, float z)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
    // String helpers
    public static string RemoveSpaces(this string text)
    {
        return text.Replace(" ", "");
    }
    public static string AddSpaces(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && i != 0)
            {
                finalText += " ";
            }
            finalText += text[i];
        }
        return finalText;
    }
    public static string ToCamelCase(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 0)
            {
                finalText += char.ToLower(text[0]);
            }
            else if (text[i] != ' ')
            {
                finalText += text[i];
            }
        }
        return finalText;
    }
    public static KeyCode ToKeyCode(string s)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), s, true);
    }
    public static string ReplaceControls(string text, List<string> controlNames)
    {
        string finalText = text;
        List<int> startIndices = new List<int>();
        List<int> endIndices = new List<int>();
        int controlsAmount = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                startIndices.Add(i + 1);
                controlsAmount++;
            }
            else if (text[i] == ']')
            {
                endIndices.Add(i);
            }
        }
        if (controlNames.Count == 0)
        { 
            for (int i = 0; i < startIndices.Count; i++)
            {
                controlNames.Add(text[startIndices[i]..endIndices[i]]);
            }
        }
        for (int i = 0; i < controlsAmount; i++)
        {
            finalText = finalText.Replace('[' + controlNames[i] + ']', '[' + PrimitiveMessenger.strings[controlNames[i]] + ']');
        }
        return finalText;
    }
}
