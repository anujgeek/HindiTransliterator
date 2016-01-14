using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace HindiTransliterator
{
    public partial class MainWindow : Window
    {
        bool IsSpecialCharCombinationsEnabled = true;
        bool IsKeyboardSoundEnglish = false;

        private void rtb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length == 0)
                return;

            string s = e.Text;

            #region KBPronounciation
            if (IsKeyboardSoundEnglish == true)
            {
                switch (s)
                {
                    case "d":
                        {
                            s = "D";
                            break;
                        }
                    case "D":
                        {
                            s = "d";
                            break;
                        }
                    case "t":
                        {
                            s = "T";
                            break;
                        }
                    case "T":
                        {
                            s = "t";
                            break;
                        }
                }
            }
            #endregion

            char PC = this[-1], PPC = this[-2], PPPC = this[-3], PPPPC = this[-4];

            #region Special char combinations
            if (PPPPC == '\u006E' && PPPC == '\u007E' && PPC == '\u0063' && PC == '\u007E' && s == "h")        //d + bh =d+b+h
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(4);
                    InsertChar('\u00F6');
                    InsertChar('\u007E');
                }
                else
                {
                    RemoveChar(2);
                    InsertChar('\u00D2');
                    InsertChar('\u007E');
                }
            }
            else if (PPC == '\u0063' && PC == '\u007E' && s == "h")             //b+h
            {
                RemoveChar(2);
                InsertChar('\u00D2');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0043' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half b + danda +h
            {
                RemoveChar(3);
                InsertChar('\u00D2');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0043' && PC == '\u007E' && s == "h")        //half b  +h
            {
                RemoveChar(2);
                InsertChar('\u00D2');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006A' && PC == '\u007E' && s == "h")        //r+h (No half r)
            {
                RemoveChar(2);
                InsertChar('\u005F');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0072' && PC == '\u007E' && s == "h")        //t+h
            {
                RemoveChar(2);
                InsertChar('\u0046');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0052' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half t + danda +h
            {
                RemoveChar(3);
                InsertChar('\u0046');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0052' && PC == '\u007E' && s == "h")        //half t  +h
            {
                RemoveChar(2);
                InsertChar('\u0046');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPPPC == '\u0042' && PPPC == '\u007E' && PPC == '\u0056' && PC == '\u007E' && s == "h")        //(T+h)+Continuation + (T) +h
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(4);
                    InsertChar('\u00F0');
                    InsertChar('\u007E');
                }
                else
                {
                    RemoveChar(2);
                    InsertChar('\u0042');
                    InsertChar('\u007E');
                }
            }
            else if (PPC == '\u0056' && PC == '\u007E' && s == "h")        //T+h (No half T)
            {
                RemoveChar(2);
                InsertChar('\u0042');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0072' && PC == '\u007E' && s == "t")        //t+t
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00D9');
                    InsertChar('\u006B');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0072');
                }
            }
            else if (PPPC == '\u0052' && PPC == '\u006B' && PC == '\u007E' && s == "t")        //half t + danda +t
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(3);
                    InsertChar('\u00D9');
                    InsertChar('\u006B');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0072');
                }
            }
            else if (PPC == '\u0052' && PC == '\u007E' && s == "t")        //half t  +t
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00D9');
                    InsertChar('\u006B');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0072');
                }
            }
            else if (PPC == '\u0056' && PC == '\u007E' && s == "T")        //T+T (No half T)
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00CD');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0056');
                }
            }
            else if (PPC == '\u00CD' && PC == '\u007E' && s == "h")        //(T+T)+h (No half T)
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00CE');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0067');
                }
            }
            else if (PPPPC == '\u0042' && PPPC == '\u007E' && PPC == '\u0056' && PC == '\u007E' && s == "h")        //(T+h)+Continuation + (T) +h
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(4);
                    InsertChar('\u00F0');
                    InsertChar('\u007E');
                }
                else
                {
                    RemoveChar(2);
                    InsertChar('\u0042');
                    InsertChar('\u007E');
                }
            }
            else if (PPPC == '\u0049' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half p + danda +h
            {
                RemoveChar(3);
                InsertChar('\u0051');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0049' && PC == '\u007E' && s == "h")        //half p  +h
            {
                RemoveChar(2);
                InsertChar('\u0051');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006C' && PC == '\u007E' && s == "h")        //s+h
            {
                RemoveChar(2);
                InsertChar('\u0027');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u004C' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half s + danda +h
            {
                RemoveChar(3);
                InsertChar('\u0027');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u004C' && PC == '\u007E' && s == "h")        //half s  +h
            {
                RemoveChar(2);
                InsertChar('\u0027');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u00CF' && PC == '\u007E' && s == "h")        //D + Dh =(D+D)+h (This comb requires a SpecialChar as PPC)
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00D4');
                    InsertChar('\u007E');
                }
                else
                {
                    RemoveChar(2);
                    InsertChar('\u00D4');
                    InsertChar('\u007E');
                }
            }
            else if (PPC == '\u004D' && PC == '\u007E' && s == "D")        //D+D
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00CF');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertChar('\u004D');
                    InsertChar('\u007E');
                }
            }
            else if (PPC == '\u00CC' && PC == '\u007E' && s == "h")        //dd +h
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u0029');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u0067');
                }
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "d")        //d+d (No half d)
            {
                if (IsSpecialCharCombinationsEnabled == true)
                {
                    RemoveChar(2);
                    InsertChar('\u00CC');
                    InsertChar('\u007E');
                }
                else
                {
                    InsertCharByInput('\u006E');
                }
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "h")        //d+h (No half d)
            {
                RemoveChar(2);
                InsertChar('\u002F');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u004D' && PC == '\u007E' && s == "h")        //D+h (No half D)
            {
                RemoveChar(2);
                InsertChar('\u003C');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0078' && PC == '\u007E' && s == "h")        //g+h
            {
                RemoveChar(2);
                InsertChar('\u00C4');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0058' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half g + danda +h
            {
                RemoveChar(3);
                InsertChar('\u00C4');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0058' && PC == '\u007E' && s == "h")        //half g  +h
            {
                RemoveChar(2);
                InsertChar('\u00C4');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0074' && PC == '\u007E' && s == "h")        //j+h
            {
                RemoveChar(2);
                InsertChar('\u003E');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0054' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half j + danda +h
            {
                RemoveChar(3);
                InsertChar('\u003E');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0054' && PC == '\u007E' && s == "h")        //half j  +h
            {
                RemoveChar(2);
                InsertChar('\u003E');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0064' && PC == '\u007E' && s == "h")        //k+h
            {
                RemoveChar(2);
                InsertChar('\u005B');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0044' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half k + danda +h
            {
                RemoveChar(3);
                InsertChar('\u005B');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0044' && PC == '\u007E' && s == "h")        //half k  +h
            {
                RemoveChar(2);
                InsertChar('\u005B');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0070' && PC == '\u007E' && s == "h")        //c+h
            {
                RemoveChar(2);
                InsertChar('\u004E');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0050' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half c + danda +h
            {
                RemoveChar(3);
                InsertChar('\u004E');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0050' && PC == '\u007E' && s == "h")        //half c  +h
            {
                RemoveChar(2);
                InsertChar('\u004E');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0075' && PC == '\u007E' && s == "h")        //n+h
            {
                RemoveChar(2);
                InsertChar('\u002E');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0055' && PPC == '\u006B' && PC == '\u007E' && s == "h")        //half n + danda +h
            {
                RemoveChar(3);
                InsertChar('\u002E');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0055' && PC == '\u007E' && s == "h")        //half n  +h
            {
                RemoveChar(2);
                InsertChar('\u002E');
                InsertChar('\u006B');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "v")        //d+v (No half d)
            {
                RemoveChar(2);
                InsertChar('\u007D');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "y")        //d+y (No half d)
            {
                RemoveChar(2);
                InsertChar('\u007C');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "r")        //d+r (No half d)
            {
                RemoveChar(2);
                InsertChar('\u00E6');
                InsertChar('\u007E');
            }
            else if (PPC == '\u006E' && PC == '\u007E' && s == "m")        //d+m (No half d)
            {
                RemoveChar(2);
                InsertChar('\u00F9');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0067' && PC == '\u007E' && s == "m")        //h+m
            {
                RemoveChar(2);
                InsertChar('\u00E3');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u00BA' && PPC == '\u006B' && PC == '\u007E' && s == "m")        //half h + danda +m
            {
                RemoveChar(3);
                InsertChar('\u00E3');
                InsertChar('\u007E');
            }
            else if (PPC == '\u00BA' && PC == '\u007E' && s == "m")        //half h  +m
            {
                RemoveChar(2);
                InsertChar('\u00E3');
                InsertChar('\u007E');
            }
            //My special combination
            else if (PPC == '\u0067' && PC == '\u007E' && s == "y")        //h+y
            {
                RemoveChar(2);
                InsertChar('\u00E1');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u00BA' && PPC == '\u006B' && PC == '\u007E' && s == "y")        //half h + danda +y
            {
                RemoveChar(3);
                InsertChar('\u00E1');
                InsertChar('\u007E');
            }
            else if (PPC == '\u00BA' && PC == '\u007E' && s == "y")        //half h  +y
            {
                RemoveChar(2);
                InsertChar('\u00E1');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0072' && PC == '\u007E' && s == "r")        //t+r
            {
                RemoveChar(2);
                InsertChar('\u003D');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0052' && PPC == '\u006B' && PC == '\u007E' && s == "r")        //half t + danda +r
            {
                RemoveChar(3);
                InsertChar('\u003D');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0052' && PC == '\u007E' && s == "r")        //half t  +r
            {
                RemoveChar(2);
                InsertChar('\u003D');
                InsertChar('\u007E');
            }

            //r matra combinations
            else if (PPC == '\u0067' && PC == '\u007E' && s == "r")        //h+r
            {
                RemoveChar(2);
                InsertChar('\u00E2');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u00BA' && PPC == '\u006B' && PC == '\u007E' && s == "r")        //half h + danda +r
            {
                RemoveChar(3);
                InsertChar('\u00E2');
                InsertChar('\u007E');
            }
            else if (PPC == '\u00BA' && PC == '\u007E' && s == "r")        //half h  +r
            {
                RemoveChar(2);
                InsertChar('\u00E2');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0064' && PC == '\u007E' && s == "r")        //k+r
            {
                RemoveChar(2);
                InsertChar('\u00D8');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0044' && PPC == '\u006B' && PC == '\u007E' && s == "r")        //half k + danda +r
            {
                RemoveChar(3);
                InsertChar('\u00D8');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0044' && PC == '\u007E' && s == "r")        //half k  +r
            {
                RemoveChar(2);
                InsertChar('\u00D8');
                InsertChar('\u007E');
            }
            //No whole char for K
            else if (PPPC == '\u005B' && PPC == '\u006B' && PC == '\u007E' && s == "r")        //half K + danda +r
            {
                RemoveChar(3);
                InsertChar('\u00A3');
                InsertChar('\u007E');
            }
            else if (PPC == '\u005B' && PC == '\u007E' && s == "r")        //half K  +r
            {
                RemoveChar(2);
                InsertChar('\u00A3');
                InsertChar('\u007E');
            }
            else if (PPPC == '\u0027' && PPC == '\u006B' && PC == '\u007E' && s == "r")        //half sh + danda + r
            {
                RemoveChar(3);
                InsertChar('\u004A');
                InsertChar('\u007E');
            }
            else if (PPC == '\u0027' && PC == '\u007E' && s == "r")        //half sh + r
            {
                RemoveChar(2);
                InsertChar('\u004A');
                InsertChar('\u007E');
            }
            #endregion

            #region Matras
            /* Matra Logic
             * Note: We cannot prevent the user from doing any grammatical mistakes involving matras or to 
             * insert a matra from the char map with no logic at all. e.g. User might apply two matras of same type to same char.
             * So, in every matra include a logic that if there is a mistake, apply the matra as it is
             * on the current caret position.
             * 
             * 1.   Matras can only be applied to WholeChar.
             * 2.   Three types of matras, one or maore than one of each type, can be applied to a WholeChar.Only AfterChar matra cam be applied
             * in combination. They have to be placed in sequence:
             *      First Before matra, then Middle matra then After matra.
             * Though before matras can be an individual char, they can only be put as part of a WholeChar just following the wholechar.
             * 
             * */
            else if (s == "a" || s == "A")
            {
                if (Spaces.Contains(PC))
                {
                    InsertChar('\u0076');
                }
                else if (PC == '\u007E')
                {
                    RemoveChar(1);
                }
                else
                {
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u006B');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
            }
            else if (s == "e")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u0073');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
                else
                {
                    InsertChar('\u002C');
                }
            }
            else if (s == "E")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u0053');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
                else
                {
                    InsertChar('\u002C');
                    InsertChar('\u0073');
                }
            }
            else if (s == "i")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;

                    while (true)                        //Discarding all matras (including danda for matra or for supporting half char)
                    {
                        if (Matra.Contains(this[-1]))
                            MoveCaretLeft();
                        else
                            break;
                    }

                    PC = this[-1];
                    PPC = this[-2];

                    if (Spaces.Contains(PC))                                                        //This is just to put matra before any other matra
                    {
                        MoveCaretLeft();                                                            //Don't worrry if MoveCaretLeft() is called on first char in document. MoveCaretLeft() checks for null and so it will not move in such cases.
                    }
                    else if (WholeChar.Contains(PC) && Spaces.Contains(PPC))                        //If PC is wholechar and PPC is a space
                    {
                        MoveCaretLeft();
                    }
                    else if (WholeChar.Contains(PC) || HalfChar.Contains(PC))
                    {
                        while (true)
                        {
                            if (Spaces.Contains(this[-1]))
                                break;
                            else if (WholeChar.Contains(this[-1]) && this[-2] != '\u007E' && !HalfChar.Contains(this[-2]))          //PC is Whoolechar
                            {
                                MoveCaretLeft();
                                break;
                            }
                            else if (HalfChar.Contains(this[-1]) && this[-2] != '\u007E' && !HalfChar.Contains(this[-2]))           //PC is half char
                            {
                                MoveCaretLeft();
                                break;
                            }
                            else if (this[-1] == '\u006B' && HalfChar.Contains(this[-2]) && this[-3] != '\u007E' && !HalfChar.Contains(this[-3]))   //Last char comb makes a whole char from a hlaf char and danda
                            {
                                MoveCaretLeft();
                                MoveCaretLeft();
                                break;
                            }
                            else
                                MoveCaretLeft();
                        }
                    }

                    InsertChar('\u0066');
                    rtb.CaretPosition = t;
                }
                else
                {
                    InsertChar('\u0062');
                }
            }
            else if (s == "I")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u0068');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
                else
                {
                    InsertChar('\u00C3');
                }
            }
            else if (s == "o")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u00A8');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
                else
                {
                    InsertChar('\u0076');
                    InsertChar('\u00A8');
                }
            }
            else if (s == "O")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);
                    TextPointer t = rtb.CaretPosition;
                    NavigateToLastMiddleMatra();
                    InsertChar('\u00A9');
                    rtb.CaretPosition = t;
                    MoveCaretRight();
                }
                else
                {
                    InsertChar('\u0076');
                    InsertChar('\u00A9');
                }
            }
            else if (s == "u")
            {
                if (this[-1] == '\u007E')
                {
                    if (this[-2] == '\u006A')    //ru
                    {
                        RemoveChar(2);
                        InsertChar('\u0023');
                    }
                    else
                    {
                        RemoveChar(1);
                        TextPointer t = rtb.CaretPosition;
                        NavigateToLastMiddleMatra();
                        InsertChar('\u0071');
                        rtb.CaretPosition = t;
                        MoveCaretRight();
                    }
                }
                else
                {
                    InsertChar('\u006D');
                }
            }
            else if (s == "U")
            {
                if (this[-1] == '\u007E')
                {
                    if (this[-2] == '\u006A')    //ruu
                    {
                        RemoveChar(2);
                        InsertChar('\u003A');
                    }
                    else
                    {
                        RemoveChar(1);
                        TextPointer t = rtb.CaretPosition;
                        NavigateToLastMiddleMatra();
                        InsertChar('\u0077');
                        rtb.CaretPosition = t;
                        MoveCaretRight();
                    }
                }
                else
                {
                    InsertChar('\u00C5');
                }
            }
            else if (s == "r")
            {
                if (this[-1] == '\u007E')
                {
                    RemoveChar(1);                                  //Remove continuation
                    PC = this[-1];
                    PPC = this[-2];

                    if (WholeChar.Contains(PC))                           //If previous char is whole char
                    {
                        TextPointer t = rtb.CaretPosition;
                        NavigateToLastBeforeMatra();
                        InsertChar('\u007A');       //Insert r matra
                        InsertChar('\u007E');       //Insert continuation
                        rtb.CaretPosition = t;
                        MoveCaretRight();
                    }
                    else if (PC == '\u006B' && HalfChar.Contains(PPC))   //If previous char is danda and PPC is half char so that two gives a single whole char
                    {
                        TextPointer t = rtb.CaretPosition;
                        NavigateToLastBeforeMatra();
                        InsertChar('\u007A');       //Insert r matra
                        InsertChar('\u007E');       //Insert continuation
                        rtb.CaretPosition = t;
                        MoveCaretRight();
                    }
                    else                                            //Previous chars do not make a char combination
                    {
                        //Consider rewise : You should not remove the matra
                        //RemoveChar(1);                              //Remove danda
                        InsertChar('\u006A');                       //Insert r
                        InsertChar('\u007E');                       //Insert continuation
                    }
                }
                else
                {
                    InsertChar('\u006A');
                    InsertChar('\u007E');
                }
            }
            else if (s == "R")
            {
                if (PC == '\u007E')
                    RemoveChar(1);
                //TextPointer t = rtb.CaretPosition;            //Uncomment to apply Navigation function
                //NavigateToLastBeforeMatra();
                InsertChar('\u0060');
                //rtb.CaretPosition = t;
                //MoveCaretRight();
            }
            #endregion

            #region Characters
            else if (s == "b")
            {
                InsertCharByInput('\u0063');
            }
            else if (s == "B")
            {
                InsertCharByInput('\u00D2');
            }
            else if (s == "c")
            {
                InsertCharByInput('\u0070');
            }
            else if (s == "C")
            {
                InsertCharByInput('\u004E');
            }
            else if (s == "d")
            {
                InsertCharByInput('\u006E');
            }
            else if (s == "D")
            {
                InsertCharByInput('\u004D');
            }
            else if (s == "f" || s == "F" || s == "P")
            {
                InsertCharByInput('\u0051');
            }
            else if (s == "g")
            {
                InsertCharByInput('\u0078');
            }
            else if (s == "G")
            {
                InsertCharByInput('\u00C4');
            }
            else if (s == "h" || s == "H")
            {
                InsertCharByInput('\u0067');
            }
            else if (s == "j")
            {
                InsertCharByInput('\u0074');
            }
            else if (s == "J")
            {
                InsertCharByInput('\u003E');
            }
            else if (s == "k")
            {
                InsertCharByInput('\u0064');
            }
            else if (s == "K")
            {
                InsertCharByInput('\u005B');
            }
            else if (s == "l")
            {
                InsertCharByInput('\u0079');
            }
            else if (s == "L")
            {
                InsertCharByInput('\u0047');
            }
            else if (s == "m")
            {
                InsertCharByInput('\u0065');
            }
            else if (s == "M")         //Bindus
            {
                if (PC == '\u007E')         //Remove continuation if present
                {
                    RemoveChar(1);
                    PC = this[-1];
                }

                if (PC == '\u005A')            //r+bindu
                {
                    RemoveChar(1);
                    InsertChar('\u00B1');
                }
                else if (PC == '\u0061')
                {
                    RemoveChar(1);
                    InsertChar('\u00A1');
                }
                else if (PC == '\u00A1')
                {
                    RemoveChar(1);
                    InsertChar('\u0057');
                }
                else if (PC == '\u0057')
                {
                    RemoveChar(1);
                    InsertChar('\u0061');
                }
                else
                    InsertChar('\u0061');
            }
            else if (s == "n")
            {
                InsertCharByInput('\u0075');
            }
            else if (s == "N")
            {
                InsertCharByInput('\u002E');
            }
            else if (s == "p")
            {
                InsertCharByInput('\u0069');
            }
            else if (s == "q")
            {
                InsertCharByInput('\u0064', false);
                InsertChar('\u002B');
                InsertChar('\u007E');
            }
            else if (s == "Q")
            {
                InsertChar('\u007E');    //Continuation char
            }
            else if (s == "s")
            {
                InsertCharByInput('\u006C');
            }
            else if (s == "S")
            {
                InsertCharByInput('\u0022');
            }
            else if (s == "t")
            {
                InsertCharByInput('\u0072');
            }
            else if (s == "T")
            {
                InsertCharByInput('\u0056');
            }
            else if (s == "v" || s == "V" || s == "w")
            {
                InsertCharByInput('\u006F');
            }
            else if (s == "W")
            {
                InsertCharByInput('\u007D');
            }
            else if (s == "x")
            {
                InsertCharByInput('\u007B');
            }
            else if (s == "X")
            {
            }
            else if (s == "y" || s == "Y")
            {
                InsertCharByInput('\u00B8');
            }
            else if (s == "z")
            {
                InsertCharByInput('\u0074', false);
                InsertChar('\u002B');
                InsertChar('\u007E');
            }
            else if (s == "Z")
            {
            }
            #endregion

            rtb.IsReadOnly = true;
        }
    }
}