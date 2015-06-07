using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronometer
{
    public class ActivitiesViewModel:ObservableObject
    {
        public ActivitiesViewModel()
        {
            activities = new ObservableCollection<ActivityForBinding>();
            Data.ActivityAdded += Data_ActivityAdded;
            LoadActivities();
        }

        private async void LoadActivities()
        {
            await Data.LoadActivities();
            RaisePropertyChanged("Activities");
        }

        void Data_ActivityAdded(object sender, EventArgs e)
        {
            const int maxWidth = 320;
            activities.Clear();
            Dictionary<int, double> stat = new Dictionary<int, double>();
            double maxSeconds = 0;
            foreach (var i in Data.Activities)
            {
                if (!stat.ContainsKey(i.CategoryId))
                {
                    stat.Add(i.CategoryId,0);
                }

                var seconds = (i.Ended - i.Started).TotalSeconds;
                stat[i.CategoryId] += seconds;
            }

            maxSeconds = stat.Values.Max();

            foreach (var i in stat.Keys)
            {
                var category = Data.Categories.Where(j => j.Id == i).First();
                var t = new ActivityForBinding() { Name = category.Name, X2 = (int)(stat[i] / maxSeconds * maxWidth), Color = category.Color, TotalSeconds = stat[i] };
                activities.Add(t);
            }
        }

        ObservableCollection<ActivityForBinding> activities;

        public ObservableCollection<ActivityForBinding> Activities
        {
            get
            {
                return activities;
            }
        }
    }
}
