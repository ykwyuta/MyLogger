using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ScreenCapture
{
    public void CaptureAllScreensToFile(string outputFilePath = ".", int scale = 100)
    {
        var fixedScale = (Rectangle rectangle) => {
            return new Rectangle(rectangle.X * scale / 100, rectangle.Y * scale / 100, rectangle.Width * scale / 100, rectangle.Height * scale / 100);
        };
        foreach (Screen screen in Screen.AllScreens)
        {
            var rect = fixedScale(screen.Bounds);
            // 画面の名前や解像度などの情報を含むファイル名を作成
            string fileName = String.Format("{0}/Screen_{1}_{2}_{3}x{4}.png", outputFilePath, screen.DeviceName.Replace("\\\\.\\DISPLAY", ""), DateTime.Now.ToString("yyyyMMddHHmmss"), rect.Width, rect.Height);
            
            using (var bitmap = new Bitmap(rect.Width, rect.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    // 特定の画面からスクリーンショットを取得
                    g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
                }
                bitmap.Save(fileName, ImageFormat.Png); // スクリーンショットをファイルに保存
            }
        }
    }
}

