using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality_Spine_Runtime.Spine.Utilities
{
    public class VertexCache<TVertexType> where TVertexType : struct
    {
        private readonly int _vertexCountPerGroup;
        private readonly List<TVertexType[]> _vertices;
        private int _currentNode;

        public VertexCache(int vertexCountPerGroup)
        {
            _vertexCountPerGroup = vertexCountPerGroup;
            _vertices = new List<TVertexType[]>();
            _currentNode = 0;
        }

        public void Restart()
        {
            _currentNode = 0;
        }

        public TVertexType[] Next()
        {
            if (_currentNode > _vertices.Count - 1)
            {
                _vertices.Add(new TVertexType[_vertexCountPerGroup]);
            }
            var vertices = _vertices[_currentNode];
            _currentNode++;
            return vertices;
        }
    }
}
