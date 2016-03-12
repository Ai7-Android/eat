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
	private void saveToFile(string fileName, VoxelStruct vs){
		FileStream sw = new FileStream (fileName, FileMode.Create, FileAccess.Write);
		System.IO.BinaryWriter bw = new System.IO.BinaryWriter (sw); 
		VoxelFormater.WriteToMagicaVoxel (vs, bw);
		sw.Close ();

	}
	[Test]
	public void MapTest(){
		return;
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

		Assert.IsNotNull (export, " not ooo");


		map.initialize();

		return;
		data = map.ask (export);

		Assert.IsNotNull (data, "data null");
		data.trample ();
		Assert.IsFalse (map.solve ());
		Debug.Log(map.isOpen ());
		return;
		map.open ();
		data = map.ask (export);
		data.trample ();
		Assert.IsTrue (map.solve (), "is true");

		return;
		/**/
	}
	[Test]
	public void BuildMapTest(){

		MapFactory factory = Component.FindObjectOfType<MapFactory> ();
		MapFloor floor = new MapFloor (new VectorInt2(7, 7));
		Assert.NotNull (factory);
		floor.def("grass");

		floor.cell ("road", 0, 0);
		floor.cell ("road", 1, 0);
		floor.cell ("road", 3 ,0);
		floor.cell ("road",  4 ,0);
		floor.cell ("road",  5,0);
		floor.cell ("road", 6,0 );

		floor.cell ("road", 0,1);
		floor.cell ("road", 1, 1);

		floor.cell ("road", 3,1 );
		floor.cell ("road", 4,1 );
		floor.cell ("road", 5,1);
		floor.cell ("road", 6,1);


		floor.cell ("road", 0,2);
		floor.cell ("road", 1,2);

		floor.cell ("road", 3,2);
		floor.cell ("road", 4,2);
		floor.cell ("road", 5,2);
		floor.cell ("road", 6,2);


		floor.cell ("road", 0,3);
		floor.cell ("road", 1,3);
		floor.cell ("road", 2,3);
		floor.cell ("road", 3,3);
		floor.cell ("road", 4,3);
		floor.cell ("road", 5,3);
		floor.cell ("road", 6,3);

		floor.cell ("road", 6,4);
		floor.cell ("road", 6,5);
		floor.cell ("road", 6,6);

		floor.cell ("road", 5,5);
		floor.cell ("road", 5,6);


		floor.cell ("road", 4,5);
		floor.cell ("road", 4,6);

		floor.cell ("road", 3,5);
		floor.cell ("road", 3,6);
		floor.cell ("road", 2,5);
		floor.cell ("road", 2,6);
		floor.cell ("road", 1,5);
		floor.cell ("road", 1,6);
		floor.cell ("road", 0,5);
		floor.cell ("road", 0,6);



		factory.add (floor);

		MapBarrier barrier = new MapBarrier ();
		barrier.block("grass");

		factory.add (barrier);
		MapModel model = factory.create();
		factory.render (model);
		saveToFile ("map.vox", model.vs);
	//	floor.build (model);

	}
	[Test]
	public void MyBuilderTest(){

		MapFactory factory = Component.FindObjectOfType<MapFactory> ();
		MapFloor floor = new MapFloor (new VectorInt2(7, 7));
		Assert.NotNull (factory);
		floor.def("grass");

		MapModel model = new MapModel ();
		floor.cell ("road", 1, 1);
		floor.cell ("road", 1, 2);
		floor.cell ("road", 1, 3);
		floor.cell ("road", 2, 2);
		floor.build (model);
		MapBarrier barrier = new MapBarrier ();
		barrier.block("grass");

		floor.build (model);
		barrier.build(model);
		floor.render (model, factory.store);
		barrier.render (model, factory.store);

		Assert.AreEqual ((int)model.barrier [0, 0], (int)(MapStore.Around.Up | MapStore.Around.Right));
		Assert.AreEqual (model.barrier [1, 0], (int)(MapStore.Around.Left|MapStore.Around.Right));
		Assert.AreEqual (model.barrier [2, 0], (int)(MapStore.Around.Left|MapStore.Around.Right|MapStore.Around.Up));
		//saveToFile ("build.vox", model.vs);

		//barrier.build (model, factory.store);
	}
	[Test]
	public void FactoryTest(){
		
		MapFactory factory = Component.FindObjectOfType<MapFactory> ();
	
		Assert.NotNull (factory);
		Debug.Log(factory);
		MapStore store = factory.store;
		store.init ();
		VoxelStruct up = store.getCell ("grass", (int)MapStore.Around.Up);
		VoxelStruct down = store.getCell ("grass",(int)(MapStore.Around.Down));
		VoxelStruct left = store.getCell ("grass", (int)MapStore.Around.Left);
		VoxelStruct right = store.getCell ("grass", (int)MapStore.Around.Right);


		FileStream sw = new FileStream ("coolup.vox", FileMode.Create, FileAccess.Write);

		System.IO.BinaryWriter bw = new System.IO.BinaryWriter (sw); 
		
		VoxelFormater.WriteToMagicaVoxel (up, bw);
		sw.Close ();
		sw = new FileStream ("cooldown.vox", FileMode.Create, FileAccess.Write);
		bw = new System.IO.BinaryWriter (sw); 
		VoxelFormater.WriteToMagicaVoxel (down, bw);
		sw.Close ();


		sw = new FileStream ("coolleft.vox", FileMode.Create, FileAccess.Write);

		bw = new System.IO.BinaryWriter (sw); 

		VoxelFormater.WriteToMagicaVoxel (left, bw);
		sw.Close ();


		sw = new FileStream ("coolright.vox", FileMode.Create, FileAccess.Write);

		bw = new System.IO.BinaryWriter (sw); 

		VoxelFormater.WriteToMagicaVoxel (right, bw);
		sw.Close ();
		/*
		//store.getCell ("grass", MapStore.Around.);
		//factory.add

		/*	//factories.add (new MapSize(8, 8));*/
		MapFloor floor = new MapFloor (new VectorInt2(8, 8));

		floor.def("grass");
		floor.cell ("road", 1, 1);

		MapModel model = new MapModel ();
		MapBarrier barrier = new MapBarrier ();
		floor.build (model);
		barrier.build (model);
		//barrier.analysis (floor.field, "grass");

		//MapStore store =  Component.FindObjectOfType<MapStore> ();

		factory.add (floor);
		factory.add (barrier);
		/*string[,] field=new string[6,6];
		for (int i = 0; i < field.GetLength (0); ++i) {
			for (int j = 0; j < field.GetLength (1); ++j) {
				field[i,j] = "road";
			}
		
		}*/

		MapModel map = factory.create ();
		VoxelStruct mesh = map.vs;
		Assert.IsNotNull (mesh);
		return;

		sw = new FileStream ("mesh.vox", FileMode.Create, FileAccess.Write);

		bw = new System.IO.BinaryWriter (sw); 

		VoxelFormater.WriteToMagicaVoxel (mesh, bw);
		sw.Close ();


		/*factories.add (new MapRoad ());
		factories.add (new MapDoor ());
		MapCtrl map = factories.create ();
		*/

	}

}
