using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Init.Tools.DevExpress
{
    public class PropertyAndValue
    {
        public object Owner { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }

        PropertyInfo _Property;
        public PropertyInfo Property
        {
            get
            {
                if (_Property == null)
                    _Property = Owner.GetType().GetProperties().FirstOrDefault(o => o.Name.ToUpper() == PropertyName.ToUpper());
                return _Property;
            }
        }
    }
}
