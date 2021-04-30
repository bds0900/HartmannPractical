using HartmannPractical.Data;

namespace HartmannPractical
{
    public interface IByteSerializer
    {
        /// <summary>
        /// Serializes generic into a byte array
        /// I added contrain on generic type since my inplementation only can convert
        /// types that in SampleData properties
        /// </summary>
        /// <typeparam name="T">Type of object to be serialized</typeparam>
        /// <param name="input">Generic object to serialize</param>
        /// <returns>Byte array representation of input</returns>
        byte[] Serialize<T>(T input) where T : SampleData; 
    }
}
