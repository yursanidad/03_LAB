using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_LAB
{
    public partial class FrmRegistration : Form
    {
        private string _fullName;
        private int _age;
        private long _contactNo;
        private long _studentNo;
        private string _contactNoDisplay;
        public FrmRegistration()
        {
            InitializeComponent();
            txtContactNo.Text = "+63";
        }
        public string FullName(string lastName, string firstName, string middleInitial)
        {
            if (Regex.IsMatch(lastName, "[a-zA-Z]+") && Regex.IsMatch(firstName, "[a-zA-Z]+") && Regex.IsMatch(middleInitial, "[a-zA-Z]+"))
            {
                return lastName + ", " + firstName + ", " + middleInitial;
            }
            else
            {
             
                throw new ArgumentNullException();
            }
        }

        public long StudentNumber(string studentNo)
        {
            if (Regex.IsMatch(studentNo, "[0-9]{11}"))
            {
                long result;
                if (long.TryParse(studentNo, out result))
                {
                    _studentNo = result;
                    return _studentNo;
                }
                else
                {
                    throw new FormatException();
                }
            }
            else
            {
               
                throw new ArgumentNullException();
            }
        }

        public int Age(string age)
        {
            if (Regex.IsMatch(age, "[0-9]{1,3}"))
            {
                int result;
                if (int.TryParse(age, out result))
                {
                    if (result > 0 && result < 150)
                    {
                        _age = result;
                        return _age;
                    }
                    else
                    {
                     
                        throw new IndexOutOfRangeException();
                    }
                }
                else
                {
                
                    throw new FormatException();
                }
            }
            else
            {
      
                throw new ArgumentNullException();
            }
        }

        public long ContactNo(string contactNo)
        {
            _contactNoDisplay = contactNo;

            if (contactNo.StartsWith("+63"))
            {
                contactNo = contactNo.Substring(3);
            }

            if (Regex.IsMatch(contactNo, @"^[0-9]{10}$"))
            {
                long result;
                if (long.TryParse(contactNo, out result))
                {
                    _contactNo = result;
                    if (contactNo.Length > 10)
                    {
                
                        throw new OverflowException();
                    }
                    else
                    {
                        return _contactNo;
                    }
                }
                else
                {
                   
                    throw new FormatException();
                }
            }
            else
            {
          
                throw new ArgumentNullException();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Set the static variables of StudentInformationClass
                StudentInformationClass.SetFullName = FullName(txtLastName.Text, txtFirstName.Text, txtMiddleInitial.Text);
                StudentInformationClass.SetStudentNo = (int)StudentNumber(txtStudentNo.Text);
                StudentInformationClass.SetProgram = cbPrograms.Text;
                StudentInformationClass.SetGender = cbGender.Text;
                StudentInformationClass.SetContactNo = ContactNo(txtContactNo.Text);
                StudentInformationClass.SetAge = Age(txtAge.Text);
                StudentInformationClass.SetBirthday = datePickerBirtday.Value.ToString("yyyy-MM-dd");

                // Create and show the confirmation form
                FrmConfirm frm = new FrmConfirm();
                frm.ShowDialog();
            }

            catch (FormatException fex)
            {
                MessageBox.Show(fex.Message + ": The input data is not in the correct format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException anex)
            {
                MessageBox.Show(anex.Message + ": One or more fields are empty or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException ofex)
            {
                MessageBox.Show(ofex.Message + ": The number is too large.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IndexOutOfRangeException ioex)
            {
                MessageBox.Show(ioex.Message + ": The input value is out of the valid range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]
    {
            "BS Information Technology",
            "BS Computer Science",
            "BS Information Systems",
            "BS in Accountancy",
            "BS in Hospitality Management",
            "BS in Tourism Management"
    };

            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }
            string[] ListOfGender = new string[]
       {
            "Male",
            "Female",

       };

            for (int i = 0; i < ListOfGender.Length; i++)
            {
                cbGender.Items.Add(ListOfGender[i].ToString());
            }
        }
    }
}
