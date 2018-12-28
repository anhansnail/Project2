using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
namespace DoAn2
{
    public partial class Form1 : Form
    {
        public string textPart1 = "<map name=\"Map\" id=\"Map\"><area alt = \"\" title=\"\" href=\"#\" shape=\"rect\" coords=\"";
        public string textPart2 = "";
        public string textPart3 = "\" /> [...]</map>";

        public Form1()
        {

            InitializeComponent();

        }

    

 

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true || (radioButton2.Checked == false && radioButton2.Checked == false))
            {
                // textBox2.Enabled = false;
                //Console.WriteLine("Hello");
                //show hộp thoại open file ra
                //nhận kết quả về qua biến kiểu dialogResult
                DialogResult result = openFileDialog1.ShowDialog();
                //kiểm tra xem người dùng đã chọn file chưa
                if (result == DialogResult.OK)
                {
                    //lấy ảnh
                    Image img = Image.FromFile(openFileDialog1.FileName);

                    //Gán ảnh
                    pictureBox1.Image = img;

                    //  pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    // pictureBox1.SetBounds();
                    //Xử lý
                }

            }
            else
            {//
             //  textBox2.Enabled = true;

                var request = WebRequest.Create(textBox2.Text);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pictureBox1.Image = Bitmap.FromStream(stream);
                }
                //Console.WriteLine("No network");


            }
            textBox1.Text = "";
            textPart2 = "";
          //  Size s = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
            //pictureBox1.Size = s;//set size pictureBox theo size ảnh
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            

            
            if (pictureBox1.Image == null) Console.WriteLine("không có ảnh");
          
            if (pictureBox1.Width == 0 || pictureBox1.Height == 0 || pictureBox1.Image.Width == 0 || pictureBox1.Image.Height == 0) Console.WriteLine("Không tồn tại");
            int WidthPicturebox = pictureBox1.Width;
            int HeightPictureBox = pictureBox1.Height;
            int WidthImage = pictureBox1.Image.Width;
            int HeightImage = pictureBox1.Image.Height;
            float ratioWidth = WidthImage / WidthPicturebox;//ti le rong,ti le x
            float ratioHeight = HeightImage / HeightPictureBox;//ti le cao,ti le y
            float newX;
            float newY;
            if ((WidthPicturebox >= WidthImage) && (HeightPictureBox >= HeightImage))
            {//neu cao rong cua anh nho hon thif tinh don gian
                newX = (float)(e.X - (WidthPicturebox - WidthImage) / 2);
                newY = (float)(e.Y - (HeightPictureBox - HeightImage) / 2);
                textPart2 += newX + ";" + newY + ";";
                string text = textPart1 + textPart2 + textPart3;
                textBox1.Text = text;
            }
            else
            {
                if ((float)ratioWidth >= (float)ratioHeight)//neu ti le ngang lon hon cao thi co theo ti le ngang
                {
                    newX = e.X;//X mới sẽ bằng chính Width của pictureBox
                    newY = e.Y - ((HeightPictureBox - (HeightImage * newX / WidthImage)) / 2);//HeightImage * newX / WidthImage là độ dài mới của ảnh
                    textPart2 += newX + ";" + newY + ";";
                    string text = textPart1 + textPart2 + textPart3;
                    textBox1.Text = text;
                }
                else
                {
                    newY = e.Y;//Y mới sẽ bằng chính Height của pictureBox
                    newX = e.X - (WidthPicturebox - WidthImage * newY / HeightImage) / 2;//WidthImage * newY / HeightImage là độ rộng mới của ảnh
                    textPart2 += newX + ";" + newY + ";";
                    string text = textPart1 + textPart2 + textPart3;
                    textBox1.Text = text;
                }

            }
        }
    }
}
