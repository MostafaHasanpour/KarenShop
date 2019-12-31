using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace KapilDGImage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // GET api/values  
        [HttpGet("{uri1}/{uri2?}/{uri3?}/{uri4?}/{uri5?}/{uri6?}/{uri7?}/{uri8?}")]
        public async Task<IActionResult> Get(string uri1, string uri2, string uri3, string uri4, string uri5, string uri6, string uri7, string uri8)
        {
            return await Task.Factory.StartNew(() =>
            {
                Byte[] b;
                var uri = $"{uri1}/{uri2}/{uri3}/{uri4}/{uri5}/{uri6}/{uri7}/{uri8}".Trim('/');
                try
                {
                    b = System.IO.File.ReadAllBytes("F:\\ShopProject\\pic\\" + string.Join('/', uri));
                }
                catch (Exception)
                {
                    b = System.IO.File.ReadAllBytes("F:\\ShopProject\\pic\\NoImage.jpg");
                }


                return File(b, "image/jpeg");
            });
        }

        public Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size  
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background  
            drawing.Clear(backColor);

            //create a brush for the text  
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
        public async Task<byte[]> ImageToByteArray(System.Drawing.Image imageIn)
        {
            return await Task.Factory.StartNew(() =>
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            });
        }
    }
}