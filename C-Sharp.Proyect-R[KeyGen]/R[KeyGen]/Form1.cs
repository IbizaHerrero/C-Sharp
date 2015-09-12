using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;

namespace Random_KeyGen_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenKey();
        }

        //#Genera la clave segun los parámetros determinados.
        private void GenKey()
        {
            try
            {
                int keyLength = Convert.ToInt32(textBox2.Text);

                char[] chrArry = new char[keyLength];
                Random Rnd = new Random(DateTime.UtcNow.Millisecond);

                for (int i = 0; i < keyLength; i++)
                {
                    if (radioButton1.Checked)
                    {                        
                        Thread.Sleep(Rnd.Next(0,200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(48, 58));
                    }
                    else if (radioButton2.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0,200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(97,123));
                    }       
                    else if (radioButton3.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(65, 91));
                    }
                    else if (radioButton4.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(32, 47));
                    }
                    else if (radioButton5.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(58, 64));
                    }
                    else if (radioButton6.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(91, 96));
                    }
                    else if (radioButton7.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(123, 127));
                    }
                    else if (radioButton8.Checked)
                    {
                        Thread.Sleep(Rnd.Next(0, 200));
                        chrArry[i] = Convert.ToChar(Rnd.Next(0, 256));
                    }
                }

                OutKeyFormat(chrArry);
            }
            catch (Exception E)
            {
                MessageBox.Show("Error: No se ha introducido el tamaño de la cadena a generar.\r\n" + E.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
            private void OutKeyFormat(char[] ckey)
            {
                if (checkBox1.Checked)
                {
                    textBox1.Text = Convert.ToBase64String(Encoding.UTF8.GetBytes(ckey), Base64FormattingOptions.None);
                }
                else if (checkBox2.Checked)
                {
                    List<string> strKeyHex = new List<string>();

                    for (int i = 0; i < 32; i++)
                    {
                        int chr = ckey[i];
                        strKeyHex.Add(chr.ToString("X"));
                    }    
                    
                    textBox1.Text = string.Join("", strKeyHex);
                }
                else if (checkBox3.Checked)
                {
                    string strKeyAscii = new string(ckey);
                    textBox1.Text = strKeyAscii;
                }
                else if (checkBox4.Checked)
                {
                    SHA1Managed SHA256 = new SHA1Managed();
                    SHA256.ComputeHash(Encoding.UTF8.GetBytes(ckey), 0, ckey.Length);
                    textBox1.Text = Convert.ToBase64String(SHA256.Hash, Base64FormattingOptions.None).Substring(0, Convert.ToInt32(textBox2.Text));
                }
            }

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox1.Checked)
                {
                    checkBox2.Enabled = false; checkBox3.Enabled = false; checkBox4.Enabled = false;
                }
                else
                {
                    checkBox2.Enabled = true; checkBox3.Enabled = true; checkBox4.Enabled = true;

                }
            }

            private void checkBox2_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox2.Checked)
                {
                    checkBox1.Enabled = false; checkBox3.Enabled = false; checkBox4.Enabled = false;
                }
                else
                {
                    checkBox1.Enabled = true; checkBox3.Enabled = true; checkBox4.Enabled = true;

                }
            }

            private void checkBox3_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox3.Checked)
                {
                    checkBox1.Enabled = false; checkBox2.Enabled = false; checkBox4.Enabled = false;
                }
                else
                {
                    checkBox1.Enabled = true; checkBox2.Enabled = true; checkBox4.Enabled = true;

                }
            }

            private void checkBox4_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox4.Checked)
                {
                    checkBox1.Enabled = false; checkBox2.Enabled = false; checkBox3.Enabled = false;
                }
                else
                {
                    checkBox1.Enabled = true; checkBox2.Enabled = true; checkBox3.Enabled = true;

                }
            }

            private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
            {
                
            }
        }
    }

