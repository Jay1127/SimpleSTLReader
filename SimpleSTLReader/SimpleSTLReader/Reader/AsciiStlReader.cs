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
    /// Ascii(text) stl format reader class
    /// </summary>
    class AsciiStlReader
    {
        /// <summary>
        /// read stl format
        /// </summary>
        /// <param name="stream">stl file stream</param>
        /// <returns>stl data</returns>
        public StlModel Read(Stream stream)
        {
            var model = new StlModel();

            using (var reader = new StreamReader(stream))
            {
                // skip header(solid [name])
                reader.ReadLine();

                // read facet loop
                while (!reader.EndOfStream)
                {
                    Facet facet = null;

                    if ((facet = ReadFacet(reader)) == null)
                    {
                        break;
                    }

                    model.Facets.Add(facet);
                }
            }

            return model;
        }

        /// <summary>
        /// read facet
        /// </summary>
        /// <param name="reader">stl file stream</param>
        /// <returns>facet</returns>
        private Facet ReadFacet(StreamReader reader)
        {
            Vector normal = ReadNormal(reader);

            if (normal == null)
            {
                return null;
            }

            Facet facet = new Facet();
            facet.Normal = normal;

            // skip outer loop
            reader.ReadLine();

            // vertex
            facet.Vertices = ReadVertices(reader);

            // skip endloop & endfacet
            reader.ReadLine();
            reader.ReadLine();

            return facet;
        }

        /// <summary>
        /// if line is last, return true
        /// </summary>
        /// <param name="line">file's each line</param>
        /// <returns>last line, return true</returns>
        private bool IsEndLine(string line)
        {
            const string endSequence = "endsolid";
            return line.Contains(endSequence);
        }

        /// <summary>
        /// Read vertices
        /// </summary>
        /// <param name="reader">stl stream</param>
        /// <returns>(triangle) vertices</returns>
        private Vector[] ReadVertices(StreamReader reader)
        {
            return Enumerable.Range(0, 3)
                             .Select(o => ReadPoint(reader.ReadLine()))
                             .ToArray();
        }

        /// <summary>
        /// Read Normal
        /// </summary>
        /// <param name="reader">stl stream</param>
        /// <returns>(triangle) normal</returns>
        private Vector ReadNormal(StreamReader reader)
        {
            string line = string.Empty;

            if (IsEndLine(line = reader.ReadLine()))
            {
                return null;
            }

            return ReadPoint(line);
        }

        /// <summary>
        /// Read point in line
        /// </summary>
        /// <param name="vectorLine">stl line</param>
        /// <returns>point3d</returns>
        private Vector ReadPoint(string vectorLine)
        {
            var vectorInfo = vectorLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var vecLength = vectorInfo.Length;

            var x = float.Parse(vectorInfo[vecLength - 3]);
            var y = float.Parse(vectorInfo[vecLength - 2]);
            var z = float.Parse(vectorInfo[vecLength - 1]);

            return new Vector(x, y, z);
        }
    }
}
