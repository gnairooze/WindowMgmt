using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ShortCuts.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region events handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setWindowsList(WindowHelper.ReadWindows());
        }

        private void btnReadWindows_Click(object sender, RoutedEventArgs e)
        {
            setWindowsList(WindowHelper.ReadWindows());
        }

        private void lstWidnows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count == 0)
            {
                return;
            }
            WindowModel item = (WindowModel)e.AddedItems[0];

            WindowModel updatedItem = WindowHelper.ResetWindow(item.MainWindowHandle, item.Id);

            setWindowsList(WindowHelper.ReadWindows());

            txtSelected.DataContext = updatedItem;

            MessageBox.Show($"{item.Summary} {Environment.NewLine} reset to{Environment.NewLine} {updatedItem.Summary}", "Reset Window");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            WindowModel model = (WindowModel)txtSelected.DataContext;

            WindowHelper.SetForground(model.MainWindowHandle);

            InputHelper.SendKeyboardInput(new InputHelper.KeyboardInput[]
{
                new InputHelper.KeyboardInput
                {
                    wScan = InputHelper.Keys.KEY_L_CTRL,
                    dwFlags = (uint)(InputHelper.KeyEventF.KeyDown | InputHelper.KeyEventF.Scancode),
                },
                new InputHelper.KeyboardInput
                {
                    wScan = InputHelper.Keys.KEY_V,
                    dwFlags = (uint)(InputHelper.KeyEventF.KeyDown | InputHelper.KeyEventF.Scancode),
                },
                new InputHelper.KeyboardInput
                {
                    wScan = InputHelper.Keys.KEY_V,
                    dwFlags = (uint)(InputHelper.KeyEventF.KeyUp | InputHelper.KeyEventF.Scancode),
                },
                new InputHelper.KeyboardInput
                {
                    wScan = InputHelper.Keys.KEY_L_CTRL,
                    dwFlags = (uint)(InputHelper.KeyEventF.KeyUp | InputHelper.KeyEventF.Scancode),
                }
});
            //InputHelper.ClickKey(InputHelper.Keys.KEY_E);

            //InputHelper.ClickKey(0x11);

            //InputHelper.SendKeyboardInput(new InputHelper.KeyboardInput[]
            //{
            //    new InputHelper.KeyboardInput
            //    {
            //        wScan = 0xe0,
            //        dwFlags = (uint)(InputHelper.KeyEventF.ExtendedKey | InputHelper.KeyEventF.Scancode),
            //    },
            //    new InputHelper.KeyboardInput
            //    {
            //        wScan = 0x30,
            //        dwFlags = (uint)(InputHelper.KeyEventF.ExtendedKey | InputHelper.KeyEventF.Scancode)
            //    }
            //});
        }
        #endregion

        private void setWindowsList(List<WindowModel> lst)
        {
            lstWidnows.ItemsSource = lst;
        }
    }
}
