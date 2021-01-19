using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLiKhachSan.Model.MappingClass
{
    public class MappingRule
    {
        public string[] sourceAttributeNames;
        public string[] targetAttributeNames;
        public MappingEngine engine;

        public MappingRule(string[] sourceAttributeNames, string[] targetAttributeNames, MappingEngine engine)
        {
            this.sourceAttributeNames = sourceAttributeNames;
            this.targetAttributeNames = targetAttributeNames;
            this.engine = engine;
        }

        //public bool ExecuteMappingRule(MyObject sourceObject, MyObject targetObject)
        //{
        //    //engine.Execute(sourceObject, new string[] { "Last Name", "Middle Name", "First Name" },
        //    //    targetObject, new string[] { "Ho va ten sinh vien" });
        //    return engine.Execute(sourceObject, sourceAttributeNames, targetObject, targetAttributeNames);
        //}

        public bool ExecuteMappingRule<T, U>(T sourceObject, U targetObject)
        {
            //engine.Execute(sourceObject, new string[] { "Last Name", "Middle Name", "First Name" },
            //    targetObject, new string[] { "Ho va ten sinh vien" });
            return engine.Execute(sourceObject, sourceAttributeNames, targetObject, targetAttributeNames);
        }

        //internal bool CanApply(MyObject sourceObject, MyObject targetObject)
        //{
        //    if (!CheckAllAttributeNames(sourceObject, sourceAttributeNames))
        //        return false;
        //    if (!CheckAllAttributeNames(targetObject, targetAttributeNames))
        //        return false;
        //    return true;
        //}

        //private bool CheckAllAttributeNames(MyObject obj, string[] strAttributeNames)
        //{
        //    for (int i = 0; i < strAttributeNames.Length; i++)
        //        if (!obj.ContainAttribute(strAttributeNames[i]))
        //            return false;
        //    return true;
        //}
    }
}