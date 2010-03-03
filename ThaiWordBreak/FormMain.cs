﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICU4NET;
using ICU4NETExtension;

namespace ThaiWordBreak
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void uxBreak_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Join("-", WordBreak(uxText.Text).ToArray()));

            using (BreakIterator bi = BreakIterator.CreateWordInstance(Locale.GetUS()))
            {
                bi.SetText(uxText.Text);

                // requires ICU4NETExtension to use Enumerate extension method
                var words = bi.Enumerate().ToList();
                uxText.Text = string.Join("-", words.ToArray());
            }


        }

        /// <summary>
        /// Using BreakIterator, the traditional way
        /// </summary>
        /// <param name="text">Text to be break</param>
        /// <returns>List of words</returns>
        private List<string> WordBreak(string text)
        {
            var sb = new StringBuilder();
            var col = new List<string>();

            using (BreakIterator bi = BreakIterator.CreateWordInstance(Locale.GetUS()))
            {
                bi.SetText(text);
                int start = bi.First(), end = bi.Next();
                while (end != BreakIterator.DONE)
                {
                    col.Add(text.Substring(start, end - start));
                    start = end; end = bi.Next();
                }
            }

            return col;
        }
    }
}
