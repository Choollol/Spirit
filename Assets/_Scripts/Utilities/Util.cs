using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Image helpers
    public static void SetAlpha(this Image image, float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
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
                finalText += Char.ToLower(text[0]);
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
    
}
