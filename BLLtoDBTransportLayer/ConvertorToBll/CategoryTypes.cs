using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using BusinessLogicLayer;

namespace BlltoDalTransport
{
    class CAllCategoryTypes
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private Dictionary<String,CCategoryType> Types { get; set; }

        public CAllCategoryTypes()
        {
            UpdateTypes();
        }

        private void UpdateTypes()
        {
            Types = new Dictionary<string, CCategoryType>();

            IDataSupplier dataSupplier = new CDataSupplierDB();
            List<CCategoryType> tps = dataSupplier.GetCategoryTypes();

            Types = new Dictionary<String, CCategoryType>();
            foreach (CCategoryType tp in tps)
                Types.Add(tp.Title, tp);
        }

        public List<CCategoryType>GetTypes()
        {
            return new List<CCategoryType>(Types.Values);
        }

        public CCategoryType GetTypeByTitle(String title)
        {
            return new CCategoryType(Types[title]);
        }

        public CCategoryType GetByCategoryId(Int32 categoryId)
        {
            foreach (CCategoryType type in Types.Values)
                if (type.CategoryID == categoryId)
                    return type;

            log.Warn($"Category Type with Id = {categoryId} not found!");
            return null;
        }
    }
}
