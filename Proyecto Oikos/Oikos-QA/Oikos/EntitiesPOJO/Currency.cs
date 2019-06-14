using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class Currency:BaseEntity
    {
        public string CodeId;
        public string ExternalCode;
        public string CodeName;

        public Currency()
        {
      
        }

        public Currency(string codeId, string externalCode, string codeName)
        {
            CodeId = codeId;
            ExternalCode = externalCode;
            CodeName = codeName;
        }
    }
}
