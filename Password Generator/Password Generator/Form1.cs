using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

/*
x - Create checkboxes
x - Each checkbox has its own string with its characters
    Characters (Upper-Case)
    Characters (Lower-Case)
    Numbers
    Space
    Special (!, $, %, &, ?, *, #, @, +, =, ;, :)
    Brackets ({, }, (, ), [, ], <,>)
    Minus
    Underline
x - Default checkbox is numbers
x - Add string to default string if checkbox is checked
x - Add a button which creates the password in the textbox
x - Show the password in the textbox


Fix
x - Clean the code if possible
x - Shuffle the default string to make the string more random
x - Hide/unhide the password 
x - Copy password
x - Doesnt remove unchecked characters
x - Change text of hide/unhide button based if its hide/unhide
x - disable clicks on the textbox

*/

namespace Password_Generator
{
    public partial class Form1 : Form
    {
        string defaultPw;
        private string rndChar;
        decimal lenChar = 10;


        public Form1()
        {
            InitializeComponent();
            checkBox3.Checked = true;
            numUpDown1.Value = lenChar;
            pwGene();
            textBox1.Text = rndChar;
            textBox1.UseSystemPasswordChar = true;
        }

        private Dictionary<string, string> checkboxCharMap = new Dictionary<string, string>
        {
            { "checkBox1", "ABCDEFGHIJKLMNOPQRSTUVWXYZ" }, // Upper-case characters
            { "checkBox2", "abcdefghijklmnopqrstuvwxyz" }, // Lower-case characters
            { "checkBox3", "0123456789" }, // Numbers
            { "checkBox4", " " }, // Space
            { "checkBox5", "_" }, // Underline
            { "checkBox6", "-" }, // Minus
            { "checkBox7", "{}()[]<>" }, // Brackets
            { "checkBox8", "!$%&=*#@+=;:" } // Special characters
         };

        // Adds characters/options to default Password
        private void cbCheck(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            string cbName = cb.Name;

            if (checkboxCharMap.TryGetValue(cbName, out string charSet))
            {
                if (cb.Checked)
                {
                    defaultPw += charSet;
                }
                else
                {
                    foreach (char c in charSet)
                    {
                        defaultPw = defaultPw.Replace(c.ToString(), "");
                    }
                }
            }
        }

        // Gets the wanted lenght of the password
        private void numUpDown(object sender, EventArgs e)
        {
            lenChar = numUpDown1.Value;
        }
      
        // Shuffles the defaultPw string
        private void shuffleString(ref string str)
        {
            char[] array = str.ToCharArray();
            Random rnd = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            str = new string(array);
        }

        // Generates the password based on the lenght and given characters, numbers etc.
        private void pwGene()
        {
            //Shuffles the defaultPw string
            shuffleString(ref defaultPw);

            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            // Create random characters based on the lenght
            for (int i = 0; i < lenChar; i++)
            {
                sb.Append(defaultPw[rnd.Next(defaultPw.Length)]);
            }
            rndChar = sb.ToString();
        }
        
        // Create password button
        private void createBttn(object sender, EventArgs e)
        {
            pwGene();
            textBox1.Text = rndChar;
        }

        // Copy password button
        private void copyBttn(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Clipboard.SetText(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Nothing to copy.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Hide/unhide button
        private void hideBttn(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !textBox1.UseSystemPasswordChar;

            if (textBox1.UseSystemPasswordChar) 
            {
                button3.Text = "Unhide";
            }
            else
            {
                button3.Text = "Hide";
            }
           
        }

        // Disable clicks on the textbox
        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
        }
    }
    
}
