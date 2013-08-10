using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IronWomen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeUsers();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        int vishalCount;
        int kedarCount;
        int abhijitCount;
        int krittikaCount;
        int rajkamalCount;
        int satyakamalCount;
        int swatiCount;
        int nilaCount;
        int totalDollars;
        int totalRupees;

        private MobileServiceCollection<IronUsers, IronUsers> ironUsersCollection;
        private IMobileServiceTable<IronUsers> ironUsersTable = App.MobileService.GetTable<IronUsers>();

        public class IronUsers
        {
            public int Id { get; set; }

            [JsonProperty(PropertyName = "Name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "Count")]
            public int Count { get; set; }
        }
        

        private enum Operation
        {
            plus, minus, none
        };
        
        private async void InitializeUsers()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                ironUsersCollection = await ironUsersTable.ToCollectionAsync();

                if (ironUsersCollection.Count <= 0)
                {
                    this.InsertTodoItem("vishal");
                    this.InsertTodoItem("kedar");
                    this.InsertTodoItem("abhijit");
                    this.InsertTodoItem("krittika");
                    this.InsertTodoItem("satyakamal");
                    this.InsertTodoItem("rajkamal");
                    this.InsertTodoItem("swati");
                    this.InsertTodoItem("nila");
                }
                else
                {
                    foreach (IronUsers ironUsers in ironUsersCollection)
                    {
                        switch (ironUsers.Name.ToLower())
                        {
                            case "vishal":
                                vishalCount = ironUsers.Count;
                                break;
                            case "kedar":
                                kedarCount = ironUsers.Count;
                                break;
                            case "abhijit":
                                abhijitCount = ironUsers.Count;
                                break;
                            case "krittika":
                                krittikaCount = ironUsers.Count;
                                break;
                            case "satyakamal":
                                satyakamalCount = ironUsers.Count;
                                break;
                            case "swati":
                                swatiCount = ironUsers.Count;
                                break;
                            case "rajkamal":
                                rajkamalCount = ironUsers.Count;
                                break;
                            case "nila":
                                nilaCount = ironUsers.Count;
                                break;
                            default:
                                break;
                        }
                    }

                    UpdateTotalDollars();
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;                
            }
        }

        private async void InsertTodoItem(string name)
        {
            IronUsers ironUser = new IronUsers()
            {
                Name = name,
                Count = 0
            };
            await ironUsersTable.InsertAsync(ironUser);            
        }

        private async Task<IronUsers> GetCurrentCount(string name)
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                ironUsersCollection = await ironUsersTable.Where(iu => iu.Name.ToLower() == name.ToLower())
                        .ToCollectionAsync();

                if (ironUsersCollection.Count <= 0)
                {
                    return null;
                }

                return ironUsersCollection[0];                
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                
                return null;
            }
        }

        private async void UpdateCount(IronUsers ironUser)
        {
            await ironUsersTable.UpdateAsync(ironUser);
        }

        private void UpdateTotalDollars()
        {
            VishalCount.Text = FormatNumberString(vishalCount);
            KedarCount.Text = FormatNumberString(kedarCount);
            AbhijitCount.Text = FormatNumberString(abhijitCount);
            KrittikaCount.Text = FormatNumberString(krittikaCount);
            RajkamalCount.Text = FormatNumberString(rajkamalCount);
            SatyakamalCount.Text = FormatNumberString(satyakamalCount);
            SwatiCount.Text = FormatNumberString(swatiCount);
            NilaCount.Text = FormatNumberString(nilaCount);

            totalDollars = vishalCount + kedarCount + krittikaCount + abhijitCount + rajkamalCount + satyakamalCount + swatiCount + nilaCount;
            TotalDollar.Text = FormatNumberString(totalDollars);
            UpdateTotalRupees();
        }

        private void UpdateTotalRupees()
        {
            totalRupees = totalDollars * 60;
            TotalRupees.Text = FormatNumberString(totalRupees);
        }

        private async Task<int> GetUpdatedCount(string name, Operation operate)
        {
            int number;
            
            IronUsers ironUser = null;
            try
            {
                ironUser = await GetCurrentCount(name);                
                number = ironUser.Count;
            }
            catch
            {
                number = 0;
            }
            switch (operate)
            {
                case Operation.plus:
                    number++;
                    break;
                case Operation.minus:
                    number--;
                    break;
                case Operation.none:
                    break;
                default:
                    break;
            }
            if (number < 0)
            {
                return 0;
            }

            ironUser.Count = number;
            UpdateCount(ironUser);
            return number;
        }

        private static string FormatNumberString(int number)
        {
            string displayNumber = "000";
            if (number < 0)
                displayNumber = "000";
            else if (number > -1 && number < 10)
                displayNumber = "00" + number.ToString();
            else if (number > 9 && number < 100)
                displayNumber = "0" + number.ToString();
            else
                displayNumber = number.ToString();
            return displayNumber;
        }

        private async void VishalPlusClicked(object sender, RoutedEventArgs e)
        {
            vishalCount = await GetUpdatedCount("vishal", Operation.plus);
            VishalCount.Text = FormatNumberString(vishalCount);
            UpdateTotalDollars();
        }

        private async void KedarPlusClicked(object sender, RoutedEventArgs e)
        {
            kedarCount = await GetUpdatedCount("kedar", Operation.plus);
            KedarCount.Text = FormatNumberString(kedarCount);
            UpdateTotalDollars();
        }

        private async void KrittikaPlusClicked(object sender, RoutedEventArgs e)
        {            
            krittikaCount = await GetUpdatedCount("krittika", Operation.plus);
            KrittikaCount.Text = FormatNumberString(krittikaCount);
            UpdateTotalDollars();
        }

        private async void AbhijitPlusClicked(object sender, RoutedEventArgs e)
        {
            abhijitCount = await GetUpdatedCount("abhijit", Operation.plus);
            AbhijitCount.Text = FormatNumberString(abhijitCount);
            UpdateTotalDollars();
        }

        private async void RajkamalPlusClicked(object sender, RoutedEventArgs e)
        {
            rajkamalCount = await GetUpdatedCount("rajkamal", Operation.plus);
            RajkamalCount.Text = FormatNumberString(rajkamalCount);
                UpdateTotalDollars();
        }

        private async void SatyakamalPlusClicked(object sender, RoutedEventArgs e)
        {
            satyakamalCount = await GetUpdatedCount("satyakamal", Operation.plus);
            SatyakamalCount.Text = FormatNumberString(satyakamalCount);
                UpdateTotalDollars();
        }

        private async void SwatiPlusClicked(object sender, RoutedEventArgs e)
        {
            swatiCount = await GetUpdatedCount("swati", Operation.plus);
            SwatiCount.Text = FormatNumberString(swatiCount);
                UpdateTotalDollars();
        }

        private async void NilaPlusClicked(object sender, RoutedEventArgs e)
        {
            nilaCount = await GetUpdatedCount("nila", Operation.plus);
            NilaCount.Text = FormatNumberString(nilaCount);
                UpdateTotalDollars();
        }

        private async void VishalMinusClicked(object sender, RoutedEventArgs e)
        {
            vishalCount = await GetUpdatedCount("vishal", Operation.minus);
            VishalCount.Text = FormatNumberString(vishalCount);
            if (vishalCount >= 0)
                UpdateTotalDollars();
        }

        private async void KedarMinusClicked(object sender, RoutedEventArgs e)
        {
            kedarCount = await GetUpdatedCount("kedar", Operation.minus);
            KedarCount.Text = FormatNumberString(kedarCount);
            if (kedarCount >= 0)
                UpdateTotalDollars();
        }

        private async void KrittikaMinusClicked(object sender, RoutedEventArgs e)
        {
            krittikaCount = await GetUpdatedCount("krittika", Operation.minus);
            KrittikaCount.Text = FormatNumberString(krittikaCount);
            if (krittikaCount >= 0)
                UpdateTotalDollars();
        }

        private async void AbhijitMinusClicked(object sender, RoutedEventArgs e)
        {
            abhijitCount = await GetUpdatedCount("abhijit", Operation.minus);
            AbhijitCount.Text = FormatNumberString(abhijitCount);
            if (abhijitCount >= 0)
                UpdateTotalDollars();
        }

        private async void RajkamalMinusClicked(object sender, RoutedEventArgs e)
        {
            rajkamalCount = await GetUpdatedCount("rajkamal", Operation.minus);
            RajkamalCount.Text = FormatNumberString(rajkamalCount);
            if (rajkamalCount >= 0)
                UpdateTotalDollars();
        }

        private async void SatyakamalMinusClicked(object sender, RoutedEventArgs e)
        {
            satyakamalCount = await GetUpdatedCount("satyakamal", Operation.minus);
            SatyakamalCount.Text = FormatNumberString(satyakamalCount);
            if (satyakamalCount >= 0)
                UpdateTotalDollars();
        }

        private async void SwatiMinusClicked(object sender, RoutedEventArgs e)
        {
            swatiCount = await GetUpdatedCount("swati", Operation.minus);
            SwatiCount.Text = FormatNumberString(swatiCount);
            if (swatiCount >= 0)
                UpdateTotalDollars();
        }

        private async void NilaMinusClicked(object sender, RoutedEventArgs e)
        {
            nilaCount = await GetUpdatedCount("nila", Operation.minus);
            NilaCount.Text = FormatNumberString(nilaCount);
            if (nilaCount >= 0)
                UpdateTotalDollars();
        }

    }
}
