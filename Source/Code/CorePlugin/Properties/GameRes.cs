/*
 * A set of static helper classes that provide easy runtime access to the games resources.
 * This file is auto-generated. Any changes made to it are lost as soon as Duality decides
 * to regenerate it.
 */
namespace GameRes
{
	public static class Data {
		public static class Scenes {
			public static Duality.ContentRef<Duality.Resources.Scene> Scene_Scene { get { return Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Scenes\Scene.Scene.res"); }}
			public static void LoadAll() {
				Scene_Scene.MakeAvailable();
			}
		}
		public static class Trex {
			public static Duality.ContentRef<Duality.Resources.Material> trex_Material { get { return Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Trex\trex.Material.res"); }}
			public static Duality.ContentRef<Duality.Resources.Pixmap> trex_Pixmap { get { return Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Trex\trex.Pixmap.res"); }}
			public static Duality.ContentRef<Duality_Spine_Runtime.Spine.Resources.SkeletonData> trex_SkeletonData { get { return Duality.ContentProvider.RequestContent<Duality_Spine_Runtime.Spine.Resources.SkeletonData>(@"Data\Trex\trex.SkeletonData.res"); }}
			public static Duality.ContentRef<Duality_Spine_Runtime.Spine.Resources.SpineAtlas> trex_SpineAtlas { get { return Duality.ContentProvider.RequestContent<Duality_Spine_Runtime.Spine.Resources.SpineAtlas>(@"Data\Trex\trex.SpineAtlas.res"); }}
			public static Duality.ContentRef<Duality.Resources.Texture> trex_Texture { get { return Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Trex\trex.Texture.res"); }}
			public static void LoadAll() {
				trex_Material.MakeAvailable();
				trex_Pixmap.MakeAvailable();
				trex_SkeletonData.MakeAvailable();
				trex_SpineAtlas.MakeAvailable();
				trex_Texture.MakeAvailable();
			}
		}
		public static void LoadAll() {
			Scenes.LoadAll();
			Trex.LoadAll();
		}
	}

}
