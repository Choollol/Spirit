using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RisingText : MonoBehaviour
{
    [SerializeField] private float riseSpeed;
    [SerializeField] private float lifeTime;

    private TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void UpdateText(string newText)
    {
        text.text = newText;
        UpdateColor();
        StartCoroutine(Rise());
    }
    private IEnumerator Rise()
    {
        float counter = 0;
        while (counter < lifeTime)
        {
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime);
            counter += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
    private void UpdateColor()
    {
        float value;
        Color.RGBToHSV(Camera.main.backgroundColor, out _, out _, out value);
        if (value < 0.5f)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.black;
        }
    }
}
