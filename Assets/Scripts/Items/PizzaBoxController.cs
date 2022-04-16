using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBoxController : MonoBehaviour
{
  [SerializeField] private GameObject[] pieces;
  private Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    StartCoroutine(openBox());

    for (int i = 0; i < GameController.achievedPieces && i < pieces.Length; i++)
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
}
