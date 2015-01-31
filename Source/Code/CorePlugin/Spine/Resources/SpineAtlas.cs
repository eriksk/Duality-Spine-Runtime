using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Duality;
using OpenTK;
using Spine;

namespace Duality_Spine_Runtime.Spine.Resources
{
    [Serializable]
    public class SpineAtlas : Resource
    {
        public new static string FileExt = Resource.GetFileExtByType(typeof (SpineAtlas));
        
        [NonSerialized]
        private Atlas _atlas;

        public Atlas Atlas
        {
            get
            {
                if (_atlas == null)
                    ReloadFromText();

                return _atlas;
            }
            set { _atlas = value; }
        }

        public string AtlasDataFile { get; set; }

        public void LoadFile(string srcFile)
        {
            SourcePath = srcFile;
            Reload();
        }

        private void ReloadFromText()
        {
            Vector2 size = GetFileFromDataFile(AtlasDataFile);
            Atlas = new Atlas(new StringReader(AtlasDataFile), System.IO.Path.GetDirectoryName(SourcePath), new DualityTextureLoader(size));
        }

        private void Reload()
        {
            AtlasDataFile = File.ReadAllText(SourcePath);

            Vector2 size = GetFileFromDataFile(AtlasDataFile);
            Atlas = new Atlas(SourcePath, new DualityTextureLoader(size));
        }

        private Vector2 GetFileFromDataFile(string content)
        {
            var line = content.Split('\n')[2].Remove(0, "size:".Length);
            float x = int.Parse(line.Split(',').First().Trim());
            float y = int.Parse(line.Split(',').Last().Trim());
            return new Vector2(x, y);
        }
    }
}
