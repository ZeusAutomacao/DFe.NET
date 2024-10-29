using System.Drawing;
using ZXing;
using ZXing.Common;

namespace NFe.Danfe.Nativo.GraphicsPrinter
{
    public static class QrCode
    {
        public static Image Gerar(string qrCode)
        {
            var bw = new ZXing.Windows.Compatibility.BarcodeWriter();
            EncodingOptions encOptions = new EncodingOptions { Width = 130, Height = 130, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = BarcodeFormat.QR_CODE;
            Bitmap imageQrCode = bw.Write(qrCode);
            return imageQrCode;
        } 
    }
}