/*
 * Note: We have used KrutiPad font. In GetPreviousChar() and GetPreviousToPreviousChar(), if a char is not present
 * (i.e. for the start of document) an unused char in Font KrutiPad \u0090 is returned.
 * 
 * Note: Space keys refers to any of Space, Tab or Enter keys.
 * 
 * Note: In normal, rtb is read and write.
 * 
 * Note: Selection Problem
 * Selection is removed after PreviewKeyUp and before KeyUp
 * 
 * In PreviewKeyDown, We first check if the key pressed is any space key is pressed. If yes, we check if previous char 
 * is a continuation char, if yes we remove it.
 * 
 * After PreviewKeyDown, any space char as a result of any space key pressed is inserted into the textbox. So, it will result in an TextChange event.
 * 
 * In Preview text input, we apply our logic to insert chars as necessary based on key pressed.
 * But at the end of it, we make rtb readonly so that TextInput has no effect.
 * 
 * In KeyUp, we make rtb writable.
 *
 * 
 * Small i matra algorithm
 * 
 * First we have to discard any matras (including danda for matra or for supporting half char) if present on LHS of caret.
 * We will then check if the char found after discarding any matra is a whole char or any space, if yes apply matra
 * Else the last char will be a half char, then discard all half chars from that position and apply matra to last half char.
 */