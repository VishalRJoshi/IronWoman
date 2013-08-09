using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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



        private enum Operation
        {
            plus, minus, none
        };

        private void UpdateTotalDollars()
        {
            totalDollars = vishalCount + kedarCount + krittikaCount + abhijitCount + rajkamalCount + satyakamalCount + swatiCount + nilaCount;
            TotalDollar.Text = FormatNumberString(totalDollars);
            UpdateTotalRupees();
        }

        private void UpdateTotalRupees()
        {
            totalRupees = totalDollars * 60;
            TotalRupees.Text = FormatNumberString(totalRupees);
        }

        private static int GetUpdatedCount(string numString, Operation operate)
        {
            int number;
            try
            {
                number = Convert.ToInt32(numString);
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
                return 0;
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

        private void VishalPlusClicked(object sender, RoutedEventArgs e)
        {
            vishalCount = GetUpdatedCount(VishalCount.Text, Operation.plus);
            VishalCount.Text = FormatNumberString(vishalCount);
            UpdateTotalDollars();
        }

        private void KedarPlusClicked(object sender, RoutedEventArgs e)
        {
            kedarCount = GetUpdatedCount(KedarCount.Text, Operation.plus);
            KedarCount.Text = FormatNumberString(kedarCount);
            UpdateTotalDollars();
        }

        private void KrittikaPlusClicked(object sender, RoutedEventArgs e)
        {
            krittikaCount = GetUpdatedCount(KrittikaCount.Text, Operation.plus);
            KrittikaCount.Text = FormatNumberString(krittikaCount);
            UpdateTotalDollars();
        }

        private void AbhijitPlusClicked(object sender, RoutedEventArgs e)
        {
            abhijitCount = GetUpdatedCount(AbhijitCount.Text, Operation.plus);
            AbhijitCount.Text = FormatNumberString(abhijitCount);
            UpdateTotalDollars();
        }

        private void RajkamalPlusClicked(object sender, RoutedEventArgs e)
        {
            rajkamalCount = GetUpdatedCount(RajkamalCount.Text, Operation.plus);
            RajkamalCount.Text = FormatNumberString(rajkamalCount);
                UpdateTotalDollars();
        }

        private void SatyakamalPlusClicked(object sender, RoutedEventArgs e)
        {
            satyakamalCount = GetUpdatedCount(SatyakamalCount.Text, Operation.plus);
            SatyakamalCount.Text = FormatNumberString(satyakamalCount);
                UpdateTotalDollars();
        }

        private void SwatiPlusClicked(object sender, RoutedEventArgs e)
        {
            swatiCount = GetUpdatedCount(SwatiCount.Text, Operation.plus);
            SwatiCount.Text = FormatNumberString(swatiCount);
                UpdateTotalDollars();
        }

        private void NilaPlusClicked(object sender, RoutedEventArgs e)
        {
            nilaCount = GetUpdatedCount(NilaCount.Text, Operation.plus);
            NilaCount.Text = FormatNumberString(nilaCount);
                UpdateTotalDollars();
        }

        private void VishalMinusClicked(object sender, RoutedEventArgs e)
        {
            vishalCount = GetUpdatedCount(VishalCount.Text, Operation.minus);
            VishalCount.Text = FormatNumberString(vishalCount);
            if (vishalCount > 0)
                UpdateTotalDollars();
        }

        private void KedarMinusClicked(object sender, RoutedEventArgs e)
        {
            kedarCount = GetUpdatedCount(KedarCount.Text, Operation.minus);
            KedarCount.Text = FormatNumberString(kedarCount);
            if (kedarCount > 0)
                UpdateTotalDollars();
        }

        private void KrittikaMinusClicked(object sender, RoutedEventArgs e)
        {
            krittikaCount = GetUpdatedCount(KrittikaCount.Text, Operation.minus);
            KrittikaCount.Text = FormatNumberString(krittikaCount);
            if (krittikaCount > 0)
                UpdateTotalDollars();
        }

        private void AbhijitMinusClicked(object sender, RoutedEventArgs e)
        {
            abhijitCount = GetUpdatedCount(AbhijitCount.Text, Operation.minus);
            AbhijitCount.Text = FormatNumberString(abhijitCount);
            if (abhijitCount > 0)
                UpdateTotalDollars();
        }

        private void RajkamalMinusClicked(object sender, RoutedEventArgs e)
        {
            rajkamalCount = GetUpdatedCount(RajkamalCount.Text, Operation.minus);
            RajkamalCount.Text = FormatNumberString(rajkamalCount);
            if (rajkamalCount > 0)
                UpdateTotalDollars();
        }

        private void SatyakamalMinusClicked(object sender, RoutedEventArgs e)
        {
            satyakamalCount = GetUpdatedCount(SatyakamalCount.Text, Operation.minus);
            SatyakamalCount.Text = FormatNumberString(satyakamalCount);
            if (satyakamalCount > 0)
                UpdateTotalDollars();
        }

        private void SwatiMinusClicked(object sender, RoutedEventArgs e)
        {
            swatiCount = GetUpdatedCount(SwatiCount.Text, Operation.minus);
            SwatiCount.Text = FormatNumberString(swatiCount);
            if (swatiCount > 0)
                UpdateTotalDollars();
        }

        private void NilaMinusClicked(object sender, RoutedEventArgs e)
        {
            nilaCount = GetUpdatedCount(NilaCount.Text, Operation.minus);
            NilaCount.Text = FormatNumberString(nilaCount);
            if (nilaCount > 0)
                UpdateTotalDollars();
        }

    }
}
