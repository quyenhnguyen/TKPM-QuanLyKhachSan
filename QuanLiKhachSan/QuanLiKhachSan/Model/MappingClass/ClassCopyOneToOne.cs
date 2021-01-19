using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model.MappingClass
{
    class ClassCopyOneToOne : MappingEngine
    {
        public override bool Execute<U, V>(U sourceObject, string[] sourceAttributeNames, V targetObject, string[] targetAttributeNames)
        {
            if (sourceAttributeNames.Length != 1)
                return false;
            if (targetAttributeNames.Length != 1)
                return false;
            if (isContainAttribute(sourceObject, sourceAttributeNames[0]) < 0)
                return false;
            if (isContainAttribute(targetObject, targetAttributeNames[0]) < 0)
                return false;
            var sourceTargetAttri = sourceObject.GetType().GetProperty(sourceAttributeNames[0]);//thuộc tính ID
            var value = sourceTargetAttri.GetValue(sourceObject);

            var propTargetAttri = targetObject.GetType().GetProperty(targetAttributeNames[0]);
            propTargetAttri.SetValue(targetObject, value, null);
            return true;


        }
        int isContainAttribute<T>(T sourceObject, string name)
        {
            for (int i = 0; i < sourceObject.GetType().GetProperties().Length; i++)
            {
                var prop = sourceObject.GetType().GetProperties()[i];
                if (prop.Name == name) return i;
            }
            return -1;
        }
    }
}
