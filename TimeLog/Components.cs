using System.ComponentModel;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TimeLog
{
    [DefaultEvent(nameof(TextChanged))]
    public class MyComponent : UserControl
    {
        private TextBox textBox1;
        private RadioButton radioButton1;
        private TextBox tBox1;
        
        private Button deleteButton; // Add a button to delete the component
        
        //?add public property to get and set the text of the textbox
        [Browsable(true)]
        public string TextBox1Text
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }
        //? add public property to get and set the text of the tBox1
        [Browsable(true)]
        public string TBox1Text
        {
            get => tBox1.Text;
            set => tBox1.Text = value;
        }
        //? add public property to get and set the checked property of the radioButton1
        [Browsable(true)]
        public bool RadioButton1Checked
        {
            get => radioButton1.Checked;
            set => radioButton1.Checked = value;
        }
       

        //?add public property to get the delete button
        [Browsable(true)]
        public Button DeleteButton => deleteButton;

        //?add public event handler for text changed event of the textBox1
        [Browsable(true)]
        public event EventHandler TextBox1TextChanged
        {
            add => textBox1.TextChanged += value;
            remove => textBox1.TextChanged -= value;
        }
        //?add public event handler for text changed event of the tBox1
        [Browsable(true)]
        public event EventHandler TBox1TextChanged
        {
            add => tBox1.TextChanged += value;
            remove => tBox1.TextChanged -= value;
        }

        [Browsable(true)]
        public event EventHandler TBox1TextExited
        {
            add => tBox1.Leave += value;
            remove => tBox1.Leave -= value;
        }
        
        //?add public event handler for checked changed event of the radioButton1
        [Browsable(true)]
        public event EventHandler RadioButton1CheckedChanged
        {
            add => radioButton1.CheckedChanged += value;
            remove => radioButton1.CheckedChanged -= value;
        }
       

        //?add public property to get and set the visibility of the delete button
        public bool DeleteButtonVisible
        {
            get => deleteButton.Visible;
            set => deleteButton.Visible = value;
        }

        public bool DeleteButtonEnabled
        {
            get => deleteButton.Enabled;
            set => deleteButton.Enabled = value;
        }


        public MyComponent()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ToolTip toolTip1 = new ToolTip();
            
            textBox1 = new TextBox();
            radioButton1 = new RadioButton();
            tBox1 = new TextBox();
            toolTip1.SetToolTip(tBox1, "Change time spent, if needed");
            deleteButton = new Button(); // Initialize the delete button
            

            // Set the properties of the controls
            textBox1.Location = new System.Drawing.Point(30, 55);
            textBox1.Size = new System.Drawing.Size(503, 20); // Set the size of textBox1
            toolTip1.SetToolTip(textBox1, "Enter description of the task");
            radioButton1.Location = new System.Drawing.Point(560, 58);
            radioButton1.Size = new System.Drawing.Size(14, 14); // Set the size of radioButton1
            radioButton1.TabStop = true; // Set the radioButton1 to be a tab stop
            radioButton1.Text = ""; // Set the text of radioButton1 
            radioButton1.UseVisualStyleBackColor = false; // Set the radioButton1 to be a radio button
            radioButton1.AutoCheck = true;
            //?improve the radio button appearance
            radioButton1.Appearance = Appearance.Button;
            radioButton1.FlatStyle = FlatStyle.Popup;
            //?improve the radio button appearance
            radioButton1.BackColor = System.Drawing.Color.Silver;
            //radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += (sender, e) => { if (radioButton1.Checked) { radioButton1.BackColor = System.Drawing.Color.Yellow; } else { radioButton1.BackColor = System.Drawing.Color.Silver; } }; // Change the color of the radioButton1 when it is checked 
            toolTip1.SetToolTip(radioButton1, "Make the task active");
            
            tBox1.Location = new System.Drawing.Point(590, 55);
            tBox1.Size = new System.Drawing.Size(48, 20); // Set the size of textBox2
            tBox1.TextAlign = HorizontalAlignment.Right; // Center the text in textBox2



            deleteButton.Location = new System.Drawing.Point(670, 55); // Set the location of the delete button
            deleteButton.Size = new System.Drawing.Size(30, 30); // Set the size of the delete button
            deleteButton.Text = "-"; // Set the text of the delete button
            deleteButton.BackColor = System.Drawing.Color.Red;
            deleteButton.Font = new System.Drawing.Font(deleteButton.Font, System.Drawing.FontStyle.Bold);
            deleteButton.ForeColor = System.Drawing.Color.White;
            deleteButton.Font = new Font(deleteButton.Font.FontFamily, 7);
            deleteButton.TextAlign = ContentAlignment.MiddleCenter; // Center the text in the button
            deleteButton.Visible = IsDeleteButtonVisible(); // Set the visibility of the delete button based on the condition
            toolTip1.SetToolTip(deleteButton, "Delete this task");

            // Add the controls to the component
            
            Controls.Add(textBox1);
            
            Controls.Add(radioButton1);
            
            Controls.Add(tBox1);
            
            Controls.Add(deleteButton); // Add the delete button to the component
                      
        }

        [Browsable(true)]
        private void checkBox1_CheckedChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        [Browsable(true)]
        public event EventHandler deleteButtonClick
        {
            add => deleteButton.Click += value;
            remove => deleteButton.Click -= value;
        }

                   

        private bool IsDeleteButtonVisible()
        {
            return string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(tBox1.Text); // Return true if textBox1 and tBox1 are empty, otherwise return false
        }

        //?component control name
        public string MyComponentName
        {
            get => this.Name;
            set => this.Name = value;
        }


    }

   
}
