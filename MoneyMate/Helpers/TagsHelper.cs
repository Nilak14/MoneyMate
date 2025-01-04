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
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Food", tagDescription = "Transection Related to Food" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Transport", tagDescription = "Transection Related to Transport" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Rent", tagDescription = "Transection Related to Rents" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Utilities", tagDescription = "Electricity, water, etc." },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Entertainment", tagDescription = "Movies, games, etc." },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Shopping", tagDescription = "Personal shopping expenses" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Healthcare", tagDescription = "Medical and health expenses" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Education", tagDescription = "Educational expenses" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Savings", tagDescription = "Savings and investments" },
            new TagsModel { tagId = Guid.NewGuid().ToString(), tagName = "Miscellaneous", tagDescription = "Other expenses" }
        };
        }

        private static async Task SaveTags(List<TagsModel> tags)
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

    }
}
