using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace GetPicture
{
    public class GetRequest
    {
        HttpWebRequest _request;
        HttpClient _client;
        HtmlDocument _htmlDocument;

        string _address;

        public string Response { get; set; }

        public GetRequest(string address)
        {
            _address = address;
        }


        public async void GetHTML(List<HtmlNode> PictureList)
        {
            var _client = new HttpClient();
            var html = await _client.GetStringAsync(_address);
            var _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(html);

            try
            {
                var PictureHTML = _htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("page-layout__content-wrapper b-page__content")).ToList();

                var PictureListItems = PictureHTML[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("serp-item__preview")).ToList();

                return;
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
