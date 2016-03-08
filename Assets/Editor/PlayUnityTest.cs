using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using GDGeek;
using System.IO;

public class PlayUnityTest {
	[Test]
	public void FirstTest(){
		
		Play play = Component.FindObjectOfType<Play> ();

		Assert.NotNull (play);
		Task playing = play.running ();
		playing.init ();
		playing.update(10.0f);
		playing.shutdown ();
	}

	[Test]
	public void FactoryTest(){
		GameFactory factory = Component.FindObjectOfType<GameFactory> ();

		Assert.NotNull (factory);
		//factory.

	}
	

}
