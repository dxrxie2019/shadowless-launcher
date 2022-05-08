using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections;
using KMCCC.Launcher;
using KMCCC.Authentication;
using SquareMinecraftLauncher;
using Panuon.UI.Silver;



namespace mc_launcher
    {
        /// <summary>
        /// MainWindow.xaml 的交互逻辑
        /// </summary>
        public partial class MainWindow : Window
        {
            LoginUI.online online = new LoginUI.online();
            public static LauncherCore Core = LauncherCore.Create();
            ArrayList java_path = new ArrayList();
        public class Setting
        {
            String Ram = "1024";
           
        }


        public MainWindow()
            {


                InitializeComponent();
                comboBox1.SelectedIndex = 0;
                java_path.Add(Environment.GetEnvironmentVariable("JAVA_HOME") + @"\bin\javaw.exe");
                javapath.Text = java_path[0].ToString();
                Username.Text = ToString();
                var versions = Core.GetVersions().ToArray();
                comboBox1.ItemsSource = versions;
                comboBox1.DisplayMemberPath = "Id";
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                var ver = (KMCCC.Launcher.Version)comboBox1.SelectedItem;
                LaunchOptions options = new LaunchOptions
                {
                    JavaPath = javapath.Text,
                    Version = ver,
                    MaxMemory = 1024,
                    Mode = LaunchMode.MCLauncher,
                    Size = new WindowSize { Height = 768, Width = 1280 }
                };
            MessageBox.Show("提示:游戏启动过程中,请不要反复点击启动按钮!(点击确定以继续启动)");
                options.Authenticator = new OfflineAuthenticator(Username.Text); //离线启动



                var result = Core.Launch(options);



                if (!result.Success)
                {
                    switch (result.ErrorType)
                    {
                        case ErrorType.AuthenticationFailed:
                            MessageBox.Show(this, "正版验证失败！请检查你的账号密码", "账号错误\n详细信息：" + result.ErrorMessage);
                            break;
                        case ErrorType.NoJAVA:
                            MessageBox.Show(result.ErrorMessage + "没有在此路径找到java!");
                            break;
                        case ErrorType.UncompressingFailed:
                            MessageBox.Show(result.ErrorMessage + "文件损坏,请重新下载");
                            break;
                        default:
                            MessageBox.Show(result.ErrorMessage + "启动错误");
                            break;
                    }
                }
            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }
        private void Button_online(object sender, RoutedEventArgs e)
        {
            ContentControl1.Content = new Frame
            {
                Content = online
            };



        
           
         }
        }
     }




