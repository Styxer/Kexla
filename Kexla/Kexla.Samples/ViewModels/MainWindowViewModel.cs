using Kexla.Samples.Interfaces;
using Kexla.Samples.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

namespace Kexla.Samples.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<ListViewItem> listItems;
        public ObservableCollection<ListViewItem> ListItems
        {
            get { return listItems; }
            set { SetProperty(ref listItems, value); }
        } 
        #endregion

        #region Buttons
        public DelegateCommand<ICloseable> PowerBtn { get; private set; }
        public DelegateCommand LinkedinBtn { get; private set; }
        public DelegateCommand GitHubBtn  { get; private set; }
        #endregion




        #region C`tor
        public MainWindowViewModel()
        {
            #region Init Buttons
            PowerBtn = new DelegateCommand<ICloseable>(PowerBtnExecute);
            LinkedinBtn = new DelegateCommand(LinkedinBtnExecute);
            GitHubBtn = new DelegateCommand(GitHubBtnExecute);
            #endregion


            PopulateList();



        }

        private void GitHubBtnExecute()
        {
            System.Diagnostics.Process.Start("https://github.com/Styxer/Kexla");
        }

        private void LinkedinBtnExecute()
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/ofir-rosner/");
        }
        #endregion

        private void PopulateList()
        {
            listItems = new ObservableCollection<ListViewItem>(
                Enumerable.Range(1, 6)
                .Select(x => new ListViewItem()
                {
                    Icon = x,
                    Text = "test",
                })
            );
        }

     

        private void PowerBtnExecute(ICloseable window)
        {
            window.Close();
        }
    }
}
