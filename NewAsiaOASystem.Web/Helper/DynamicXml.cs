using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NewAsiaOASystem.Web.Helper
{
    public class DynamicXml: DynamicObject, IEnumerable
    {
        private readonly List<XElement> _elements;

        public DynamicXml(string text)
        {
            XDocument document = XDocument.Parse(text);
            this._elements = new List<XElement> { document.Root };
        }

        protected DynamicXml(IEnumerable<XElement> elements)
        {
            this._elements = new List<XElement>(elements);
        }

        protected DynamicXml(XElement element)
        {
            this._elements = new List<XElement> { element };
        }

        public IEnumerator GetEnumerator()
        {
            foreach (XElement iteratorVariable0 in this._elements)
            {
                yield return new DynamicXml(iteratorVariable0);
            }
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int num = (int)indexes[0];
            result = new DynamicXml(this._elements[num]);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (binder.Name == "Value")
            {
                result = this._elements[0].Value;
            }
            else if (binder.Name == "Count")
            {
                result = this._elements.Count;
            }
            else
            {
                XAttribute attribute = this._elements[0].Attribute(XName.Get(binder.Name));
                if (attribute != null)
                {
                    result = attribute;
                }
                else
                {
                    IEnumerable<XElement> source = this._elements.Descendants<XElement>(XName.Get(binder.Name));
                    if ((source == null) || (source.Count<XElement>() == 0))
                    {
                        return false;
                    }
                    result = new DynamicXml(source);
                }
            }
            return true;
        }
    }
}