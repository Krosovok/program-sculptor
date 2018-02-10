using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace UI.Content
{
    public class DocumentBuilder
    {
        private readonly FlowDocument document = new FlowDocument();

        public FlowDocument Document => document;

        public DocumentBuilder AddContent(Stream contentStream, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            string content = Content(contentStream, encoding);

            return AddContent(content);
        }

        public DocumentBuilder AddContent(string content)
        {
            document.Blocks.Add(new Paragraph(new Run(content)));
            return this;
        }

        private static string Content(Stream contentStream, Encoding encoding)
        {
            byte[] buffer = ReadBytes(contentStream);
            string content = encoding.GetString(buffer);
            return content;
        }

        private static byte[] ReadBytes(Stream contentStream)
        {
            byte[] buffer = new byte[contentStream.Length];
            contentStream.Read(buffer, 0, (int) contentStream.Length);
            return buffer;
        }
    }
}
