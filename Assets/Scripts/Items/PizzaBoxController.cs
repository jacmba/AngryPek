using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBoxController : MonoBehaviour
{
  [SerializeField] private GameObject[] pieces;
  [SerializeField] private GameData data;
  private Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    StartCoroutine(openBox());

    for (int i = 0; i < data.pieces - 1 && i < pieces.Length; i++)
    {
      pieces[i].SetActive(true);
    }
  }

  // Update is called once per frame
  void Update()
  {
    // ToDo loop stuff
  }

  IEnumerator openBox()
  {
    yield return new WaitForSeconds(1f);
    animator.SetTrigger("Open");
  }

  public void AddPiece()
  {
    GameObject piece = pieces[data.pieces - 1];
    piece.SetActive(true);
    Animator animator = piece.GetComponent<Animator>();
    animator.enabled = true;
  }
}
