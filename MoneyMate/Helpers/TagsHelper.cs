using MoneyMate.Models;
using System.Text.Json;

namespace MoneyMate.Helpers
{
    public class TagsHelper
    {

        private static string tagsFilePath = Path.Combine(FileSystem.AppDataDirectory, "tags.json");
        private static List<TagsModel> GetDefaultTags()
        {
            return new List<TagsModel>
        {
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Yearly", tagDescription = "Annual transactions that occur once a year" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Monthly", tagDescription = "Recurring transactions that occur every month," },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Food", tagDescription = " Transaction related to food and dining, including groceries, dining out, or takeaways" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Drinks", tagDescription = "Transactions on beverages, including both non-alcoholic and alcoholic drinks" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Clothes", tagDescription = "Transactions related to clothing, footwear, and accessories" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Gadgets", tagDescription = " Transaction related to electronic devices, gadgets, or tech-related purchases." },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Miscellaneous", tagDescription = "Transactions that don’t fit into any specific category, covering a wide range of general transactions." },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Fuel", tagDescription = "Transactions on fuel for transportation, whether for a car, bike, or any other vehicle" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "EMI ", tagDescription = "Regular payments made to repay loans, credit cards, or other financed purchases" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Party", tagDescription = "Expenses related to parties, celebrations, or social gatherings, including food, decorations, and entertainment" },
            new TagsModel { tagId = "1", tagName = "Self", tagDescription = "Debt taken Due to having more expenses than the total balance" }
        };
        }

        public static async Task SaveTags(List<TagsModel> tags)
        {
            var json = JsonSerializer.Serialize(tags);
            await File.WriteAllTextAsync(tagsFilePath, json);
        }


        public static async  Task<List<TagsModel>> InitializeOrGetTags()
        {
            List<TagsModel> tags = new List<TagsModel>();

            if (!File.Exists(tagsFilePath))
            {
                tags = GetDefaultTags();
                await SaveTags(tags);
            }
            else
            {
                var json = await File.ReadAllTextAsync(tagsFilePath);
                tags = JsonSerializer.Deserialize<List<TagsModel>>(json) ?? new List<TagsModel>();
            }
            return tags;
        }

        public static async Task AddTag(TagsModel tag)
        {
            var tags = await InitializeOrGetTags();
            tags.Add(tag);
            await SaveTags(tags);
        }

        public static async Task<TagsModel> GetTagById(string tagId)
        {
            var tags = await InitializeOrGetTags();
            return tags.FirstOrDefault(tag => tag.tagId == tagId);
        }

    }
}
