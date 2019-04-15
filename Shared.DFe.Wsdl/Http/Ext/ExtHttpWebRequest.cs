using System.Net;
using System.Text;

namespace DFe.Http.Ext
{
    public static class ExtHttpWebRequest
    {
        public static void ComposeContentType(this HttpWebRequest http, string contentType, Encoding encoding, string action)
        {
            if (encoding == null && action == null)
                http.ContentType = contentType;

            StringBuilder stringBuilder = new StringBuilder(contentType);

            if (encoding != null)
            {
                stringBuilder.Append("; charset=");
                stringBuilder.Append(encoding.WebName);
            }

            if (action != null)
            {
                stringBuilder.Append("; action=\"");
                stringBuilder.Append(action);
                stringBuilder.Append("\"");
            }

            http.ContentType = stringBuilder.ToString();
        }
    }
}