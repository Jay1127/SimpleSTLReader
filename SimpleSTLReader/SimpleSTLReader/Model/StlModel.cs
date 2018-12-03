using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSTLReader.Model
{
    /// <summary>
    /// after reading stl file, store data
    /// </summary>
    public class StlModel
    {
        /// <summary>
        /// triangle faces in stl format
        /// </summary>
        public List<Facet> Facets;

        /// <summary>
        /// vertices in stl format
        /// </summary>
        public List<Vector> Vertices
        {
            get
            {
                return Facets.SelectMany(facet => facet.Vertices).ToList();
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public StlModel()
        {
            Facets = new List<Facet>();
        }
    }
}
