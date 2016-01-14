using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace HindiTransliterator
{
    public partial class MainWindow : Window
    {
        private void InsertChar(char c)
        {
            if (rtb.Selection.Text.Length == 0)
            {
                TextPointer insertionPosition;
                if (rtb.CaretPosition.IsAtInsertionPosition == true)
                {
                    insertionPosition = rtb.CaretPosition;
                }
                else
                {
                    insertionPosition = rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Forward);
                }

                if (rtb.Document.Blocks.Count == 0)
                    rtb.Document.Blocks.Add(new Paragraph(new Run()));

                int offset = rtb.Document.ContentStart.GetOffsetToPosition(insertionPosition);
                insertionPosition.InsertTextInRun(c.ToString());
                rtb.CaretPosition = rtb.Document.ContentStart.GetPositionAtOffset(offset + 1, LogicalDirection.Forward);
            }
            else
            {
                TextPointer selectionStartPosition = rtb.Selection.Start;
                TextRange selection = new TextRange(rtb.Selection.Start, rtb.Selection.End);
                selection.Text = "";
                rtb.CaretPosition.InsertTextInRun(c.ToString());
                rtb.CaretPosition = rtb.CaretPosition.GetPositionAtOffset(1, LogicalDirection.Forward);
            }
        }

        private void RemoveChar(int Count)
        {
            for (int i = 0; i <= Count - 1; i++)
            {
                TextPointer end = rtb.CaretPosition;
                TextPointer start = rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Backward);
                TextRange tr = new TextRange(start, end);
                tr.Text = "";
                rtb.CaretPosition = start;
            }
        }

        //Note: a caret pos is always an insertion point
        private void MoveCaretLeft()
        {
            if (rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Backward) != null)
                rtb.CaretPosition = rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Backward);
        }

        private void MoveCaretRight()
        {
            if (rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Forward) != null)
                rtb.CaretPosition = rtb.CaretPosition.GetNextInsertionPosition(LogicalDirection.Forward);
        }

        private void InsertCharByInput(char c, bool AppendContinuation = true)
        {
            //c is char to be inserted

            char PC = this[-1];
            char PPC = this[-2];
            char halfChar = GetHalfCharOf(PPC);

            if (PC == '\u007E')                         //If previous char is continuation
            {
                if (PPC == '\u006A')                    //Previous to previous char is r
                {
                    RemoveChar(2);                      //Remove continuation and r

                    if (HalfChar.Contains(c))           //If char to be inserted is itself a half char, append a danda
                    {
                        InsertChar(c);
                        InsertChar('\u006B');
                    }
                    else                                //else do not append danda
                    {
                        InsertChar(c);
                    }

                    InsertChar('\u005A');               //Append r matra

                    if (AppendContinuation == true)
                        InsertChar('\u007E');               //Append continuation
                }
                else if (halfChar != '\u0090')          //if PPC can be made half
                {
                    RemoveChar(2);                      //Remove continuation and danda
                    InsertChar(halfChar);

                    if (HalfChar.Contains(c))     //If char to be inserted is itself a half char, append a danda
                    {
                        InsertChar(c);
                        InsertChar('\u006B');
                    }
                    else                                //else do not append danda
                    {
                        InsertChar(c);
                    }

                    if (AppendContinuation == true)
                        InsertChar('\u007E');               //Append continuation
                }
                else if (this[-2] == '\u006B' && HalfChar.Contains(this[-3]))             //If PPC is a half char (PPC =danda and PPPC= half char)         
                {
                    RemoveChar(2);                      //Remove continuation and danda

                    if (HalfChar.Contains(c))     //If char to be inserted is itself a half char, append a danda
                    {
                        InsertChar(c);
                        InsertChar('\u006B');
                    }
                    else                                //else do not append danda
                    {
                        InsertChar(c);
                    }

                    if (AppendContinuation == true)     //Append continuation
                        InsertChar('\u007E');
                }
                else
                {
                    if (HalfChar.Contains(c))     //If char to be inserted is itself a half char, append a danda
                    {
                        InsertChar(c);
                        InsertChar('\u006B');
                    }
                    else                                //else do not append danda
                    {
                        InsertChar(c);
                    }

                    if (AppendContinuation == true)
                        InsertChar('\u007E');               //Append continuation
                }
            }
            else
            {
                if (HalfChar.Contains(c))     //If char to be inserted is itself a half char, append a danda
                {
                    InsertChar(c);
                    InsertChar('\u006B');
                }
                else                                //else do not append danda
                {
                    InsertChar(c);
                }

                if (AppendContinuation == true)
                    InsertChar('\u007E');               //Append continuation
            }
        }

        private void NavigateToFirstMatra()             //Navigates to first matra on specified char
        {
            char[] t = Matra;

            while (true)
            {
                if (this[-1] == '\u006B' && HalfChar.Contains(this[-2]))
                    break;
                else if (t.Contains(this[-1]))
                    MoveCaretLeft();
                else
                    break;
            }
        }

        private void NavigateToLastMatra(bool Includei = false)
        {
            char[] t;
            if (Includei == false)
                t = MatraWOi;
            else
                t = Matra;

            while (true)
            {
                if (t.Contains(this[1]))
                    MoveCaretRight();
                else
                    break;
            }
        }

        private void NavigateToLastMiddleMatra(bool Includei = false)
        {
            char[] t;
            if (Includei == false)
                t = MiddleMatraWOi;
            else
                t = MiddleMatra;

            if (t.Contains(this[-1]))                   //If last matra also belongs to the same matra position,return so as to place matra thereitself
                return;

            NavigateToFirstMatra();

            while (true)
            {
                char c = this[1];
                if (t.Contains(c) || BeforeMatra.Contains(c))
                    MoveCaretRight();
                else
                    break;
            }
        }

        private void NavigateToLastBeforeMatra()
        {
            char[] t = BeforeMatra;

            if (t.Contains(this[-1]))
                return;

            NavigateToFirstMatra();

            while (true)
            {
                if (t.Contains(this[1]))
                    MoveCaretRight();
                else
                    break;
            }
        }

        private void NavigateToLastAfterMatra()
        {
            char[] t = AfterMatra;

            if (t.Contains(this[-1]))
                return;

            NavigateToFirstMatra();

            while (true)
            {
                char c = this[1];
                if (t.Contains(c) || BeforeMatra.Contains(c) || MiddleMatra.Contains(c))
                    MoveCaretRight();
                else
                    break;
            }
        }

        private char GetHalfCharOf(char c)
        {
            switch (c)
            {
                case '\u0063':
                    {
                        return '\u0043';
                    }
                case '\u00D2':
                    {
                        return '\u0048';
                    }
                case '\u0070':
                    {
                        return '\u0050';
                    }
                case '\u0051':
                    {
                        return '\u00B6';
                    }
                case '\u0078':
                    {
                        return '\u0058';
                    }
                case '\u00C4':
                    {
                        return '\u003F';
                    }
                case '\u0067':
                    {
                        return '\u00BA';
                    }
                case '\u0074':
                    {
                        return '\u0054';
                    }
                case '\u003E':
                    {
                        return '\u00F7';
                    }
                case '\u0064':
                    {
                        return '\u0044';
                    }
                case '\u0079':
                    {
                        return '\u0059';
                    }
                case '\u0065':
                    {
                        return '\u0045';
                    }
                case '\u0075':
                    {
                        return '\u0055';
                    }
                case '\u0069':
                    {
                        return '\u0049';
                    }
                case '\u006C':
                    {
                        return '\u004C';
                    }
                case '\u0072':
                    {
                        return '\u0052';
                    }
                case '\u006F':
                    {
                        return '\u004F';
                    }
                case '\u003D':
                    {
                        return '\u00AB';
                    }
                case '\u004A':
                    {
                        return '\u00DC';
                    }
                default:
                    {
                        return '\u0090';
                    }
            }
        }

        //Note: Indexer will give tabs and newlines as 144(90 Hex) as it is not able to recognize them in hindi fonts
        public char this[int i]
        {
            get
            {
                int Count;
                int offset;
                char[] buffer;
                if (i < 0)
                {
                    offset = -i;
                    buffer = new char[offset];

                    //i is negative means previous char
                    Count = rtb.CaretPosition.GetTextInRun(LogicalDirection.Backward, buffer, 0, offset);
                    if (Count == offset)
                    {
                        return buffer[0];
                    }
                    else
                        return '\u0090';         //Char to distinguish
                }
                else if (i == 0)
                {
                    return '\u0090';
                }
                else
                {
                    offset = i;
                    buffer = new char[offset];

                    Count = rtb.CaretPosition.GetTextInRun(LogicalDirection.Forward, buffer, 0, offset);
                    if (Count == offset)
                    {
                        return buffer[0];
                    }
                    else
                        return '\u0090';         //Char to distinguish
                }
            }
        }
    }
}