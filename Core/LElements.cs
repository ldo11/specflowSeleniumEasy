using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luatsqa.coreauto
{
    public class Elements
    {
        private readonly IList<IWebElement> _elements;

        public Elements(IList<IWebElement> list)
        {
            _elements = list;
        }

        public By FoundBy { get; set; }

        public bool IsEmpty => Count == 0;

        public int Count => _elements.Count;
    }
}
