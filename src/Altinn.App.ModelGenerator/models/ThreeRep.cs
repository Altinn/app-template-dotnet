using System;
using System.Collections.Generic;
using System.Linq;

namespace Altinn.App.ModelGenerator
{
    public class ThreeRep
    {
        private readonly string Name;
        public readonly Dictionary<string, ThreeRep> dict = new Dictionary<string, ThreeRep>();

        public ThreeRep(string name)
        {
            Name = name;
        }

        public void AddDottedPath(string dottedPath)
        {
            var path = dottedPath.Split(new char[] { '.' }, 2);
            var t = path[0];
            if (!dict.ContainsKey(t))
            {
                dict[t] = new ThreeRep(t);
            }
            if (path.Length == 2)
            {
                var rest = path[1];
                dict[t].AddDottedPath(rest);
                return;
            }
        }
        public string ToPoco()
        {
            var classname = Name;
            var props = dict.Select(element => $"\n\tpublic {(element.Value.dict.Count == 0 ? "string" : element.Key)}? {element.Key} {{ get; set; }}").ToList();
            if (props.Count > 0)
                return $"public partial class {classname}\n{{{string.Join("", props)}\n}}\n\n" + string.Join("\n\n", dict.Values.Select(tr => tr.ToPoco()));

            return "";
        }
    }
}