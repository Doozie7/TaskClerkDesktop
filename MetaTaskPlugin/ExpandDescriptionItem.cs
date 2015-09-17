using System;
using System.Xml.Serialization;

namespace MetaTaskPlugin
{
    [Serializable]
    public class ExpandDescriptionItem
    {

        private Guid _taskDescriptionId = Guid.Empty;
        private int _percent;
        
        public ExpandDescriptionItem()
        {
        }

        public ExpandDescriptionItem(Guid taskDescriptionId, int percent)
        {
            this._taskDescriptionId = taskDescriptionId;
            this._percent = percent;
        }

        [XmlAttribute(AttributeName="taskDescriptionId")]
        public Guid TaskDescriptionId
        {
            get 
            {
                return _taskDescriptionId; 
            }
            set 
            { 
                _taskDescriptionId = value; 
            }
        }

        [XmlAttribute(AttributeName = "percent")]
        public int Percent
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
            }
        }
    }
}
