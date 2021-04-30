using HartmannPractical.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HartmannPractical
{
    public class ByteSerializer : IByteSerializer    
    {
        public byte[] Serialize<T>(T input) where T : SampleData
        {
            List<byte> ret = new List<byte>();
            SortedList<int, PropertyInfo> memo = new SortedList<int, PropertyInfo>();
            Type type = input.GetType();
            var properties = type.GetProperties();

            if (properties != null)
            {
                foreach(var propertyInfo in properties)
                {
                    OrderAttribute orderInfo = propertyInfo.GetCustomAttribute(typeof(OrderAttribute)) as OrderAttribute;
                    if(orderInfo!=null)
                    {
                        memo.Add(orderInfo.Position, propertyInfo);
                    }
                }

                foreach(var t in memo)
                {
                    var property = t.Value.GetValue(input);
                    switch (property)
                    {
                        case DateTime d:
                            ret.AddRange(DateTimeToBytes(d));
                            break;
                        case string s:
                            //get max length that specified in attribute
                            var strAtt = t.Value.GetCustomAttribute(typeof(StringLengthAttribute)) as StringLengthAttribute;
                            ret.AddRange(StrToBytes(s, strAtt.MaximumLength));
                            break;
                        case int i:
                            ret.AddRange(IntToBytes(i));
                            break;
                        case SampleFlags f:
                            ret.AddRange(FlagsToBytes(f));
                            break;
                    }
                }
                
            }
            if(!BitConverter.IsLittleEndian)
            {
                ret.Reverse();
            }

            return ret.ToArray();

        }
        private byte[] StrToBytes(string str,int length)
        {
            byte[] byteArray = new byte[length];
            var bytes = Encoding.ASCII.GetBytes(str);
            if(bytes.Length<length)
            {
                bytes.CopyTo(byteArray, 0);
                return byteArray; 
            }
            return bytes;
        }
        private byte[] DateTimeToBytes(DateTime dateTime)
        {
            return BitConverter.GetBytes(dateTime.Ticks);
        }
        private byte[] IntToBytes(int integer)
        {
            return BitConverter.GetBytes(integer);
        }
        private byte[] FlagsToBytes(SampleFlags test)
        {
            return BitConverter.GetBytes((int)test);
        }
    }
}
