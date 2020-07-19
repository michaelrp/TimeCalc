namespace TimeCalc.Utils
{
    public class CubeIcons
    {
        public static string Css(string puzzle)
        {
            var iconCss = "";

            switch (puzzle)
            {
                case "3x3":
                    iconCss = "cubing-icon event-333";
                    break;
                case "2x2":
                    iconCss = "cubing-icon event-222";
                    break;
                case "4x4":
                    iconCss = "cubing-icon event-444";
                    break;
                case "5x5":
                    iconCss = "cubing-icon event-555";
                    break;
                case "6x6":
                    iconCss = "cubing-icon event-666";
                    break;
                case "7x7":
                    iconCss = "cubing-icon event-777";
                    break;
                case "3x3 BLD":
                    iconCss = "cubing-icon event-333bf";
                    break;
                case "3x3 OH":
                    iconCss = "cubing-icon event-333oh";
                    break;
                case "Clock":
                    iconCss = "cubing-icon event-clock";
                    break;
                case "Megaminx":
                    iconCss = "cubing-icon event-minx";
                    break;
                case "Pyraminx":
                    iconCss = "cubing-icon event-pyram";
                    break;
                case "Skewb":
                    iconCss = "cubing-icon event-skewb";
                    break;
                case "Square-1":
                    iconCss = "cubing-icon event-sq1";
                    break;
                case "4x4 BLD":
                    iconCss = "cubing-icon event-444bf";
                    break;
                case "5x5 BLD":
                    iconCss = "cubing-icon event-555bf";
                    break;
                case "3x3 MBL":
                    iconCss = "cubing-icon event-333mbf";
                    break;
            }

            return iconCss;
        }
    }
}