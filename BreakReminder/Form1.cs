using Newtonsoft.Json;
using QuoteAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakReminder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var quote = await GetQuote();
            var time = DateTime.Now.ToString("HH:mm:ss tt");
            var title = "Do you need a break?";

            var heyThere = $"Hey there!\nThe time is {time}\n\n{quote}";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            MessageBox.Show(heyThere, title, buttons, MessageBoxIcon.Warning);
            Application.Exit();
        }

        private async Task<string> GetQuote()
        {
            var url = "https://zenquotes.io/api/random";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync();

                    List<Model> quoteResponse = JsonConvert.DeserializeObject<List<Model>>(response);
                    foreach (var item in quoteResponse)
                    {
                        var quote = item.Q;
                        var author = item.A;
                        var heyThere = $"Hourly quote\n{quote}\n-{author}";
                        return heyThere;
                    }
                }
                return "Oops, something went wrong with the quote!";
            }
        }

    }
}
