using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blue.Model.BusinessModule;

namespace Blue.Model
{
    /// <summary>
    /// 字段比较类
    /// </summary>
    public class DataFieldComparer : IEqualityComparer<ExtendedCustomDataFieldInfo>
    {
        public bool Equals(ExtendedCustomDataFieldInfo x, ExtendedCustomDataFieldInfo y)
        {

            if (x == null || y == null)
            {
                return false;
            }

            return x.DataFieldId == y.DataFieldId;
        }

        public int GetHashCode(ExtendedCustomDataFieldInfo obj)
        {
            if (obj == null) return 0;

            int nameCode = obj.Name == null ? 0 : obj.Name.GetHashCode();

            return obj.Sorting * 32 + nameCode;
        }
    }
}
