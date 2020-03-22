using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Twittie
{
    
    public partial class Form1 : Form
    {
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        public Form1()
        {
            InitializeComponent();
        }
        async void Reposting(TextBox boxe, TextBox boxe1, IWebDriver web)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                IWebElement login_btn = web.FindElement(By.XPath("//a[@href='/login']"));
                login_btn.Click();

                Thread.Sleep(2000);
                IWebElement login_input = web.FindElement(By.XPath("//input[@name='session[username_or_email]']"));

                login_input.SendKeys(boxe.Text);
                IWebElement pass_input = web.FindElement(By.XPath("//input[@name='session[password]']"));
                pass_input.SendKeys(boxe1.Text);
                IWebElement login = web.FindElement(By.XPath("//div[@role='button']"));
                login.Click();
                Thread.Sleep(3000);

                web.Navigate().GoToUrl("https://twitter.com/search?q=giveaway&src=typed_query&pf=on");
                Thread.Sleep(4000);
                //IWebElement search_input = web.FindElement(By.XPath("//input[@data-testid='SearchBox_Search_Input']"));
                //search_input.SendKeys("giveaway" + OpenQA.Selenium.Keys.Enter);

                IJavaScriptExecutor js = web as IJavaScriptExecutor;
                int i = 0;
                while (true)
                {
                    
                    Thread.Sleep(5000);
                    var tweet = web.FindElements(By.XPath("//div[@data-testid='tweet']"));
                    Thread.Sleep(1500);
                    foreach (IWebElement element in tweet)
                    {
                        //string t = element.Text.ToCharArray();

                        try
                        {
                            IWebElement retweet = element.FindElement(By.XPath("//div[@data-testid='retweet']"));
                            //IWebElement like = element.FindElement(By.XPath("//div[@data-testid='like']"));
                            //like.Click();
                            //Thread.Sleep(1000);
                            retweet.Click();
                            Thread.Sleep(2000);
                            IWebElement retweetConfirm = element.FindElement(By.XPath("//div[@data-testid='retweetConfirm']"));
                            retweetConfirm.Click();
                            Thread.Sleep(2000);
                            //IWebElement comment = element.FindElement(By.XPath("//div[@data-testid='reply']"));
                            //comment.Click();
                            Thread.Sleep(3000);
                            //IWebElement input_comment = element.FindElement(By.XPath("//div[@data-testid='tweetTextarea_0']"));
                            //input_comment.SendKeys("@baby_pochka Let's go!" + i);
                            i++;
                            Thread.Sleep(4000);
                            //input_comment = element.FindElement(By.XPath("//div[@data-testid='tweetButton']"));
                            //input_comment.Click();


                        }
                        catch
                        {
                            Console.WriteLine("Problem");
                        }


                    }

                    //Console.WriteLine(t);
                    Thread.Sleep(1000);
                    js.ExecuteScript("window.scrollBy(0,1000)");
                }
            }
            );
            
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Acc[] accs = new Acc[2];
            for(int i = 0; i < accs.Length; i++)
            {
                accs[i] = new Acc();
            }
            TextBox[] boxes = new TextBox[4];
            boxes[0] = textBox1;
            boxes[1] = textBox2;
            boxes[2] = textBox3;
            boxes[3] = textBox4;

            boxes[0].Text = "og_seekg";
            boxes[1].Text = "@xS=tJ%)xhK6T;9";
            boxes[2].Text = "SuperSir7";
            boxes[3].Text = "kaRboew89";
            for (int i = 0, j=0; i < accs.Length; i+=1, j+=2 )
            {
                //accs[i].web = new OpenQA.Selenium.Chrome.ChromeDriver();
                //accs[i].web.Navigate().GoToUrl("http://twitter.com");
                
                Reposting(boxes[j], boxes[j+1], accs[i].web);
            }


        }

        private void TableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void TableBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class Acc
    {
        public IWebDriver web;
        public string name;
        public string pass;
        public Acc()
        {
            web = new OpenQA.Selenium.Chrome.ChromeDriver();
            web.Navigate().GoToUrl("http://twitter.com");
        }
    }
}
