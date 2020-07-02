using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Xpf.Core;

namespace SimpleApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _taskInvoker.Click += async delegate { await ExecOperationByTask(); };
            _threadInvoker.Click += ExecOperationByThread;
        }

        private void ExecOperationByThread(object sender, RoutedEventArgs e)
        {
            //1) Enable loading
            Loading1.IsSplashScreenShown = true;
            //2) Disable button
            _threadInvoker.IsEnabled = false;
            //3) Execute method by newly created thread
            new Thread(() =>
            {
                try
                {
                    var response = WebRequest.Create("https://localhost:44345/api/operation").GetResponse();
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            Dispatcher.Invoke(() => SetTextBlockResult(_result1, reader.ReadLine()));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Dispatcher.Invoke(() => Loading1.IsSplashScreenShown = false);
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    Dispatcher.Invoke(() =>
                    {
                        //4) Hide loading
                        Loading1.IsSplashScreenShown = false;
                        //5) Enable button
                        _threadInvoker.IsEnabled = true;
                    });
                }
            }).Start();
        }

        private void SetTextBlockResult(TextBlock txtBlock, string result) => txtBlock.Text = result;

        private Task<string> CallWebApiMethod()
        {
            return Task.Run(() =>
            {
                var response = WebRequest.Create("https://localhost:44345/api/operation").GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadLine();
                    }
                }

            });
        }
        private async Task ExecOperationByTask()
        {
            //1) Enable loading
            Loading2.IsSplashScreenShown = true;
            //2) Disable button
            _taskInvoker.IsEnabled = false;
            try
            {
                //3) Execute method by Task
                _result2.Text = await CallWebApiMethod();
            }
            catch (Exception exception)
            {
                Loading2.IsSplashScreenShown = false;
                MessageBox.Show(exception.Message);
            }
            finally
            {
                //4) Hide loading
                Loading2.IsSplashScreenShown = false;
                //5) Enable button
                _taskInvoker.IsEnabled = true;
            }
        }

    }
}
