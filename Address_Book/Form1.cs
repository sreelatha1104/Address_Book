using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Address_Book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Person> people = new List<Person>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\Address Book-Sreelatha"))
                Directory.CreateDirectory(path + "\\Address Book-Sreelatha");
            if (!File.Exists(path + "\\Address Book-Sreelatha\\settings.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter(path + "\\Address Book-Sreelatha\\settings.xml",Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = textBox1.Text;
            p.Email = textBox2.Text;
            p.StreetAddress = textBox3.Text;
            p.AdditionalNotes = textBox4.Text;
            p.Birthday = dateTimePicker1.Value;
            people.Add(p);
            listView1.Items.Add(p.Name);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox1.Text = people[listView1.SelectedItems[0].Index].Name;
                textBox2.Text = people[listView1.SelectedItems[0].Index].Email;
                textBox3.Text = people[listView1.SelectedItems[0].Index].StreetAddress;
                textBox4.Text = people[listView1.SelectedItems[0].Index].AdditionalNotes;
                dateTimePicker1.Value = people[listView1.SelectedItems[0].Index].Birthday;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Remove();
        }
        void Remove()
        {
            try {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                people.RemoveAt(listView1.SelectedItems[0].Index);
            }
            catch { }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            people[listView1.SelectedItems[0].Index].Name = textBox1.Text;
            people[listView1.SelectedItems[0].Index].Email = textBox2.Text;
            people[listView1.SelectedItems[0].Index].StreetAddress = textBox3.Text;
            people[listView1.SelectedItems[0].Index].Birthday = dateTimePicker1.Value;
            people[listView1.SelectedItems[0].Index].AdditionalNotes = textBox4.Text;
            listView1.SelectedItems[0].Text = textBox1.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\Address Book-Sreelatha\\settings.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();
            foreach(Person p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xStreetAddress = xDoc.CreateElement("StreetAddress");
                XmlNode xNotes = xDoc.CreateElement("Notes");
                XmlNode xBirthday = xDoc.CreateElement("Birthday");
                xName.InnerText = p.Name;
                xEmail.InnerText = p.Email;
                xStreetAddress.InnerText = p.StreetAddress;
                xNotes.InnerText = p.AdditionalNotes;
                xBirthday.InnerText = p.Birthday.ToFileTime().ToString();
                xTop.AppendChild(xName);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xStreetAddress);
                xTop.AppendChild(xNotes);
                xTop.AppendChild(xBirthday);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save(path + "\\Address Book-Sreelatha\\settings.xml");
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Remove();
            }
        }
    }
    class Person
    {
        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string StreetAddress
        {
            get;
            set;
        }
        public string AdditionalNotes
        {
            get;
            set;
        }
        public DateTime Birthday
        {
            get;
            set;
        }
    }
}
