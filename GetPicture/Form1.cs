using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        WebBrowser browser;

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

            browser = new WebBrowser();


            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            status.Text = e.ProgressPercentage.ToString() + " загружено";
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

            Text = "GetPicture" + " | получаем доступ...";

            //if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='CheckboxCaptcha']").Equals("Я не робот")) 
            //{
            //    Captcha(html);
            //        }
            //else                     

            try
            {

                var ProductHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("page-layout__content-wrapper b-page__content")).ToList();

                var ProductlistItems = ProductHtml[0].Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Contains("serp-item__preview")).Take(max);
                int i = 0;

                LV.LargeImageList = ImgList;
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
                    pic_size.Add(picitem.InnerText.Replace("&nbsp;", "").Replace("HD", ""));
                    ListViewItem litem = new ListViewItem(new string[] { picitem.InnerText.Replace("&nbsp;", "").Replace("HD", "") });
                    litem.ImageIndex = i;



                    LV.Items.Add(litem);
                    var ulink = System.Uri.UnescapeDataString(picture_url);
                    i++;
                    Text = $"GetPicture" + $" | получаем {i} изображений";

                    if (i == max)
                        break;
                }
            }
            catch (Exception ex)
            {
                Text = "GetPicture" + " | Забанили на Яндексе, перерыв 5 минут";
                // Captcha(item);
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
            box.Click += delegate { view.Close(); };

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
                MessageBox.Show(Application.ProductName,"er", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void LV_MouseClick(object sender, MouseEventArgs e)
        {
            string slovo = picname.Text;
            if (LV.SelectedIndices.Count >= 0)
                pic_index = LV.Items.IndexOf(LV.SelectedItems[0]);
            string pic_url = prev_links[pic_index].ToString();
            var undecodedlink = links[pic_index].ToString();
            var decodelink = System.Uri.UnescapeDataString(undecodedlink);

            string[] s = pic_size[pic_index].Split('×');
            width_pic = s[0];
            height_pic = s[1];
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

        private void picname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (picname.Text != "")
                {
                    GetPicList(picname.Text);
                    downloadbtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show(this, "Введите слово для поиска изображений!",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        public void CheckCaptcha(string s)
        {
            // Invoke(() => this.Text = "Задержка между запросами " + searchdelay.ToString() + "c.");
            string url = "https://yandex.ru/images/search?text=" + s;
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.AllowAutoRedirect = true;//Разрешаем автоматический редирект
            req.ContentType = "application/x-www-form-urlencoded";
            req.Referer = "http://google.com";//Реферер. Тут можно указать любой URL
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";

            CookieContainer cookie_container = new CookieContainer();
            req.CookieContainer = cookie_container;

            var resp = (HttpWebResponse)req.GetResponse();
            CookieCollection cookie_collection = new CookieCollection();
            cookie_collection = resp.Cookies;
            cookie_container.Add(cookie_collection);

            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string html = sr.ReadToEnd();
            sr.Close();
            //Cоздаем объект класса HtmlDocument
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //Загружаем в doc полученный HTML
            doc.LoadHtml(html);

            //Парсим страницу на наличие капчи
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='CheckboxCaptcha-Label']");

            //если в запросе попалась капча - увеличиваем выдержку между запросами
            if (nodes != null)
            {
                // Properties.Settings.Default.searchdelay = ++searchdelay;
                Properties.Settings.Default.Save();
            }

            //пока капча присутствует - выполняем
            while (nodes != null)
            {
                if (nodes != null)
                {
                    HtmlAgilityPack.HtmlNodeCollection inputs = nodes[0].SelectNodes("//div[@class='CheckboxCaptcha']");
                    string key_captcha = inputs[0].GetAttributeValue("value", "false").Replace("&amp", "");
                    string return_path_captcha = inputs[0].GetAttributeValue("value", "false").Replace("&amp;", "&");
                    //Парсим страницу на получение тэга <img>, в котором прописана капча
                    HtmlAgilityPack.HtmlNode image = doc.DocumentNode.SelectSingleNode("//div[@class='CheckboxCaptcha']//img");
                    //Получаем URL картинки капчи (путь по которому её можно скачать)
                    string url_captcha = image.GetAttributeValue("src", "true");
                    //Создаем форму ввода капчи
                    Form formCaptcha = new Form();
                    formCaptcha.Show();
                    formCaptcha.StartPosition = FormStartPosition.CenterScreen;
                    //Отображаем в PictureBox на форме картинку капчи
                    PictureBox pictureBox1 = new PictureBox();
                    formCaptcha.Controls.Add(pictureBox1);
                    pictureBox1.ImageLocation = url_captcha;
                    //устанавливаем время автозакрытия окна ввода капчи
                    // formCaptcha.captchadelay = captchadelay;
                    //Отображаем форму ввода капчи для пользователя
                    TextBox tbPassword = new TextBox();
                    formCaptcha.Controls.Add(tbPassword);


                    //если капча была введена
                    if (tbPassword.Text != "")
                    {
                        key_captcha = WebUtility.UrlEncode(key_captcha);
                        return_path_captcha = WebUtility.UrlEncode(return_path_captcha);
                        string password = WebUtility.UrlEncode(tbPassword.Text);
                        //формируем URL для отправки кода капчи
                        url = @"https://www.yandex.ru/checkcaptcha";// + "?key=" + key_captcha + "&retpath=" + return_path_captcha + "&rep=" + password;

                        try
                        {
                            //запрашиваем составленный URL
                            req = (HttpWebRequest)WebRequest.Create(url);
                            req.CookieContainer = cookie_container;
                            req.ContentType = "application/x-www-urlencoded";
                            req.Referer = "http://google.com";//Реферер. Тут можно указать любой URL
                            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                            resp = (HttpWebResponse)req.GetResponse();
                            resp.Cookies = cookie_collection;
                            //если капча не прошла (ответный URL не был найден)
                            if (resp.ResponseUri == null)
                            {
                                url = "https://yandex.ru/images/search?text=" + s;
                            }
                            else //если капча прошла удачно
                            {
                                url = resp.ResponseUri.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            // InvokeIfNeeded(() => LogException("Отправка кода капчи: ", ex, dt_exceptions));
                            url = "https://yandex.ru/images/search?text=" + s;
                        }
                    }
                    else //если капча не была введена, и окно закрылось по таймеру - запрашиваем
                    {
                        url = "https://yandex.ru/images/search?text=" + s;
                    }
                }
                req = (HttpWebRequest)WebRequest.Create(url);
                //запрашиваем исходный req и обновляем resp
                resp = (HttpWebResponse)req.GetResponse();
                //получаем html
                sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                html = sr.ReadToEnd();
                //Загружаем в doc полученный HTML
                doc.LoadHtml(html);
                //Парсим страницу на наличие капчи 
                //если капча присутствует, то цикл продолжится 
                nodes = doc.DocumentNode.SelectNodes("//div[@class='form form_state_image form_error_no form_audio_yes i-bem']");
                //автоматически увеличиваем время задержки капчи, если капча после задержки опять вылезла, сохраняем настройки
                if (nodes != null)
                    //Properties.Settings.Default.captchadelay = ++captchadelay;
                    Properties.Settings.Default.Save();
            }
            sr.Close();
            // return html;
        }

        private void timeout_Tick(object sender, EventArgs e)
        {
            GetPicList(picname.Text);
        }

        public void Captcha(string doc)
        {
            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(doc);

            var nodes = htmldoc.DocumentNode.SelectSingleNode("//div[@class='CheckboxCaptcha']");
            var cap = nodes.InnerText.Replace("Нажмите, чтобы продолжить", "");
            var lab1 = nodes.InnerText.Replace("Я не робот", "");
            var capdoc = nodes.InnerHtml;
            if (nodes != null)
            {
                //HtmlNode image = htmldoc.DocumentNode.SelectSingleNode("//div[@class='Captcha']//img");
                //  var url_image = image.GetAttributeValue("src", "");
                Form captcha = new Form();
                captcha.Text = cap;
                captcha.FormBorderStyle = FormBorderStyle.FixedDialog;
                captcha.MinimizeBox = false;
                captcha.MaximizeBox = false;
                captcha.Size = new Size(400, 150);
                captcha.Show();

                Label labcap = new Label();
                labcap.Location = new Point(5, 9);
                labcap.Size = new Size(160, 20);
                labcap.Text = lab1;

                Button okbtn = new Button();
                okbtn.Text = "Подтвердить";
                okbtn.Location = new Point(170, 5);
                okbtn.AutoSize = true;
                okbtn.Click += Okbtn_Click;

                //browser.Location = new Point(10, 10);
                //browser.Size = new Size(280, 120);
                //browser.DocumentText = doc;
                //browser.Dock = DockStyle.Fill;

                //PictureBox box = new PictureBox();
                //box.LoadAsync("");

                // captcha.Controls.Add(box);
                captcha.Controls.Add(labcap);
                captcha.Controls.Add(okbtn);
                // captcha.Controls.Add(browser);

            }
        }

        private async void Okbtn_Click(object sender, EventArgs e)
        {
            string postdata = "/checkcaptcha?key=ea385719-4d32722f-16457e4f-a5fdaa82_2/1642343712/4fded01a60cbdc10b27a13d4f08abd7f_4e6686042915f593ab1f01ffb6019ba3&";
            var encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(postdata);
            string url = "";
            browser.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
        }

        private void picname_Click(object sender, EventArgs e)
        {
            picname.Clear();
        }
    }
}
