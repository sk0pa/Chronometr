using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace Chronometer
{
    public static class Data
    {
        static Data()
        {
            Activities = new List<Activity>();
        }

        public static ObservableCollection<Category> Categories;

        public static List<Activity> Activities;

        public static async Task SetupDB()
        {
            if (!await IsDbExists())
            {
                await CreateDB();
                await FillDefaultCategories();
            }
        }

        public const string DbName = "db";

        public static async Task<bool> IsDbExists()
        {
            bool result = true;
            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(DbName);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static async Task CreateDB()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);
            await conn.CreateTablesAsync(
                typeof(Category),
                typeof(Activity));
        }

        private static async Task FillDefaultCategories()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);

            await conn.InsertAllAsync(new Category[]
            {
                new Category() { Name = "+", ColorRGB = 0x333333 },
                new Category() { Name = "Sleep", ColorRGB = 0x3270FF },
                new Category() { Name = "Eat", ColorRGB = 0xFF3D6A },
                new Category() { Name = "Walk", ColorRGB = 0x4AF03D }
            });
        }

        public static async void AddActivity(int categoryId, DateTime start, DateTime end)
        {
            var a = new Activity() { CategoryId = categoryId, Started = start, Ended = end };
            Activities.Add(a);

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);
            await conn.InsertAsync(a);

            RaiseAddActivity();
        }

        public static async void AddCategory(string name, Color color)
        {
            var category = new Category() { Name = name, ColorRGB = ColorTools.ColorToArgb(color) };
            Categories.Add(category);

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);
            await conn.InsertAsync(category);
        }

        public static async void DeleteCategory(Category category)
        {
            Categories.Remove(category);
            Activities.RemoveAll(i=>i.CategoryId==category.Id);

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);
            await conn.ExecuteAsync("delete from Activities where CategoryId = ?", category.Id);
            await conn.DeleteAsync(category);
            RaiseAddActivity();
        }

        private static void RaiseAddActivity()
        {
            if (ActivityAdded != null)
            {
                ActivityAdded(null, null);
            }
        }

        public static async Task<ObservableCollection<Category>> LoadCategories()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);

            var list = await conn.Table<Category>().ToListAsync();

            return new ObservableCollection<Category>(list);
        }

        public static async Task LoadActivities()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);

            Activities = await conn.Table<Activity>().ToListAsync();

            RaiseAddActivity();
        }

        public static event EventHandler ActivityAdded;


        public static  async void UpdateCategory(Category category)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DbName);
            var list = await conn.UpdateAsync(category);
        }

        
    }
}
