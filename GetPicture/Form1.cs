using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace GetPicture
{
    public partial class Form1 : Form
    {
        List<string> links;
        List<string> prev_links;
        List<string> pic_size;
        string width_pic;
        string height_pic;
        ImageList ImgList;
        WebClient wClient;
        BackgroundWorker worker;
        int pic_index;
        int max = 100;

        public Form1()
        {
            InitializeComponent();

            links = new List<string>();
            prev_links = new List<string>();
            pic_size = new List<string>();
            ImgList = new ImageList();
            wClient = new WebClient();
            worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            status.Text = e.ProgressPercentage.ToString()+" загружено";
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Файлы загружены!", "GetPicture");
            status.Text = "Готово";
            progressBar1.Value = 30;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                string item = Path.GetFileName(listlinks.Items[i].ToString());
                string path = Application.StartupPath + @"\" + $"{picname.Text}";
                DirectoryInfo dir = Directory.CreateDirectory(path);
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(new Uri(listlinks.Items[i].ToString()), Path.Combine(path, item));
                    }
                    catch (Exception)
                    {
                        i++;
                    }
                }
                worker.ReportProgress(i);
            }
        }

        public void Clear()
        {
            LV.Clear();
            links.Clear();
            listlinks.Items.Clear();
            prev_links.Clear();
            ImgList.Images.Clear();
            progressBar1.Value = 0;
            picname.Text = "";
        }

        public async void GetPicList(string item)
        {
            LV.Clear();
            links.Clear();
            listlinks.Items.Clear();
            prev_links.Clear();
            ImgList.Images.Clear();
            progressBar1.Value = 0;

            //Parse
            var url = $"https://yandex.ru/images/search?text={item}";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("page-layout__content-wrapper b-page__content")).ToList();

            var ProductlistItems = ProductHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("serp-item__preview")).ToList();
            int i = 0;

            LV.LargeImageList = ImgList;
            LV.LargeImageList.ImageSize = new Size(120,67);
            ImgList.ColorDepth = ColorDepth.Depth32Bit;
            //Список
            foreach (var picitem in ProductlistItems)
            {
                //Картинка 
                var picture_preview_url = picitem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");
                var picture_url = picitem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                //Logs
                prev_links.Add("https:" + picture_preview_url);
                ImgList.Images.Add(LoadImage(prev_links[i]));
                links.Add("https://www.yandex.ru" + picture_url);
                pic_size.Add(picitem.InnerText.Replace("&nbsp;", "").Replace("HD", ""));
                ListViewItem litem = new ListViewItem(new string[] { picitem.InnerText.Replace("&nbsp;", "").Replace("HD", "") });
                litem.ImageIndex = i;


                
                LV.Items.Add(litem);
                var ulink = System.Uri.UnescapeDataString(picture_url);
                listlinks.Items.Add(ulink.Replace($"/images/search?pos={i}&amp;img_url=", "")
                    .Replace($"&amp;text={item}&amp;rpt=simage",""));
                i++;
                if (i == max)
                    break;
            }
        }

        public void ViewFullPicture(string p_url)
        {
            Form view = new Form();
            view.Show();
            view.FormBorderStyle = FormBorderStyle.FixedDialog;
            view.StartPosition = FormStartPosition.CenterParent;
            PictureBox box = new PictureBox();
            view.Controls.Add(box);
            box.Dock = DockStyle.Fill;
            box.LoadAsync(p_url);
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            
            view.Size = new Size(Convert.ToInt32(width_pic)/2,Convert.ToInt32(height_pic)/2);
           // view.Text = pic_size[pic_index].ToString();
            view.Text = pic_index.ToString();
        }

        private Image LoadImage(string url)
        {
            System.Net.WebRequest request =
            System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
            response.GetResponseStream();
            Bitmap bmp = new Bitmap(responseStream);
            responseStream.Dispose();
            return bmp;
        }

        private void searchbtn_Click_1(object sender, EventArgs e)
        {
            if (picname.Text != "")
            {
                GetPicList(picname.Text);
                downloadbtn.Enabled = true;
            }
            else
            {
                MessageBox.Show(this,"Введите слово для поиска изображений!",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void LV_MouseClick(object sender, MouseEventArgs e)
        {
            string slovo = picname.Text;
            if (LV.SelectedIndices.Count >= 0)
                pic_index = LV.Items.IndexOf(LV.SelectedItems[0]);
            string pic_url = prev_links[pic_index].ToString();
            var undecodedlink = links[pic_index].ToString();
            var decodelink = System.Uri.UnescapeDataString(undecodedlink)
                .Replace($"https://www.yandex.ru/images/search?pos={pic_index}&amp;img_url=", "")
                .Replace($"&amp;text={slovo}&amp;rpt=simage", "")
                .Replace("&amp;lr=36", "")
                .Remove('.jpg' );

            string[] s = pic_size[pic_index].Split('×');
            width_pic = s[0];
            height_pic = s[1];  
            ViewFullPicture(decodelink);
        }

        private void linkbox_Click(object sender, EventArgs e)
        {
            linkbox.SelectAll();
            linkbox.Copy();
        }

        private void downloadbtn_Click(object sender, EventArgs e)
        {               
             worker.RunWorkerAsync();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}
