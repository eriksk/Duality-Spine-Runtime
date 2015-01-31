using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Duality;

namespace Duality_Spine_Runtime.Spine.Resources
{
    [Serializable]
    public class SkeletonData : Resource
    {
        public new static string FileExt = Resource.GetFileExtByType(typeof(SkeletonData));

        private string _content;

        public string Content
        {
            get { return _content; }
        }
        
        public void LoadFile(string srcFile)
        {
            SourcePath = srcFile;
            Reload();
        }

        private void Reload()
        {
            _content = File.ReadAllText(SourcePath);
        }
    }
}
