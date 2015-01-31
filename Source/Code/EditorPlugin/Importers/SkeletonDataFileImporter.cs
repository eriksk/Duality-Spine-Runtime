using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Duality;
using Duality.Editor;
using Duality_Spine_Runtime.Spine.Resources;

namespace Duality_Spine_Runtime.Editor.Importers
{
    public class SkeletonDataFileImporter : IFileImporter
    {
        public bool CanImportFile(string srcFile)
        {
            string ext = Path.GetExtension(srcFile).ToLower();
            return ext == ".json" && IsSkeletonFile(srcFile);
        }

        private bool IsSkeletonFile(string srcFile)
        {
            try
            {
                return File.ReadAllText(srcFile).Substring(0, 1024).Contains("skeleton");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string[] GetOutputFiles(string srcFile, string targetName, string targetDir)
        {
            string targetResPath = PathHelper.GetFreePath(Path.Combine(targetDir, targetName), SkeletonData.FileExt);
            return new string[] { targetResPath };
        }

        public void ImportFile(string srcFile, string targetName, string targetDir)
        {
            string[] output = this.GetOutputFiles(srcFile, targetName, targetDir);

            var data = new SkeletonData();
            data.LoadFile(srcFile);
            data.Save(output.First());
        }

        public bool IsUsingSrcFile(ContentRef<Resource> r, string srcFile)
        {
            return r.As<SkeletonData>() != null && r.Res.SourcePath == srcFile;
        }

        public void ReimportFile(ContentRef<Resource> r, string srcFile)
        {
            SkeletonData f = r.Res as SkeletonData;

            if (f != null)
                f.LoadFile(srcFile);
        }
    }
}
