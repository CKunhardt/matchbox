using Matchbox.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matchbox.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskList : ContentPage
    {
        public TaskList()
        {
            InitializeComponent();
            BindingContext = new TaskListViewModel();
        }
    }
}