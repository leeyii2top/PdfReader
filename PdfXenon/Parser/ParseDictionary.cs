﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PdfXenon.Standard
{
    public class ParseDictionary : ParseObject
    {
        private List<string> _names;
        private List<ParseObject> _values;
        private Dictionary<string, ParseObject> _dictionary;

        public ParseDictionary(List<string> names, List<ParseObject> values)
        {
            _names = names;
            _values = values;
        }

        public override int Output(StringBuilder sb, int indent)
        {
            BuildDictionary();
            string blank = new string(' ', indent);

            sb.Append("<<");
            indent += 2;

            int index = 0;
            int count = _dictionary.Count;
            foreach (KeyValuePair<string, ParseObject> entry in _dictionary)
            {
                if ((index == 1) && (count == 2))
                    sb.Append(" ");
                else if (index > 0)
                    sb.Append("  ");

                sb.Append($"{entry.Key} ");
                int entryIndent = entry.Key.Length + 1;
                entry.Value.Output(sb, entryIndent);

                if (count > 2)
                {
                    sb.Append("\n");
                    sb.Append(blank);
                }

                index++;
            }

            sb.Append(">>");
            return indent;
        }

        public int Count { get => _names != null ? _names.Count : _dictionary.Count; }

        public bool ContainsName(string name)
        {
            BuildDictionary();
            return _dictionary.ContainsKey(name);
        }

        public Dictionary<string, ParseObject>.KeyCollection Names
        {
            get
            {
                BuildDictionary();
                return _dictionary.Keys;
            }
        }

        public Dictionary<string, ParseObject>.ValueCollection Values
        {
            get
            {
                BuildDictionary();
                return _dictionary.Values;
            }
        }

        public Dictionary<string, ParseObject>.Enumerator GetEnumerator()
        {
            BuildDictionary();
            return _dictionary.GetEnumerator();
        }

        public ParseObject this[string name]
        {
            get
            {
                BuildDictionary();
                return _dictionary[name];
            }

            set
            {
                BuildDictionary();
                _dictionary[name] = value;
            }
        }

        public T OptionalValue<T>(string name) where T : ParseObject
        {
            BuildDictionary();

            ParseObject entry;
            if (_dictionary.TryGetValue(name, out entry))
            {
                if (entry is T)
                    return (T)entry;
                else
                    throw new ApplicationException($"Dictionary entry is type '{entry.GetType().Name}' instead of mandatory type of '{typeof(T).Name}'.");
            }

            return null;
        }

        public T MandatoryValue<T>(string name) where T : ParseObject
        {
            BuildDictionary();

            ParseObject entry;
            if (_dictionary.TryGetValue(name, out entry))
            {
                if (entry is T)
                    return (T)entry;
                else
                    throw new ApplicationException($"Dictionary entry is type '{entry.GetType().Name}' instead of mandatory type of '{typeof(T).Name}'.");
            }
            else
                throw new ApplicationException($"Dictionary is missing mandatory name '{name}'.");
        }

        private void BuildDictionary()
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, ParseObject>();

                int count = Count;
                for(int i=0; i<count; i++)
                    _dictionary.Add(_names[i], _values[i]);

                _names = null;
                _values = null;
            }
        }
    }
}
