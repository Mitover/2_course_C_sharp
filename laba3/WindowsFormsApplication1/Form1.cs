using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Api : Form
    {
        public Api()
        {
            InitializeComponent();
        }

        private void Api_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Список кафедр ВШЭКН";
            var get = new Uri("https://api.vk.com/method/database.getChairs?faculty_id=2216353&count=100&v=5.73");

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                using (Stream stream = client.OpenRead(get))
                {
                    if (stream == null)
                        return;

                    var serializer = new DataContractJsonSerializer(typeof(Response<ВШЭКН>));
                    var response = (Response<ВШЭКН>)serializer.ReadObject(stream);
                    textBox1.Text = "\0";
                    foreach (var item in response.Value.Items)
                    {
                        textBox1.Text += item.Title + Environment.NewLine;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Мои друзья";
            var get = new Uri("https://api.vk.com/method/friends.get?user_id=15878842&count=100&v=5.74&fields=domain");

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                using (Stream stream = client.OpenRead(get))
                {
                    if (stream == null)
                        return;

                    var serializer = new DataContractJsonSerializer(typeof(Response<БАЕ>));
                    var response = (Response<БАЕ>)serializer.ReadObject(stream);
                    textBox1.Text = "\0";
                    foreach (var item in response.Value.Items)
                    {
                        textBox1.Text += item.First + " " + item.Last + Environment.NewLine;
                    }
                }
            }
        }

        public void callback(string id)
        {
            label1.Text = "Обсуждение";
            string to = "https://api.vk.com/method/board.getTopics?group_id=" + id + "&count=50&v=5.74";
            var get = new Uri(to);

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                using (Stream stream = client.OpenRead(get))
                {
                    if (stream == null)
                        return;

                    var serializer = new DataContractJsonSerializer(typeof(Response<ВШЭКН>));
                    var response = (Response<ВШЭКН>)serializer.ReadObject(stream);
                    textBox1.Text = "\0";
                    if (response.Value != null && response.Value.Items != null)
                    {
                        foreach (var item in response.Value.Items)
                        {
                            textBox1.Text += item.Title + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 окно = new Form3();
            окно.Owner = this;
            окно.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 окно = new Form2();
            окно.ShowDialog();
        }
    }

    [DataContract]
    public class Response<T>
    {
        [DataMember(Name = "response")]
        public ResponseValue<T> Value { get; set; }
    }

    [DataContract]
    public class ResponseValue<T>
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "items")]
        public T[] Items { get; set; }
    }

    [DataContract]
    public class БАЕ
    {
        [DataMember(Name = "first_name")]
        public string First { get; set; }

        [DataMember(Name = "last_name")]
        public string Last { get; set; }
    }

    [DataContract]
    public class ВШЭКН
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
