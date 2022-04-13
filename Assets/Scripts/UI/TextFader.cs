using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
  [SerializeField]
  private float alpha = 1f;

  [SerializeField]
  private float fadeSpeed = .5f;

  [SerializeField]
  private int direction = 1;

  private Text text;

  // Start is called before the first frame update
  void Start()
  {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update()
  {
    alpha += direction * (fadeSpeed * Time.deltaTime);
    if (alpha >= 1f)
    {
      alpha = 1f;
      direction = -1;
    }
    else if (alpha <= 0f)
    {
      alpha = 0f;
      direction = 1;
    }
    text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
  }
}
