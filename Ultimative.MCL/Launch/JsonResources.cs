using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL.Launch
{
    public struct AssetObject
    {
        public int FileSize { get; set; }
        public string FileName { get; set; }
        public string HttpUrl { get; set; }
        public string SHA1Code { get; set; }
    }

    public struct Library
    {
        public string Name { get; set; }
        public AssetObject Artifact { get; set; }
        public AssetObject Classifier { get; set; }
    }
}
