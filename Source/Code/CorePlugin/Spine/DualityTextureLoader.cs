using OpenTK;
using Spine;

namespace Duality_Spine_Runtime.Spine
{
    public class DualityTextureLoader : TextureLoader
    {
        private readonly Vector2 _size;

        public DualityTextureLoader(Vector2 size)
        {
            _size = size;
        }

        public void Load(AtlasPage page, string path)
        {
            // We don't really load anything, just set the size of the page. We use a material later anyway-
            page.rendererObject = null;
            page.width = (int)_size.X;
            page.height = (int)_size.Y;

        }

        public void Unload(object texture)
        {
        }
    }
}