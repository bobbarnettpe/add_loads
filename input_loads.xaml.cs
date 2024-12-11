using System.Windows;
using System.Windows.Input;
using TextBox = System.Windows.Forms.TextBox;

namespace addloads
{
    /// <summary>
    /// Interaction logic for input_loads.xaml
    /// </summary>
    public partial class input_loads : Window
    {
        public input_loads(double constLd, double sDl, double sLl)
        {
            InitializeComponent();

            txtCONST.Text = constLd.ToString("F0");
            txtSDL.Text = sDl.ToString("F0");
            txtSLL.Text = sLl.ToString("F0");
        }

        private void butOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string GetTextBoxConst()
        {
            return txtCONST.Text;
        }
        public string GetTextBoxSDL()
        {
            return txtSDL.Text;
        }
        public string GetTextBoxSLL()
        {
            return txtSLL.Text;
        }

        /*private void tb_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter & (sender as TextBox).AcceptsReturn == false) MoveToNextUIElement(e);
        }

        void MoveToNextUIElement(System.Windows.Input.KeyEventArgs e)
        {
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            TraversalRequest request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                if (elementWithFocus.MoveFocus(request)) e.Handled = true;
            }
        }
        */
    }
}

