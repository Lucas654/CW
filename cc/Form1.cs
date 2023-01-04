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
using System.Net.NetworkInformation;

namespace cc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trace();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        public void Trace()
        {
            string host = textBox1.Text;
            int maxHops = 30;

            listBox1.Items.Add($"Tracing route to {host} with a maximum of {maxHops} hops:");

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions(1, true);
            options.DontFragment = true;

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            int ttl = 1;
            IPAddress address = Dns.GetHostEntry(host).AddressList[0];

            while (ttl <= maxHops)
            {
                PingReply reply = pingSender.Send(address, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    listBox1.Items.Add($"{ttl}\t{reply.Address.ToString()}");
                    break;
                }
                else
                {
                    listBox1.Items.Add($"{ttl}\t{reply.Status.ToString()}");
                }

                ttl++;
                options.Ttl = ttl;
            }

            listBox1.Items.Add("Trace complete.");
        }
    }
}
