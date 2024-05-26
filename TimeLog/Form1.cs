using System.Configuration;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TimeLog
{
    public partial class Form1 : Form
    {
        private Button addButton; // Declare a private Button field
        private RadioButton standBy;
        private Label standByLabel;
        private int currentBottom = 20; // Declare a private int field
        private int componentCount = 0; // Declare a private int field
        private List<int> times = new List<int>();
        private Label? totalTime;
        private static string rootdir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ConfigurationManager.AppSettings["LogDirectory"]);
        private static string logname = ConfigurationManager.AppSettings["LogName"];
        private TimeLogger timeLogger = new TimeLogger(rootdir, logname);
        DateTime start;
        DateTime stop;
        long diff;
        TimeSpan elapsedSpan;
        Double result = 0;
      
        public Form1()
        {
            InitializeComponent();
            this.Text += DateTime.Now.ToString(" dd-MM-yyyy");
            ToolTip toolTip1 = new ToolTip();
            addButton = new ManagingControls().ConfigureAddButton();
            toolTip1.SetToolTip(addButton, "Add a new task");
            addButton.Click += AddButton_Click;
            Controls.Add(addButton);

            standBy = new ManagingControls().ConfigureStandByButton();
            standBy.CheckedChanged += (sender, e) => { if (standBy.Checked) { standBy.BackColor = System.Drawing.Color.Yellow; } else { standBy.BackColor = System.Drawing.Color.Silver; } }; // Change the color of the radioButton1 when it is checked 
            standBy.CheckedChanged += standBy_CheckedChanged;
            standBy.Checked = true;
            toolTip1.SetToolTip(standBy, "Stop all timers");
            Controls.Add(standBy);

            standByLabel = new ManagingControls().ConfigureStandByLabel();
            Controls.Add(standByLabel);

            List<string> previousTasks = timeLogger.LoadLog();
            if (previousTasks.Count > 0)
            {
                Control[] tbx = null;
                int i = 1;
                foreach (string content in previousTasks)
                {
                        AddButton_Click(addButton, null);
                        tbx = Controls.Find("myComponent" + i.ToString(), true);
                        if (tbx != null && tbx.Length > 0)
                        {
                            ((MyComponent)tbx[0]).TextBox1Text = content;
                        }
                    i++;
                }
            }
            else
            {
                AddButton_Click(addButton, null);
            }

        }

        private void AddButton_Click(object sender, EventArgs? e)
        {
            MyComponent? newComponent = new MyComponent();
            newComponent.Location = new Point(10, currentBottom);
            //Add new time to the list
            times.Add(0);
            newComponent.TBox1Text = times[componentCount].ToString();
            // Adjust the size of newComponent to fit all controls
            newComponent.Size = new Size(newComponent.Controls.Cast<Control>().Max(c => c.Right) + 10, newComponent.Controls.Cast<Control>().Max(c => c.Bottom));
            currentBottom += 50;
            // Handle events for controls in newComponent
            newComponent.TextBox1TextChanged += MyComponent_TextBox1_TextChanged;
            newComponent.TBox1TextExited += MyComponent_TBox1_TextExited;
            newComponent.deleteButtonClick += MyComponent_deleteButtonClick;
            newComponent.RadioButton1CheckedChanged += MyComponent_RadioButton1CheckedChanged;

            newComponent.MyComponentName = ("myComponent" + ++componentCount);
            Controls.Add(newComponent);
            if (componentCount > 1)
            {
                MyComponent? previousComponent = Controls.Find("myComponent" + (componentCount - 1), true).FirstOrDefault() as MyComponent;
                if (previousComponent != null)
                {
                    previousComponent.DeleteButtonEnabled = false;
                }
            }

            // Check if the added component's bottom is greater than the form's height
            if (newComponent.Bottom > this.Height)
            {
                this.AutoScroll = true;
            }

            // Create and configure the addButton
            if (totalTime == null)
            {
                totalTime = new Label();
                totalTime.Text = "Total time in minutes: " + times.Sum().ToString().PadLeft(20);
                totalTime.Font = new Font(addButton.Font.FontFamily, 10);
                totalTime.Size = new System.Drawing.Size(300, 20);
                totalTime.Location = new Point(368, currentBottom + 50);
                Controls.Add(totalTime);
            }
            else
            {
                totalTime.Text = "Total time in minutes: " + times.Sum().ToString().PadLeft(20);
                totalTime.Location = new Point(368, currentBottom + 50);
            }

        }

        private void updateTotalTime()
        {
            totalTime.Text = "Total time in minutes: " + times.Sum().ToString().PadLeft(20);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //?save the log
            timeLogger.Log("***" + this.Text + "***");
            foreach (MyComponent myComponent in Controls.OfType<MyComponent>())
            {
                if (myComponent.TextBox1Text.Trim().Length > 0)
                {
                    timeLogger.Log(myComponent.TextBox1Text + " ==> " + myComponent.TBox1Text);
                }
            }
           
        }

        private void MyComponent_RadioButton1CheckedChanged(object sender, EventArgs e)
        {

            RadioButton? radioButton = sender as RadioButton;
            MyComponent? myComponent = radioButton?.Parent as MyComponent;
            if (myComponent != null && radioButton != null)
            {
                if (radioButton.Checked)
                {
                    UncheckOtherRadioButtons(checkedRadioButton: sender as RadioButton);
                    start = DateTime.Now;
                    timeLogger.Log("Activated " + myComponent.Name.Substring(11) + ": " + myComponent.TextBox1Text);
                    //myComponent.TBox1Text = start.ToString("HH:mm:ss");
                    
                    standBy.Checked = false;
                }
                else
                {
                    result = timeSpan(start);
                    myComponent.TBox1Text = Math.Round(Double.Parse(myComponent.TBox1Text) + result, 0).ToString();
                    times[Convert.ToInt32(myComponent.Name.Substring(11)) - 1] += Convert.ToInt32(result);
                    timeLogger.Log("Deactivated " + myComponent.Name.Substring(11) + ": " + myComponent.TextBox1Text);
                    totalTime.Text = "Total time in minutes: " + times.Sum().ToString().PadLeft(20);
                }
            }
        }

        // 3. Implement the event handler method to perform the desired actions when the delete button is clicked.
        private void MyComponent_deleteButtonClick(object sender, EventArgs e)
        {
            //?check that this is the last added component

            Button? deleteButton = sender as Button;
            MyComponent? myComponent = deleteButton?.Parent as MyComponent;
            if (myComponent.Name == "myComponent" + componentCount)
            {
                Controls.Remove(myComponent);
                times.RemoveAt(componentCount - 1);
                updateTotalTime();
                currentBottom -= 50;
                componentCount--;
            }
            //find the previous component and enable the delete button
            if (componentCount > 0)
            {
                MyComponent? previousComponent = Controls.Find("myComponent" + (componentCount), true).FirstOrDefault() as MyComponent;
                if (previousComponent != null && previousComponent.TextBox1Text.Trim().Length == 0)
                {
                    previousComponent.DeleteButtonEnabled = true;
                }
                totalTime.Location = new Point(368, currentBottom + 50);
            }
            else
            {
                //?remove the total time label
                Controls.Remove(totalTime);
                totalTime = null;
            }

        }


        private void MyComponent_TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            MyComponent? myComponent = textbox?.Parent as MyComponent;
            string parentName = myComponent?.Name;
            if (myComponent.Name == "myComponent" + componentCount)
            {
                if (!string.IsNullOrEmpty(textbox?.Text))
                {
                    if (myComponent.DeleteButtonEnabled)
                    {
                        // Perform the necessary operations on myComponent
                        myComponent.DeleteButtonEnabled = false;
                    }
                }
                else
                {

                    // Perform the necessary operations on myComponent
                    myComponent.DeleteButtonEnabled = true;

                }
            }
        }

        private void MyComponent_TBox1_TextExited(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            MyComponent? myComponent = textbox?.Parent as MyComponent;
            if (myComponent != null)
            {
                if (textbox.Text.Trim().Length > 0)
                {
                    times[Convert.ToInt32(myComponent.Name.Substring(11)) - 1] = Convert.ToInt32(textbox.Text);
                    updateTotalTime();
                }
            }
        }

        private double timeSpan(DateTime start)
        {
            stop = DateTime.Now;
            diff = stop.Ticks - start.Ticks;
            elapsedSpan = new TimeSpan(diff);
            return elapsedSpan.TotalMinutes;
        }

        private void UncheckOtherRadioButtons(RadioButton checkedRadioButton)
        {
            foreach (Control control in Controls)
            {
                if (control is MyComponent myComponent)
                {
                    foreach (Control innerControl in myComponent.Controls)
                    {
                        if (innerControl is RadioButton radioButton && radioButton != checkedRadioButton)
                        {
                            radioButton.Checked = false;
                        }
                    }
                }
            }
        }

        private void standBy_CheckedChanged(object sender, EventArgs e)
        {
            if (standBy.Checked)
            {
                UncheckOtherRadioButtons(checkedRadioButton: sender as RadioButton);
            }
        }
    }
}
