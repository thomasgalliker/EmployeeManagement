using System.ServiceModel.Channels;
using System.Xml;

namespace Employee.Service.MessageFormatters
{
    class RawBodyWriter : BodyWriter
    {
        private readonly byte[] content;
        public RawBodyWriter(byte[] content)
            : base(true)
        {
            this.content = content;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteStartElement("Binary");
            writer.WriteBase64(this.content, 0, this.content.Length);
            writer.WriteEndElement();
        }
    }
}

