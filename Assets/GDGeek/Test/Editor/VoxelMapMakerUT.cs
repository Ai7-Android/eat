using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using GDGeek;
using System.IO;

public class VoxelMapMakerUT {
	[Test]
	public void SpliceVoxelTest(){

		FileStream sr2 = new FileStream ("fly2.vox", FileMode.OpenOrCreate, FileAccess.Read);


		System.IO.BinaryReader br2 = new System.IO.BinaryReader (sr2); 

		VoxelStruct vs = VoxelFormater.ReadFromMagicaVoxel (br2);

		sr2.Close ();
		SpliceVoxel splice = new SpliceVoxel ();
		splice.addVoxel(vs, new VectorInt3(0, 0, 0));
		splice.addVoxel(vs, new VectorInt3(10, 10, 10));
		VoxelStruct vs2 = splice.spliceAll ();

		FileStream sw = new FileStream ("fly3.vox", FileMode.Create, FileAccess.Write);

		System.IO.BinaryWriter bw = new System.IO.BinaryWriter (sw); 
		VoxelFormater.WriteToMagicaVoxel (vs2, bw);
		sw.Close ();
		/*

		Assert.AreEqual(vs.main.name, vs2.main.name);
		Assert.AreEqual(vs.main.size, vs2.main.size);
		Assert.AreEqual(vs.main.chunks, vs2.main.chunks);


		Assert.AreEqual(vs.size.box, vs2.size.box);
		Assert.AreEqual(vs.size.name, vs2.size.name);
		Assert.AreEqual(vs.size.size, vs2.size.size);
		Assert.AreEqual(vs.size.chunks, vs2.size.chunks);
		*/

		//Assert.AreEqual(vs.rgba.palette.Length, vs2.rgba.palette.Length);
		/*for (int i = 0; i < vs.rgba.palette.Length; ++i) {
			Assert.AreEqual(vs.rgba.palette[i], vs2.rgba.palette[i]);
		}*/
		/*//		Debug.Log (vs2.rgba.palette.Length);
		Assert.AreEqual(vs.rgba.name, vs2.rgba.name);
		Assert.AreEqual(vs.rgba.size, vs2.rgba.size);
		Assert.AreEqual(vs.rgba.chunks, vs2.rgba.chunks);
*/

	}

	[Test]
	public void VoxelMapMakerTest(){/*
		VoxelMapMaker vmm = Component.FindObjectOfType<VoxelMapMaker> ();
		VoxelStruct map = vmm.building ();
		FileStream sw = new FileStream ("map.vox", FileMode.Create, FileAccess.Write);
		System.IO.BinaryWriter bw = new System.IO.BinaryWriter (sw); 
		VoxelFormater.WriteToMagicaVoxel (map, bw);
		sw.Close ();

		Debug.Log (vmm);*/
	}
    [Test]
	public void VoxelFormaterTest()
    {
        //Arrange
       // var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
       // var newGameObjectName = "My game object";
        //gameObject.name = newGameObjectName;

		//VoxelWriter writer = gameObject.GetComponent<VoxelWriter> ();
		//VoxelFormater formater = gameObject.GetComponent<VoxelFormater> ();

		FileStream sr = new FileStream (".//Assets//Voxel//chr_cop2.bytes", FileMode.OpenOrCreate, FileAccess.Read);


		System.IO.BinaryReader br = new System.IO.BinaryReader (sr); 


		VoxelStruct vs = VoxelFormater.ReadFromMagicaVoxel (br);


		FileStream sw = new FileStream ("fly2.vox", FileMode.Create, FileAccess.Write);

		System.IO.BinaryWriter bw = new System.IO.BinaryWriter (sw); 
		VoxelFormater.WriteToMagicaVoxel (vs, bw);


		sw.Close ();
		sr.Close ();

		FileStream sr2 = new FileStream ("fly2.vox", FileMode.OpenOrCreate, FileAccess.Read);

	
		System.IO.BinaryReader br2 = new System.IO.BinaryReader (sr2); 

		VoxelStruct vs2 = VoxelFormater.ReadFromMagicaVoxel (br2);

		Assert.AreEqual(vs.main.name, vs2.main.name);
		Assert.AreEqual(vs.main.size, vs2.main.size);
		Assert.AreEqual(vs.main.chunks, vs2.main.chunks);


		Assert.AreEqual(vs.size.box, vs2.size.box);
		Assert.AreEqual(vs.size.name, vs2.size.name);
		Assert.AreEqual(vs.size.size, vs2.size.size);
		Assert.AreEqual(vs.size.chunks, vs2.size.chunks);


		Assert.AreEqual(vs.rgba.palette.Length, vs2.rgba.palette.Length);
		for (int i = 0; i < vs.rgba.palette.Length; ++i) {
			Assert.AreEqual(vs.rgba.palette[i], vs2.rgba.palette[i]);
		}
//		Debug.Log (vs2.rgba.palette.Length);
		Assert.AreEqual(vs.rgba.name, vs2.rgba.name);
		Assert.AreEqual(vs.rgba.size, vs2.rgba.size);
		Assert.AreEqual(vs.rgba.chunks, vs2.rgba.chunks);

		sr2.Close ();

		for (int i = 0; i < VoxelFormater.palette_.Length; ++i) {
			
			ushort s = VoxelFormater.palette_ [i];
			Color c = VoxelFormater.Short2Color (s);
			ushort s2 = VoxelFormater.Color2Short (c);
			Color c2 = VoxelFormater.Short2Color (s2);
			Assert.AreEqual(s, s2);
			Assert.AreEqual(c, c2);

		}


		//Debug.Log ();
		Assert.AreEqual(vs.datas.Length, vs2.datas.Length);
//		Debug.Log (vs2.datas.Length);
		for (int i = 0; i < vs.datas.Length; ++i) {
			Assert.AreEqual(vs.datas[i].color, vs2.datas[i].color);
			Assert.AreEqual(vs.datas[i].x, vs2.datas[i].x);
			Assert.AreEqual(vs.datas[i].y, vs2.datas[i].y);
			Assert.AreEqual(vs.datas[i].z, vs2.datas[i].z);
		}


		Assert.AreEqual (vs.datas.Length, vs.datas.Length);	
    }
}
