using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingText : MonoBehaviour
{
    float time = 0.0f;
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(string text, Vector3 position)
    {
        GetComponent<Text>().text = text;
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + Time.deltaTime*100.0f, rectTransform.position.z);

        if(time > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
