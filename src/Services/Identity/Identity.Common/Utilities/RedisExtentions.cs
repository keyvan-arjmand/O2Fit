using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public static class RedisExtentions
    {
        static readonly string[] nix = new string[0];
        public static string[] ToStringArray(this RedisValue[] values)
        {
            if (values == null) return null;
            if (values.Length == 0) return nix;
            return Array.ConvertAll(values, x => (string)x);
        }
    }
}
