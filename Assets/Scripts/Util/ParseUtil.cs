using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class ParseUtil
{
    public static Type ParseType(string typeName)
    {
        Type ret = null;
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; ++i)
        {
            var types = assemblies[i].GetTypes();
            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.Name.Equals(typeName))
                    {
                        ret = type;
                        break;
                    }
                }
                if (ret != null)
                    break;
            }
        }
        return ret;
    }

    public static List<int> ParseIntList(string _value, char _split = '|')
    {
        List<int> ret = new List<int>();

        if (string.IsNullOrEmpty(_value))
            return ret;

        string[] arr = _value.Split(_split);


        for (int i = 0; i < arr.Length; i++)
        {
            ret.Add(int.Parse(arr[i]));
        }

        return ret;
    }

    public static List<string> ParseStrList(string _value, char _split = '|')
    {
        if (string.IsNullOrEmpty(_value))
            return new List<string>();

        return _value.Split(_split).ToList();
    }
}
