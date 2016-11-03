namespace QualScheme
{
    public class Constants
    {
#if DEBUG
        public static string imagePath = "..//..//ChemLabImages//";
        public static string textPath = "..//..//ChemLabTexts//";
#else
        public static string imagePath = "ChemLabImages//";
        public static string textPath = "ChemLabTexts//";
#endif
        public string getTxtPath() { return textPath;  }
        public string getImgPath() { return imagePath; }
    }
}
