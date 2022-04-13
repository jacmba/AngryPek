using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameControllerTests
{
  [Test]
  public void GameControllerTestDataClean()
  {
    GameController.maxAttempts = 5;
    GameController.level = 10;

    GameController.Clean();
    Assert.AreEqual(GameController.maxAttempts, 3);
    GameController.level = 1;
  }
}
