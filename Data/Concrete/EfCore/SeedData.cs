using FitGain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FitGain.Data.Concrete.EfCore;
public static class SeedData
{
    public static void TestData(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<GymContext>();

        if(context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "Chest", Url = "chest", Color = TagColors.danger},
                    new Tag { Text = "Back", Url = "back" , Color = TagColors.primary},
                    new Tag { Text = "Shoulders", Url = "shoulders" , Color = TagColors.secondary },
                    new Tag { Text = "Legs", Url = "legs" , Color = TagColors.success},
                    new Tag { Text = "Arms", Url = "arms" , Color = TagColors.warning},
                    new Tag { Text = "Abs", Url = "abs" , Color = TagColors.danger},
                    new Tag { Text = "Glutes", Url = "glutes" , Color = TagColors.secondary}
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "emreuzer",Name = "Emre Uzer", Email = "emreuzer@gmail.com",Password = "123456", Image = "p1.jpg"},
                    new User { UserName = "ahmetyılmaz",Name = "Ahmet Yılmaz", Email = "ahmetyılmaz@gmail.com",Password = "121221", Image = "p2.jpg" },
                    new User { UserName = "sılakara",Name = "Sıla Kara", Email = "sılakara@gmail.com",Password = "555555", Image = "p3.jpg" },
                    new User { UserName = "ferahduman",Name = "Ferah Duman", Email = "ferah@gmail.com",Password = "777777", Image = "p3.jpg" }
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Bench Press",
                        Description = "A primary exercise for developing the chest muscles, it also works the shoulders and triceps.",
                        Content = "A primary exercise for developing the chest muscles, it also works the shoulders and triceps.",
                        Url = "bench-press",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Where(t => t.Text == "Chest").ToList(),
                        Image = "bench.jpg",
                        UserId = 1,
                        Comments = new List<Comment> {
                            new Comment {Text = "A good move",PublishedOn = DateTime.Now.AddDays(-20),UserId = 1},
                            new Comment {Text = "It's very important to do this exercise properly.",PublishedOn = DateTime.Now.AddDays(-10),UserId = 2},
                        }
                    },

                     new Post
                     {
                         Title = "Dumbbell Fly",
                         Description = "An isolation exercise that works the chest muscles through stretching and contraction, helping to widen the chest.",
                         Content = "An isolation exercise that works the chest muscles through stretching and contraction, helping to widen the chest.",
                         Url = "dumbbell-fly",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-10),
                         Tags = context.Tags.Where(t => t.Text == "Chest").ToList(),
                         Image = "dumbbell.jpg",
                         UserId = 1
                     },

                     new Post
                     {
                         Title = "Chest Press",
                         Description = "A basic pushing exercise, usually performed on a machine, that works the chest, front shoulders, and triceps.",
                         Content = "A basic pushing exercise, usually performed on a machine, that works the chest, front shoulders, and triceps.",
                         Url = "chest-press",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-10),
                         Tags = context.Tags.Where(t => t.Text == "Chest").ToList(),
                         Image = "chest.jpg",
                         UserId = 1
                     },

                     new Post
                     {
                         Title = "Lat Pulldown",
                         Description = "A vertical pulling movement that increases back width and targets the latissimus dorsi (lats) muscles.",
                         Content = "A vertical pulling movement that increases back width and targets the latissimus dorsi (lats) muscles.",
                         Url = "lat-pulldown",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-20),
                         Tags = context.Tags.Where(t => t.Text == "Back").ToList(),
                         Image = "lat.jpg",
                         UserId = 2
                     },

                     new Post
                     {
                         Title = "Deadlift",
                         Description = "A ground-to-stand lifting exercise that works almost every muscle group in the body, especially the hamstrings, back, and glutes.",
                         Content = "A ground-to-stand lifting exercise that works almost every muscle group in the body, especially the hamstrings, back, and glutes.",
                         Url = "deadlift",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-20),
                         Tags = context.Tags.Where(t => t.Text == "Legs" || t.Text == "Back" || t.Text == "Glutes").ToList(),
                         Image = "deadlift.jpg",
                         UserId = 2
                     },

                     new Post
                     {
                         Title = "Seated Row",
                         Description = "This seated movement helps to thicken the back and targets the muscles around the shoulder blades.",
                         Content = "This seated movement helps to thicken the back and targets the muscles around the shoulder blades.",
                         Url = "seated-row",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-20),
                         Tags = context.Tags.Where(t => t.Text == "Back").ToList(),
                         Image = "seated.jpg",
                         UserId = 2
                     },

                     new Post
                     {
                         Title = "Shoulder Press",
                         Description = "A pushing exercise, typically done with dumbbells or a barbell, that strengthens the shoulder muscles.",
                         Content = "A pushing exercise, typically done with dumbbells or a barbell, that strengthens the shoulder muscles.",
                         Url = "shoulder-press",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-30),
                         Tags = context.Tags.Where(t => t.Text == "Shoulders").ToList(),
                         Image = "shoulder.jpg",
                         UserId = 3
                     },

                     new Post
                     {
                         Title = "Lateral Raise",
                         Description = "A movement used to isolate the side of the shoulders and give them a wider appearance.",
                         Content = "A movement used to isolate the side of the shoulders and give them a wider appearance.",
                         Url = "lateral-raise",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-30),
                         Tags = context.Tags.Where(t => t.Text == "Shoulders").ToList(),
                         Image = "lat.jpg",
                         UserId = 3
                     },

                     new Post
                     {
                         Title = "Leg Press",
                         Description = "A machine exercise that strengthens the front and back of the legs with less pressure on the knees.",
                         Content = "A machine exercise that strengthens the front and back of the legs with less pressure on the knees.",
                         Url = "leg-press",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-40),
                         Tags = context.Tags.Where(t => t.Text == "Legs").ToList(),
                         Image = "leg.jpg",
                         UserId = 4
                     },

                     new Post
                     {
                         Title = "Squat",
                         Description = "A fundamental lower-body exercise that targets the legs and glutes, which are among the largest muscle groups in the body.",
                         Content = "A fundamental lower-body exercise that targets the legs and glutes, which are among the largest muscle groups in the body.",
                         Url = "squat",
                         IsActive = true,
                         PublishedOn = DateTime.Now.AddDays(-40),
                         Tags = context.Tags.Where(t => t.Text == "Legs").ToList(),
                         Image = "squat.jpg",
                         UserId = 4
                     },

                    new Post
                    {
                        Title = "Biceps Curl",
                        Description = "An arm exercise that isolates and strengthens the biceps muscle on the front of the arm.",
                        Content = "An arm exercise that isolates and strengthens the biceps muscle on the front of the arm.",
                        Url = "biceps-curl",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = context.Tags.Where(t => t.Text == "Arms").ToList(),
                        Image = "biceps.jpg",
                        UserId = 4
                    },

                    new Post
                    {
                        Title = "Triceps Pushdown",
                        Description = "An exercise in the pushing direction that targets the triceps muscle on the back of the arm.",
                        Content = "An exercise in the pushing direction that targets the triceps muscle on the back of the arm.",
                        Url = "triceps-pushdown",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = context.Tags.Where(t => t.Text == "Arms").ToList(),
                        Image = "triceps.jpg",
                        UserId = 4
                    },

                    new Post
                    {
                        Title = "Crunch",
                        Description = "A basic abdominal exercise performed to strengthen and tighten the core muscles.",
                        Content = "A basic abdominal exercise performed to strengthen and tighten the core muscles.",
                        Url = "crunch",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Where(t => t.Text == "Abs").ToList(),
                        Image = "crunch.jpg",
                        UserId = 1
                    },

                    new Post
                    {
                        Title = "Plank",
                        Description = "A static holding exercise that works the entire body, specifically strengthening the abs and core.",
                        Content = "A static holding exercise that works the entire body, specifically strengthening the abs and core.",
                        Url = "plank",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Where(t => t.Text == "Abs").ToList(),
                        Image = "plank.jpg",
                        UserId = 1
                    },

                    new Post
                    {
                        Title = "Hip Thrust",
                        Description = "A movement that most effectively targets the glute muscles and is a weighted version of the glute bridge.",
                        Content = "A movement that most effectively targets the glute muscles and is a weighted version of the glute bridge.",
                        Url = "bench-press",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-30),
                        Tags = context.Tags.Where(t => t.Text == "Legs" || t.Text == "Glutes").ToList(),
                        Image = "hip.jpg",
                        UserId = 3
                    }

                );

                context.SaveChanges();
            }
        }
    }
}