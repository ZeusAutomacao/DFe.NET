using System.Drawing;
using ZXing;
using ZXing.Common;

namespace GraphicsPrinter
{
    public class QrCode
    {
        public static Image Gerar(string qrCode)
        {
            var bw = new BarcodeWriter();
            var encOptions = new EncodingOptions { Width = 130, Height = 130, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = BarcodeFormat.QR_CODE;
            var imageQrCode = new Bitmap(bw.Write(qrCode));
            return imageQrCode;
        } 
    }
}