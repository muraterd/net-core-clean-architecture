﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

public static class EnumExtentions
{
    public static string GetDescription<T>(this T source)
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }
}