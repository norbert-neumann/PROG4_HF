namespace SolarSystem.UI
{
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
    using System.Windows.Shapes;
    using SolarSystem.UI.Data;
    using SolarSystem.UI.VM;

    /// <summary>
    /// Interaction logic for EditorWindow.xaml.
    /// </summary>
    [CLSCompliant(false)]
    public partial class EditorWindow : Window
    {
        private EditorViewModel VM;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorWindow"/> class.
        /// </summary>
        public EditorWindow()
        {
            this.InitializeComponent();
            this.VM = this.FindResource("VM") as EditorViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorWindow"/> class.
        /// Creates a new window for editing a planet.
        /// </summary>
        /// <param name="newPlanet">Planet to be edited.</param>
        public EditorWindow(VMPlanet newPlanet)
            : this()
        {
            this.VM.Planet = newPlanet;
        }

        private VMPlanet Planet
        {
            get { return this.VM.Planet; }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
