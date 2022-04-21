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
    Game game = new Game(3, 3);
    Assert.AreEqual(game.enemies, 3);
  }

  [Test]
  public void TestKillEnemy()
  {
    Game game = new Game(5, 3);
    game.KillEnemy();
    Assert.AreEqual(game.enemies, 4);
  }

  [Test]
  public void TestFinishGameWithoutEnemiesAndThreeOrMoreLives()
  {
    Game game = new Game(0, 3);
    int stars = game.Finish();
    Assert.AreEqual(stars, 3);
    stars = game.Finish();
    Assert.AreEqual(stars, 3);
  }

  [Test]
  public void TestFinishGameWithoutEnemiesAndOneLife()
  {
    Game game = new Game(0, 1);
    int stars = game.Finish();
    Assert.AreEqual(stars, 1);
  }

  [Test]
  public void TestFinishGameWithEnemiesAndThreeOrMoreLives()
  {
    Game game = new Game(3, 3);
    int stars = game.Finish();
    Assert.AreEqual(stars, 0);
  }

  [Test]
  public void TestHasPizzaShouldBeFalseByDefault()
  {
    Game game = new Game(0, 3);
    Assert.IsFalse(game.hasPizza);
  }

  [Test]
  public void TestHasPizzaShouldBeTrueAfterCollecting()
  {
    Game game = new Game(0, 3);
    game.CollectPizza();
    Assert.IsTrue(game.hasPizza);
  }

  [Test]
  public void TestKillPek()
  {
    Game game = new Game(0, 3);

    bool dead = game.KillPek();
    Assert.IsFalse(dead);

    dead = game.KillPek();
    Assert.IsFalse(dead);

    dead = game.KillPek();
    Assert.IsTrue(dead);

    dead = game.KillPek();
    Assert.IsTrue(dead);
    Assert.AreEqual(0, game.attempts);
  }
}
