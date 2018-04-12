using Matchbox.Models;
using Matchbox.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matchbox.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetail : ContentPage
    {
        public TaskDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(item);
        }
    }
}