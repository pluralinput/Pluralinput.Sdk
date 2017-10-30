using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Cursors.Annotations;
using Pi = Pluralinput.Sdk;

namespace Cursors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Pi.InputManager inputManager;

        public ObservableCollection<CustomCursor> Cursors { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            inputManager = new Pi.InputManager();
            var cursors = inputManager.Devices.Mice.Select(m => new CustomCursor(m));
            Cursors = new ObservableCollection<CustomCursor>(cursors);
            DataContext = this;

            inputManager.Devices.Mice.First().ButtonUp += (o, e) =>
            {
                Console.WriteLine($"{o}: ButtonUp {e.Button}");
            };
        }

        public class CustomCursor : INotifyPropertyChanged
        {
            private Pi.Mouse mouse;

            public CustomCursor(Pi.Mouse mouse)
            {
                this.mouse = mouse;
                Name = mouse.DeviceName;

                mouse.Move += Mouse_Move;
            }

            public long LastX { get; set; }
            public long LastY { get; set; }
            public string Name { get; set; }

            private void Mouse_Move(object sender, Pi.MouseMoveInputEventArgs e)
            {
                LastX = e.LastX;
                LastY = e.LastY;
                OnPropertyChanged(nameof(LastX));
                OnPropertyChanged(nameof(LastY));
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
