using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSTLReader.Model
{
    /// <summary>
    /// Define triangle face
    /// </summary>
    public class Facet
    {
        /// <summary>
        /// triangle normal vector
        /// </summary>
        public Vector Normal { get; set; }

        /// <summary>
        /// triangle vertices
        /// </summary>
        public Vector[] Vertices { get; set; }

        /// <summary>
        /// attribute(usually not used)
        /// </summary>
        public ushort Attributes { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public Facet()
            : this(new Vector(), new Vector[3])
        {

        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="normal">triangle normal</param>
        /// <param name="vertices">triangle vertices</param>
        /// <param name="attributes">attribute</param>
        public Facet(Vector normal, Vector[] vertices, ushort attributes = 0)
        {
            this.Normal = normal;
            this.Vertices = vertices;
            this.Attributes = attributes;
        }
    }
}
