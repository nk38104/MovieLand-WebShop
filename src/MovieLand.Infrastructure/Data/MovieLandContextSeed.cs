using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Data
{
    public class MovieLandContextSeed
    {
        private static int _retryMax = 10;

        public static async Task SeedAsync(MovieLandContext movieLandContext, ILoggerFactory loggerFactory, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                movieLandContext.Database.Migrate();
                movieLandContext.Database.EnsureCreated();

                // roles - superadmin
                await SeedRolesAsync(roleManager);
                await SeedSuperAdminAsync(userManager);

                // directors - genres - reviews
                await SeedDirectorsAsync(movieLandContext);
                await SeedGenresAsync(movieLandContext);
                // Uncomment line below just for testing purpose if you want some data before you implement user management
                //await SeedReviewsAsync(movieLandContext);

                // movies - moviesdirectors - moviesgenres - lists
                await SeedMoviesAsync(movieLandContext);
                await SeedMoviesDirectorsAsync(movieLandContext);
                await SeedMoviesGenresAsync(movieLandContext);
                await SeedListAndMoviesAsync(movieLandContext);

                // Uncomment line below just for testing purpose if you want some data before you implement user management
                // compares and favorites
                //await SeedCompareAndMoviesAsync(movieLandContext);
                //await SeedFavoriteAndMoviesAsync(movieLandContext);

                // Uncomment code below just for testing purpose if you want some data before you implement user management
                // cart and cart items - order and order items
                //await SeedCartAndItemsAsync(movieLandContext);
                //await SeedOrderAndItemsAsync(movieLandContext);
            }
            catch (Exception exception)
            {
                if (retryForAvailability < _retryMax)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<MovieLandContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(movieLandContext, loggerFactory, roleManager, userManager, retryForAvailability);
                }
                throw;
            }
        }


        private static async Task SeedDirectorsAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Directors.Any())
                return;

            var directors = new List<Director>()
            {
                new Director() { Name = "Frank Darabont"},          // 1
                new Director() { Name = "Francis Ford Coppola"},    // 2
                new Director() { Name = "Christopher Nolan"},       // 3
                new Director() { Name = "Sidney Lumet"},            // 4
                new Director() { Name = "Steven Spielberg"},        // 5
                new Director() { Name = "Peter Jackson"},           // 6
                new Director() { Name = "Quentin Tarantino"},       // 7
                new Director() { Name = "Sergio Leone"},            // 8
                new Director() { Name = "David Fincher"},           // 9
                new Director() { Name = "Robert Zemeckis"},         // 10
                new Director() { Name = "Irvin Kershner"},          // 11
                new Director() { Name = "Lana Wachowski"},          // 12
                new Director() { Name = "Lilly Wachowski"},         // 13
                new Director() { Name = "Martin Scorsese"},         // 14
                new Director() { Name = "Milos Forman"},            // 15
                new Director() { Name = "Akira Kurosawa"},          // 16
                new Director() { Name = "Jonathan Demme"},          // 17
                new Director() { Name = "Fernando Meirelles"},      // 18
                new Director() { Name = "Kátia Lund"},              // 19
                new Director() { Name = "Roberto Benigni"},         // 20
                new Director() { Name = "George Lucas"},            // 21
                new Director() { Name = "Hayao Miyazaki"},          // 22
            };

            movieLandContext.Directors.AddRange(directors);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedGenresAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Genres.Any())
                return;

            var genres = new List<Genre>()
            {
                new Genre() { Name = "Action"},         // 1
                new Genre() { Name = "Adventure"},      // 2
                new Genre() { Name = "Sci-Fi"},         // 3
                new Genre() { Name = "Drama"},          // 4
                new Genre() { Name = "Romance"},        // 5
                new Genre() { Name = "Thriller"},       // 6
                new Genre() { Name = "Comedy"},         // 7
                new Genre() { Name = "Fantasy"},        // 8
                new Genre() { Name = "Horror"},         // 9
                new Genre() { Name = "Mystery"},        // 10
                new Genre() { Name = "Western"},        // 11
                new Genre() { Name = "Crime"},          // 12
                new Genre() { Name = "Animation"},      // 13
                new Genre() { Name = "Documentary"},    // 14
                new Genre() { Name = "Family"},         // 15
                new Genre() { Name = "Biography"},      // 16
                new Genre() { Name = "History"},        // 17
            };

            movieLandContext.Genres.AddRange(genres);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedReviewsAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Reviews.Any())
                return;

            var reviews = new List<Review>()
            {
                new Review
                {
                    Username = "bg123",
                    Email = "bg123@oss.unist.hr",
                    Rate = 4.3,
                    Comment = "Does this work?",
                },
                new Review
                {
                    Username = "mz001",
                    Email = "mz001@oss.unist.hr",
                    Rate = 4.8,
                    Comment = "This is the test.",
                },
                new Review
                {
                    Username = "bg123",
                    Email = "bg123@oss.unist.hr",
                    Rate = 4.8,
                    Comment = "This does work. Nice :)",
                }
            };

            movieLandContext.Reviews.AddRange(reviews);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedMoviesAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Movies.Any())
                return;

            var movies = new List<Movie>
            {
                new Movie() // 1
                {
                    Title = "The Shawshank Redemption",
                    Slug = "the-shawshank-redemption",
                    PictureUri = "https://upload.wikimedia.org/wikipedia/en/8/81/ShawshankRedemptionMoviePoster.jpg",
                    ReleaseYear = "1994",
                    Summary = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Description = "The Shawshank Redemption is a 1994 American drama film written and directed by Frank Darabont, based on the 1982 Stephen King novella Rita Hayworth and Shawshank Redemption. It tells the story of banker Andy Dufresne (Tim Robbins), who is sentenced to life in Shawshank State Penitentiary for the murders of his wife and her lover, despite his claims of innocence. Over the following two decades, he befriends a fellow prisoner, contraband smuggler Ellis \"Red\" Redding (Morgan Freeman), and becomes instrumental in a money-laundering operation led by the prison warden Samuel Norton (Bob Gunton). William Sadler, Clancy Brown, Gil Bellows, and James Whitmore appear in supporting roles.",
                    Duration = "144",
                    UnitPrice = 50,
                    UnitsInStock = 100,
                    Rate = 4.3,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 2
                {
                    Title = "The Godfather",
                    Slug = "the-godfather",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    ReleaseYear = "1972",
                    Summary = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                    Description = "The Godfather is a 1972 American crime film directed by Francis Ford Coppola, who co-wrote the screenplay with Mario Puzo, based on Puzo's best-selling 1969 novel of the same name. The film stars Marlon Brando, Al Pacino, James Caan, Richard Castellano, Robert Duvall, Sterling Hayden, John Marley, Richard Conte, and Diane Keaton. It is the first installment in The Godfather trilogy. The story, spanning from 1945 to 1955, chronicles the Corleone family under patriarch Vito Corleone (Brando), focusing on the transformation of his youngest son, Michael Corleone (Pacino), from reluctant family outsider to ruthless mafia boss.",
                    Duration = "175",
                    UnitPrice = 50,
                    UnitsInStock = 150,
                    Rate = 4.3,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 3
                {
                    Title = "The Godfather: Part II",
                    Slug = "the-godfather-part-2",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BMWMwMGQzZTItY2JlNC00OWZiLWIyMDctNDk2ZDQ2YjRjMWQ0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    ReleaseYear = "1974",
                    Summary = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.",
                    Description = "The Godfather Part II is a 1974 American epic crime film produced and directed by Francis Ford Coppola from the screenplay co-written with Mario Puzo, starring Al Pacino, Robert Duvall, Diane Keaton, Robert De Niro, Talia Shire, Morgana King, John Cazale, Mariana Hill, and Lee Strasberg. It is the second installment in The Godfather trilogy. Partially based on Puzo's 1969 novel The Godfather, the film is both a sequel and a prequel to The Godfather, presenting parallel dramas: one picks up the 1958 story of Michael Corleone (Pacino), the new Don of the Corleone family, protecting the family business in the aftermath of an attempt on his life; the prequel covers the journey of his father, Vito Corleone (De Niro), from his Sicilian childhood to the founding of his family enterprise in New York City.",
                    Duration = "202",
                    UnitPrice = 45,
                    UnitsInStock = 120,
                    Rate = 4.0,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 4
                {
                    Title = "The Dark Knight",
                    Slug = "the-dark-knight",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
                    ReleaseYear = "2008",
                    Summary = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Description = "Set within a year after the events of Batman Begins (2005), Batman, Lieutenant James Gordon, and new District Attorney Harvey Dent successfully begin to round up the criminals that plague Gotham City, until a mysterious and sadistic criminal mastermind known only as \"The Joker\" appears in Gotham, creating a new wave of chaos. Batman's struggle against The Joker becomes deeply personal, forcing him to \"confront everything he believes\" and improve his technology to stop him. A love triangle develops between Bruce Wayne, Dent, and Rachel Dawes.",
                    Duration = "152",
                    UnitPrice = 40,
                    UnitsInStock = 100,
                    Rate = 4.0,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 5
                {
                    Title = "12 Angry Men",
                    Slug = "12-angry-men",
                    PictureUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/12_Angry_Men_%281957_film_poster%29.jpg/800px-12_Angry_Men_%281957_film_poster%29.jpg",
                    ReleaseYear = "1957",
                    Summary = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.",
                    Description = "The defense and the prosecution have rested, and the jury is filing into the jury room to decide if a young man is guilty or innocent of murdering his father. What begins as an open-and-shut case of murder soon becomes a detective story that presents a succession of clues creating doubt, and a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, AND each other. Based on the play, all of the action takes place on the stage of the jury room.",
                    Duration = "96",
                    UnitPrice = 40,
                    UnitsInStock = 80,
                    Rate = 4.0,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 6
                {
                    Title = "Schindler's List",
                    Slug = "schindlers-list",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BNDE4OTMxMTctNmRhYy00NWE2LTg3YzItYTk3M2UwOTU5Njg4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                    ReleaseYear = "1993",
                    Summary = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    Description = "Oskar Schindler is a vain and greedy German businessman who becomes an unlikely humanitarian amid the barbaric German Nazi reign when he feels compelled to turn his factory into a refuge for Jews. Based on the true story of Oskar Schindler who managed to save about 1100 Jews from being gassed at the Auschwitz concentration camp, it is a testament to the good in all of us.",
                    Duration = "195",
                    UnitPrice = 35,
                    UnitsInStock = 100,
                    Rate = 3.9,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 7
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    Slug = "the-lord-of-the-rings-the-return-of-the-king",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BNzA5ZDNlZWMtM2NhNS00NDJjLTk4NDItYTRmY2EwMWZlMTY3XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    ReleaseYear = "2003",
                    Summary = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    Description = "The final confrontation between the forces of good and evil fighting for control of the future of Middle-earth. Frodo and Sam reach Mordor in their quest to destroy the One Ring, while Aragorn leads the forces of good against Sauron's evil army at the stone city of Minas Tirith.",
                    Duration = "201",
                    UnitPrice = 35,
                    UnitsInStock = 130,
                    Rate = 3.9,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 8
                {
                    Title = "Pulp Fiction",
                    Slug = "pulp-fiction",
                    PictureUri = "https://upload.wikimedia.org/wikipedia/hr/8/82/Pulp_Fiction_cover.jpg",
                    ReleaseYear = "1994",
                    Summary = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    Description = "Jules Winnfield (Samuel L. Jackson) and Vincent Vega (John Travolta) are two hit men who are out to retrieve a suitcase stolen from their employer, mob boss Marsellus Wallace (Ving Rhames). Wallace has also asked Vincent to take his wife Mia (Uma Thurman) out a few days later when Wallace himself will be out of town. Butch Coolidge (Bruce Willis) is an aging boxer who is paid by Wallace to lose his fight. The lives of these seemingly unrelated people are woven together comprising of a series of funny, bizarre and uncalled-for incidents.",
                    Duration = "154",
                    UnitPrice = 35,
                    UnitsInStock = 100,
                    Rate = 3.9,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 9
                {
                    Title = "The Good, the Bad and the Ugly",
                    Slug = "the-good-the-bad-and-the-ugly",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BOTQ5NDI3MTI4MF5BMl5BanBnXkFtZTgwNDQ4ODE5MDE@._V1_.jpg",
                    ReleaseYear = "1966",
                    Summary = "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.",
                    Description = "Blondie (The Good) (Clint Eastwood) is a professional gunslinger who is out trying to earn a few dollars. Angel Eyes (The Bad) (Lee Van Cleef) is a hitman who always commits to a task and sees it through, as long as he is paid to do so. And Tuco (The Ugly) (Eli Wallach) is a wanted outlaw trying to take care of his own hide. Tuco and Blondie share a partnership together making money off of Tuco's bounty. When Blondie and Tuco come across a horse carriage loaded with dead bodies, they soon learn from the only survivor, Bill Carson (Antonio Casale), that he and a few other men have buried a stash of gold in a cemetery. Unfortunately, Carson dies and Tuco only finds out the name of the cemetery, while Blondie finds out the name on the grave. Now the two must keep each other alive in order to find the gold. Angel Eyes (who had been looking for Bill Carson) discovers that Tuco and Blondie met with Carson and knows they know the location of the gold. All he needs is for the two to lead him to it. Now The Good, the Bad and the Ugly must all battle it out to get their hands on two hundred thousand dollars worth of gold.",
                    Duration = "178",
                    UnitPrice = 40,
                    UnitsInStock = 100,
                    Rate = 3.9,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 10
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    Slug = "the-lord-of-the-rings-the-fellowship-of-the-ring",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg",
                    ReleaseYear = "2001",
                    Summary = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                    Description = "An ancient Ring thought lost for centuries has been found, and through a strange twist of fate has been given to a small Hobbit named Frodo. When Gandalf discovers the Ring is in fact the One Ring of the Dark Lord Sauron, Frodo must make an epic quest to the Cracks of Doom in order to destroy it. However, he does not go alone. He is joined by Gandalf, Legolas the elf, Gimli the Dwarf, Aragorn, Boromir, and his three Hobbit friends Merry, Pippin, and Samwise. Through mountains, snow, darkness, forests, rivers and plains, facing evil and danger at every corner the Fellowship of the Ring must go. Their quest to destroy the One Ring is the only hope for the end of the Dark Lords reign.",
                    Duration = "178",
                    UnitPrice = 35,
                    UnitsInStock = 200,
                    Rate = 3.8,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 11
                {
                    Title = "Fight Club",
                    Slug = "fight-club",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                    ReleaseYear = "1999",
                    Summary = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",
                    Description = "A nameless first person narrator (Edward Norton) attends support groups in attempt to subdue his emotional state and relieve his insomniac state. When he meets Marla (Helena Bonham Carter), another fake attendee of support groups, his life seems to become a little more bearable. However when he associates himself with Tyler (Brad Pitt) he is dragged into an underground fight club and soap making scheme. Together the two men spiral out of control and engage in competitive rivalry for love and power.",
                    Duration = "139",
                    UnitPrice = 40,
                    UnitsInStock = 250,
                    Rate = 3.8,
                    Reviews = movieLandContext.Reviews.ToList(),
                }, 
                new Movie() // 12
                {
                    Title = "Forrest Gump",
                    Slug = "forrest-gump",
                    PictureUri = "https://upload.wikimedia.org/wikipedia/bs/6/67/Forrest_Gump_poster.jpg",
                    ReleaseYear = "1994",
                    Summary = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                    Description = "Forrest Gump is a simple man with a low I.Q. but good intentions. He is running through childhood with his best and only friend Jenny. His 'mama' teaches him the ways of life and leaves him to choose his destiny. Forrest joins the army for service in Vietnam, finding new friends called Dan and Bubba, he wins medals, creates a famous shrimp fishing fleet, inspires people to jog, starts a ping-pong craze, creates the smiley, writes bumper stickers and songs, donates to people and meets the president several times. However, this is all irrelevant to Forrest who can only think of his childhood sweetheart Jenny Curran, who has messed up her life. Although in the end all he wants to prove is that anyone can love anyone.",
                    Duration = "142",
                    UnitPrice = 35,
                    UnitsInStock = 250,
                    Rate = 3.8,
                    Reviews = movieLandContext.Reviews.ToList(),
                },            
                new Movie() // 13
                {
                    Title = "Inception",
                    Slug = "inception",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg",
                    ReleaseYear = "2010",
                    Summary = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    Description = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible, inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea, but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.",
                    Duration = "148",
                    UnitPrice = 50,
                    UnitsInStock = 300,
                    Rate = 3.8,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 14
                {
                    Title = "The Lord of the Rings: The Two Towers",
                    Slug = "the-lord-of-the-rings-the-two-towers",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BZGMxZTdjZmYtMmE2Ni00ZTdkLWI5NTgtNjlmMjBiNzU2MmI5XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                    ReleaseYear = "2002",
                    Summary = "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.",
                    Description = "The continuing quest of Frodo and the Fellowship to destroy the One Ring. Frodo and Sam discover they are being followed by the mysterious Gollum. Aragorn, the Elf archer Legolas, and Gimli the Dwarf encounter the besieged Rohan kingdom, whose once great King Theoden has fallen under Saruman's deadly spell.",
                    Duration = "179",
                    UnitPrice = 40,
                    UnitsInStock = 100,
                    Rate = 3.7,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 15
                {
                    Title = "Star Wars: Episode V - The Empire Strikes Back",
                    Slug = "star-wars-episode-V-the-empire-strikes-back",
                    PictureUri = "https://images-na.ssl-images-amazon.com/images/I/91eOgodm4nL.jpg",
                    ReleaseYear = "1980",
                    Summary = "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued across the galaxy by Darth Vader and bounty hunter Boba Fett.",
                    Description = "Luke Skywalker, Han Solo, Princess Leia and Chewbacca face attack by the Imperial forces and its AT-AT walkers on the ice planet Hoth. While Han and Leia escape in the Millennium Falcon, Luke travels to Dagobah in search of Yoda. Only with the Jedi Master's help will Luke survive when the Dark Side of the Force beckons him into the ultimate duel with Darth Vader.",
                    Duration = "124",
                    UnitPrice = 50,
                    UnitsInStock = 200,
                    Rate = 3.7,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 16
                {
                    Title = "The Matrix",
                    Slug = "the-matrix",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg",
                    ReleaseYear = "1999",
                    Summary = "When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.",
                    Description = "Thomas A. Anderson is a man living two lives. By day he is an average computer programmer and by night a hacker known as Neo. Neo has always questioned his reality, but the truth is far beyond his imagination. Neo finds himself targeted by the police when he is contacted by Morpheus, a legendary computer hacker branded a terrorist by the government. As a rebel against the machines, Neo must confront the agents: super-powerful computer programs devoted to stopping Neo and the entire human rebellion.",
                    Duration = "136",
                    UnitPrice = 50,
                    UnitsInStock = 250,
                    Rate = 3.7,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 17
                {
                    Title = "Goodfellas",
                    Slug = "goodfellas",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    ReleaseYear = "1990",
                    Summary = "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen Hill and his mob partners Jimmy Conway and Tommy DeVito in the Italian-American crime syndicate.",
                    Description = "Henry Hill might be a small time gangster, who may have taken part in a robbery with Jimmy Conway and Tommy De Vito, two other gangsters who might have set their sights a bit higher. His two partners could kill off everyone else involved in the robbery, and slowly start to think about climbing up through the hierarchy of the Mob. Henry, however, might be badly affected by his partners' success, but will he consider stooping low enough to bring about the downfall of Jimmy and Tommy?",
                    Duration = "146",
                    UnitPrice = 40,
                    UnitsInStock = 150,
                    Rate = 3.7,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 18
                {
                    Title = "One Flew Over the Cuckoo's Nest",
                    Slug = "one-flew-over-the-cuckoos-nest",
                    PictureUri = "https://upload.wikimedia.org/wikipedia/en/2/26/One_Flew_Over_the_Cuckoo%27s_Nest_poster.jpg",
                    ReleaseYear = "1975",
                    Summary = "A criminal pleads insanity and is admitted to a mental institution, where he rebels against the oppressive nurse and rallies up the scared patients.",
                    Description = "McMurphy has a criminal past and has once again gotten himself into trouble and is sentenced by the court. To escape labor duties in prison, McMurphy pleads insanity and is sent to a ward for the mentally unstable. Once here, McMurphy both endures and stands witness to the abuse and degradation of the oppressive Nurse Ratched, who gains superiority and power through the flaws of the other inmates. McMurphy and the other inmates band together to make a rebellious stance against the atrocious Nurse.",
                    Duration = "133",
                    UnitPrice = 40,
                    UnitsInStock = 130,
                    Rate = 3.7,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 19
                {
                    Title = "Seven Samurai",
                    Slug = "seven-samurai",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BOWE4ZDdhNmMtNzE5ZC00NzExLTlhNGMtY2ZhYjYzODEzODA1XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_.jpg",
                    ReleaseYear = "1954",
                    Summary = "A poor village under attack by bandits recruits seven unemployed samurai to help them defend themselves.",
                    Description = "A veteran samurai, who has fallen on hard times, answers a village's request for protection from bandits. He gathers 6 other samurai to help him, and they teach the townspeople how to defend themselves, and they supply the samurai with three small meals a day. The film culminates in a giant battle when 40 bandits attack the village.",
                    Duration = "207",
                    UnitPrice = 35,
                    UnitsInStock = 100,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 20
                {
                    Title = "Se7en",
                    Slug = "se7en",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                    ReleaseYear = "1995",
                    Summary = "Two detectives, a rookie and a veteran, hunt a serial killer who uses the seven deadly sins as his motives.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    Duration = "127",
                    UnitPrice = 45,
                    UnitsInStock = 170,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 21
                {
                    Title = "The Silence of the Lambs",
                    Slug = "the-silence-of-the-lambs",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                    ReleaseYear = "1991",
                    Summary = "A young F.B.I. cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.",
                    Description = "F.B.I. trainee Clarice Starling (Jodie Foster) works hard to advance her career, while trying to hide or put behind her West Virginia roots, of which if some knew, would automatically classify her as being backward or white trash. After graduation, she aspires to work in the agency's Behavioral Science Unit under the leadership of Jack Crawford (Scott Glenn). While she is still a trainee, Crawford asks her to question Dr. Hannibal Lecter (Sir Anthony Hopkins), a psychiatrist imprisoned, thus far, for eight years in maximum security isolation for being a serial killer who cannibalized his victims. Clarice is able to figure out the assignment is to pick Lecter's brains to help them solve another serial murder case, that of someone coined by the media as \"Buffalo Bill\" (Ted Levine), who has so far killed five victims, all located in the eastern U.S., all young women, who are slightly overweight (especially around the hips), all who were drowned in natural bodies of water, and all who were stripped of large swaths of skin. She also figures that Crawford chose her, as a woman, to be able to trigger some emotional response from Lecter. After speaking to Lecter for the first time, she realizes that everything with him will be a psychological game, with her often having to read between the very cryptic lines he provides. She has to decide how much she will play along, as his request in return for talking to him is to expose herself emotionally to him. The case takes a more dire turn when a sixth victim is discovered, this one from who they are able to retrieve a key piece of evidence, if Lecter is being forthright as to its meaning. A potential seventh victim is high profile Catherine Martin (Brooke Smith), the daughter of Senator Ruth Martin (Diane Baker), which places greater scrutiny on the case as they search for a hopefully still alive Catherine. Who may factor into what happens is Dr. Frederick Chilton (Anthony Heald), the warden at the prison, an opportunist who sees the higher profile with Catherine, meaning a higher profile for himself if he can insert himself successfully into the proceedings.",
                    Duration = "118",
                    UnitPrice = 40,
                    UnitsInStock = 100,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 22
                {
                    Title = "City of God",
                    Slug = "city-of-god",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BOTMwYjc5ZmItYTFjZC00ZGQ3LTlkNTMtMjZiNTZlMWQzNzI5XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                    ReleaseYear = "2002",
                    Summary = "In the slums of Rio, two kids' paths diverge as one struggles to become a photographer and the other a kingpin.",
                    Description = "Brazil, 1960s, City of God. The Tender Trio robs motels and gas trucks. Younger kids watch and learn well...too well. 1970s: Li'l Zé has prospered very well and owns the city. He causes violence and fear as he wipes out rival gangs without mercy. His best friend Bené is the only one to keep him on the good side of sanity. Rocket has watched these two gain power for years, and he wants no part of it. he keeps getting swept up in the madness. All he wants to do is take pictures. 1980s: Things are out of control between the last two remaining gangs...will it ever end? Welcome to the City of God.",
                    Duration = "130",
                    UnitPrice = 30,
                    UnitsInStock = 100,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 23
                {
                    Title = "Life Is Beautiful",
                    Slug = "life-is-beautiful",
                    PictureUri = "https://www.cinetaste.se/wp-content/uploads/2019/01/life-is-beautiful-movie-art-silk-poster-20x30.jpg",
                    ReleaseYear = "1997",
                    Summary = "When an open-minded Jewish librarian and his son become victims of the Holocaust, he uses a perfect mixture of will, humor, and imagination to protect his son from the dangers around their camp.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    Duration = "116",
                    UnitPrice = 30,
                    UnitsInStock = 100,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 24
                {
                    Title = "Spirited Away",
                    Slug = "spirited-away",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BMjlmZmI5MDctNDE2YS00YWE0LWE5ZWItZDBhYWQ0NTcxNWRhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                    ReleaseYear = "2001",
                    Summary = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.",
                    Description = "Chihiro and her parents are moving to a small Japanese town in the countryside, much to Chihiro's dismay. On the way to their new home, Chihiro's father makes a wrong turn and drives down a lonely one-lane road which dead-ends in front of a tunnel. Her parents decide to stop the car and explore the area. They go through the tunnel and find an abandoned amusement park on the other side, with its own little town. When her parents see a restaurant with great-smelling food but no staff, they decide to eat and pay later. However, Chihiro refuses to eat and decides to explore the theme park a bit more. She meets a boy named Haku who tells her that Chihiro and her parents are in danger, and they must leave immediately. She runs to the restaurant and finds that her parents have turned into pigs. In addition, the theme park turns out to be a town inhabited by demons, spirits, and evil gods. At the center of the town is a bathhouse where these creatures go to relax. The owner of the bathhouse is the evil witch Yubaba, who is intent on keeping all trespassers as captive workers, including Chihiro. Chihiro must rely on Haku to save her parents in hopes of returning to their world.",
                    Duration = "125",
                    UnitPrice = 30,
                    UnitsInStock = 80,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                },
                new Movie() // 25
                {
                    Title = "Star Wars: Episode IV - A New Hope",
                    Slug = "star-wars-episode-IV-a-new-hope",
                    PictureUri = "https://m.media-amazon.com/images/M/MV5BNzVlY2MwMjktM2E4OS00Y2Y3LWE3ZjctYzhkZGM3YzA1ZWM2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    ReleaseYear = "1977",
                    Summary = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                    Description = "The Imperial Forces, under orders from cruel Darth Vader, hold Princess Leia hostage in their efforts to quell the rebellion against the Galactic Empire. Luke Skywalker and Han Solo, captain of the Millennium Falcon, work together with the companionable droid duo R2-D2 and C-3PO to rescue the beautiful princess, help the Rebel Alliance and restore freedom and justice to the Galaxy.",
                    Duration = "121",
                    UnitPrice = 50,
                    UnitsInStock = 250,
                    Rate = 3.6,
                    Reviews = movieLandContext.Reviews.ToList(),
                }
            };

            movieLandContext.Movies.AddRange(movies);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedMoviesDirectorsAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.MovieDirectors.Any())
                return;

            var newMovieDirectorsLists = new List<MovieDirector>
            {
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Shawshank Redemption").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Frank Darabont").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Francis Ford Coppola").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather: Part II").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Francis Ford Coppola").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Dark Knight").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Christopher Nolan").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "12 Angry Men").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Sidney Lumet").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Schindler's List").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Steven Spielberg").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Return of the King").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Peter Jackson").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Pulp Fiction").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Quentin Tarantino").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Good, the Bad and the Ugly").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Sergio Leone").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Fellowship of the Ring").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Peter Jackson").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Fight Club").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "David Fincher").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Forrest Gump").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Robert Zemeckis").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Inception").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Christopher Nolan").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Two Towers").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Peter Jackson").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode V - The Empire Strikes Back").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Irvin Kershner").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Matrix").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Lana Wachowski").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Matrix").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Lilly Wachowski").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Goodfellas").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Martin Scorsese").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "One Flew Over the Cuckoo's Nest").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Milos Forman").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Seven Samurai").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Akira Kurosawa").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Se7en").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "David Fincher").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Silence of the Lambs").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Jonathan Demme").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "City of God").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Fernando Meirelles").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "City of God").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Kátia Lund").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Life Is Beautiful").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Roberto Benigni").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode IV - A New Hope").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "George Lucas").FirstOrDefault(),
                },
                new MovieDirector
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Spirited Away").FirstOrDefault(),
                    Director = movieLandContext.Directors.Where(d => d.Name == "Hayao Miyazaki").FirstOrDefault(),
                },
            };

            movieLandContext.MovieDirectors.AddRange(newMovieDirectorsLists);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedMoviesGenresAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.MovieGenres.Any())
                return;

            var newMovieGenreLists = new List<MovieGenre>
            {
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Shawshank Redemption").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather: Part II").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Godfather: Part II").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Dark Knight").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Dark Knight").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Dark Knight").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "12 Angry Men").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "12 Angry Men").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Schindler's List").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Biography").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Schindler's List").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Schindler's List").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "History").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Return of the King").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Return of the King").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Return of the King").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Pulp Fiction").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Pulp Fiction").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Good, the Bad and the Ugly").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Western").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Fellowship of the Ring").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Fellowship of the Ring").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Fellowship of the Ring").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Fight Club").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Forrest Gump").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Forrest Gump").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Romance").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Inception").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Inception").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Inception").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Sci-Fi").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Two Towers").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Two Towers").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Lord of the Rings: The Two Towers").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode V - The Empire Strikes Back").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode V - The Empire Strikes Back").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode V - The Empire Strikes Back").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Fantasy").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Matrix").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Matrix").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Sci-Fi").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Goodfellas").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Biography").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Goodfellas").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Goodfellas").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "One Flew Over the Cuckoo's Nest").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Seven Samurai").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Seven Samurai").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Seven Samurai").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Se7en").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Se7en").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Se7en").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Mystery").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Silence of the Lambs").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Silence of the Lambs").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "The Silence of the Lambs").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Thriller").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "City of God").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Crime").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "City of God").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Life Is Beautiful").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Comedy").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Life Is Beautiful").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Drama").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Life Is Beautiful").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Romance").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode IV - A New Hope").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Action").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode IV - A New Hope").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Star Wars: Episode IV - A New Hope").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Fantasy").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Spirited Away").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Animation").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Spirited Away").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Adventure").FirstOrDefault(),
                },
                new MovieGenre
                {
                    Movie = movieLandContext.Movies.Where(m => m.Title == "Spirited Away").FirstOrDefault(),
                    Genre = movieLandContext.Genres.Where(g => g.Name == "Family").FirstOrDefault(),
                },
            };

            movieLandContext.MovieGenres.AddRange(newMovieGenreLists);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedListAndMoviesAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Lists.Any())
                return;

            var lists = new List<List>()
            {
                new List
                {
                    Name = "BEST SELLERS",
                    Description = "Top selling movies in the store.",
                    PictureUri = "",
                },
                new List
                {
                    Name = "NEW ARRIVAL",
                    Description = "The latest movies in the store.",
                    PictureUri = "",
                },
            };

            var newMovieLists = new List<MovieList>
            {
                new MovieList
                {
                    List = lists.FirstOrDefault(l => l.Name == "BEST SELLERS"),
                    Movie = movieLandContext.Movies.OrderBy(m => m.UnitsInStock).FirstOrDefault()
                },
                new MovieList
                {
                    List = lists.FirstOrDefault(l => l.Name == "BEST SELLERS"),
                    Movie = movieLandContext.Movies.OrderBy(m => m.UnitsInStock).Skip(1).FirstOrDefault()
                },
                new MovieList
                {
                    List = lists.FirstOrDefault(l => l.Name == "NEW ARRIVAL"),
                    Movie = movieLandContext.Movies.OrderBy(m => m.Id).LastOrDefault()
                }
            };

            movieLandContext.Lists.AddRange(lists);
            movieLandContext.MovieLists.AddRange(newMovieLists);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedCompareAndMoviesAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Compares.Any())
                return;

            var compares = new List<Compare>()
            {
                new Compare
                {
                    Username = "mz001"
                }
            };

            var newMovieCompares = new List<MovieCompare>()
            {
                new MovieCompare
                {
                    Movie = movieLandContext.Movies.Where(m => m.Id % 2 == 1).FirstOrDefault(),
                    Compare = compares.FirstOrDefault()
                },
                new MovieCompare
                {
                    Movie = movieLandContext.Movies.Where(m => m.Id % 2 == 1).Skip(1).FirstOrDefault(),
                    Compare = compares.FirstOrDefault()
                }
            };

            movieLandContext.Compares.AddRange(compares);
            movieLandContext.MovieCompares.AddRange(newMovieCompares);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedFavoriteAndMoviesAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Favorites.Any())
                return;

            var favorites = new List<Favorite>()
            {
                new Favorite
                {
                    Username = "bg123"
                }
            };

            var newMovieFavorites= new List<MovieFavorite>()
            {
                new MovieFavorite
                {
                    Movie = movieLandContext.Movies.Where(m => m.Id % 2 == 1).FirstOrDefault(),
                    Favorite = favorites.FirstOrDefault()
                },
                new MovieFavorite
                {
                    Movie = movieLandContext.Movies.Where(m => m.Id % 2 == 1).Skip(1).FirstOrDefault(),
                    Favorite = favorites.FirstOrDefault()
                }
            };

            movieLandContext.Favorites.AddRange(favorites);
            movieLandContext.MovieFavorites.AddRange(newMovieFavorites);

            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedCartAndItemsAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Carts.Any())
                return;

            var movieOne = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Shawshank Redemption");
            var movieTwo = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Godfather");
            var movieThree = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Godfather: Part II");

            var carts = new List<Cart>()
            {
                new Cart
                {
                    Username = "bg123",
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Movie = movieOne,
                            Quantity = 2,
                            UnitPrice = movieOne.UnitPrice,
                            TotalPrice = 2 * movieOne.UnitPrice
                        },
                        new CartItem
                        {
                            Movie = movieTwo,
                            Quantity = 1,
                            UnitPrice = movieTwo.UnitPrice,
                            TotalPrice = 1 * movieTwo.UnitPrice
                        },
                        new CartItem
                        {
                            Movie = movieThree,
                            Quantity = 1,
                            UnitPrice = movieThree.UnitPrice,
                            TotalPrice = 1 * movieThree.UnitPrice
                        }
                    }
                }
            };

            movieLandContext.Carts.AddRange(carts);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedOrderAndItemsAsync(MovieLandContext movieLandContext)
        {
            if (movieLandContext.Orders.Any())
                return;

            var movieOne = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Shawshank Redemption");
            var movieTwo = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Godfather");
            var movieThree = movieLandContext.Movies.FirstOrDefault(m => m.Title == "The Godfather: Part II");

            var orders = new List<Order>()
            {
                new Order
                {
                    Username = "bg123",
                    FirstName = "Branko",
                    LastName = "Gabelica",
                    Email = "bg123@oss.unist.hr",
                    ContactNumber = "0911234567",
                    City = "Split",
                    ShippingAddress = "Vukovarska 108",
                    PostalCode = "2100",
                    Country = "Croatia",
                    PaymentMethod = PaymentMethod.Cash,
                    Status = OrderStatus.InProgress,
                    DateOrdered = DateTime.Now,
                    GrandTotal = 195,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Movie = movieOne,
                            Quantity = 2,
                            UnitPrice = movieOne.UnitPrice,
                            TotalPrice = 2 * movieOne.UnitPrice
                        },
                        new OrderItem
                        {
                            Movie = movieTwo,
                            Quantity = 1,
                            UnitPrice = movieTwo.UnitPrice,
                            TotalPrice = 1 * movieTwo.UnitPrice
                        },
                        new OrderItem
                        {
                            Movie = movieThree,
                            Quantity = 1,
                            UnitPrice = movieThree.UnitPrice,
                            TotalPrice = 1 * movieThree.UnitPrice
                        }
                    }
                }
            };

            movieLandContext.Orders.AddRange(orders);
            await movieLandContext.SaveChangesAsync();
        }


        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("SuperAdmin").Result == null)
            {
                await CreateRole(roleManager, "SuperAdmin");
            }
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                await CreateRole(roleManager, "Admin");
            }
            if (roleManager.FindByNameAsync("Client").Result == null)
            {
                await CreateRole(roleManager, "Client");
            }
        }


        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = new IdentityRole();
            role.Name = roleName;
            role.NormalizedName = roleName.ToUpper();
            
            await roleManager.CreateAsync(role);
        }


        private static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager)
        {
            var defaultEmail = "superadmin@movieland.com";
            var defaultPassword = "superadmin123";

            if (userManager.FindByEmailAsync(defaultEmail).Result == null)
            {
                var newSuperAdmin = new IdentityUser { Email = defaultEmail, UserName = defaultEmail };
                var result = await userManager.CreateAsync(newSuperAdmin, defaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newSuperAdmin, "SuperAdmin");
                }
            }
        }
    }
}
