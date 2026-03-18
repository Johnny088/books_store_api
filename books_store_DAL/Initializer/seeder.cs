using books_store_DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace books_store_DAL.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope(); // create scope
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // get _context

            await context.Database.MigrateAsync();

            var genres = new List<GenreEntity>();
            if (!context.Genres.Any())
            {
                genres.AddRange(
                   new GenreEntity { Name = "Fantasy" },
                   new GenreEntity { Name = "drama" },
                   new GenreEntity { Name = "Detective" },
                   new GenreEntity { Name = "Mystery" },
                   new GenreEntity { Name = "Fiction" },
                   new GenreEntity { Name = "Biography" },
                   new GenreEntity { Name = "History" },
                   new GenreEntity { Name = "Horror" },
                   new GenreEntity { Name = "Romance" },
                   new GenreEntity { Name = "Fantastic" },
                   new GenreEntity { Name = "Thriller" },
                   new GenreEntity { Name = "self Development" }
            );
                   
                await context.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }
            else
            {
                genres = await context.Genres.ToListAsync();
            }
            var authors = new List<AuthorEntity>();
            if (!context.Authors.Any())
            {
                authors.AddRange(
                   // 1. Brian Tracy
                   new AuthorEntity
                   {
                       Name = "Brian Tracy",
                       BirthDate = new DateTime(1944, 1, 5).ToUniversalTime(),
                       Books = new List<BookEntity> {
                        new BookEntity { Title = "Eat That Frog!", Pages = 128, PublishedYear = 2001, Genres = new List<GenreEntity> { genres[11] } },
                        new BookEntity { Title = "No Excuses!", Pages = 304, PublishedYear = 2010, Genres = new List<GenreEntity> { genres[11] } },
                        new BookEntity { Title = "The Psychology of Selling", Pages = 240, PublishedYear = 1985, Genres = new List<GenreEntity> { genres[11] } },
                        new BookEntity { Title = "Goals!", Pages = 288, PublishedYear = 2003, Genres = new List<GenreEntity> { genres[11] } },
                        new BookEntity { Title = "Maximum Achievement", Pages = 352, PublishedYear = 1993, Genres = new List<GenreEntity> { genres[11] } }

                        }
                   },

                    // 2. Tony Robbins
                    new AuthorEntity
                    {
                        Name = "Tony Robbins",
                        BirthDate = new DateTime(1960, 2, 29).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "Awaken the Giant Within", Pages = 544, PublishedYear = 1991, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Unlimited Power", Pages = 448, PublishedYear = 1986, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Money: Master the Game", Pages = 688, PublishedYear = 2014, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "Unshakeable", Pages = 256, PublishedYear = 2017, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "Life Force", Pages = 704, PublishedYear = 2022, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 3. Stephen Covey
                    new AuthorEntity
                    {
                        Name = "Stephen Covey",
                        BirthDate = new DateTime(1932, 10, 24).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "The 7 Habits of Highly Effective People", Pages = 381, PublishedYear = 1989, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "First Things First", Pages = 384, PublishedYear = 1994, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The 8th Habit", Pages = 432, PublishedYear = 2004, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Principle-Centered Leadership", Pages = 336, PublishedYear = 1991, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The Leader in Me", Pages = 304, PublishedYear = 2008, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 4. Robert Kiyosaki
                    new AuthorEntity
                    {
                        Name = "Robert Kiyosaki",
                        BirthDate = new DateTime(1947, 4, 8).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "Rich Dad Poor Dad", Pages = 336, PublishedYear = 1997, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "Cashflow Quadrant", Pages = 376, PublishedYear = 1998, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "Rich Dad's Guide to Investing", Pages = 512, PublishedYear = 2000, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "Increase Your Financial IQ", Pages = 256, PublishedYear = 2008, Genres = new List<GenreEntity> { genres[2] } },
                        new BookEntity { Title = "The Business of the 21st Century", Pages = 134, PublishedYear = 2010, Genres = new List<GenreEntity> { genres[2] } }
                        }
                    },

                    // 5. Dale Carnegie
                    new AuthorEntity
                    {
                        Name = "Dale Carnegie",
                        BirthDate = new DateTime(1888, 11, 24).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "How to Win Friends and Influence People", Pages = 291, PublishedYear = 1936, Genres = new List<GenreEntity> { genres[0], genres[1] } },
                        new BookEntity { Title = "How to Stop Worrying and Start Living", Pages = 306, PublishedYear = 1948, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The Quick and Easy Way to Effective Speaking", Pages = 273, PublishedYear = 1962, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Lincoln the Unknown", Pages = 305, PublishedYear = 1932, Genres = new List<GenreEntity> { genres[4] } },
                        new BookEntity { Title = "The Leader in You", Pages = 214, PublishedYear = 1993, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 6. Napoleon Hill
                    new AuthorEntity
                    {
                        Name = "Napoleon Hill",
                        BirthDate = new DateTime(1883, 10, 26).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "Think and Grow Rich", Pages = 238, PublishedYear = 1937, Genres = new List<GenreEntity> { genres[0], genres[2] } },
                        new BookEntity { Title = "The Law of Success", Pages = 640, PublishedYear = 1928, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Outwitting the Devil", Pages = 288, PublishedYear = 2011, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Success Through a Positive Mental Attitude", Pages = 384, PublishedYear = 1959, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The Master-Key to Riches", Pages = 272, PublishedYear = 1945, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 7. John C. Maxwell
                    new AuthorEntity
                    {
                        Name = "John C. Maxwell",
                        BirthDate = new DateTime(1947, 2, 20).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "The 21 Irrefutable Laws of Leadership", Pages = 336, PublishedYear = 1998, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Developing the Leader Within You", Pages = 224, PublishedYear = 1993, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The 15 Invaluable Laws of Growth", Pages = 288, PublishedYear = 2012, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Failing Forward", Pages = 224, PublishedYear = 2000, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "How Successful People Think", Pages = 160, PublishedYear = 2009, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 8. Zig Ziglar
                    new AuthorEntity
                    {
                        Name = "Zig Ziglar",
                        BirthDate = new DateTime(1926, 11, 6).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "See You at the Top", Pages = 384, PublishedYear = 1975, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Secrets of Closing the Sale", Pages = 416, PublishedYear = 1984, Genres = new List<GenreEntity> { genres[1] } },
                        new BookEntity { Title = "Over the Top", Pages = 336, PublishedYear = 1994, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Born to Win", Pages = 272, PublishedYear = 2012, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Ziglar on Selling", Pages = 336, PublishedYear = 1991, Genres = new List<GenreEntity> { genres[1] } }
                        }
                    },

                    // 9. Jim Rohn
                    new AuthorEntity
                    {
                        Name = "Jim Rohn",
                        BirthDate = new DateTime(1930, 9, 17).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "7 Strategies for Wealth & Happiness", Pages = 176, PublishedYear = 1985, Genres = new List<GenreEntity> { genres[0], genres[2] } },
                        new BookEntity { Title = "The Five Major Pieces to the Life Puzzle", Pages = 128, PublishedYear = 1991, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Twelve Pillars", Pages = 128, PublishedYear = 2005, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The Seasons of Life", Pages = 125, PublishedYear = 1981, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "The Art of Exceptional Living", Pages = 200, PublishedYear = 2003, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    },

                    // 10. Seth Godin
                    new AuthorEntity
                    {
                        Name = "Seth Godin",
                        BirthDate = new DateTime(1960, 7, 10).ToUniversalTime(),
                        Books = new List<BookEntity> {
                        new BookEntity { Title = "Purple Cow", Pages = 160, PublishedYear = 2002, Genres = new List<GenreEntity> { genres[1] } },
                        new BookEntity { Title = "Linchpin", Pages = 256, PublishedYear = 2010, Genres = new List<GenreEntity> { genres[0] } },
                        new BookEntity { Title = "Tribes", Pages = 160, PublishedYear = 2008, Genres = new List<GenreEntity> { genres[1] } },
                        new BookEntity { Title = "This Is Marketing", Pages = 288, PublishedYear = 2018, Genres = new List<GenreEntity> { genres[1] } },
                        new BookEntity { Title = "The Dip", Pages = 96, PublishedYear = 2007, Genres = new List<GenreEntity> { genres[0] } }
                        }
                    }
                );
                await context.AddRangeAsync(authors);
                await context.SaveChangesAsync();
            }
        }
    }
}
