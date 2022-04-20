using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game data", order = 0)]
public class GameData : ScriptableObject
{
  public int level;
  public int stars;
  public int totalStars;
  public int pieces;
  public int attempts;
  public int maxAttempts;

  public void Clean()
  {
    level = 1;
    stars = 0;
    totalStars = 0;
    pieces = 0;
    maxAttempts = Constants.MAX_ATTEMPTS;
    attempts = maxAttempts;
  }
}
