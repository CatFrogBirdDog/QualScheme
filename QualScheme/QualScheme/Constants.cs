namespace QualScheme
{
    public class Constants
    {
#if RELEASE
        public static string imagePath = ".//ChemLabImages";
#elif DEBUG
        public static string imagePath = "..//..//ChemLabImages//";
#endif

        public string getImgPath() { return imagePath;  }
    }
}
