using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using BytescoutScreenCapturingLib;
using System.Runtime.InteropServices;

namespace PlaybackCapturePOC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string path)
        {
            Capturer capture = new Capturer();
            capture.CapturingType = CaptureAreaType.catScreen;
            capture.OutputFileName = "C:/scrn/SampleVideo.wmv";
            capture.OutputHeight = 800;
            capture.OutputWidth = 1600;

            if (capture.CapturingType != CaptureAreaType.catScreen && capture.CapturingType != CaptureAreaType.catWebcamFullScreen)
            {
                // set border style
                capture.CaptureAreaBorderType = CaptureAreaBorderType.cabtDashed;
                capture.CaptureAreaBorderColor = (uint)ColorTranslator.ToOle(Color.Red);
            }

            capture.Run();
            Thread.Sleep(5000); // Record for 5 seconds; Timeout.Infitite is to set it to infinity value

            capture.Stop(); // stop video capturing

            // Release resources
            Marshal.ReleaseComObject(capture);
            capture = null;

            Process.Start("C:/scrn/SampleVideo.wmv"); //Autoplay the video

            //Capture("C:/scrn/Screenshot.bmp"); For screenshot reference
            return View();
        }

        //This function is for capturing individual screenshots. Placed it for reference but is not really used in the program
        public static void Capture(string CapturedFilePath)
        {
            Bitmap bitmap = new Bitmap
            (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
            graphics.CopyFromScreen(25, 25, 25, 25, bitmap.Size);

            bitmap.Save(CapturedFilePath, ImageFormat.Bmp);  
        }

    }
}
