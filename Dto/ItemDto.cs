using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace Dto
{
    [DataContract]
    public class ItemDto
    {
        public ItemDto()
        {

        }

        public ItemDto(string code,string name,decimal value)
        {
            this.Code = code;
            this.Name = name;
            this.Value = value;
        }

        public string DictCode { get; set; }
        public string DictName { get; set; }
        public string SortID { get; set; }
        public decimal DictValue { get; set; }

        int _id = -1;

        public int ID
        {
            get
            {
                if (_id < 0)
                {
                    int.TryParse(this.Code, out _id);
                }
                return _id;
            }
        }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public bool IsDefault { get; set; }
    }
}
