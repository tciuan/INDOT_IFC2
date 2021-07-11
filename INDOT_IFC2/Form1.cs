using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Microsoft.VisualBasic;


namespace INDOT_IFC2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e) // informaiton extraction
        {
            //string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "rac_advanced_sample_project.ifc";
            string fileName = Text;
            var model = IfcStore.Open(fileName);
            //select objects                           

            string str = Interaction.InputBox("Please input what kind object you need", "Ifc inforamtion extraction", "", 50, 50);

            if (str == "slab")
            {
                var allSlab = model.Instances.OfType<IIfcSlab>();
                using (StreamWriter sw = new StreamWriter("Ifcslab.txt"))
                {
                    foreach (var s in allSlab)
                    {
                        sw.WriteLine(s);

                    }
                }
            }

            if (str == "door")
            {
                var allDoors = model.Instances.OfType<IIfcDoor>();
                using (StreamWriter sw = new StreamWriter("Ifcdoor.txt"))
                {
                    foreach (var s in allDoors)
                    {
                        sw.WriteLine(s);

                    }
                }
            }

            if (str == "window")
            {
                var allWindows = model.Instances.OfType<IIfcWindow>();
                using (StreamWriter sw = new StreamWriter("Ifcwindow.txt"))
                {
                    foreach (var s in allWindows)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            
            //Console.WriteLine($"Door ID: {theDoor.GlobalId}, Name: {theDoor.Name}");         


        }

        private void button2_Click(object sender, EventArgs e) //look for specific object
        {            
            //string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "rac_advanced_sample_project.ifc";
            string fileName = Text;
            var model = IfcStore.Open(fileName);
            //var id = "3FZFp0nq9AAhRAecLpGs5a";
            var id= Interaction.InputBox("Please input Global ID:", "Ifc_Info_Checking", "", 50, 50);
            var theDoor = model.Instances.FirstOrDefault<IIfcDoor>(d => d.GlobalId == id);

                      
            
            var properties = theDoor.IsDefinedBy
                .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                .OfType<IIfcPropertySingleValue>();
            
            using (StreamWriter sw = new StreamWriter("Ifcobject.txt"))
                foreach (var property in properties)
            { 
                sw.WriteLine($"Property: {property.Name}, Value: {property.NominalValue}");

            }

            

            
        }

       

        private void button3_Click(object sender, EventArgs e)// get inspection results
        {
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "ifcbridge-model01_Rev.ifc";
            var model = IfcStore.Open(fileName);
            var allBeam = model.Instances.OfType<IIfcBeam>();
            foreach (var s in allBeam)
            {
                //s.Description;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) // carelab picture box
        {
            Help.ShowHelp(this, "https://polytechnic.purdue.edu/facilities/construction-automation-robotics-and-ergonomics-care-lab");
        }

        private void pictureBox2_Click(object sender, EventArgs e) // autoIC picture box
        {
            Help.ShowHelp(this, "https://polytechnic.purdue.edu/autoic-lab");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            Help.ShowHelp(this, "https://www.in.gov/indot");

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) // select input file
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            this.Text = file.SafeFileName;
        }

       
    }
}
