using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
  [SerializeField]
  private float time = 10f;

  private float timer;

  // Start is called before the first frame update
  void Start()
  {
    timer = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;
    if (timer > time)
    {
      Destroy(gameObject);
    }
  }
}
