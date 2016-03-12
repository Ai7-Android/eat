using UnityEngine;
using System.Collections;
namespace GDGeek{
	public interface IMapBuilder {

		void build(MapModel map, MapStore store);

	}
}
