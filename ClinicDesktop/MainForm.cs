using ClinicServiceClientnamespace;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace ClinicDesktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        

        private void buttonLoadClients_Click(object sender, EventArgs e)
        {
            ClinicServiceClient clinicServiceClient = new ClinicServiceClient("http://localhost:5008/", new HttpClient());
            var clients = clinicServiceClient.GetAllAllAsync().Result;
            List <ListViewItem> clientItems = new List<ListViewItem>();
            foreach (Client client in clients)
            {
                ListViewItem item = new ListViewItem()
                {
                    Text = client.ClientId.ToString(),
                    SubItems =
                    {
                        client.SurName,
                        client.FirstName,
                        client.Patronymic
                    }
                };
                clientItems.Add(item);
            }
            listViewClients.Items.Clear();
            listViewClients.Items.AddRange(clientItems.ToArray());
        }
    }
}
