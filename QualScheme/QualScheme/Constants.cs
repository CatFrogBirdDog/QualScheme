namespace QualScheme
{
    public class Constants
    {
#if RELEASE
        public static string imagePath = ".//ChemLabImages";
        public static string textPath = ".//Resources//";
#elif DEBUG
        public static string imagePath = "..//..//ChemLabImages//";
        public static string textPath = "..//..//ChemLabTexts//";
#endif
        public string getTxtPath() { return textPath;  }
        public string getImgPath() { return imagePath; }
    }
}
