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
    /// stl file reader class
    /// </summary>
    public class StlReader
    {
        /// <summary>
        /// Read stl format
        /// </summary>
        /// <param name="filePath">stl file path</param>
        /// <returns>stl mesh data</returns>
        public StlModel Read(string filePath)
        {
            if (!IsStlFile(filePath))
            {
                throw new ArgumentException($"{filePath} is not stl format.");
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (IsBinaryFile(stream))
                {
                    return new BinaryStlReader().Read(stream);
                }
                else
                {
                    return new AsciiStlReader().Read(stream);
                }
            }
        }

        /// <summary>
        /// if stl file is binary format, return true
        /// </summary>
        /// <param name="stream">stl stream</param>
        /// <returns>binary format, true</returns>
        private bool IsBinaryFile(Stream stream)
        {
            const string asciiFlag = "solid";

            // binary의 헤더가 solid로 시작하는 경우 처리해야함.
            return ReadHeader(stream) != asciiFlag;
        }

        /// <summary>
        /// check stl file format
        /// </summary>
        /// <param name="filePath">checked file path</param>
        /// <returns>stl file, return true</returns>
        private bool IsStlFile(string filePath)
        {
            return Path.GetExtension(filePath) == ".stl";
        }

        /// <summary>
        /// Read header in stl format(header use file format check)
        /// </summary>
        /// <param name="stream">stl stream</param>
        /// <returns>header</returns>
        private string ReadHeader(Stream stream)
        {
            var buffer = new byte[5];

            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);

            return Encoding.ASCII.GetString(buffer);
        }
    }
}
