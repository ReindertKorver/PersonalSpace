using PersonalSpaceUI.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using WebApi.Entities;

namespace PersonalSpaceUI.Pages.Main
{
    /// <summary>
    /// Interaction logic for Documents.xaml
    /// </summary>
    public partial class Documents : Page
    {
        public Documents()
        {
            InitializeComponent();
            GetDocuments();
        }

        public async void GetDocuments()
        {
            try
            {
                List<Document> userResponse = await BaseConnection.DoRequest<List<Document>>(null, HttpMethod.Get, headers: new (string, string)[] { ("Authorization", BaseConnection.GetAuth(ApplicationRuntimeData.AuthUser.Username, ApplicationRuntimeData.AuthUser.Password)), }, addToURL: new Document().APILocation);
                DocumentsList.ItemsSource = userResponse.Where(d => d.UserId == ApplicationRuntimeData.CurrentUser.Id);
                DocumentsList.DisplayMemberPath = "Description";
            }
            catch (Exception e)
            {
            }
        }

        private void DocumentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Document doc = (Document)DocumentsList.SelectedItem;
                URL.Text = BaseConnection.APIURL + doc.Url;
                Description.Text = doc.Description;
                UserName.Text = doc.User.Username;
                var webClient = new WebClient();
                webClient.Headers.Add("Authorization", BaseConnection.GetAuth(ApplicationRuntimeData.AuthUser.Username, ApplicationRuntimeData.AuthUser.Password));
                var data = webClient.DownloadData(BaseConnection.APIURL + doc.Url);
                var package = System.IO.Packaging.Package.Open(new MemoryStream(data));
                XpsDocument xpsDocument = new XpsDocument(package,
                                                  CompressionOption.SuperFast,
                                                  BaseConnection.APIURL + doc.Url);
                FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
                Viewer.Document = fds;
            }
            catch (Exception ex)
            {
            }
        }
    }
}