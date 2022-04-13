using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTests
{
  // A Test behaves as an ordinary method
  [Test]
  public void GameTestsSimplePasses()
  {
    // Use the Assert class to test conditions
  }

  [Test]
  public void TestBuildGameInstance()
  {
    Game game = new Game(3);
    Assert.AreEqual(game.enemies, 3);
  }

  [Test]
  public void TestKillEnemy()
  {
    Game game = new Game(5);
    game.KillEnemy();
    Assert.AreEqual(game.enemies, 4);
  }

  [Test]
  public void TestFinishGameWithoutEnemiesAndThreeOrMoreLives()
  {
    Game game = new Game(0);
    int stars = game.Finish(3);
    Assert.AreEqual(stars, 3);
    stars = game.Finish(5);
    Assert.AreEqual(stars, 3);
  }

  [Test]
  public void TestFinishGameWithoutEnemiesAndOneLife()
  {
    Game game = new Game(0);
    int stars = game.Finish(1);
    Assert.AreEqual(stars, 1);
  }

  [Test]
  public void TestFinishGameWithEnemiesAndThreeOrMoreLives()
  {
    Game game = new Game(3);
    int stars = game.Finish(3);
    Assert.AreEqual(stars, 0);

    stars = game.Finish(5);
    Assert.AreEqual(stars, 2);
  }
}
