using System;
using System.Collections.Generic;

namespace QuanLiKhachSan.Model.MappingClass
{
    public class MappingEngine
    {

        public virtual bool Execute<U, V>(U sourceObject, string[] sourceAttributeNames, V targetObject, string[] targetAttributeNames)
        {
            return true;
        }

        private static Dictionary<string, MappingEngine> engines = new Dictionary<string, MappingEngine>();

        static MappingEngine()
        {
            engines.Add("DefaultCopyOneToOneOfClass", new ClassCopyOneToOne());

        }
        internal static MappingEngine FromName(string strEngineName)
        {
            return engines[strEngineName];
        }
    }
}