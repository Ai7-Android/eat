using UnityEngine;
using System.Collections;
namespace GDGeek{
	public interface IMapBuilder {

		void build(MapModel map);
		void render(MapModel map, MapStore store);

	}
}
