using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuanLiKhachSan.Model.MappingClass
{
    public class MappingManager
    {
        private List<MappingRule> rules = new List<MappingRule>();
        public bool AddRule(string[] sourceAttributeNames, string[] targetAttributeNames, MappingEngine engine)
        {
            return AddRule(new MappingRule(sourceAttributeNames, targetAttributeNames, engine));
        }

        public bool AddRule(string[] sourceAttributeNames, string[] targetAttributeNames, string strEngineName)
        {
            MappingEngine engine = MappingEngine.FromName(strEngineName);
            return AddRule(new MappingRule(sourceAttributeNames, targetAttributeNames, engine));
        }



        private bool AddRule(MappingRule mappingRule)
        {
            rules.Add(mappingRule);
            return true;
        }

        //public bool MapObjectToObject(MyObject sourceObject, MyObject targetObject)
        //{
        //    try
        //    {
        //        foreach (MappingRule rule in rules)
        //        {
        //            if (rule.CanApply(sourceObject, targetObject))
        //                rule.ExecuteMappingRule(sourceObject, targetObject);
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        internal void LoadRulesFromFile(string strFileName)
        {
            StreamReader sr = new StreamReader(strFileName);

            int nRules = ReadInteger(sr);
            for (int i = 0; i < nRules; i++)
            {
                MappingRule rule;
                rule = LoadOneRule(sr);
                AddRule(rule);
            }


            sr.Close();
        }

        private MappingRule LoadOneRule(StreamReader sr)
        {
            string[] sourceAttributeNames = ReadAttributeNames(sr);
            string[] targetAttributeNames = ReadAttributeNames(sr);
            string strEngineName = sr.ReadLine();
            MappingRule rule = new MappingRule(
                sourceAttributeNames,
                targetAttributeNames,
                MappingEngine.FromName(strEngineName));

            return rule;
        }

        private string[] ReadAttributeNames(StreamReader sr)
        {
            int nAttributes = ReadInteger(sr);
            string[] res = new string[nAttributes];
            for (int i = 0; i < nAttributes; i++)
                res[i] = sr.ReadLine();
            return res;
        }

        private int ReadInteger(StreamReader sr)
        {
            string s;
            s = sr.ReadLine();
            return int.Parse(s);
        }

        public bool MapObjectToObject<U, V>(U sourceObject, V targetObject)
        {
            try
            {
                foreach (MappingRule rule in rules)
                {
                    //if (rule.CanApply(sourceObject, targetObject))
                    //    rule.ExecuteMappingRule(sourceObject, targetObject);
                    rule.ExecuteMappingRule(sourceObject, targetObject);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}