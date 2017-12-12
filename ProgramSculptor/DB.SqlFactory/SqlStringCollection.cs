using System;
using System.Configuration;

namespace DB.SqlFactory
{
    public sealed class SqlStringCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SqlStringElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SqlStringElement)element).Name;
        }

        public SqlStringElement this[int index]
        {
            get { return (SqlStringElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public SqlStringElement this[string name]
        {
            get
            {
                return (SqlStringElement)BaseGet(name);
            }
            set
            {
                int index = 0;

                SqlStringElement element = (SqlStringElement)BaseGet(name);
                if (element != null)
                {
                    index = BaseIndexOf(element);
                    if (index > -1)
                    {
                        BaseRemoveAt(index);
                    }
                }

                BaseAdd(index, value);
            }
        }
    }
}