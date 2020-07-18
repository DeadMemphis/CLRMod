//using System;
//using UnityEngine;

//namespace RedSkies
//{
//    // Token: 0x02000015 RID: 21
//    public static class GUITexture
//    {
//        // Token: 0x060000A3 RID: 163 RVA: 0x00005104 File Offset: 0x00003304
//        static GUITexture()
//        {
//            GUITexture.Red.SetPixel(0, 0, Color.red);
//            GUITexture.Red.Apply();
//            GUITexture.Orange = new Texture2D(1, 1, 5, false);
//            GUITexture.Orange.SetPixel(0, 0, Color.Lerp(Color.red, Color.yellow, 0.5f));
//            GUITexture.Orange.Apply();
//            GUITexture.Yellow = new Texture2D(1, 1, 5, false);
//            GUITexture.Yellow.SetPixel(0, 0, Color.yellow);
//            GUITexture.Yellow.Apply();
//            GUITexture.Green = new Texture2D(1, 1, 5, false);
//            GUITexture.Green.SetPixel(0, 0, Color.green);
//            GUITexture.Green.Apply();
//            GUITexture.Blue = new Texture2D(1, 1, 5, false);
//            GUITexture.Blue.SetPixel(0, 0, new Color(0.08f, 0.3f, 0.4f, 1f));
//            GUITexture.Blue.Apply();
//            GUITexture.Purple = new Texture2D(1, 1, 5, false);
//            GUITexture.Purple.SetPixel(0, 0, Color.Lerp(Color.blue, Color.red, 0.5f));
//            GUITexture.Purple.Apply();
//            GUITexture.White = new Texture2D(1, 1, 5, false);
//            GUITexture.White.SetPixel(0, 0, Color.white);
//            GUITexture.White.Apply();
//            GUITexture.PitchBlack = new Texture2D(1, 1, 5, false);
//            GUITexture.PitchBlack.SetPixel(0, 0, Color.black);
//            GUITexture.PitchBlack.Apply();
//            GUITexture.Black = new Texture2D(1, 1, 5, false);
//            GUITexture.Black.SetPixel(0, 0, Color.Lerp(Color.gray, Color.black, 0.8f));
//            GUITexture.Black.Apply();
//            GUITexture.Gray = new Texture2D(1, 1, 5, false);
//            GUITexture.Gray.SetPixel(0, 0, Color.gray);
//            GUITexture.Gray.Apply();
//            GUITexture.DarkGray = new Texture2D(1, 1, 5, false);
//            GUITexture.DarkGray.SetPixel(0, 0, Color.Lerp(Color.gray, Color.black, 0.75f));
//            GUITexture.DarkGray.Apply();
//            GUITexture.Clear = new Texture2D(1, 1, 5, false);
//            GUITexture.Clear.SetPixel(0, 0, Color.clear);
//            GUITexture.Clear.Apply();
//            GUITexture.Magenta = new Texture2D(1, 1, 5, false);
//            GUITexture.Magenta.SetPixel(0, 0, Color.magenta);
//            GUITexture.Magenta.Apply();
//        }

//        // Token: 0x04000058 RID: 88
//        public static readonly Texture2D Red = new Texture2D(1, 1, 5, false);

//        // Token: 0x04000059 RID: 89
//        public static readonly Texture2D Orange;

//        // Token: 0x0400005A RID: 90
//        public static readonly Texture2D Yellow;

//        // Token: 0x0400005B RID: 91
//        public static readonly Texture2D Green;

//        // Token: 0x0400005C RID: 92
//        public static readonly Texture2D Blue;

//        // Token: 0x0400005D RID: 93
//        public static readonly Texture2D Purple;

//        // Token: 0x0400005E RID: 94
//        public static readonly Texture2D White;

//        // Token: 0x0400005F RID: 95
//        public static readonly Texture2D PitchBlack;

//        // Token: 0x04000060 RID: 96
//        public static readonly Texture2D Black;

//        // Token: 0x04000061 RID: 97
//        public static readonly Texture2D Gray;

//        // Token: 0x04000062 RID: 98
//        public static readonly Texture2D DarkGray;

//        // Token: 0x04000063 RID: 99
//        public static readonly Texture2D Clear;

//        // Token: 0x04000064 RID: 100
//        public static readonly Texture2D Magenta;
//    }
//}
