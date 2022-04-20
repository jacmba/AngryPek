using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
  [SerializeField] private int index;
  [SerializeField] private GameObject nextStar;
  [SerializeField] private GameData data;
  private AudioSource audioSource;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    eventBus = EventBus.GetInstance();
  }

  public void Grown()
  {
    audioSource.Play();
    if (nextStar != null && data.stars > index)
    {
      nextStar.SetActive(true);
    }
    else
    {
      eventBus.ShowPizza();
    }
  }
}
