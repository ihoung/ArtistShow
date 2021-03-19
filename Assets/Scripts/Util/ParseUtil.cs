using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
}
