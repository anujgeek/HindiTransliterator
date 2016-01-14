using System.Windows;

namespace HindiTransliterator
{
    public partial class MainWindow : Window
    {
        char[] HalfChar = new char[] { '\u0043', '\u0048', '\u0050', '\u00B6', '\u0058', '\u003F', '\u00BA', '\u0054', '\u00F7', '\u0044', '\u0059', '\u0045', '\u0055', '\u0049', '\u004C', '\u0052', '\u004F', '\u005B', '\u002E', '\u0022', '\u007B', '\u00B8', '\u0046', '\u0027', '\u002F', '\u0178', '\u00DC', '\u00AB', '\u00D9' };
        char[] WholeChar = new char[] { '\u0063', '\u00D2', '\u0070', '\u004E', '\u006E', '\u004D', '\u0051', '\u0078', '\u00C4', '\u0067', '\u0074', '\u003E', '\u0064', '\u0079', '\u0047', '\u0065', '\u0075', '\u0069', '\u006A', '\u006C', '\u0072', '\u0056', '\u006F', '\u007D', '\u005F', '\u007C', '\u00E6', '\u00F9', '\u00E3', '\u00F6', '\u004B', '\u00CC', '\u2219', '\u00E2', '\u00E1', '\u00D8', '\u00A3', '\u003D', '\u0076', '\u002C', '\u0062', '\u00C3', '\u006D', '\u00C5', '\u0023', '\u003A', '\u004A' };

        char[] Spaces = new char[] { '\u0090', '\u0020' };

        char[] Matra = new char[]
        {                                   '\u002B',   //Down Bindu
                                            '\u007A',   //r
                                            '\u0060',   //R

                                            '\u006B',   //Danda
                                            '\u0073',   //e
                                            '\u0053',   //E
                                            '\u0066',   //i
                                            '\u0068',   //I
                                            '\u00A8',   //o
                                            '\u00A9',   //O
                                            '\u0071',   //u
                                            '\u0077',   //U

                                            '\u005A',   //Any Char + r
                                            '\u00B1',   //Any char + r + Bindu
                                            '\u0061',   //Bindu
                                            '\u0057',   //Chand
                                            '\u00A1'    //ChandraBindu
        };

        char[] MatraWOi = new char[]
        {                                   '\u002B',   //Down Bindu
                                            '\u007A',   //r
                                            '\u0060',   //R

                                            '\u006B',   //Danda
                                            '\u0073',   //e
                                            '\u0053',   //E
                                            '\u0068',   //I (No small i matra in middle)
                                            '\u00A8',   //o
                                            '\u00A9',   //O
                                            '\u0071',   //u
                                            '\u0077',   //U

                                            '\u005A',   //Any Char + r
                                            '\u00B1',   //Any char + r + Bindu
                                            '\u0061',   //Bindu
                                            '\u0057',   //Chand
                                            '\u00A1'    //ChandraBindu
        };

        char[] BeforeMatra = new char[]
        {                                   '\u002B',   //Down Bindu
                                            '\u007A',   //r
                                            '\u0060'    //R                                            
        };

        char[] MiddleMatra = new char[]
        {                                   '\u006B',   //Danda
                                            '\u0073',   //e
                                            '\u0053',   //E
                                            '\u0066',   //i
                                            '\u0068',   //I
                                            '\u00A8',   //o
                                            '\u00A9',   //O
                                            '\u0071',   //u
                                            '\u0077'    //U
        };

        char[] MiddleMatraWOi = new char[]
        {                                   '\u006B',   //Danda
                                            '\u0073',   //e
                                            '\u0053',   //E
                                            '\u0068',   //I (No small i matra in middle)
                                            '\u00A8',   //o
                                            '\u00A9',   //O
                                            '\u0071',   //u
                                            '\u0077'    //U
        };

        char[] AfterMatra = new char[]
        {                                   '\u005A',   //Any Char + r
                                            '\u00B1',   //Any char + r + Bindu
                                            '\u0061',   //Bindu
                                            '\u0057',   //Chand
                                            '\u00A1'    //ChandraBindu
        };
    }
}
