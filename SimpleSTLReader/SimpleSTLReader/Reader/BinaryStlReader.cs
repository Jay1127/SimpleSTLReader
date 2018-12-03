using SimpleSTLReader.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSTLReader.Reader
{
    /// <summary>
    /// Binary stl format reader class
    /// </summary>
    class BinaryStlReader
    {
        /// <summary>
        /// read stl format
        /// </summary>
        /// <param name="stream">stl file stream</param>
        /// <returns>stl data</returns>
        public StlModel Read(Stream stream)
        {
            var model = new StlModel();

            using (var reader = new BinaryReader(stream, Encoding.ASCII))
            {
                // skip header
                reader.ReadBytes(80);

                uint triangleCount = reader.ReadUInt32();

                for (int i = 0; i < triangleCount; i++)
                {
                    var facet = ReadFacet(reader);
                    model.Facets.Add(facet);
                }
            }

            return model;
        }

        /// <summary>
        /// read triangle face
        /// </summary>
        /// <param name="reader">stl stream</param>
        /// <returns>triangle face</returns>
        private Facet ReadFacet(BinaryReader reader)
        {
            var normal = ReadPoint(reader);
            var vertices = ReadVertices(reader);
            var attribute = reader.ReadUInt16();

            return new Facet(normal, vertices, attribute);
        }

        /// <summary>
        /// read triangle vertices
        /// </summary>
        /// <param name="reader">stl stream</param>
        /// <returns>triangle vertices</returns>
        private Vector[] ReadVertices(BinaryReader reader)
        {
            return Enumerable.Range(0, 3)
                             .Select(o => ReadPoint(reader))
                             .ToArray(); ;
        }

        /// <summary>
        /// read point
        /// </summary>
        /// <param name="reader">stl stream</param>
        /// <returns>point3d</returns>
        private Vector ReadPoint(BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float z = reader.ReadSingle();

            return new Vector(x, y, z);
        }
    }
}
