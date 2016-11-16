﻿//MIT, 2016, WinterDev
// some code from icu-project
// © 2016 and later: Unicode, Inc. and others.
// License & terms of use: http://www.unicode.org/copyright.html#License

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LayoutFarm.TextBreaker.Custom
{

    public class WordVisitor
    {
        CustomBreaker ownerBreak;
        List<int> breakAtList = new List<int>();
        char[] buffer;
        int bufferLen;
        int startIndex;
        int currentIndex;
        char currentChar;
        int latestBreakAt;

        public WordVisitor(CustomBreaker ownerBreak)
        {
            this.ownerBreak = ownerBreak;
        }
        public void LoadText(char[] buffer, int index)
        {

            this.buffer = buffer;
            this.bufferLen = buffer.Length;
            this.startIndex = currentIndex = index;
            this.currentChar = buffer[currentIndex];
        }

        public int CurrentIndex
        {
            get { return this.currentIndex; }
        }
        public char Char
        {
            get { return currentChar; }
        }
        public bool ReadNext()
        {
            if (currentIndex < bufferLen + 1)
            {
                currentIndex++;
                currentChar = buffer[currentIndex];
                return true;
            }
            return false;
        }
        public bool IsEnd
        {
            get { return currentIndex >= bufferLen - 1; }
        }
        internal bool FoundWord;

        public void AddWordBreakAt(int index)
        {

#if DEBUG
            if (index == latestBreakAt)
            {
                throw new NotSupportedException();
            }
#endif
            this.latestBreakAt = index;
            breakAtList.Add(index);

            this.FoundWord = true;
        }
        public int LatestBreakAt
        {
            get { return this.latestBreakAt; }
        }
        public void SetCurrentIndex(int index)
        {
            this.currentIndex = index;
            if (index < buffer.Length)
            {
                this.currentChar = buffer[index]; 
            }
        }
        public bool CanbeStartChar(char c)
        {
            return ownerBreak.CanBeStartChar(c);
        }
        public List<int> GetBreakList()
        {
            return breakAtList;
        }
    }

}