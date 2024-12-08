using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        private SoundPlayer AchievementSound;

        private int ClickCount = 0; // Class-level variable to hold the click count

        private bool Tricky = false;
        private int TrickyLevel = 1;

        public Form1()
        {
            InitializeComponent();

            // Initialize the SoundPlayer using a relative path
            AchievementSound = new SoundPlayer("achievement.wav");

            async void MoveButton()
            {
                while (true)
                {
                    if (Tricky)
                    {
                        button1.Location = new Point(
                            random.Next(button1.Size.Width, this.ClientSize.Width - button1.Size.Width),
                            random.Next(button1.Size.Height, this.ClientSize.Height - button1.Size.Height)
                        );
                    }

                    // Delay to control the speed of the loop and prevent UI freezing
                    await Task.Delay((int)(10 * 1000 / TrickyLevel));
                }
            }

            MoveButton();
        }

        // Add a private clickCount variable to track the count
        private int clickCount = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            // Increment the click count by 1
            clickCount++;

            // Check if the click count is a multiple of 100
            if (clickCount % 100 == 0)
            {
                if (achievements.Text == "You haven't unlocked any achievements yet!")
                {
                    achievements.Text = "You got " + clickCount + " clicks \n";
                    button1.Text = "Click, Click, Click!";
                    AchievementSound.Play();
                }
                else
                {
                    achievements.Text += "You got " + clickCount + " clicks \n";
                    AchievementSound.Play();
                    Tricky = true;
                    TrickyLevel += 1;
                }
            }

            // Update label1 with the new click count
            label1.Text = clickCount.ToString();
            label1.Size = new Size(130, 23);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string saveFilePath = "clickCount.txt"; // Define the path for the save file

            if (File.Exists(saveFilePath)) // Check if the file exists
            {
                try
                {
                    string savedClickCount = File.ReadAllText(saveFilePath); // Read the saved count
                    if (int.TryParse(savedClickCount, out clickCount)) // Try to parse it as an integer
                    {
                        label1.Text = clickCount.ToString(); // Set the label to the saved count
                    }
                    else
                    {
                        MessageBox.Show("Invalid saved data. Starting count from 0."); // Handle invalid data
                        clickCount = 0; // Reset to 0 if invalid
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading click count: " + ex.Message); // Error handling
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void shopButton_Click(object sender, EventArgs e)
        {
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string saveFilePath = "clickCount.txt"; // Define the save file path

            try
            {
                File.WriteAllText(saveFilePath, clickCount.ToString()); // Save the click count to the file
                MessageBox.Show("Click count saved successfully!"); // Confirm save
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving click count: " + ex.Message); // Error handling
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            // Reset the click count to 0
            clickCount = 0;

            // Update the label to reflect the reset count
            label1.Text = clickCount.ToString();

            // Clear any achievements text or reset it to the default state
            achievements.Text = "You haven't unlocked any achievements yet!";

            // Optionally, you could also reset any other UI elements related to the game state
            button1.Text = "Start Clicking!"; // Reset button text if needed

            // You may also choose to clear the saved data
            string saveFilePath = "clickCount.txt"; // Define the save file path
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath); // Delete the saved file to reset the progress
            }

            MessageBox.Show("Click count has been reset!"); // Confirm reset
        }

        private void achievements_Click(object sender, EventArgs e)
        {

        }
    }
}
