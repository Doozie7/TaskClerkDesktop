using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MetaTaskPlugin
{
    [Serializable]
    public class DescriptionToExpand
    {
        private string _comment;
        private Guid _descriptionToExpandId;
        private List<ExpandDescriptionItem> _descriptionsUsedInExpand;

        public DescriptionToExpand()
        {
            _comment = String.Empty;
            _descriptionToExpandId = Guid.Empty;
            _descriptionsUsedInExpand = new List<ExpandDescriptionItem>();
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [XmlAttribute(AttributeName = "taskDescriptionId")]
        public Guid DescriptionToExpandId
        {
            get { return _descriptionToExpandId; }
            set { _descriptionToExpandId = value; }
        }

        public List<ExpandDescriptionItem> DescriptionsUsedInExpand
        {
            get { return _descriptionsUsedInExpand; }
            set { _descriptionsUsedInExpand = value; }
        }

        public static DescriptionToExpand Example()
        {
            DescriptionToExpand example = new DescriptionToExpand
            {
                _comment = "Example - Installed to demonstrate the structure of the xml file",
                _descriptionToExpandId = new Guid("{D0ACD727-5E7A-4e47-BC23-E4FFE7989378}")
            };
            example._descriptionsUsedInExpand.Add(new ExpandDescriptionItem(new Guid("{1562BD81-145B-4a8f-A4B2-680A26C254C2}"), 50));
            example._descriptionsUsedInExpand.Add(new ExpandDescriptionItem(new Guid("{39BD7D47-E1A7-41d5-9B5B-D00C66096376}"), 50));
            return example;
        }
    }
}
