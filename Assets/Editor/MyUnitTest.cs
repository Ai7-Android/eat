using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using GDGeek;
using System.IO;

public class MyUnitTest {
	[Test]
	public void FirstTest(){
		Layout[] layout = Component.FindObjectsOfType<Layout> ();

		for (int i = 0; i < layout.Length; ++i) {
			layout[i].doIt ();
		}
		Debug.Log ("first Test" + layout.Length);

	}
	[Test]
	public void MapTest(){
		MapCtrl map = Component.FindObjectOfType<MapCtrl> ();
		Assert.NotNull (map);
		IMapData data = map.ask (new Vector2 (0, 0));
		Assert.IsTrue(data.pass (new Vector2 (1, 0)));

		Vector2 cell = map.position2cell (new Vector2 (0, 0));
		Assert.AreEqual (cell, new Vector2(0,0));
		Assert.AreEqual (map.position2cell (new Vector2 (15, 15)), new Vector2(0,0));
		Assert.AreEqual (map.position2cell (new Vector2 (16, 16)), new Vector2(1,1));

		Assert.AreEqual (map.position2cell (new Vector2 (0, 16)), new Vector2(0,1));

		data = map.ask (new Vector2 (-1, -1));
		Assert.IsFalse (data.pass (new Vector2 (1, 0)));
		data = map.ask (new Vector2 (1, 1));
		Assert.IsTrue (data.pass (new Vector2 (1, 0)));


		data = map.ask (new Vector2 (8, 1));
		Assert.IsFalse (data.pass (new Vector2 (1, 0)));
		data = map.ask (map.position2cell(new Vector2 (-1, 34)));
		Assert.IsFalse (data.pass (new Vector2 (1, 0)));
		Vector2 export = map.export ();
		Assert.IsNotNull (export);

		map.initialize();
		data = map.ask (export);
		data.trample ();
		Assert.IsFalse (map.solve ());
		Debug.Log(map.isOpen ());

		map.open ();
		data = map.ask (export);
		data.trample ();
		Assert.IsTrue (map.solve ());

	}

}
